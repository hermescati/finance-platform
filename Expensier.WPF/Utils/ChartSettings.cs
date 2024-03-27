using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System.Collections.ObjectModel;


namespace Expensier.WPF.Utils
{
    public class ChartSettings
    {
        private static readonly SKColor _secondaryColor = SKColor.Parse( App.Current.FindResource( "SecondaryColor" ).ToString() );
        private static readonly SKColor _accentColor = SKColor.Parse( App.Current.FindResource( "AccentColor" ).ToString() );
        private static readonly SKColor _foregroundColor = SKColor.Parse( App.Current.FindResource( "ForegroundColor" ).ToString() );
        private static readonly SKColor _shadeColor = SKColor.Parse( App.Current.FindResource( "ShadeColor" ).ToString() );
        public static readonly SKColor PrimaryColor = SKColor.Parse( App.Current.FindResource( "PrimaryColor" ).ToString() );

        private static SKColor[] _pieChartColors;

        private static readonly string _fontFamily = App.Current.FindResource( "Lato" ).ToString();

        private static readonly int _strokeThickness = 2;
        private static readonly int _geometrySize = 8;
        private static readonly int _lineSmoothness = 1;
        private static readonly int _textSize = 14;
        private static readonly int _cornerRadius = 8;
        private static readonly int _padding = 18;
        public static readonly LiveChartsCore.Measure.Margin DrawMargin = new LiveChartsCore.Measure.Margin( 0, -40, 0, 0 );
        public static readonly LiveChartsCore.Measure.Margin AnotherMargin = new LiveChartsCore.Measure.Margin( 0 );


        public static readonly int outerRadiusOffset = 0;
        public static readonly int maxRadialColumnWidth = 56;


        public static ISeries DefaultLineSeries()
        {
            return new LineSeries<double>
            {
                Values = new ObservableCollection<double>(),

                Fill = new LinearGradientPaint(
                    new[] { _secondaryColor.WithAlpha( 50 ), _secondaryColor.WithAlpha( 0 ) },
                    new SKPoint( 0.5f, 0 ),
                    new SKPoint( 0.5f, 1 ) ),
                Stroke = new SolidColorPaint( _secondaryColor ) { StrokeThickness = _strokeThickness },
                GeometryFill = new SolidColorPaint( _secondaryColor ),

                GeometryStroke = new SolidColorPaint( _secondaryColor ) { StrokeThickness = _strokeThickness },
                GeometrySize = _geometrySize,
                LineSmoothness = _lineSmoothness,

                DataPadding = new LvcPoint( 0, 0.5f ),
                EnableNullSplitting = false,
                YToolTipLabelFormatter = ( chartPoint ) => $"{chartPoint.Coordinate.PrimaryValue:C2}"
            };
        }


        public static ISeries AssetLineSeries( bool negative = false )
        {
            return new LineSeries<double>
            {
                Values = new ObservableCollection<double>(),

                Fill = new LinearGradientPaint(
                    new[] {
                        negative ? _accentColor.WithAlpha( 50 ) : _secondaryColor.WithAlpha( 50 ),
                        negative ? _accentColor.WithAlpha( 0 ) : _secondaryColor.WithAlpha( 0 )
                    },
                    new SKPoint( 0.5f, 0 ),
                    new SKPoint( 0.5f, 1 ) ),
                Stroke = new SolidColorPaint( negative ? _accentColor : _secondaryColor ) { StrokeThickness = _strokeThickness },

                GeometrySize = 0,
                LineSmoothness = _lineSmoothness,

                DataPadding = new LvcPoint( 0, 0.0f ),
                IsHoverable = false,
            };
        }


        public static ISeries[] DefaultColumnSeries()
        {
            return new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Values = new ObservableCollection<double>(),

                    Fill = new SolidColorPaint( _shadeColor ),
                    Stroke = null,

                    Padding = _padding,
                    Rx = _cornerRadius,
                    Ry = _cornerRadius,

                    IsHoverable = false,
                    IgnoresBarPosition = true,
                },
                new ColumnSeries<double>
                {
                    Values = new ObservableCollection<double>(),

                    Fill = new SolidColorPaint( _secondaryColor ),
                    Stroke = new SolidColorPaint( _secondaryColor ) { StrokeThickness = _strokeThickness },

                    Padding = _padding,
                    Rx = _cornerRadius,
                    Ry = _cornerRadius,

                    IgnoresBarPosition = true,
                    YToolTipLabelFormatter = ( chartPoint ) => $"{chartPoint.Coordinate.PrimaryValue:C2}"
                }
            };
        }


        public static Axis DefaultXAxis( bool showAxis = true )
        {
            return new Axis
            {
                TextSize = showAxis ? _textSize : 0,
                LabelsPaint = new SolidColorPaint( _foregroundColor.WithAlpha( 170 ) ) { FontFamily = _fontFamily }
            };
        }


        public static Axis DefaultYAxis( bool showAxis = true )
        {
            return new Axis
            {
                Labeler = Labelers.Currency,
                TextSize = showAxis ? _textSize : 0,

                LabelsPaint = new SolidColorPaint( _foregroundColor.WithAlpha( 170 ) ) { FontFamily = _fontFamily },
                SeparatorsPaint = showAxis ? new SolidColorPaint( _foregroundColor.WithAlpha( 102 ) )
                {
                    StrokeThickness = 0.5f,
                    PathEffect = new DashEffect( new float[] { 4, 4 } )
                } : null
            };
        }


        public static SKColor ApplyPieChartColor( int index )
        {
            _secondaryColor.ToHsv( out float hue, out float saturation, out float brightness );

            _pieChartColors = new SKColor[]
            {
                SKColor.FromHsv(hue, saturation, 90),
                SKColor.FromHsv(hue, saturation, 70),
                SKColor.FromHsv(hue, saturation, 50),
                SKColor.FromHsv(hue, saturation, 40),
                SKColor.FromHsv(hue, saturation, 30),
                SKColor.FromHsv(hue, saturation, 20),
            };

            return _pieChartColors[index];
        }

    }
}
