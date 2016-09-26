using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using PigsInTheWoods.Data;
using PigsInTheWoods.Models.NavViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Controllers
{
    public class ClientControllerBase : Controller
    {
        protected ApplicationDbContext _db;

        public ClientControllerBase(ApplicationDbContext db)
        {
            _db = db;
        }

        public override ViewResult View()
        {
            Nav();
            return base.View();
        }

        public override ViewResult View(object model)
        {
            Nav();
            return base.View(model);
        }

        public override ViewResult View(string viewName)
        {
            Nav();
            return base.View(viewName);
        }

        public override ViewResult View(string viewName, object model)
        {
            Nav();
            return base.View(viewName, model);
        }

        private void Nav()
        {
            ViewData["PagesViewModel"] = PagesViewModel.Get(_db);
        }
    }
}
