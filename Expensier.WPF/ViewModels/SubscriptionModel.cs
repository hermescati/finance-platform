using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class SubscriptionModel : ViewModelBase
    {
        public string SubscriptionPlan { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }

        public SubscriptionModel(string subscriptionPlan, DateTime dueDate, double amount)
        {
            SubscriptionPlan = subscriptionPlan;
            DueDate = dueDate;
            Amount = amount;
        }
    }
}
