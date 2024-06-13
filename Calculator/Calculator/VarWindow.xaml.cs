using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Models;
using WpfApp2.Validators;
using WpfApp2.ViewModels;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class VarWindow : Window
    {
        public VarWindow()
        {
            InitializeComponent();
            DataContext = new VarsViewModel();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            VarModel varModel = (this.DataContext as VarsViewModel).SelectedVar;
            if (ValidateVar(varModel.Name, varModel.Value) && !varModel.Saved)
            {
                VarModel varToRemove = (this.DataContext as VarsViewModel)?.Vars.FirstOrDefault(_var => _var.Name == varModel.Name && _var.Saved);
                if (varToRemove != null)
                {
                    this.ErrorText.Content = "Уже есть такая переменная";
                    this.ErrorText.Visibility = Visibility.Visible;
                    return;
                }
                var saveCommand = (this.DataContext as VarsViewModel)?.SaveCommand;
                if (saveCommand != null && saveCommand.CanExecute(varModel))
                {
                    saveCommand.Execute(varModel);
                }
            }
        }

        private bool ValidateVar(string name, string value)
        {
            if (!VarsValidator.ValidateName(name))
            {
                this.ErrorText.Content = "Некоректное имя";
                return false;
            }
            if (!VarsValidator.ValidateValue(value))
            {
                this.ErrorText.Content = "Некоректное значение";
                return false;
            }
            return true;
        }
    }
}
