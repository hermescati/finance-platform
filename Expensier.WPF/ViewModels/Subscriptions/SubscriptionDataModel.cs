using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionDataModel : ViewModelBase
    {
        public string CompanyName { get; set; }
        public string SubscriptionPlan { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public string SubscriptionCycle { get; set; } 

        public SubscriptionDataModel(string companyName, string subscriptionPlan, DateTime dueDate, double amount, string subscriptionCycle)
        {
            CompanyName = companyName;
            SubscriptionPlan = subscriptionPlan;
            DueDate = dueDate;
            Amount = amount;
            SubscriptionCycle = subscriptionCycle;
        }
    }
}
