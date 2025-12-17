using EventController.Models.DAO.Implements;
using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyDatabase")));

builder.Services.AddHostedService<EventCheckerService>();
builder.Services.AddScoped<EventCategoryDAO>();
builder.Services.AddScoped<EventDAO>();
builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<VenueDAO>();
builder.Services.AddScoped<RegistrationDAO>();
builder.Services.AddScoped<NotificationDAO>();
builder.Services.AddScoped<EmailVerificationTokenDAO>();
builder.Services.AddScoped<PaymentDAO>();
builder.Services.AddScoped<BillDAO>();
builder.Services.AddScoped<CommentDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

