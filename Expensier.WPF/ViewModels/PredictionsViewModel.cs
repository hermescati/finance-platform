using Expensier.WPF.Commands;
using Expensier.WPF.Models;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class PredictionsViewModel : ViewModelBase
    {
        private readonly KNNRegression _knnModel;
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }
        private readonly IEnumerable<TransactionDataModel> _transactions;

        private double _predictionResult;
        public double PredictionResult
        {
            get
            {
                return _predictionResult;
            }
            set
            {
                _predictionResult = value;
                OnPropertyChanged(nameof(PredictionResult));
            }
        }

        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        public PredictionsViewModel(TransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionDataModel>();

            TransactionViewModel = new TransactionViewModel(transactionStore, transactions => transactions
                .OrderBy(t => t.ProcessDate)
                .Where(t => t.IsCredit == true)
                .Where(t => t.ProcessDate >= new DateTime(DateTime.Now.Year, 1, 1) && t.ProcessDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                .GroupBy(t => t.ProcessDate.Month)
                .Select(g => new TransactionDataModel(
                    new DateTime(DateTime.Now.Year, g.Key, DateTime.DaysInMonth(DateTime.Now.Year, g.Key)),
                    g.Sum(t => t.Amount))));

            _transactions = TransactionViewModel.Transactions;

            _knnModel = new KNNRegression(3);
            AddTrainingData(_transactions);
            PredictData();
        }

        private void AddTrainingData(IEnumerable<TransactionDataModel> transactions)
        {
            double[][] inputData = new double[transactions.Count()][];
            double[] dataLabels = new double[transactions.Count()];
            List<TransactionDataModel> transactionsList = transactions.ToList();

            for (int i = 0; i < transactionsList.Count(); i++)
            {
                inputData[i] = new double[]
                {
                    transactionsList[i].Amount,
                    TranslateDateToDays(transactionsList[i].ProcessDate)
                };

                dataLabels[i] = transactionsList[i].Amount;
            }

            _knnModel.Fit(inputData, dataLabels);
        }

        private void PredictData()
        {
            DateTime predictionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            double date = TranslateDateToDays(predictionDate);

            double[] input = new double[]
            {
                date
            };

            PredictionResult = _knnModel.Predict(input);
        }

        private double TranslateDateToDays(DateTime dateToTranslate)
        {
            DateTime referenceDate = new DateTime(DateTime.Now.Year, 1, 1);

            return (dateToTranslate - referenceDate).TotalDays;
        }
    }
}
