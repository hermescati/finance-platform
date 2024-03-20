using Expensier.WPF.DataObjects;
using Expensier.WPF.Models;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Expensier.WPF.ViewModels.Transactions
{
    public class PredictionsViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly KNNRegression _knnModel;
        private readonly LinearRegression _linearModel;
        
        public TransactionViewModel TransactionViewModel { get; }

        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;


        private bool _correctData = true;
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
        private double _linearResult;
        private double _forecastedResult;
        public double ForecastedResult
        {
            get => _forecastedResult;
            set
            {
                _forecastedResult = value;
                OnPropertyChanged( nameof( ForecastedResult ) );
            }
        }


        public PredictionsViewModel( TransactionStore transactionStore )
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionModel>();

            TransactionViewModel = new TransactionViewModel(
                transactionStore,
                transactions => transactions );

            IEnumerable<PredictionModel> filteredTransactions = TransactionViewModel.Transactions
                .Where( t => t.IsCredit )
                .Where( t => t.ProcessedDate >= new DateTime( DateTime.Now.Year, 1, 1 ) && t.ProcessedDate < new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ) )
                .OrderBy( t => t.ProcessedDate )
                .GroupBy( t => t.ProcessedDate.Month )
                .Select( g => new PredictionModel(
                    new DateTime( DateTime.Now.Year, g.Key, DateTime.DaysInMonth( DateTime.Now.Year, g.Key ) ),
                    g.Sum( t => t.Amount ) ) );

            _knnModel = new KNNRegression( 3 );
            _linearModel = new LinearRegression();

            TrainModels( filteredTransactions, filteredTransactions.Count() );

            if ( !CorrectData ) return;

            PredictData( filteredTransactions.Count() );
            CalculateResults();
        }


        private void TrainModels( IEnumerable<PredictionModel> transactions, int transactionsNumber )
        {
            if ( transactionsNumber < 2 )
            {
                CorrectData = false;
                return;
            }

            double[] inputX = new double[transactionsNumber];
            double[] inputY = new double[transactionsNumber];

            List<PredictionModel> transactionsList = transactions.ToList();

            foreach ( PredictionModel transaction in transactionsList )
            {
                int index = transactionsList.IndexOf( transaction );

                inputX[index] = TranslateDateToDays( transactionsList[index].Date );
                inputY[index] = transactionsList[index].Amount;
            }

            _linearModel.Fit( inputX, inputY );

            if ( transactionsNumber > 3 )
                _knnModel.Fit( inputX, inputY );
        }


        private void PredictData( int transactionsNumber )
        {
            DateTime predictionDate = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth( DateTime.Now.Year, DateTime.Now.Month ) );
            double[] predictionInput = new double[] { TranslateDateToDays( predictionDate ) };

            _linearResult = _linearModel.Predict( predictionInput[0] );

            if ( transactionsNumber > 3 )
                _knnResult = _knnModel.Predict( predictionInput );
        }


        private void CalculateResults()
        {
            if ( _linearResult == 0 && _knnResult == 0 )
            {
                CorrectData = false;
                return;
            }

            ForecastedResult = _linearResult + _knnResult / 2;
        }


        private double TranslateDateToDays( DateTime dateToTranslate )
        {
            DateTime referenceDate = new DateTime( DateTime.Now.Year, 1, 1 );

            return ( dateToTranslate - referenceDate ).TotalDays;
        }
    }
}
