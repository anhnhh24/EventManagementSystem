using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EventController.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly RegistrationDAO _registrationDAO;
        private readonly PaymentDAO _paymentDAO;
        private readonly UserDAO _userDAO;
        private readonly BillDAO _billDAO;
        private readonly EventDAO _eventDAO;


        public PaymentController(ILogger<PaymentController> logger, IConfiguration configuration, RegistrationDAO registration, PaymentDAO paymentDAO, UserDAO userDAO, BillDAO billDAO, EventDAO eventDAO)
        {
            _logger = logger;
            _configuration = configuration;
            _registrationDAO = registration;
            _paymentDAO = paymentDAO;
            _userDAO = userDAO;
            _billDAO = billDAO;
            _eventDAO = eventDAO;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VNPayRedirect(long Total)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
                return RedirectToAction("SignIn", "Authentication");

            var user = _userDAO.GetUserByEmail(currentUser.Email);
            var listRegistration = _registrationDAO
                .getPendingUserRegistration(user.UserID)
                .Select(r => r.RegistrationID)
                .ToList();

            if (listRegistration == null || !listRegistration.Any())
            {
                TempData["ErrorMessage"] = "Không có đăng ký nào để thanh toán!";
                return RedirectToAction("Index", "Registration");
            }

            if (Total == 0)
            {
                foreach (var regId in listRegistration)
                    _registrationDAO.UpdateStatusById(regId, "Success");

                var bill = await _billDAO.CreateBillAsync(user.UserID, listRegistration, 0);
                var payment = new Payment
                {
                    Amount = 0,
                    BillID = bill.BillID,
                    PaymentTime = DateTime.Now,
                    Status = "Success",
                    OrderInfo = "Free registration",
                    TransactionCode = Guid.NewGuid().ToString().Substring(0, 12)
                };
                _paymentDAO.AddPayment(payment);

                TempData["Message"] = "Đăng ký thành công không cần thanh toán.";
                return RedirectToAction("History", "Registration");
            }

            var createdBill = await _billDAO.CreateBillAsync(user.UserID, listRegistration, Total);
            int BillId = createdBill.BillID;

            string txnCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12);
            string orderInfo = "Thanh toán đăng ký sự kiện";

            string tmnCode = _configuration["VNPay:TmnCode"];
            string hashSecret = _configuration["VNPay:HashSecret"];
            string vnpUrl = _configuration["VNPay:Url"];
            string returnUrl = $"{_configuration["VNPay:ReturnUrl"]}?billId={BillId}";
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            var payParams = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", tmnCode },
                { "vnp_Amount", ((long)(Total * 100)).ToString() },
                { "vnp_CurrCode", "VND" },  
                { "vnp_TxnRef", txnCode },
                { "vnp_OrderInfo", orderInfo },
                { "vnp_OrderType", "billpayment" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", returnUrl },
                { "vnp_IpAddr", ipAddress },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                { "vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss") }
             };

            string queryData = string.Join("&", payParams.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}"));
            string secureHash = HmacSHA512(hashSecret, queryData);
            payParams.Add("vnp_SecureHash", secureHash);

            var paymentUrl = $"{vnpUrl}?{string.Join("&", payParams.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}"))}";


            var pendingPayment = new Payment
            {
                Amount = Total,
                BillID = BillId,
                Status = "Pending",
                TransactionCode = txnCode,
                OrderInfo = orderInfo,
                ExpireTime = DateTime.Now.AddMinutes(15) 
            };
            _paymentDAO.AddPayment(pendingPayment);

            return Redirect(paymentUrl);
        }

        [HttpGet]
        public IActionResult ReturnPayment(string billId)
        {
            try
            {
                var hashSecret = _configuration["VNPay:HashSecret"];
                var queryParams = Request.Query
                    .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
                    .ToDictionary(k => k.Key, k => k.Value.ToString());

                if (!queryParams.TryGetValue("vnp_SecureHash", out var receivedHash))
                {
                    TempData["ErrorMessage"] = "Lack of key!";
                    return RedirectToAction("Index", "Home");
                }

                var sortedParams = queryParams
                    .Where(kvp => kvp.Key != "vnp_SecureHash")
                    .OrderBy(kvp => kvp.Key)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                string txnCode = queryParams.GetValueOrDefault("vnp_TxnRef");
                string responseCode = queryParams.GetValueOrDefault("vnp_ResponseCode");
                string orderInfo = queryParams.GetValueOrDefault("vnp_OrderInfo", "No Info");
                string amountStr = queryParams.GetValueOrDefault("vnp_Amount", "0");

                var payment = _paymentDAO.GetPaymentByTransactionCode(txnCode);
                if (payment == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy giao dịch thanh toán!";
                    return RedirectToAction("Index", "Home");
                }

                if (payment.Status != "Pending")
                {
                    TempData["Message"] = "Giao dịch đã được xử lý trước đó.";
                    return View("ReturnPayment", payment);
                }

                payment.PaymentTime = DateTime.Now;
                payment.Amount = long.TryParse(amountStr, out var amt) ? amt / 100 : 0;
                payment.OrderInfo = orderInfo;
                payment.Status = responseCode == "00" ? "Success" : "Failed";

                _paymentDAO.UpdatePayment(payment);
                var events = new Event();

                if (responseCode == "00" && int.TryParse(billId, out int validBillId))
                {
                    var regs = _billDAO.GetRegistrationByBillId(validBillId);
                    foreach (var reg in regs)
                    {
                        _registrationDAO.UpdateStatusById(reg.RegistrationID, "Success");
                        _eventDAO.IncreaseEventAttendees(reg.EventID, reg.Quantity);
                    }

                    var bill = _billDAO.GetBillByBillId(validBillId);
                    if (bill != null) bill.Status = "Paid";
                    _billDAO.UpdateBill(bill);
                }

                return View("ReturnPayment", payment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ReturnPayment: " + ex.Message);
                TempData["ErrorMessage"] = "Có lỗi trong quá trình xử lý thanh toán.";
                return RedirectToAction("Index", "Home");
            }
        }

        private static string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

    }
}
