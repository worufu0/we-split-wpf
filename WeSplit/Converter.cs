using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WeSplit
{
    class RelativeToAbsoluteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            string result = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Images\\{relative}";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class SignToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string signString = (string)value;
            string result;

            if (signString[0] == '-')
            {
                result = "#FF4848";
            }

            else
            {
                result = "#79FF52";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class SelectedToDisabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int selectedIndex = (int)value;

            if (selectedIndex == -1)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = (bool)value;
            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = (bool)value;
            return !result;
        }
    }

    class IntegerToRadioValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integerValue = (int)value;

            if (integerValue == int.Parse(parameter.ToString()))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ItemCountToEmtyNoficationCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int itemCount = (int)value;
            Visibility result;

            if (itemCount == 0)
            {
                result = Visibility.Visible;
            }

            else
            {
                result = Visibility.Collapsed;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class WindowHeightToTitleTextSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double windowHeight = (double)value;
            double result = windowHeight / 26;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double titleTextSize = (double)value;
            double result = titleTextSize * 26;
            return result;
        }
    }

    class WindowHeightToHeaderTextSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double windowHeight = (double)value;
            double result = windowHeight / 39;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double headerTextSize = (double)value;
            double result = headerTextSize * 39;
            return result;
        }
    }

    class WindowHeightToNormalTextSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double windowHeight = (double)value;
            double result = windowHeight / 56.56;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double normalTextSize = (double)value;
            double result = normalTextSize * 56.56;
            return result;
        }
    }

    class ListViewHeightToItemHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double listViewHeight = (double)value;
            double result = listViewHeight / 1.25;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double titleTextSize = (double)value;
            double result = titleTextSize * 1.25;
            return result;
        }
    }

    class ItemHeightToItemWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double itemHeight = (double)value;
            double result = itemHeight * 1.57;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double itemWidth = (double)value;
            double result = itemWidth / 1.57;
            return result;
        }
    }
}
