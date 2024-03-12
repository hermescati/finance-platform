using Expensier.WPF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Expensier.WPF.Converters
{
    public class CompanyNameToLogoConverter : IValueConverter
    {
        private Dictionary<CompanyName, ImageSource> companyLogoMap;

        public CompanyNameToLogoConverter()
        {
            InitializeCompanyLogoMap();
        }

        private void InitializeCompanyLogoMap()
        {
            companyLogoMap = new Dictionary<CompanyName, ImageSource>
            {
                { CompanyName.Adobe, App.Current.FindResource("AdobeLogo") as ImageSource },
                { CompanyName.Figma, App.Current.FindResource("FigmaLogo") as ImageSource },
                { CompanyName.Apple, App.Current.FindResource("AppleLogo") as ImageSource },
                { CompanyName.Spotify, App.Current.FindResource("SpotifyLogo") as ImageSource },
                { CompanyName.Netflix, App.Current.FindResource("NetflixLogo") as ImageSource },
                { CompanyName.Disney, App.Current.FindResource("DisneyLogo") as ImageSource }
            };
        }

        public object Convert(object value, Type targetType, object paramter, CultureInfo culture)
        {
            if (value == null || !(value is string))
            {
                return null;
            }

            string companyName = (string)value;
            if (Enum.TryParse(companyName, out CompanyName name) && companyLogoMap.ContainsKey( name))
            {
                return companyLogoMap[name];
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
