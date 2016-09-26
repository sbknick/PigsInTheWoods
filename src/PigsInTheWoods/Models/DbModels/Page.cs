using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Models.DbModels
{
    public class Page
    {
        public int Id { get; set; }

        public int OrderIndex { get; set; }
        public string PageName { get; set; }
        public string Route { get; set; }
        
        public virtual ICollection<SubPage> SubPages { get; set; }
    }
}
