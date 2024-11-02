using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace u23668475_HW03.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<student> Students { get; set; }
        public IEnumerable<book> Books { get; set; }
        public IEnumerable<borrow> Borrows { get; set; }
        public IEnumerable<author> Authors { get; set; }
        public IEnumerable<type> Types { get; set; }
        public IEnumerable<ChartArchive> ChartArchives { get; set; }
    }
}