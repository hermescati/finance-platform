using System;

namespace Expensier.WPF.DataObjects
{
    public class PredictionModel
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }


        public PredictionModel( DateTime date, double amount )
        {
            Date = date;
            Amount = amount;
        }
    }
}
