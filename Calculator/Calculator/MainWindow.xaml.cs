using System;
using System.Windows;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string inputExpression = ExpressionInput.Text;

            try
            {

                //ResultOutput.Text = Solving(inputExpression).ToString();
            }
            catch (Exception ex)
            {
                ResultOutput.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ExpressionInput.Text = string.Empty;
            ResultOutput.Text = string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
