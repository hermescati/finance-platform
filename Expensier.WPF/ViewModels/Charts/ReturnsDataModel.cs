using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ReturnsDataModel : ViewModelBase
    {
        public DateTime RecordedDate { get; set; }
        public double ReturnPercentage { get; set; }
        public string DateFormat { get; set; }

        public ReturnsDataModel(DateTime recordedDate, double returnPercentage)
        {
            RecordedDate = recordedDate;
            ReturnPercentage = returnPercentage;
        }

        public ReturnsDataModel(string dateFormat, double returnPercentage)
        {
            DateFormat = dateFormat;
            ReturnPercentage = returnPercentage;
        }
    }
}
