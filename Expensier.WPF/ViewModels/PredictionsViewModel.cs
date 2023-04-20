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
        private readonly LinearRegression _linearModel;
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }
        private readonly IEnumerable<TransactionDataModel> _transactions;

        private double _knnResult;
        public double KnnResult
        {
            get
            {
                return _knnResult;
            }
            set
            {
                _knnResult = value;
                OnPropertyChanged(nameof(KnnResult));
            }
        }

        private double _linearResult;
        public double LinearResult
        {
            get
            {
                return _linearResult;
            }
            set
            {
                _linearResult = value;
                OnPropertyChanged(nameof(LinearResult));
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
            _linearModel = new LinearRegression();

            TrainKnnModel(_transactions);
            TrainLinearModel(_transactions);
            PredictData();
        }

        private void TrainLinearModel(IEnumerable<TransactionDataModel> transactions)
        {
            double[] inputX = new double[transactions.Count()];
            double[] inputY = new double[transactions.Count()];
            List<TransactionDataModel> transactionsList = transactions.ToList();

            for (int i = 0; i < transactionsList.Count(); i++)
            {
                inputX[i] = TranslateDateToDays(transactionsList[i].ProcessDate);
                inputY[i] = transactionsList[i].Amount;
            }

            _linearModel.Fit(inputX, inputY);
        }

        private void TrainKnnModel(IEnumerable<TransactionDataModel> transactions)
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

            KnnResult = _knnModel.Predict(input);
            LinearResult = _linearModel.Predict(date);
        }

        private double TranslateDateToDays(DateTime dateToTranslate)
        {
            DateTime referenceDate = new DateTime(DateTime.Now.Year, 1, 1);

            return (dateToTranslate - referenceDate).TotalDays;
        }
    }
}
