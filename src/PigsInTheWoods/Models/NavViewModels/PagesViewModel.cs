using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PigsInTheWoods.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Models.NavViewModels
{
    public class PagesViewModel
    {
        public PageViewModel[] Pages { get; set; }

        private static PagesViewModel _cached;

        public static PagesViewModel Get(ApplicationDbContext db)
        {
            if (_cached != null)
                return _cached;

            _cached = new PagesViewModel
            {
                Pages = new PageViewModel[]
                {
                    new PageViewModel
                    {
                        Id = 1,
                        OrderIndex = 1,
                        PageName = "About",
                        Route = "/Home",
                    },
                    new PageViewModel
                    {
                        Id = 2,
                        OrderIndex = 2,
                        PageName = "History",
                        Route = "/Home/History",
                    },
                    new PageViewModel
                    {
                        Id = 3,
                        OrderIndex = 3,
                        PageName = "Meet the Pigs",
                        Route = "/Home/Pigs",
                        SubPages = new SubPageViewModel[]
                        {
                            new SubPageViewModel
                            {
                                Id = 1,
                                OrderIndex = 1,
                                PageId = 3,
                                PageName = "Pig 1",
                                Route = "/Home/Pig/1",
                            },
                            new SubPageViewModel
                            {
                                Id = 2,
                                OrderIndex = 2,
                                PageId = 3,
                                PageName = "Pig 2",
                                Route = "/Home/Pig/2",
                            },
                            new SubPageViewModel
                            {
                                Id = 3,
                                OrderIndex = 3,
                                PageId = 3,
                                PageName = "Pig 3",
                                Route = "/Home/Pig/3",
                            },
                            new SubPageViewModel
                            {
                                Id = 4,
                                OrderIndex = 4,
                                PageId = 3,
                                PageName = "Pig 4",
                                Route = "/Home/Pig/4",
                            },
                        }
                    },
                    new PageViewModel
                    {
                        Id = 4,
                        OrderIndex = 4,
                        PageName = "Volunteering",
                        Route = "/Home/Volunteering",
                    },
                    new PageViewModel
                    {
                        Id = 5,
                        OrderIndex = 5,
                        PageName = "Donate",
                        Route = "/Home/Donate",
                    },
                    new PageViewModel
                    {
                        Id = 6,
                        OrderIndex = 6,
                        PageName = "Contact",
                        Route = "/Home/Contact",
                    },
                }
            };

            return _cached;


            var pageData =
                from p in db.Pages
                orderby p.OrderIndex
                select new
                {
                    Page = p,
                    SubPages = p.SubPages.OrderBy(sp => sp.OrderIndex),
                };

            var pgVM = new PagesViewModel
            {
                Pages = pageData.Select(p => new PageViewModel
                {
                    Id = p.Page.Id,
                    OrderIndex = p.Page.OrderIndex,
                    PageName = p.Page.PageName,
                    Route = p.Page.Route,
                    SubPages = p.SubPages.Select(sp => new SubPageViewModel
                    {
                        Id = sp.Id,
                        OrderIndex = sp.OrderIndex,
                        PageName = sp.PageName,
                        Route = sp.Route,
                    })
                    .ToArray()
                })
                .ToArray()
            };

            return _cached = pgVM;
        }
    }

    public class PageViewModel
    {
        public int Id { get; set; }
        public int OrderIndex { get; set; }
        public string PageName { get; set; }
        public string Route { get; set; }

        public SubPageViewModel[] SubPages { get; set; }
    }

    public class SubPageViewModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int OrderIndex { get; set; }
        public string PageName { get; set; }
        public string Route { get; set; }

        public PageViewModel Page { get; set; }
    }
}
