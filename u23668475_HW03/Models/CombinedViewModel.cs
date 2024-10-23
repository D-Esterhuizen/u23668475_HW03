using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u23668475_HW03.Models
{
    public class CombinedViewModel
    {
        public IPagedList<student> Students { get; set; }
        public IEnumerable<book> Books { get; set; }
        public IEnumerable<borrow> Borrows { get; set; }
        public IEnumerable<author> Authors { get; set; }
    }
}