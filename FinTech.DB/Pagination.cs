using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB
{
    public  class Pagination<T>
    {
        public List<T> Translist { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
