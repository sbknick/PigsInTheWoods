using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PigsInTheWoods.Data;
using PigsInTheWoods.Models.SiteViewModels;

namespace PigsInTheWoods.Controllers
{
    public class HomeController : ClientControllerBase
    {
        public HomeController(ApplicationDbContext db)
         : base(db)
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Volunteering()
        {
            return View();
        }

        public IActionResult Donate()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Pigs()
        {
            return View();
        }

        public IActionResult Pig(int id)
        {
            var pig = new PigViewModel
            {

            };

            return View("PigSingle", pig);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
