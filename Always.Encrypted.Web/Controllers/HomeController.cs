using Always.Encrypted.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Always.Encrypted.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Always_Encrypted_DBContext _DBContext;

        public HomeController(ILogger<HomeController> logger, Always_Encrypted_DBContext DBContext)
        {
            _logger = logger;
            _DBContext = DBContext;
        }
        public IActionResult Index()
        {
            return View(_DBContext.ClientInfo.ToList());
        }
        public IActionResult _PartialViewClientData()
        {
            return PartialView(_DBContext.ClientInfo.ToList());

        }
        [HttpPost]
        public bool AddClientData(MainModel MainModelObj)
        {
            ClientInfo ClientObj = new ClientInfo();
            ClientObj.ClientName = MainModelObj.ClientName.Trim();
            ClientObj.VisaNo = MainModelObj.VisaNo.Trim();
            _DBContext.ClientInfo.Add(ClientObj);
            _DBContext.SaveChanges();
            return true;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
