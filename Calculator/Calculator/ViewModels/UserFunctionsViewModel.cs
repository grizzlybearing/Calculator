
using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Model;

namespace WpfApp2.ViewModel
{
    internal class UserFunctionsViewModel: BindableBase
    
        {
            public ObservableCollection<UserFunction> Functions { get; set; }

            private string _newFunction;
            public string NewFunction
            {
                get =>_newFunction;
                set => SetProperty(ref _newFunction, value);
            }

            public ICommand AddFunctionCommand { get; set; }
            public ICommand DeleteFunctionCommand { get; set; }

            public UserFunctionsViewModel()
            {
                Functions = new ObservableCollection<UserFunction>();

                AddFunctionCommand = new DelegateCommand(AddFunction);
                DeleteFunctionCommand = new DelegateCommand<UserFunction>(DeleteFunction);
            }

        private void AddFunction()
        {
            if (!string.IsNullOrWhiteSpace(NewFunction) && FunctionValidator.ValidateFunction(NewFunction))
            {
                Functions.Add(new UserFunction { Function = NewFunction});
                NewFunction = string.Empty;
            }
        }

        private void DeleteFunction(UserFunction function)
        {
            if (function != null)
            {
                Functions.Remove(function);
            }
        }
    }
    
    
 }

