using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace TestExerciserPro.TEViews.AutoTesting.Converter
{
    [ValueConversion(typeof(bool), typeof(bool))]
    class FlipBoolConverter : IValueConverter
    {
        public static FlipBoolConverter Instance = new FlipBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}
