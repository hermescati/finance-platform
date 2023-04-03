using Expensier.Domain.Services.Portfolio;
using Expensier.WPF.State.Portfolio;
using LiveCharts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class PortfolioPerformanceViewModel : ViewModelBase
    {
        private readonly PortfolioStore _portfolioStore;
        private readonly ObservableCollection<ReturnsDataModel> _returns;

        private bool _listEmpty;
        public bool ListEmpty
        {
            get
            {
                return _listEmpty;
            }
            set
            {
                _listEmpty = value;
                OnPropertyChanged(nameof(ListEmpty));
            }
        }

        private bool _listNotEmpty;
        public bool ListNotEmpty
        {
            get
            {
                return _listNotEmpty;
            }
            set
            {
                _listNotEmpty = value;
                OnPropertyChanged(nameof(ListNotEmpty));
            }
        }

        private ChartValues<double> _chartSeries;
        public ChartValues<double> ChartSeries
        {
            get
            {
                return _chartSeries;
            }
            set
            {
                _chartSeries = value;
                OnPropertyChanged(nameof(ChartSeries));
            }
        }

        public ObservableCollection<string> xAxis { get; }

        public IEnumerable<ReturnsDataModel> Returns => _returns;

        public PortfolioPerformanceViewModel(PortfolioStore portfolioStore)
        {
            _portfolioStore = portfolioStore;
            _returns = new ObservableCollection<ReturnsDataModel>();
            xAxis = new ObservableCollection<string>();

            _portfolioStore.StateChanged += Returns_StateChanged;

            ResetReturns();
        }

        private void ResetReturns()
        {
            IEnumerable<ReturnsDataModel> returns = _portfolioStore.Returns
                .Select(r => new ReturnsDataModel(r.RecordedDate, r.ReturnPercentage))
                .OrderByDescending(o => o.RecordedDate);

            _returns.Clear();
            foreach (ReturnsDataModel dataModel in returns)
            {
                _returns.Add(dataModel);
            }

            if (_returns.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            ConstructSeries(_returns);
        }

        private void Returns_StateChanged()
        {
            ResetReturns();
        }

        private void ConstructSeries(IEnumerable<ReturnsDataModel> returns)
        {
            returns = returns
                .Where(r => r.RecordedDate.Month == DateTime.Now.Month && r.RecordedDate.Year == DateTime.Now.Year)
                .GroupBy(r => r.RecordedDate.Date)
                .Select(g => new ReturnsDataModel(g.Key.ToString("ddd, d"), g.Sum(r => r.ReturnPercentage)));

            ChartSeries = new ChartValues<double>(returns.Select(r => r.ReturnPercentage));

            xAxis.Clear();
            foreach (string record in returns.Select(r => r.DateFormat))
            {
                xAxis.Add(record);
            }
        }
    }
}
