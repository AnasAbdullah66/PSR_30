using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PSR_Add_Document.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;
using static System.Net.WebRequestMethods;
using System.Net.Sockets;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PSR_Add_Document.ViewModel;
using Humanizer;
using PdfSharpCore.Pdf.Content.Objects;
using System.Diagnostics.Metrics;
using System.Transactions;
using PSR_Add_Document.Models.GlobalClass;
using Microsoft.Extensions.Options;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace PSR_Add_Document.Controllers
{
    public class CustomersController : Controller
    {

        private readonly CustomerDbContext _context;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment environment;
        private readonly Config _config;

        // SessionCustomer 

        public const string SessionAccountNo = "SessionAccountNo";
        public const string SessionCustomerName = "SessionCustomerName";
        public const string SessionCustomerId = "SessionCustomerId";
        public const string SessionTinNumber = "SessionTinNumber";

        public const string SessionAddress = "SessionAddress";
        public const string SessionMobileNumber = "SessionMobileNumber";
        public const string SessionGender = "SessionGender";
        public const string SessionBrn = "SessionBrn";
        public const string SessionEmail = "SessionEmail";
        public const string SessionDOB = "SessionDOB";

        public CustomersController(CustomerDbContext context, IConfiguration configuration, IWebHostEnvironment environment, IOptions<Config> config)
        {
            _context = context;
            _configuration = configuration;
            this.environment = environment;
            _config = config.Value;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return _context.Customers != null ?
                        View(await _context.Customers.ToListAsync()) :
                        Problem("Entity set 'CustomerDbContext.Customers'  is null.");

        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,AccountNumber,Address,MobileNumber,Gender,Brn,DOB,Email,TinNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }


        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,AccountNumber,Address,MobileNumber,Gender,Brn,DOB")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'CustomerDbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }



        public IActionResult OTP()
        {
            return View();
        }

        [HttpPost]

        public IActionResult OTP(VMOTPManage otpInput)
        {
            // Retrieve the OTP data for the current user from the database
            var userAccountNo = HttpContext.Session.GetString(SessionAccountNo);




            return View();
        }


        //===================OTP===========================================================
        private string GenerateOTP()
        {
            const int otpLength = 4;
            const string characters = "0123456789";
            var random = new Random();
            var otp = new char[otpLength];

            for (int i = 0; i < otpLength; i++)
            {
                otp[i] = characters[random.Next(characters.Length)];
            }
            return new string(otp);
        }

        private static string GenerateRandomKey(int keySize)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var key = new char[keySize];

            for (int i = 0; i < keySize; i++)
            {
                key[i] = characters[random.Next(characters.Length)];
            }

            return new string(key);
        }

        public IActionResult SendOtpEmail(VMCustomer objCustomer)
        {

            if (ModelState.IsValid)
            {

                if (objCustomer.IsEmail == null && objCustomer.IsMobNo == null)
                {
                    //ModelState.AddModelError("ErrorShow", "Please Select MobNo or Email.");

                    TempData["ErrorShows"] = "Please Select MobNo or Email.";

                    return RedirectToAction("Login", new { id = TempData["id"] });
                }
                else
                {
                    OTPManage otpData = new OTPManage();

                    otpData.OTP = GenerateOTP();
                    otpData.OtpCreateTime = DateTime.Now;
                    otpData.OtpLastingTime = DateTime.Now.AddMinutes(20);
                    otpData.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
                    otpData.IPADDRESS = GetIp();
                    otpData.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
                    _context.OTPManage.Add(otpData);
                    _context.SaveChanges();


                    if (objCustomer.IsMobNo == "1")
                    {
                        SendMobileToUser(otpData.OTP);//Send Otp Mobile No
                    }
                    else
                    {
                        SendEmailToUser(otpData.OTP);//Send Otp To Mail
                    }

                    return RedirectToAction(nameof(ValidCheckOTP));

                }

            }
            return View();
        }


        public IActionResult ValidCheckOTP()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ValidCheckOTP(VMOTPManage objOtpManager)
        {

            if (ModelState.IsValid)
            {
                var getUserMobNo = HttpContext.Session.GetString(SessionMobileNumber);
                //var getOPTManagerData = _context.OTPManage.Where(x => x.MobileNumber == getUserMobNo).FirstOrDefault();

                var getOPTManagerData = _context.OTPManage.
                                 OrderByDescending(s => s.MobileNumber == getUserMobNo).
                                 Select(b => new
                                 {
                                     b.OTPId,
                                     b.OTP,
                                     b.OtpCreateTime,
                                     b.CustomerId,
                                     b.OtpLastingTime,
                                     b.AccountNumber
                                 }).FirstOrDefault();


                DateTime now = DateTime.Now;


                if ((now > getOPTManagerData.OtpCreateTime) && (now < getOPTManagerData.OtpLastingTime))
                {
                    if (getOPTManagerData.OTP == objOtpManager.OTP)
                    {
                        return RedirectToAction(nameof(AddDocument));
                    }
                    else
                    {
                        ModelState.AddModelError("OTP", "Input OTP is not valid.");
                    }

                }
                else
                {
                    ModelState.AddModelError("OTP", "Input OTP is time over.");
                }
            }

            return View();
        }

        //public IActionResult ReSendOtp()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var getUserMobNo = HttpContext.Session.GetString(SessionMobileNumber);

        //        var GetOPTManagerLastData = _context.OTPManage.
        //                                    OrderByDescending(s => s.MobileNumber == getUserMobNo).
        //                                    Select(b => new
        //                                    {
        //                                        b.OTPId,
        //                                        b.OTP,

        //                                        b.MobileNumber,
        //                                        b.OtpCreateTime,
        //                                        b.CustomerId,
        //                                        b.OtpLastingTime,
        //                                        b.AccountNumber
        //                                    }).FirstOrDefault();

        //        if (!string.IsNullOrEmpty(GetOPTManagerLastData.MobileNumber))
        //        {
        //            var getOTPId = GetOPTManagerLastData.OTPId;
        //            var otpManage = _context.OTPManage.Find(getOTPId);
        //            _context.OTPManage.Remove(otpManage);
        //            _context.SaveChanges(); //Delete                                          

        //        }



        //        OTPManage otpData = new OTPManage();

        //        otpData.OTP = GenerateOTP();
        //        otpData.OtpCreateTime = DateTime.Now;
        //        otpData.OtpLastingTime = DateTime.Now.AddMinutes(20);
        //        otpData.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
        //        otpData.IPADDRESS = GetIp();
        //        otpData.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
        //        _context.OTPManage.Add(otpData);
        //        _context.SaveChanges();

        //        SendEmailToUser(otpData.OTP); //Send Otp To Mail


        //        return RedirectToAction(nameof(ValidCheckOTP));

        //        //==================

        //        //var getUserMobNo = HttpContext.Session.GetString(SessionMobileNumber);
        //        //var getOTPCountByMobNo = _context.OTPManage.Where(x => x.AccountNumber == getUserMobNo).ToList();

        //        //int countOTPByMobNo = getOTPCountByMobNo.Count();
        //        //DateTime now = DateTime.Now;


        //        //          var GetOPTManagerLastData = _context.OTPManage.
        //        //OrderByDescending(s => s.MobileNumber == getUserMobNo).
        //        //Select(b => new
        //        //{
        //        //    OTPId = b.OTPId,
        //        //    OTP = b.OTP,
        //        //    LasOtpCreateTimetName = b.OtpCreateTime,
        //        //    CustomerId = b.CustomerId,
        //        //    OtpLastingTime = b.OtpLastingTime,
        //        //    IPADDRESS = b.IPADDRESS,
        //        //    AccountNumber = b.AccountNumber
        //        //}).FirstOrDefault();

        //        //if (countOTPByMobNo== 2)
        //        //{
        //        //    OTPManage otpData = new OTPManage();

        //        //    otpData.OTP = GenerateOTP();
        //        //    otpData.OtpCreateTime = DateTime.Now;
        //        //    otpData.OtpLastingTime = DateTime.Now.AddMinutes(20);
        //        //    otpData.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
        //        //    otpData.IPADDRESS = GetIp();
        //        //    otpData.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
        //        //    _context.OTPManage.Add(otpData);
        //        //    _context.SaveChanges();

        //        //    SendEmailToUser(otpData.OTP); //Send Otp To Mail

        //        //    return RedirectToAction(nameof(ValidCheckOTP));
        //        //}
        //        //else if (countOTPByMobNo == 3)
        //        //{
        //        //    OTPManage otpData = new OTPManage();

        //        //    otpData.OTP = GenerateOTP();
        //        //    otpData.OtpCreateTime = DateTime.Now;
        //        //    otpData.OtpLastingTime = DateTime.Now.AddMinutes(20);
        //        //    otpData.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
        //        //    otpData.IPADDRESS = GetIp();
        //        //    otpData.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
        //        //    _context.OTPManage.Add(otpData);
        //        //    _context.SaveChanges();

        //        //    SendEmailToUser(otpData.OTP); //Send Otp To Mail

        //        //    return RedirectToAction(nameof(ValidCheckOTP));
        //        //}
        //        //else
        //        //{
        //        //    ModelState.AddModelError("OTP", "Please Wait 20 min for Next OTP.");
        //        //}

        //    }

        //    return View();

        //}

        public IActionResult ReSendOtp()
        {
            if (ModelState.IsValid)
            {
                var getUserMobNo = HttpContext.Session.GetString(SessionMobileNumber);

                // Get the session variable for the number of OTP resends
                var otpResendCount = HttpContext.Session.GetInt32("OtpResendCount") ?? 0;

                if (otpResendCount >= 3)
                {
                    // If OTP has been resent three times in a row, check the time elapsed
                    var lastOtpResendTime = HttpContext.Session.GetString("LastOtpResendTime");

                    if (!string.IsNullOrEmpty(lastOtpResendTime))
                    {
                        DateTime lastResendTime = DateTime.Parse(lastOtpResendTime);
                        DateTime currentTime = DateTime.Now;

                        // If less than 30 minutes have passed since the last resend, display an error message
                        if (currentTime.Subtract(lastResendTime).TotalMinutes < 30)
                        {
                            ViewData["Error"] = "You have exceeded the maximum number of OTP resends. Please try again after 30 minutes.";
                            return View(nameof(ValidCheckOTP));
                        }
                    }
                }

                // Remove the previous OTP data (if any)
                var GetOPTManagerLastData = _context.OTPManage
                    .Where(x => x.MobileNumber == getUserMobNo)
                    .FirstOrDefault();

                if (GetOPTManagerLastData != null)
                {
                    var getOTPId = GetOPTManagerLastData.OTPId;
                    var otpManage = _context.OTPManage.Find(getOTPId);
                    _context.OTPManage.Remove(otpManage);
                    _context.SaveChanges();
                }

                // Generate and save new OTP data
                OTPManage otpData = new OTPManage();
                otpData.OTP = GenerateOTP();
                otpData.OtpCreateTime = DateTime.Now;
                otpData.OtpLastingTime = DateTime.Now.AddMinutes(20);
                otpData.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
                otpData.IPADDRESS = GetIp();
                otpData.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
                _context.OTPManage.Add(otpData);
                _context.SaveChanges();

                SendEmailToUser(otpData.OTP); //Send OTP to Mail

                // Update session variables for OTP resend count and last resend time
                HttpContext.Session.SetInt32("OtpResendCount", otpResendCount + 1);
                HttpContext.Session.SetString("LastOtpResendTime", DateTime.Now.ToString());

                return RedirectToAction(nameof(ValidCheckOTP));
            }

            return View();
        }


        public void SendEmailToUser(string sendOTP)
        {
            //Send Otp to User
            string recipientEmail = HttpContext.Session.GetString(SessionEmail);
            string sendingMail = _config.MailAddress;
            string host = _config.Host;
            string port = _config.Port;
            MailMessage Mail = new MailMessage();
            //Mail.From = new MailAddress("cardstatementInfo@mynrbbank.com");
            Mail.From = new MailAddress(sendingMail);

            //Mail.Bcc.Add("");
            Mail.To.Add(recipientEmail);
            Mail.Subject = "One Time Password";
            Mail.Body = ("Your OTP is " + sendOTP);

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = true;
            try
            {
                int Message = 1;
                smtp.Host = host;
                smtp.Port = Convert.ToInt32(port);
                smtp.Send(Mail);
                Mail.Dispose();
            }
            catch (Exception ex)
            {
                //throw ex.Message;
            }
        }




        public void SendMobileToUser(string sendOTP)
        {
            try
            {
                string customerMobile = HttpContext.Session.GetString(SessionMobileNumber);
                string sid = _config.SId;
                string user = _config.User;
                string pass = _config.Password;
                string URI = _config.SSLUrl;

                string myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + customerMobile + "&sms[0][1]=" + System.Web.HttpUtility.UrlEncode("Your OTP is " + sendOTP + ". You may change the PIN from any NRB Bank ATM Booth anytime. Call: 16568") + "&sid=" + sid;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
                byte[] data = Encoding.ASCII.GetBytes(myParameters);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                // Process the response if needed
                // XmlDocument xmlDocument = new XmlDocument();
                // xmlDocument.LoadXml(responseString);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error or display an error message)
                string errorMessage = $"Failed to send SMS: {ex.Message}";
                // Log the error or display the error message as needed
            }
        }

        //=====================End of Send OTP By Email===============================================


        //=====================Send OTP By SMS===============================================
        public async Task SendSMS(Customer customer)
        {
            try
            {
                string otp = GenerateOTP();
                string messageBody = $"Your OTP is {otp} of Account: {customer.AccountNumber}. Call: 16568";
                string sid = "NRBBL";
                string user = "nrbbl";
                string pass = "nrb@ssl";
                string URI = "http://192.168.236.2/pushapi/dynamic/server.php"; // Get the SMS URL from appsettings.json

                string myParameters = $"user={user}&pass={pass}&sms[0][0]={customer.MobileNumber}&sms[0][1]={Uri.EscapeDataString(messageBody)}&sms[0][2]={string.Empty}&sid={sid}";

                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(myParameters, Encoding.ASCII, "application/x-www-form-urlencoded");
                    var response = await httpClient.PostAsync(URI, content);

                    //Process the response
                    response.EnsureSuccessStatusCode(); // Ensure the request is successful
                    string responseString = await response.Content.ReadAsStringAsync();

                    // Process the response if needed
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error or display an error message)
                string errorMessage = $"Failed to send SMS: {ex.Message}";
                // Log the error or display the error message as needed
            }
        }

        //=====================End of Send OTP By SMS===============================================

        //=============================================================================ssssssssssssssss
        public async Task<IActionResult> Login(int? id)
        {
            TempData["id"] = null;

            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            //var customer = await _context.Customers
            //    .FirstOrDefaultAsync(m => m.CustomerId == id);


            TempData["id"] = id;

            VMCustomer objTransaction = new VMCustomer();
            var objQuery = _context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            objTransaction.CustomerId = objQuery.CustomerId;
            objTransaction.CustomerName = !string.IsNullOrEmpty(objQuery.CustomerName) ? objQuery.CustomerName : " ";
            objTransaction.AccountNumber = !string.IsNullOrEmpty(objQuery.AccountNumber) ? objQuery.AccountNumber : " ";
            objTransaction.Address = !string.IsNullOrEmpty(objQuery.Address) ? objQuery.Address : " ";
            objTransaction.MobileNumber = !string.IsNullOrEmpty(objQuery.MobileNumber) ? objQuery.MobileNumber : " ";
            objTransaction.TinNumber = objQuery.TinNumber;
            objTransaction.Gender = objQuery.Gender;
            objTransaction.Brn = objQuery.Brn;
            objTransaction.Email = !string.IsNullOrEmpty(objQuery.Email) ? objQuery.Email : "  ";
            objTransaction.DOB = objQuery.DOB;
            //objTransaction.IsMobNo = !string.IsNullOrEmpty(objQuery.IsMobNo) ? objQuery.IsMobNo : " ";
            //objTransaction.IsEmail = !string.IsNullOrEmpty(objQuery.IsEmail) ? objQuery.IsEmail : " ";


            if (objTransaction == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString(SessionAccountNo, objTransaction.AccountNumber);
            HttpContext.Session.SetString(SessionCustomerName, objTransaction.CustomerName);
            HttpContext.Session.SetInt32(SessionCustomerId, objTransaction.CustomerId);
            HttpContext.Session.SetString(SessionTinNumber, value: objTransaction.TinNumber);


            HttpContext.Session.SetString(SessionAddress, objTransaction.Address);
            HttpContext.Session.SetString(SessionMobileNumber, objTransaction.MobileNumber);
            HttpContext.Session.SetInt32(SessionGender, objTransaction.Gender);
            HttpContext.Session.SetString(SessionBrn, objTransaction.Brn);
            HttpContext.Session.SetString(SessionEmail, objTransaction.Email);

            //HttpContext.Session.SetInt32(SessionDOB, customer.DOB);


            return View(objTransaction);

        }


        //==================================SAVE OTP===================================
        [HttpPost]
        public string SaveOtp()
        {
            if (ModelState.IsValid)
            {
                // Create a new OTPManage object
                OTPManage otpData = new OTPManage
                {
                    OTP = GenerateOTP(),
                    OtpCreateTime = DateTime.Now,
                    CustomerId = Convert.ToInt32(HttpContext.Session.GetString(SessionCustomerId)),
                    OtpLastingTime = DateTime.Now.AddMinutes(20), // You can set the OtpTime property to the current date and time
                    MobileNumber = HttpContext.Session.GetString(SessionMobileNumber),
                    IPADDRESS = GetIp(),
                    AccountNumber = HttpContext.Session.GetString(SessionAccountNo)
                };
                _context.OTPManage.Add(otpData);
                _context.SaveChanges();
                return "MESSEGE SEND SUCCESSFULLY";
            }
            else
            {
                return "MESSEGE SENDING FAILED";
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewDocuments(string searchString, DateTime? fromDate, DateTime? toDate, string sortColumn, string sortDirection, int page = 1)
        {
            int pageSize = 20; // Number of items per page

            var documents = _context.CustomerDocuments.AsQueryable(); // Get all documents

            // Apply search filters
            if (!string.IsNullOrEmpty(searchString))
            {
                documents = documents.Where(d =>
                    d.CustomerName.Contains(searchString) ||
                    d.TinNumber == searchString);
            }

            if (fromDate.HasValue)
            {
                documents = documents.Where(d => d.ProcessDate >= fromDate);
            }

            if (toDate.HasValue)
            {
                documents = documents.Where(d => d.ProcessDate <= toDate);
            }

            // Apply sorting
            switch (sortColumn)
            {
                case "customerName":
                    documents = (sortDirection == "asc")
                        ? documents.OrderBy(d => d.CustomerName)
                        : documents.OrderByDescending(d => d.CustomerName);
                    break;
                case "assessmentYear":
                    documents = (sortDirection == "asc")
                        ? documents.OrderBy(d => d.AssesmentYear)
                        : documents.OrderByDescending(d => d.AssesmentYear);
                    break;
                // Add more cases for other columns as needed
                default:
                    documents = documents.OrderByDescending(d => d.ProcessDate);
                    break;
            }

            // Paging
            var totalItems = documents.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var paginatedDocuments = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(paginatedDocuments);
        }



        public IActionResult AddDocument()
        {
            VMCustomerDocument objVMCustomerDocument = new VMCustomerDocument();

            objVMCustomerDocument.AccountNumber = HttpContext.Session.GetString(SessionAccountNo);
            objVMCustomerDocument.CustomerName = HttpContext.Session.GetString(SessionCustomerName);
            objVMCustomerDocument.CustomerId = HttpContext.Session.GetInt32(SessionCustomerId) ?? 0;
            objVMCustomerDocument.TinNumber = HttpContext.Session.GetString(SessionTinNumber);

            objVMCustomerDocument.Address = HttpContext.Session.GetString(SessionAddress);
            objVMCustomerDocument.MobileNumber = HttpContext.Session.GetString(SessionMobileNumber);
            objVMCustomerDocument.Brn = HttpContext.Session.GetString(SessionBrn);
            objVMCustomerDocument.Email = HttpContext.Session.GetString(SessionEmail);
            //objVMCustomerDocument.DOB=HttpContext.Session.GetString(SessionDOB);
            objVMCustomerDocument.Gender = HttpContext.Session.GetInt32(SessionGender);

            ViewBag.AccountNumber = objVMCustomerDocument.AccountNumber;
            ViewBag.CustomerName = objVMCustomerDocument.CustomerName;
            ViewBag.CustomerId = objVMCustomerDocument.CustomerId;
            ViewBag.TinNumber = objVMCustomerDocument.TinNumber;
            ViewBag.Address = objVMCustomerDocument.Address;
            ViewBag.MobileNumber = objVMCustomerDocument.MobileNumber;
            ViewBag.Gender = objVMCustomerDocument.Gender;
            ViewBag.Brn = objVMCustomerDocument.Brn;
            ViewBag.Email = objVMCustomerDocument.Email;


            return View(objVMCustomerDocument);
        }


        //=========================================================
        //file upload verification to account number against AccesmentYear
        [HttpPost]
        public IActionResult AddDocument(VMCustomerDocument model)
        {
            //CustomerDocument cd = new CustomerDocument();
            // Check if the user has already uploaded a document for the same assessment year

            //var existingDocument = cd.AccountNumber == model.AccountNumber && cd.AssesmentYear == model.AssesmentYear;
            var existingDocument = _context.CustomerDocuments
                .FirstOrDefault(cd => cd.CustomerId.ToString() == model.CustomerId.ToString() && cd.AssesmentYear == model.AssesmentYear);
            //if (existingDocument.Status != "9" && existingDocument.Status != "0")
            if (existingDocument != null)
            {
                TempData["ErrorMessage"] = "You have already uploaded a document for the same assessment year.";
                return View("SessionYearError");
            }
            else
            {
                // Check if a file is uploaded
                if (model.Document != null && model.Document.Length > 0)
                {
                    string webRoot = environment.WebRootPath;
                    string folder = "Images";
                    string fileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(model.Document.FileName);
                    string fileToWrite;
                    string newFilePath;
                    if (fileExtension.ToLower() == ".pdf")
                    {
                        // Check PDF file size (200 KB limit)
                        if (model.Document.Length > 200 * 1024)
                        {
                            TempData["ErrorMessage"] = "Invalid file size. PDF files should be up to 200 KB.";
                            return View();
                        }
                        fileToWrite = Path.Combine(webRoot, folder, fileName + fileExtension);
                        newFilePath = Path.Combine(folder, fileName + fileExtension);
                    }
                    else if (IsImageFileExtension(fileExtension.ToLower()))
                    {
                        // Check image file size (100 KB limit)
                        if (model.Document.Length > 100 * 1024)
                        {
                            TempData["ErrorMessage"] = "Invalid file size.Image files should be up to 100 KB.";
                            return View();
                        }
                        fileToWrite = Path.Combine(webRoot, folder, fileName + fileExtension);
                        newFilePath = Path.Combine(folder, fileName + fileExtension);
                    }
                    else
                    {
                        // Invalid file type, handle accordingly (e.g., show an error message)
                        TempData["ErrorMessage"] = "Invalid file type. Only PDF and image files are allowed.";
                        return View();
                    }
                    using (var stream = new FileStream(fileToWrite, FileMode.Create))
                    {
                        model.Document.CopyTo(stream);
                    }
                    var tinNumber = HttpContext.Session.GetString(SessionTinNumber); // Get the existing tinNumber value from the session

                    if (model.TinNumber != tinNumber) // Check if the new tinNumber is different from the existing value
                    {
                        HttpContext.Session.SetString(SessionTinNumber, model.TinNumber); // Update the session value with the new tinNumber
                    }

                    var ipnumber = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();


                    //var ipAddress = ipnumber.
                    var customerDocument = new CustomerDocument
                    {
                        CustomerId = (int)HttpContext.Session.GetInt32(SessionCustomerId),
                        //CustomerId = model.CustomerId,
                        CustomerName = HttpContext.Session.GetString(SessionCustomerName),
                        //AccountNumber = HttpContext.Session.GetString(SessionAccountNo),
                        AccountNumber = MaskAccountNumber(HttpContext.Session.GetString(SessionAccountNo)),
                        //TinNumber = model.TinNumber,
                        TinNumber = HttpContext.Session.GetString(SessionTinNumber),// Use the updated TinNumber value from the session
                        AssesmentYear = model.AssesmentYear,
                        Reference = model.Reference,
                        RefNumber = model.RefNumber,
                        Document = newFilePath,
                        SubIP = GetIp(),
                        SubmissionDate = DateTime.Now,
                        Status = 0.ToString(),
                        ProcessDate = DateTime.Now,
                        ProcessIP = GetIp(),
                        //ProcessUser=
                    };
                    _context.CustomerDocuments.Add(customerDocument);
                    _context.SaveChanges();
                    return RedirectToAction("FeedBack");
                }
                // No file uploaded, handle accordingly (e.g., show an error message)
                TempData["ErrorMessage"] = "No file uploaded.";
                return View();
            }
        }

        //0000000000000000000000000000000000000000000


        [HttpPost]
        public IActionResult UpdateStatus(int id, int newStatus)
        {
            var document = _context.CustomerDocuments.FirstOrDefault(cd => cd.CustomerDocumentId == id);
            if (document != null)
            {
                document.Status = newStatus.ToString();
                _context.SaveChanges();

                // Get the customer associated with the document
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == document.CustomerId);

                // Send email notification based on the status
                if (newStatus == 1)
                {
                    // Document Accepted
                    SendEmailToUser(customer, "Document accepted.");
                }
                else if (newStatus == 9)
                {
                    // Document Declined
                    SendEmailToUser(customer, "Document declined. Please resubmit the document.");
                }

                return Ok(); // Status code 200 (OK) will be returned for successful update
            }
            return NotFound(); // Status code 404 (Not Found) if the document is not found
        }


        public void SendEmailToUser(Customer customer, string message)
        {
            string recipientEmail = customer.Email; // Use the Email property from the Customer object
            string sendingMail = _config.MailAddress;
            string host = _config.Host;
            string port = _config.Port;

            MailMessage Mail = new MailMessage();
            Mail.From = new MailAddress(sendingMail);
            Mail.To.Add(recipientEmail);
            Mail.Subject = "Document Status Update";
            Mail.Body = $"Hello {customer.CustomerName},\n\nYour document with TIN Number: {customer.TinNumber} has been {message}\n\nThank you,\nThe Admin Team.";

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = true;
            try
            {
                smtp.Host = host;
                smtp.Port = Convert.ToInt32(port);
                smtp.Send(Mail);
                Mail.Dispose();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log or display error message)
            }
        }


        //00000000000000000000000

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

        private bool IsImageFileExtension(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more image file extensions if needed

            return imageExtensions.Contains(fileExtension);
        }


        //Document upload success view 
        public IActionResult FeedBack()
        {
            return View();
        }

        //Session Year Document upload error view 
        public IActionResult SessionYearError()
        {
            return View();
        }

        public async Task<IActionResult> ViewCustomerDetails(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            // Retrieve the latest CustomerDocument for the customer
            var latestCustomerDocument = await _context.CustomerDocuments
                .Where(cd => cd.CustomerId == id)
                .OrderByDescending(cd => cd.SubmissionDate)
                .FirstOrDefaultAsync();

            // Populate the VMCustomerDetails model
            var viewModel = new VMCustomerDetails
            {
                // Customer Properties
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                AccountNumber = MaskAccountNumber(customer.AccountNumber),
                Address = customer.Address,
                MobileNumber = customer.MobileNumber,
                TinNumber = customer.TinNumber,
                Gender = customer.Gender,
                Brn = customer.Brn,
                Email = customer.Email,
                DOB = customer.DOB,
                // CustomerDocument Properties
                //CustomerDocumentId = latestCustomerDocument?.CustomerDocumentId ?? 0,
                AssesmentYear = latestCustomerDocument?.AssesmentYear ?? 0,
                DocumentPath = latestCustomerDocument?.Document,
                Reference = latestCustomerDocument?.Reference,
                RefNumber = latestCustomerDocument?.RefNumber,
                SubmissionDate = latestCustomerDocument?.SubmissionDate,
                SubUser = latestCustomerDocument?.SubUser,
                SubIP = latestCustomerDocument?.SubIP,
                ProcessDate = latestCustomerDocument?.ProcessDate,
                ProcessUser = latestCustomerDocument?.ProcessUser,
                ProcessIP = latestCustomerDocument?.ProcessIP,
                Status = latestCustomerDocument?.Status,
                Remark = latestCustomerDocument?.Remark,
                Location = latestCustomerDocument?.Location,
                BF1 = latestCustomerDocument?.BF1,
                BF2 = latestCustomerDocument?.BF2,
                BF3 = latestCustomerDocument?.BF3,
                BF4 = latestCustomerDocument?.BF4,
                BF5 = latestCustomerDocument?.BF5,
                BF6 = latestCustomerDocument?.BF6
            };
            return View(viewModel);
        }





        //[HttpPost]
        //public IActionResult Export()
        //{
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        DataTable dt = this.GetCustomers().Tables[0];
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
        //        }
        //    }
        //}


        // Other using statements...

        public IActionResult Export()
        {
            DataSet ds = new DataSet();

            // Get data from Customers table and convert to DataTable
            DataTable customerTable = new DataTable("Customers");
            customerTable.Columns.Add("CustomerId", typeof(int));
            customerTable.Columns.Add("CustomerName", typeof(string));
            customerTable.Columns.Add("AccountNumber", typeof(string));
            customerTable.Columns.Add("Address", typeof(string));
            customerTable.Columns.Add("MobileNumber", typeof(string));
            customerTable.Columns.Add("TinNumber", typeof(string));
            customerTable.Columns.Add("Gender", typeof(int));
            customerTable.Columns.Add("Brn", typeof(string));
            customerTable.Columns.Add("Email", typeof(string));
            customerTable.Columns.Add("DOB", typeof(DateTime));

            // Fetch customer data and add rows to the DataTable
            List<Customer> customers = _context.Customers.ToList(); // Implement this method to fetch data from the Customers table
            foreach (var customer in customers)
            {
                customerTable.Rows.Add(
                    customer.CustomerId,
                    customer.CustomerName,
                    customer.AccountNumber,
                    customer.Address,
                    customer.MobileNumber,
                    customer.TinNumber,
                    customer.Gender,
                    customer.Brn,
                    customer.Email,
                    customer.DOB
                );
            }

            // Get data from CustomerDocuments table and convert to DataTable
            DataTable customerDocumentTable = new DataTable("CustomerDocuments");
            customerDocumentTable.Columns.Add("CustomerDocumentId", typeof(int));
            customerDocumentTable.Columns.Add("CustomerId", typeof(int));
            customerDocumentTable.Columns.Add("CustomerName", typeof(string));
            customerDocumentTable.Columns.Add("AccountNumber", typeof(string));
            customerDocumentTable.Columns.Add("TinNumber", typeof(string));
            customerDocumentTable.Columns.Add("AssesmentYear", typeof(int));
            customerDocumentTable.Columns.Add("Document", typeof(string));
            customerDocumentTable.Columns.Add("Reference", typeof(string));
            customerDocumentTable.Columns.Add("RefNumber", typeof(string));
            customerDocumentTable.Columns.Add("SubmissionDate", typeof(DateTime));
            customerDocumentTable.Columns.Add("SubUser", typeof(string));
            customerDocumentTable.Columns.Add("SubIP", typeof(string));
            customerDocumentTable.Columns.Add("ProcessDate", typeof(DateTime));
            customerDocumentTable.Columns.Add("ProcessUser", typeof(string));
            customerDocumentTable.Columns.Add("ProcessIP", typeof(string));
            customerDocumentTable.Columns.Add("Status", typeof(string));
            customerDocumentTable.Columns.Add("Remark", typeof(string));
            customerDocumentTable.Columns.Add("Location", typeof(string));
            customerDocumentTable.Columns.Add("BF1", typeof(string));
            customerDocumentTable.Columns.Add("BF2", typeof(string));
            customerDocumentTable.Columns.Add("BF3", typeof(string));
            customerDocumentTable.Columns.Add("BF4", typeof(string));
            customerDocumentTable.Columns.Add("BF5", typeof(string));
            customerDocumentTable.Columns.Add("BF6", typeof(string));

            // Fetch customer document data and add rows to the DataTable
            List<CustomerDocument> customerDocuments = _context.CustomerDocuments.ToList(); // Implement this method to fetch data from the CustomerDocuments table
            foreach (var customerDocument in customerDocuments)
            {
                customerDocumentTable.Rows.Add(
                    customerDocument.CustomerDocumentId,
                    customerDocument.CustomerId,
                    customerDocument.CustomerName,
                    customerDocument.AccountNumber,
                    customerDocument.TinNumber,
                    customerDocument.AssesmentYear,
                    customerDocument.Document,
                    customerDocument.Reference,
                    customerDocument.RefNumber,
                    customerDocument.SubmissionDate,
                    customerDocument.SubUser,
                    customerDocument.SubIP,
                    customerDocument.ProcessDate,
                    customerDocument.ProcessUser,
                    customerDocument.ProcessIP,
                    customerDocument.Status,
                    customerDocument.Remark,
                    customerDocument.Location,
                    customerDocument.BF1,
                    customerDocument.BF2,
                    customerDocument.BF3,
                    customerDocument.BF4,
                    customerDocument.BF5,
                    customerDocument.BF6
                );
            }

            // Add both DataTables to the DataSet
            ds.Tables.Add(customerTable);
            ds.Tables.Add(customerDocumentTable);

            using (XLWorkbook wb = new XLWorkbook())
            {
                // Add each DataTable as a worksheet to the workbook
                foreach (DataTable dt in ds.Tables)
                {
                    wb.Worksheets.Add(dt);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream); // Save the workbook to the memory stream
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }


        //https://www.aspsnippets.com/Articles/Export-DataTable-DataSet-to-Excel-file-in-ASPNet-MVC.aspx

        private DataSet GetCustomers()
        {
            DataSet ds = new DataSet();
            string constr = @"server=W-HO-IT-056\SQLEXPRESS;database=PSR_DB;User ID=anas;password=Test@123";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT c.CustomerId,c.CustomerName,c.AccountNumber,c.Address,c.MobileNumber,c.TinNumber,c.Gender,c.Brn,c.Email,c.DOB,cd.AssesmentYear,cd.Document,cd.ProcessIP FROM Customers c INNER JOIN CustomerDocuments cd ON c.CustomerId = cd.CustomerId";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }

            return ds;
        }
    }
}










