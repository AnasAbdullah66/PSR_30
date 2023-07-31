using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid UserID or Password";
                //return RedirectToAction(nameof(Index));
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            
            return RedirectToAction("Index");
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
            var roles = _context.Roles.ToList();
            ViewBag.RolesList = new SelectList(roles, "RoleID", "UserRole");
            return View();
        }

        
        [HttpPost]
        public IActionResult Registration(CustomerUserLogin cuLogin)
        {
            var roles = _context.Roles.ToList();

            //if (ModelState.IsValid)
            //{
                var cus = new CustomerUserLogin
                {
                    UserName = cuLogin.UserName,
                    Password = cuLogin.Password,
                    BranchID = cuLogin.BranchID,
                    Department = cuLogin.Department,
                    ContactNo = cuLogin.ContactNo,
                    UserRole = Convert.ToInt32(Request.Form["UserRole"]), // Assuming the dropdown's name is "UserRole"
                    UserStatus = cuLogin.UserStatus,
                    LoginID = cuLogin.LoginID,
                    LoginStatus = cuLogin.LoginStatus,
                    IPAddress = GetIp(),
                    EntryDate = DateTime.Now,
                    EmpID = cuLogin.EmpID,
                    LastLoginDate = DateTime.Now,
                    //Active = cuLogin.Active == true ? "yes" : "no",
                    Email = cuLogin.Email,
                };

                _context.CustomerUserLogins.Add(cus);
                _context.SaveChanges();
            ViewBag.RolesList = roles;
            // Redirect to the login page after successful registration.
            return View("Index");
            //}

            /*ViewBag.RolesList = roles;*/ // Pass roles list to the view in case of validation errors.
            //return View(cuLogin);
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