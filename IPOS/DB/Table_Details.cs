using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOS.DB
{
    public class Table_Details
    {
        public int ID { get; set; }
        public string nameTable { get; set; }
        public string Status { get; set; }
        public bool IsTakeAway { get; set; }
    }
}
