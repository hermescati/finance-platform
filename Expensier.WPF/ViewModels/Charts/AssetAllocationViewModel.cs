using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Assets;
using Expensier.WPF.Utils;
using Expensier.WPF.ViewModels.Assets;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;


namespace Expensier.WPF.ViewModels.Charts
{
    public class AssetAllocationViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        public AssetViewModel AssetViewModel { get; }


        private readonly IEnumerable<AssetModel> _assets;
        public IEnumerable<AssetModel> Assets => _assets;

        private readonly ObservableCollection<ISeries> _series;
        public IEnumerable<ISeries> Series => _series;

        private readonly ObservableCollection<ChartModel> _legend;
        public IEnumerable<ChartModel> Legend => _legend;


        private double _totalValue;
        public double TotalValue
        {
            get => _totalValue;
            set
            {
                _totalValue = value;
                OnPropertyChanged( nameof( TotalValue ) );
            }
        }


        public AssetAllocationViewModel( AssetStore assetStore )
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetModel>();
            _series = new ObservableCollection<ISeries>();
            _legend = new ObservableCollection<ChartModel>();

            AssetViewModel = new AssetViewModel( assetStore,
                assets => assets
                .OrderBy( c => c.PurchaseDate ) );

            _assets = AssetViewModel.Assets;

            ConstructChart( _assets );
        }


        private void ConstructChart( IEnumerable<AssetModel> assets )
        {
            _series.Clear();

            List<ChartModel> grouppedAssets = assets
                .GroupBy( a => a.Asset.Symbol )
                .Select( g => new ChartModel(
                    label: g.Key.ToString().ToUpper(),
                    percentage: g.Sum( a => a.TotalValue ),
                    detailedLabel: g.Select( a => a.Asset.Name ).FirstOrDefault() ) )
                .OrderByDescending( g => g.SeriesValue )
                .ToList();

            if ( grouppedAssets.Count() > 5 )
            {
                double otherAssetsValue = grouppedAssets.Skip( 5 ).Sum( g => g.SeriesValue );
                grouppedAssets = grouppedAssets.Take( 5 ).ToList();
                grouppedAssets.Add( new ChartModel( "Others", otherAssetsValue ) );
            }

            TotalValue = grouppedAssets.Sum( a => a.SeriesValue );

            foreach ( ChartModel group in grouppedAssets )
            {
                int index = grouppedAssets.IndexOf( group );

                ISeries pieSeries = new PieSeries<double>
                {
                    Name = group.SeriesLabel == "Others" 
                        ? group.SeriesLabel 
                        : $"{group.DetailedLabel} ({group.SeriesLabel})",
                    Values = new ObservableCollection<double> { group.SeriesValue },
                    Fill = new SolidColorPaint( ChartSettings.ApplyPieChartColor( index ) ),
                    OuterRadiusOffset = ChartSettings.outerRadiusOffset,
                    MaxRadialColumnWidth = ChartSettings.maxRadialColumnWidth,
                    ToolTipLabelFormatter = ( chartPoint ) => $"{group.SeriesValue:C2}",
                };

                _series.Add( pieSeries );

                ConstructLegend( _series );
            }
        }


        private void ConstructLegend( ObservableCollection<ISeries> seriesCollection )
        {
            _legend.Clear();

            foreach ( ISeries series in seriesCollection )
            {
                int index = seriesCollection.IndexOf( series );

                double assetValue = series.Values.Cast<double>().ToList()[0];
                SKColor seriesColor = ChartSettings.ApplyPieChartColor( index );

                ChartModel legendData = new ChartModel(
                    label: series.Name,
                    percentage: assetValue / TotalValue,
                    color: new SolidColorBrush( Color.FromArgb( seriesColor.Alpha, seriesColor.Red, seriesColor.Green, seriesColor.Blue ) ) );

                _legend.Add( legendData );
            }
        }
    }
}
