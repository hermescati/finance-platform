﻿using Expensier.Domain.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class TransactionDataModel : ViewModelBase
    {
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessDate { get; set; }
        public string DateFormat { get; set; }
        public bool IsCredit { get; }
        public IEnumerable<string> Type { get; set; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public TransactionDataModel(DateTime processDate, double amount)
        {
            ProcessDate = processDate;
            Amount = amount;
        }

        public TransactionDataModel(string dateFormat, double amount)
        {
            DateFormat = dateFormat;
            Amount = amount;
        }

        public TransactionDataModel(string dateFormat, IEnumerable<string> transactionType)
        {
            DateFormat = dateFormat;
            Type = transactionType;
        }

        public TransactionDataModel(string transactionName, DateTime processDate, double amount, string transactionType, bool isCredit)
        {
            TransactionName = transactionName;
            ProcessDate = processDate;
            Amount = amount;
            TransactionType = transactionType;
            IsCredit = isCredit;
        }
    }
}
