using Expensier.WPF.State.Crypto;
using Expensier.WPF.ViewModels.Cryptos;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Expensier.WPF.ViewModels.Charts
{
    public class AssetAllocationViewModel : ViewModelBase
    {
        private readonly CryptoStore _cryptoStore;
        private readonly IEnumerable<CryptoDataModel> _cryptos;
        public CryptoViewModel CryptoViewModel { get; }
        public IEnumerable<CryptoDataModel> Cryptos => _cryptos;

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

        public SeriesCollection Series { get; }
        public ColorsCollection SeriesColors { get; }

        public AssetAllocationViewModel(CryptoStore cryptoStore)
        {
            _cryptos = new ObservableCollection<CryptoDataModel>();

            Series = new SeriesCollection();
            SeriesColors = new ColorsCollection();

            _cryptoStore = cryptoStore;
            CryptoViewModel = new CryptoViewModel(cryptoStore,
                cryptos => cryptos
                .OrderBy(c => c.TotalValue));

            _cryptos = CryptoViewModel.Cryptos;

            if (_cryptos.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            ConstructChart(_cryptos);
        }

        private void ConstructChart(IEnumerable<CryptoDataModel> cryptos)
        {
            Series.Clear();
            Series.AddRange(cryptos
                .GroupBy(c => c.Crypto.Symbol)
                .Select(g => new PieSeries
                {
                    Title = g.Key.ToString(),
                    Values = new ChartValues<double>(new[]
                    {
                        g.Sum(c => c.TotalValue)
                    }),
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 25, 27)),
                    StrokeThickness = 8
                }));

            SeriesColors.Clear();
            SeriesColors.AddRange(new[] { "#FFA7DDBC", "#FF64927C", "#FF497F76", "#FF255F5B", "#FF3C5549", "#FF2C3E35" }
                      .Select(ColorConverter.ConvertFromString)
                      .OfType<Color>()
                      .ToList());
        }
    }
}
