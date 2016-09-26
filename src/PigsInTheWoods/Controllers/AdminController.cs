using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PigsInTheWoods.Data;
using PigsInTheWoods.Models;
using PigsInTheWoods.Models.NavViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pages()
        {
            return View();
        }

        public PartialViewResult PageNavEditPartial()
        {
            var pages = PagesViewModel.Get(_db);
            return PartialView(pages);
        }
    }
}
