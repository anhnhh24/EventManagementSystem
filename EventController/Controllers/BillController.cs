using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class BillController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BillController> _logger;
        private readonly RegistrationDAO _registrationDAO;
        private readonly PaymentDAO _paymentDAO;
        private readonly UserDAO _userDAO;
        private readonly BillDAO _billDAO;
        private readonly EventDAO _eventDAO;


        public BillController(ILogger<BillController> logger, IConfiguration configuration, RegistrationDAO registration, PaymentDAO paymentDAO, UserDAO userDAO, BillDAO billDAO, EventDAO eventDAO)
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
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
                return RedirectToAction("SignIn", "Authentication");

            var user = _userDAO.GetUserByEmail(currentUser.Email);
            var bills = _billDAO.GetBillsByUserId(user.UserID);
            return View(bills);
        }
        public IActionResult BillDetail(int id)
        {
            var bill = _billDAO.GetBillByBillId(id); 
            if (bill == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy hóa đơn!";
                return RedirectToAction("Index", "Registration");
            }

            var viewModel = new BillDetailViewModel
            {
                BillID = bill.BillID,
                Status = bill.Status,
                CreatedAt = bill.CreatedAt,
                TotalAmount = bill.TotalAmount,
                Registrations = bill.Registrations.Select(reg => new RegistrationInfo
                {
                    EventTitle = reg.Event?.Title ?? "N/A", 
                    Quantity = reg.Quantity,
                    Total = reg.Total,
                    Status = reg.Status
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
