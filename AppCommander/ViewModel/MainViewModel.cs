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

        private Appl _selectedApp;
        public Appl SelectedApp
        {
            get { return _selectedApp; }
            set
            {
                if (_selectedApp != value)
                {
                    _selectedApp = value;
                    OnPropertyChanged(); 
                }
            }
        }

        private bool _isMainViewActive;
        public bool IsMainViewActive
        {
            get { return _isMainViewActive; }
            set
            {
                if (_isMainViewActive != value)
                {
                    _isMainViewActive = value;
                    
                    // Sort this guy everytime the Mainview get's displayed
                    // TODO: What happens when we have a lot of Apps?
                    List<Appl> tmp = AppList.ToList<Appl>();
                    tmp.Sort();
                    
                    AppList.Clear();
                    tmp.ForEach(a => AppList.Add(a));

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

        private SimpleCommand _addApp;
        public SimpleCommand CmdAddApp
        {
            get
            {
                if (_addApp == null) _addApp = new SimpleCommand(AddApp);
                return _addApp; 
            }
        }

        private RelayCommand<string> _setApp;
        public RelayCommand<string> CmdSetApp
        {
            get
            {
                if (_setApp == null) _setApp = new RelayCommand<string>(SetApp);
                return _setApp; 
            }
        }

        private RelayCommand<Appl> _removeApp;
        public RelayCommand<Appl> CmdRemoveApp
        {
            get
            {
                if (_removeApp == null) _removeApp = new RelayCommand<Appl>(RemoveApp);  
                return _removeApp;
            }
        }

        private RelayCommand<Appl> _saveApp;
        public RelayCommand<Appl> CmdSaveApp
        {
            get
            {
                if (_saveApp == null) _saveApp = new RelayCommand<Appl>(SaveApp);
                return _saveApp;
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

        private RelayCommand<string> _rate;
        public RelayCommand<string> CmdRate
        {
            get
            {
                if (_rate == null) _rate = new RelayCommand<string>(Rate); 
                return _rate;
            }
        }


        #endregion

        #region Command Helpers

        private void AddApp()
        {
            SelectedApp = new Appl() { GUID = Guid.NewGuid().ToString() }; 
            IsMainViewActive = false;
            IsEditViewActive = true; 
        }

        private void SaveApp(Appl appl)
        {
            AppList.Remove(SelectedApp);
            AppList.Add(SelectedApp);
            SelectedApp = null; 

            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);

            IsEditViewActive = false;
            IsMainViewActive = true; 
        }

        private void SetApp(string GUID) {

            SelectedApp = Serializer.DeSerializeByGUID(GUID, ConfigWrapper.XMLPath);
            IsEditViewActive = true;
            IsMainViewActive = false;

        }

        private void RemoveApp(Appl appl)
        {
            AppList.Remove(appl);
            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);

            IsEditViewActive = false;
            IsMainViewActive = true; 
        }

        private void Save(bool placeholder)
        {
            throw new NotImplementedException(); 
        }

        private void Load(bool placeholder)
        {
            throw new NotImplementedException(); 
        }

        private void Rate(string ranking)
        {
            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);
        }

        #endregion


        /// <summary>
        /// Konstruktor Klasse der MainViewModel
        /// </summary>
        public MainViewModel()
        {
            Data = new ModelHelper();

            // Füge alle Einträge aus dem XML in die AppListe ein
            Serializer.DeSerializeFromXML<List<Appl>>(ConfigWrapper.XMLPath).ForEach(a => AppList.Add(a));

            IsEditViewActive = false;
            IsMainViewActive = true;


        }
    }
}
