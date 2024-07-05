using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FinancialTransactionTextInterpreter.Logic.Converters;
public class BoolToVisibilityConverter : IValueConverter
{
					private bool _invert = false;
					private Visibility _nonVisibleState = Visibility.Collapsed;

					public static BoolToVisibilityConverter FalseToCollapsed { get; private set; }
					public static BoolToVisibilityConverter FalseToHidden { get; private set; }
					public static BoolToVisibilityConverter TrueToCollapsed { get; private set; }
					public static BoolToVisibilityConverter TrueToHidden { get; private set; }

					static BoolToVisibilityConverter()
					{
										FalseToCollapsed = new BoolToVisibilityConverter();
										FalseToHidden = new BoolToVisibilityConverter
										{
															_nonVisibleState = Visibility.Hidden
										};
										TrueToCollapsed = new BoolToVisibilityConverter
										{
															_invert = true
										};
										TrueToHidden = new BoolToVisibilityConverter
										{
															_nonVisibleState = Visibility.Hidden,
															_invert = true
										};
					}
					private BoolToVisibilityConverter() { }

					public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
					{
										return _InvertIfNeeded((bool)value) ? Visibility.Visible : _nonVisibleState;
					}

					public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
					{
										return _InvertIfNeeded(!((Visibility)value == _nonVisibleState));
					}

					private bool _InvertIfNeeded(bool value)
					{
										return _invert ? !value : value;
					}
}