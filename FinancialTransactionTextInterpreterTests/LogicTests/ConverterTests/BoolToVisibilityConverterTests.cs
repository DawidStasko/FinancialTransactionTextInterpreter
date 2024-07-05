using FinancialTransactionTextInterpreter.Logic.Converters;
using System.Windows;
namespace FinancialTransactionTextInterpreterTests.LogicTests.ConverterTests;
public class BoolToVisibilityConverterTests
{
					[Theory]
					[MemberData(nameof(ConvertData))]
					public void Convert_WhenCalledBoolean_ReturnsExpectedVisibility(BoolToVisibilityConverter converter, bool value, Visibility expected)
					{

										Visibility result = (Visibility)converter.Convert(value, typeof(Visibility), null, null);

										Assert.Equal(expected, result);
					}

					public static IEnumerable<object[]> ConvertData =>
									new List<object[]>
									{
													new object[] { BoolToVisibilityConverter.FalseToCollapsed, true, Visibility.Visible },
													new object[] { BoolToVisibilityConverter.FalseToCollapsed, false, Visibility.Collapsed },
													new object[] { BoolToVisibilityConverter.FalseToHidden, true, Visibility.Visible },
													new object[] { BoolToVisibilityConverter.FalseToHidden, false, Visibility.Hidden },
													new object[] {BoolToVisibilityConverter.TrueToCollapsed, true, Visibility.Collapsed },
													new object[] {BoolToVisibilityConverter.TrueToCollapsed, false, Visibility.Visible },
													new object[] {BoolToVisibilityConverter.TrueToHidden, true, Visibility.Hidden },
													new object[] {BoolToVisibilityConverter.TrueToHidden, false, Visibility.Visible },
									};
}
