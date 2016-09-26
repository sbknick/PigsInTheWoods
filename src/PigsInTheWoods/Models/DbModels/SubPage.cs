using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Models.DbModels
{
    public class SubPage
    {
        public int Id { get; set; }

        public int PageId { get; set; }
        public int OrderIndex { get; set; }
        public string PageName { get; set; }
        public string Route { get; set; }

        [ForeignKey(nameof(PageId))]
        public virtual Page Page { get; set; }
    }
}
