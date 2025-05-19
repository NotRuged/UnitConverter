using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Class
{
    public class UnitDefinition
    {
        public int Nr { get; set; }
        public int CategoryID { get; set; }
        public string Unit { get; set; }
        public double Factor { get; set; }
    }

}
