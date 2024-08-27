using Avalonia.Controls;
using Avalonia.Interactivity;
using System;  // Für DivideByZeroException
using MyCrossPlatformApp.Calculator;  // Namespace der CalculatorLogic-Klasse

namespace MyCrossPlatformApp.Calculator
{
    public partial class CalculatorWindow : Window
    {
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

    private void OnAddClick(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(InputA.Text);
            double b = double.Parse(InputB.Text);
            double result = _calculator.Add(a, b);
            ResultTextBlock.Text = "Result: " + result;
        }


        private void OnSubtractClick(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(InputA.Text);
            double b = double.Parse(InputB.Text);
            double result = _calculator.Subtract(a, b);
            ResultTextBlock.Text = "Result: " + result;
        }

        private void OnMultiplyClick(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(InputA.Text);
            double b = double.Parse(InputB.Text);
            double result = _calculator.Multiply(a, b);
            ResultTextBlock.Text = "Result: " + result;
        }

        private void OnDivideClick(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(InputA.Text);
            double b = double.Parse(InputB.Text);
            try
            {
                double result = _calculator.Divide(a, b);
                ResultTextBlock.Text = "Result: " + result;
            }
            catch (DivideByZeroException ex)
            {
                ResultTextBlock.Text = "Error: " + ex.Message;
            }
        }
    }
}