using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartDataModel : ViewModelBase
    {
        public string DateLabel { get; set; }
        public double TotalAmount { get; set; }


        public ChartDataModel(string  dateLabel, double amount )
        {
            DateLabel = dateLabel;
            TotalAmount = amount;
        }
    }
}
