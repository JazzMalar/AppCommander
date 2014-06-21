using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCommander.Common.Commands;
using AppCommander.Model;
using AppCommander.Common.Config;
using System.Threading;
using System.ComponentModel;

namespace AppCommander.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
 
        public ModelHelper Data;

        #region ObservableCollections

        private readonly ObservableCollection<Appl> _appList = new ObservableCollection<Appl>();
        public ObservableCollection<Appl> AppList
        {
            get { return _appList; }
        }

        #endregion
        
        #region Properties

        private bool _isMainViewActive;
        public bool IsMainViewActive
        {
            get { return _isMainViewActive; }
            set
            {
                if (_isMainViewActive != value)
                {
                    _isMainViewActive = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEditViewActive;
        public bool IsEditViewActive
        {
            get { return _isEditViewActive; }
            set
            {
                if (_isEditViewActive != value)
                {
                    _isEditViewActive = value;
                    OnPropertyChanged();
                }
            }
        }

	    #endregion 

        #region Commands

        private RelayCommand<int> _setApp;
        public RelayCommand<int> CmdSetApp
        {
            get
            {
                if (_setApp == null) _setApp = new RelayCommand<int>(SetApp);
                return _setApp; 
            }
        }

        private RelayCommand<int> _removeApp;
        public RelayCommand<int> CmdRemoveApp
        {
            get
            {
                if (_removeApp == null) _removeApp = new RelayCommand<int>(RemoveApp);  
                return _removeApp;
            }
        }

        private RelayCommand<bool> _save;
        public RelayCommand<bool> CmdSave
        {
            get
            {
                if (_save == null) _save = new RelayCommand<bool>(Save);
                return _save;
            }
        }

        private RelayCommand<bool> _load;
        public RelayCommand<bool> CmdLoad
        {
            get
            {
                if (_load == null) _load = new RelayCommand<bool>(Load);
                return _load;    
            }
        }

        private RelayCommand<int> _rate;
        public RelayCommand<int> CmdRate
        {
            get
            {
                if (_rate == null) _rate = new RelayCommand<int>(Rate); 
                return _rate;
            }
        }


        #endregion

        #region Command Helpers
        
        private void SetApp(int ID) {
            throw new NotImplementedException(); 
        }

        private void RemoveApp(int ID)
        {
            throw new NotImplementedException(); 
        }

        private void Save(bool placeholder)
        {
            throw new NotImplementedException(); 
        }

        private void Load(bool placeholder)
        {
            throw new NotImplementedException(); 
        }

        private void Rate(int ID)
        {
            throw new NotImplementedException(); 
        }

        #endregion


        /// <summary>
        /// Konstruktor Klasse der MainViewModel
        /// </summary>
        public MainViewModel()
        {
            IsEditViewActive = false;
            IsMainViewActive = true;

            Data = new ModelHelper();

            AppList.Add(new Appl() { Name = "Test App", Ranking = 4 } );

        }
    }
}
