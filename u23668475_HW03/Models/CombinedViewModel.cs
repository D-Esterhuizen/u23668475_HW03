using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u23668475_HW03.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<student> students { get; set; }
        public IEnumerable<book> books { get; set; }
        public IEnumerable<borrow> borrows { get; set; }
        public IEnumerable<author> authors { get; set; }
    }
}