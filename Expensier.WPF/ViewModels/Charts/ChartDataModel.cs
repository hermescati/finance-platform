using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartDataModel : ViewModelBase
    {
        public string Label { get; set; }
        public double TotalAmount { get; set; }


        public ChartDataModel(string label, double amount )
        {
            Label = label;
            TotalAmount = amount;
        }
    }
}
