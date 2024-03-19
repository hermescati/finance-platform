using Expensier.WPF.Commands;
using Expensier.WPF.DataObjects;
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
        private readonly IEnumerable<TransactionModel> _transactions;

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

        public IEnumerable<TransactionModel> Transactions => _transactions;

        //public PredictionsViewModel(TransactionStore transactionStore)
        //{
        //    _transactionStore = transactionStore;
        //    _transactions = new ObservableCollection<TransactionModel>();

        //    TransactionViewModel = new TransactionViewModel(transactionStore, transactions => transactions
        //        .OrderBy(t => t.ProcessedDate )
        //        .Where(t => t.IsCredit == true)
        //        .Where(t => t.ProcessedDate >= new DateTime(DateTime.Now.Year, 1, 1) && t.ProcessedDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
        //        .GroupBy(t => t.ProcessedDate.Month)
        //        .Select(g => new TransactionModel(
        //            new DateTime(DateTime.Now.Year, g.Key, DateTime.DaysInMonth(DateTime.Now.Year, g.Key)),
        //            g.Sum(t => t.Amount))));

        //    _transactions = TransactionViewModel.Transactions;

        //    _knnModel = new KNNRegression(3);
        //    _linearModel = new LinearRegression();

        //    TrainKnnModel(_transactions);
        //    TrainLinearModel(_transactions);
        //    PredictData();
        //}

        private void TrainLinearModel(IEnumerable<TransactionModel> transactions)
        {
            double[] inputX = new double[transactions.Count()];
            double[] inputY = new double[transactions.Count()];
            List<TransactionModel> transactionsList = transactions.ToList();

            for (int i = 0; i < transactionsList.Count(); i++)
            {
                inputX[i] = TranslateDateToDays(transactionsList[i].ProcessedDate );
                inputY[i] = transactionsList[i].Amount;
            }

            _linearModel.Fit(inputX, inputY);
            CalculateError(inputX, inputY);
        }

        private void TrainKnnModel(IEnumerable<TransactionModel> transactions)
        {
            double[] inputData = new double[transactions.Count()];
            double[] dataLabels = new double[transactions.Count()];
            List<TransactionModel> transactionsList = transactions.ToList();

            for (int i = 0; i < transactionsList.Count(); i++)
            {
                inputData[i] = TranslateDateToDays(transactionsList[i].ProcessedDate );
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

        private void CalculateError(double[] trainX, double[] trainY)
        {
            double errorLR = _linearModel.MeanAbsoluteError(trainX, trainY);
            double errorLRPercentage = _linearModel.MeanAbsolutePercentageError(trainX, trainY);

            double errorKNN = _knnModel.MeanAbsoluteError(trainX, trainY);
            double errorKNNPercentage = _knnModel.MeanAbsolutePercentageError(trainX, trainY);
        }
    }
}
