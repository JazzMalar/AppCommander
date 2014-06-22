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
using System.Windows;

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

        private string _titel;
        public string Titel
        {
            get { return _titel; }
            set
            {
                if (_titel != value)
                {
                    _titel = value;
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

        private SimpleCommand _exitApplication;
        public SimpleCommand CmdExitApplication
        {
            get
            {
                if (_exitApplication == null) _exitApplication = new SimpleCommand(ExitApplication);
                return _exitApplication; 
            }
        }

        private RelayCommand<string> _savePicture;
        public RelayCommand<string> CmdSavePicture
        {
            get
            {
                if (_savePicture == null) _savePicture = new RelayCommand<string>(SavePicture);
                return _savePicture; 
            }
        }

        private SimpleCommand _cancelEdit;
        public SimpleCommand CmdCancelEdit
        {
            get
            {
                if (_cancelEdit == null) _cancelEdit = new SimpleCommand(CancelEdit);
                return _cancelEdit;
            }
        }

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

        private SimpleCommand _save;
        public SimpleCommand CmdSave
        {
            get
            {
                if (_save == null) _save = new SimpleCommand(Save);
                return _save;
            }
        }

        private SimpleCommand _load;
        public SimpleCommand CmdLoad
        {
            get
            {
                if (_load == null) _load = new SimpleCommand(Load);
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

        private void ExitApplication()
        {
            Application.Current.MainWindow.Close(); 
        }

        private void SavePicture(string picture)
        {
            SelectedApp.Picture = picture;
            AppList.Remove(SelectedApp);
            AppList.Add(SelectedApp); 
        }

        private void CancelEdit()
        {
            ChangeView();
        }

        private void AddApp()
        {
            SelectedApp = new Appl() { GUID = Guid.NewGuid().ToString() };
            ChangeView();
        }

        private void SaveApp(Appl appl)
        {
            AppList.Remove(SelectedApp);
            AppList.Add(SelectedApp);
            SelectedApp = null; 

            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);

            ChangeView();
        }

        private void SetApp(string GUID) {

            SelectedApp = Serializer.DeSerializeByGUID(GUID, ConfigWrapper.XMLPath);
            ChangeView();

        }

        private void RemoveApp(Appl appl)
        {
            AppList.Remove(appl);
            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);

            ChangeView();
        }

        private void Save()
        {
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);
        }

        private void Load()
        {
            AppList.Clear();
            Serializer.DeSerializeFromXML<List<Appl>>(ConfigWrapper.XMLPath).ForEach(a => AppList.Add(a));
        }

        private void Rate(string ranking)
        {
            //TODO: Should we really save everytime an app gets changed? 
            Serializer.SerializeToXML<List<Appl>>(AppList.ToList<Appl>(), ConfigWrapper.XMLPath);
        }

        #endregion

        #region methods
        private void ChangeView()
        {
            IsEditViewActive = !IsEditViewActive;
            IsMainViewActive = !IsMainViewActive;

            if (IsMainViewActive) { Titel = "Eingetragene Apps"; }
            else if (IsEditViewActive) { Titel = "App anpassen"; }
        }
        #endregion

        /// <summary>
        /// Konstruktor Klasse der MainViewModel
        /// </summary>
        public MainViewModel()
        {
            Titel = "Eingetragene Apps";
            string loadFrom = ConfigWrapper.XMLPath; 

            if (loadFrom.Equals(String.Empty))
            {
                loadFrom = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                loadFrom += @"\\apps.xml";
                ConfigWrapper.XMLPath = loadFrom;

                this.Save(); 
            }

            // Füge alle Einträge aus dem XML in die AppListe ein
            Serializer.DeSerializeFromXML<List<Appl>>(loadFrom).ForEach(a => AppList.Add(a));

            IsEditViewActive = false;
            IsMainViewActive = true;


        }
    }
}
