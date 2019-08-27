using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SpootLight.Controllers
{
    #region HeaderToImageConverter
    [ValueConversion(typeof(string), typeof(bool))]
    public class ExcelController : IValueConverter
    {
        public static ExcelController Instance = new ExcelController();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((value as string).EndsWith(".xlsx") || (value as string).Contains(".xls"))
            {
                Uri uri = new Uri("pack://application:,,,/Img/excel.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                if ((value as string).Contains(@"\"))
                {
                    Uri uri = new Uri("pack://application:,,,/Img/diskdrive.png");
                    BitmapImage source = new BitmapImage(uri);
                    return source;
                }
                else
                {
                    Uri uri = new Uri("pack://application:,,,/Img/folder.png");
                    BitmapImage source = new BitmapImage(uri);
                    return source;
                }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
    #endregion // DoubleToIntegerConverter
}
