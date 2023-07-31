using Microsoft.AspNetCore.Mvc;
using PSR_Add_Document.Models;
using PSR_Add_Document.Models.GlobalClass;
using PSR_Add_Document.Models.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace PSR_Add_Document.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerDbContext _context;

        public HomeController(ILogger<HomeController> logger, CustomerDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Index(string userName, string password)
        {
            var user = _context.branchUserLogins.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user != null)
            {
                return RedirectToAction("AddDocument", "Customers");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid UserID or Password";
                //return RedirectToAction(nameof(Index));
                return View();
            }
        }

        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    // Authenticate the user based on the provided username and password.
        //    // For simplicity, we'll assume the authentication is successful.

        //    // Example authentication logic:
        //    bool isAuthenticated = true;
        //    var user = _context.CustomerUserLogins.FirstOrDefault(u => u.UserName == username && u.Password == password);

        //    if (isAuthenticated && user != null)
        //    {
        //        // Check if the username starts with "NRB" to determine user type.
        //        if (username.StartsWith("NRB", StringComparison.OrdinalIgnoreCase))
        //        {
        //            user.UserType = UserType.BranchUser;
        //        }
        //        else
        //        {
        //            user.UserType = UserType.Customer;
        //        }

        //        // Save the user type in the session for further use.
        //        HttpContext.Session.SetString("UserType", user.UserType.ToString());

        //        // Redirect to the appropriate page based on user type.
        //        if (user.UserType == UserType.BranchUser)
        //        {
        //            return RedirectToAction("BranchDashboard", "Branch");
        //        }
        //        else
        //        {
        //            return RedirectToAction("CustomerDashboard", "Customer");
        //        }
        //    }
        //    else
        //    {
        //        // Invalid credentials or user not found, show an error message.
        //        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        //        return View();
        //    }
        //}





        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(CustomerUserLogin cuLogin)
        {
            if (ModelState.IsValid)
            {
                var cus = new CustomerUserLogin
                {
                    UserName = cuLogin.UserName,
                    Password = cuLogin.Password,
                    BranchID = cuLogin.BranchID,
                    Department=cuLogin.Department,
                    ContactNo = cuLogin.ContactNo,
                    UserRole = cuLogin.UserRole,
                    UserStatus = cuLogin.UserStatus,
                    LoginID = cuLogin.LoginID,
                    LoginStatus = cuLogin.LoginStatus,
                    IPAddress = GetIp(),
                    EntryDate = DateTime.Now,
                    EmpID = cuLogin.EmpID,
                    LastLoginDate = DateTime.Now ,
                    Active = cuLogin.Active,
                    Email = cuLogin.Email,

                };

                _context.CustomerUserLogins.Add(cuLogin);
                _context.SaveChanges();
                return RedirectToAction("Login", "Home");
            }

            return View(cuLogin);
        }

        public IActionResult Register(CustomerUserLogin newUser)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerUserLogins.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login", "CustomerUser");
            }

            // If the ModelState is invalid, return back to the registration form to show validation errors.
            return View(newUser);
        }

        private string GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddresses = new List<string>();

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddresses.Add(ip.ToString());
                }
            }

            string ip2 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            if (ip2 != null)
            {
                ipAddresses.Add(ip2);
                // Assign the value of ip2 to the SubIP field
            }

            return ipAddresses.FirstOrDefault(); // Return the first IP address from the list, or null if the list is empty
        }

        //MaskAccountNumber
        private string MaskAccountNumber(string accountNumber)
        {
            if (!string.IsNullOrEmpty(accountNumber) && accountNumber.Length > 10)
            {
                var maskedAccountNumber = accountNumber.Substring(0, 6) + new string('*', accountNumber.Length - 10) + accountNumber.Substring(accountNumber.Length - 4);
                return maskedAccountNumber;
            }

            return accountNumber;
        }
    }
}