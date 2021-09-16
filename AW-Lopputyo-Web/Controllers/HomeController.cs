using AW_Lopputyo_Web.Models;
using loppuprojekti.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AW_Lopputyo_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _endPoint;
        private readonly string _subscriptionkey;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _endPoint = configuration.GetConnectionString("EndPoint");
            _subscriptionkey = configuration.GetConnectionString("SubscriptionKey");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Login form where the user can put his data, in order to log into the application.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string? message)
        {
            var session = HttpContext.Session;
            if (!string.IsNullOrEmpty(session.GetString("DeviceId")))
            {
                return RedirectToAction("ViewStatistics");
            }
            ViewData["message"] = message ?? null;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginInfo loginInfo)
        {
            var apiUtil = new LoginApi(_configuration, _logger);
            var userExists = apiUtil.UserExists(loginInfo);
            if (userExists.Result)
            {
                var session = HttpContext.Session;
                session.SetString("DeviceId", loginInfo.DeviceId);
                return RedirectToAction("ViewStatistics");
            }

            string message = "";
            if (userExists.Result == true)
            {
                message = "true";
            } 
            else if (userExists.Result == false)
            {
                message = "false";
            }
            ViewData["message"] = message;
            return View();
        }
        /// <summary>
        /// Creates the registering form for the unregisters user to put his data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateNewUser()
        {
            return View();
        }

        /// <summary>
        /// Get the data entered by the user into the register form, through a POST request and sends them then to the backend, which insert them to the database.
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNewUser(NewUserInfo newUserInfo)
        {
            string message = "";
            if (newUserInfo.Password1 != newUserInfo.Password2)
            {
                message = "Passwords did not match!";
                ViewData["message"] = message;
                return View();
            }

            var apiUtil = new LoginApi(_configuration, _logger);
            var response = apiUtil.CreateUser(newUserInfo);

            if (response.Result == true)
            {
                message = "true";
            }
            else if (response.Result == false)
            {
                message = "false";
            }
            ViewData["message"] = message;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Sets all data needed for the charts to ViewBags.
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewStatistics()
        {
            var session = HttpContext.Session;
            var deviceId = session.GetString("DeviceId");
            if (deviceId!= null)
            {
                // testi
                AnalyticsApi api = new AnalyticsApi();
                var history = api.AllData(deviceId, $"{_endPoint}/api/dataanalytics", _subscriptionkey).Result;
                ViewBag.History = JsonConvert.SerializeObject(history);
                var data7days = api.AllData(deviceId, $"{_endPoint}/api/dataanalytics/7days", _subscriptionkey).Result;
                ViewBag.Data7Days = JsonConvert.SerializeObject(data7days);
                var data1day = api.AllData(deviceId, $"{_endPoint}/api/dataanalytics/1day", _subscriptionkey).Result;
                ViewBag.Data1Day = JsonConvert.SerializeObject(data1day);
                var happiestDomains = api.HappiestDomains(deviceId, $"{_endPoint}/api/dataanalytics/domainemotion", _subscriptionkey).Result;
                ViewBag.HappiestDomains = JsonConvert.SerializeObject(happiestDomains);
                var saddestDomains = api.SaddestDomains(deviceId, $"{_endPoint}/api/dataanalytics/domainemotion", _subscriptionkey).Result;
                ViewBag.SaddestDomains = JsonConvert.SerializeObject(saddestDomains);
                var happiestHours = api.HappiestHours(deviceId, $"{_endPoint}/api/dataanalytics/domainemotion", _subscriptionkey).Result;
                ViewBag.HappiestHours = JsonConvert.SerializeObject(happiestHours);
                var saddestHours = api.SaddestHours(deviceId, $"{_endPoint}/api/dataanalytics/domainemotion", _subscriptionkey).Result;
                ViewBag.SaddestHours = JsonConvert.SerializeObject(saddestHours);
                var happiestDays = api.HappiestDays(deviceId, $"{_endPoint}/api/dataanalytics/weekday", _subscriptionkey).Result;
                ViewBag.HappiestDays = JsonConvert.SerializeObject(happiestDays);
                // testi

                return View();
            }
            return RedirectToAction("Login");
        }

        public IActionResult WelcomeText()
        {
            return View();
        }

        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            session.SetString("DeviceId", "");
            return RedirectToAction("Login");
        }
    }
}

