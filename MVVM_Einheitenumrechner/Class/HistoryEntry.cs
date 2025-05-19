using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Class
{
    public class HistoryEntry
    {
        public int ConversionID { get; set; }
        public DateTime Timestamp { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }  // hinzufügen
        public string FromUnit { get; set; }
        public string ToUnit { get; set; }
        public double InputValue { get; set; }
        public string ResultValue { get; set; }
    }



}
