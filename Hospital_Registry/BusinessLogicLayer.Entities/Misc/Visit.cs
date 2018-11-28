using BusinessLogicLayer.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Misc
{
    public class Visit
    {
        public DateTime Date { get; set; }
        public Doctor Doctor { get; set; }
        public string Cause { get; set; }
        public string Result { get; set; }
    }
}
