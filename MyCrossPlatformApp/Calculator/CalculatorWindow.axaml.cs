using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace MyCrossPlatformApp.Calculator
{
    public partial class CalculatorWindow : Window
    {
        private double _currentValue = 0;
        private string _currentOperator = "";
        private bool _isNewEntry = true;
        private CalculatorLogic _calculator;

        public CalculatorWindow()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            _calculator = new CalculatorLogic();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string buttonContent = (sender as Button).Content.ToString();
            Console.WriteLine($"Button clicked: {buttonContent}");

            if (_isNewEntry)
            {
                ResultTextBox.Text = buttonContent;
                _isNewEntry = false;
            }
            else
            {
                ResultTextBox.Text += buttonContent;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clear button clicked");
            ResultTextBox.Text = "0";
            _currentValue = 0;
            _currentOperator = "";
            _isNewEntry = true;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"Equal button clicked. Current Value: {_currentValue}, Current Operator: {_currentOperator}");

            if (double.TryParse(ResultTextBox.Text, out double newValue))
            {
                Console.WriteLine($"Parsed newValue: {newValue}");

                switch (_currentOperator)
                {
                    case "+":
                        _currentValue += newValue;
                        break;
                    case "-":
                        _currentValue -= newValue;
                        break;
                    case "*":
                        _currentValue *= newValue;
                        break;
                    case "/":
                        if (newValue == 0)
                        {
                            Console.WriteLine("Error: Division by zero");
                            ResultTextBox.Text = "Error";
                            _currentValue = 0;
                            _currentOperator = "";
                            _isNewEntry = true;
                            return;
                        }
                        _currentValue /= newValue;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid operator");
                        ResultTextBox.Text = "Error";
                        _currentValue = 0;
                        _currentOperator = "";
                        _isNewEntry = true;
                        return;
                }

                Console.WriteLine($"Calculation result: {_currentValue}");
                ResultTextBox.Text = _currentValue.ToString();
                _currentOperator = "";
                _isNewEntry = true;
            }
            else
            {
                Console.WriteLine("Error: Invalid input");
                ResultTextBox.Text = "Error";
                _currentValue = 0;
                _currentOperator = "";
                _isNewEntry = true;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string operatorContent = (sender as Button).Content.ToString();
            Console.WriteLine($"Operator clicked: {operatorContent}");

            if (double.TryParse(ResultTextBox.Text, out double newValue))
            {
                // Wenn bereits ein Operator vorhanden ist, wird erst die vorherige Berechnung durchgeführt
                if (_currentOperator != "")
                {
                    Console.WriteLine("Processing previous operation before setting new operator");
                    EqualButton_Click(sender, e);
                }
                else
                {
                    _currentValue = newValue;
                }

                _currentOperator = operatorContent;
                _isNewEntry = true;

                Console.WriteLine($"New operator set: {_currentOperator}, Current value: {_currentValue}");
            }
            else
            {
                Console.WriteLine("Error: Invalid input when setting operator");
                ResultTextBox.Text = "Error";
                _currentValue = 0;
                _currentOperator = "";
                _isNewEntry = true;
            }
        }

    }
}
