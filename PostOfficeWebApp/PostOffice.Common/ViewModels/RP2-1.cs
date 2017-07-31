using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.Common.ViewModels
{
    public class RP2_1
    {
        public int? STT { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalCash { get; set; }
        public decimal? VatOfTotalTalCash { get; set; }
        public decimal? TotalDebt { get; set; }
        public decimal? VatOfTotalDebt { get; set; }
        public decimal? TotalEarn { get; set; }
    }
}
