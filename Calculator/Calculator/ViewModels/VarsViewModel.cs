using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfApp2.Commands;
using WpfApp2.Models;

namespace WpfApp2.ViewModels
{
    public class VarsViewModel : INotifyPropertyChanged
    {
        private VarModel selectedVar;
        public VarModel SelectedVar
        {
            get { return selectedVar; }
            set
            {
                selectedVar = value;
                OnPropertyChanged("SelectedVar");
            }
        }

        public ObservableCollection<VarModel> Vars
        {
            get;
            set;
        }

        public List<VarModel> SavedVars
        {
            get { return Vars.Where(v => v.Saved).ToList(); }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      VarModel _var = new VarModel();
                      Vars.Insert(0, _var);
                      selectedVar = _var;
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      VarModel _var = obj as VarModel;
                      _var.Saved = true;
                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      VarModel _var = obj as VarModel;
                      if (_var != null)
                      {
                          Vars.Remove(_var);
                      }
                  }));
            }
        }

        public VarsViewModel()
        {
            this.Vars = new ObservableCollection<VarModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
