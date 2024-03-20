using Expensier.WPF.DataObjects;
using Expensier.WPF.Models;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Expensier.WPF.ViewModels.Transactions.PredictionModel;


namespace Expensier.WPF.ViewModels.Transactions
{
    public class PredictionModel
    {
        public enum PredictionModels
        {
            KNN,
            Linear
        }


        public DateTime Date { get; set; }
        public double Amount { get; set; }


        public PredictionModel( DateTime date, double amount )
        {
            Date = date;
            Amount = amount;
        }
    }

    public class PredictionsViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly KNNRegression _knnModel;
        private readonly LinearRegression _linearModel;
        public TransactionViewModel TransactionViewModel { get; }


        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;


        private bool _correctData;
        public bool CorrectData
        {
            get => _correctData;
            set
            {
                _correctData = value;
                OnPropertyChanged( nameof( CorrectData ) );
            }
        }

        private double _knnResult;
        public double KnnResult
        {
            get => _knnResult;
            set
            {
                _knnResult = value;
                OnPropertyChanged( nameof( KnnResult ) );
            }
        }

        private double _linearResult;
        public double LinearResult
        {
            get => _linearResult;
            set
            {
                _linearResult = value;
                OnPropertyChanged( nameof( LinearResult ) );
            }
        }


        public PredictionsViewModel( TransactionStore transactionStore )
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionModel>();

            TransactionViewModel = new TransactionViewModel(
                transactionStore, transactions => transactions
                .Where( t => t.IsCredit )
                .Where( t => t.ProcessedDate >= new DateTime( DateTime.Now.Year, 1, 1 ) && t.ProcessedDate < new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ) )
                .OrderBy( t => t.ProcessedDate ) );

            IEnumerable<PredictionModel> filteredTransactions = TransactionViewModel.Transactions
                .GroupBy( t => t.ProcessedDate.Month )
                .Select( g => new PredictionModel(
                    new DateTime( DateTime.Now.Year, g.Key, DateTime.DaysInMonth( DateTime.Now.Year, g.Key ) ),
                    g.Sum( t => t.Amount ) ) );

            int transactionsNumber = filteredTransactions.Count();

            //_knnModel = new KNNRegression( 3 );
            _linearModel = new LinearRegression();

            TrainLinearModel( filteredTransactions, transactionsNumber );
            //TrainKnnModel( filteredTransactions, transactionsNumber );


            PredictData();
        }


        private void TrainLinearModel( IEnumerable<PredictionModel> transactions, int count )
        {
            double[] inputX = new double[count];
            double[] inputY = new double[count];

            List<PredictionModel> transactionsList = transactions.ToList();

            foreach ( PredictionModel transction in transactionsList )
            {
                int index = transactionsList.IndexOf( transction );

                inputX[index] = TranslateDateToDays( transactionsList[index].Date );
                inputY[index] = transactionsList[index].Amount;
            }

            _linearModel.Fit( inputX, inputY );
        }


        private void TrainKnnModel( IEnumerable<PredictionModel> transactions, int count )
        {
            double[] inputData = new double[count];
            double[] dataLabels = new double[count];

            List<PredictionModel> transactionsList = transactions.ToList();

            for ( int i = 0; i < transactionsList.Count(); i++ )
            {
                inputData[i] = TranslateDateToDays( transactionsList[i].Date );
                dataLabels[i] = transactionsList[i].Amount;
            }

            _knnModel.Fit( inputData, dataLabels );
        }

        private void PredictData()
        {
            DateTime predictionDate = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth( DateTime.Now.Year, DateTime.Now.Month ) );
            double date = TranslateDateToDays( predictionDate );

            double[] input = new double[]
            {
                date
            };

            //KnnResult = _knnModel.Predict( input );
            LinearResult = _linearModel.Predict( date );
        }

        private double TranslateDateToDays( DateTime dateToTranslate )
        {
            DateTime referenceDate = new DateTime( DateTime.Now.Year, 1, 1 );

            return (dateToTranslate - referenceDate).TotalDays;
        }

        //private void CalculateError( double[] trainX, double[] trainY )
        //{
        //    double errorLR = _linearModel.MeanAbsoluteError( trainX, trainY );
        //    double errorLRPercentage = _linearModel.MeanAbsolutePercentageError( trainX, trainY );

        //    double errorKNN = _knnModel.MeanAbsoluteError( trainX, trainY );
        //    double errorKNNPercentage = _knnModel.MeanAbsolutePercentageError( trainX, trainY );
        //}
    }
}
