﻿using ContactsApp.Filters;
using ContactsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactsApp.Controllers {
    [LoggedUserPage]
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}