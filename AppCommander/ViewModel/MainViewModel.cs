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
        private readonly ObservableCollection<Position> _currentPositionList = new ObservableCollection<Position>(); 
        public ObservableCollection<Position> CurrentPositionList
        {
            get { return _currentPositionList; } 
        }
#endregion
        
        #region Properties

		private Customer _currentCustomer;

        public Customer CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }
            set
            {
                _currentCustomer = value;
                OnPropertyChanged();
            }
        }

        private Order _currentOrder;

        public Order CurrentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDetailViewActive;
        public bool IsDetailViewActive
        {
            get { return _isDetailViewActive; }
            set
            {
                if (_isDetailViewActive != value)
                {
                    _isDetailViewActive = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isOrderCommitted;
        public bool IsOrderCommitted
        {
            get { return _isOrderCommitted; }
            set
            {
                if (_isOrderCommitted != value)
                {
                    _isOrderCommitted = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isProductListNotEmpty;
        public bool IsProductListNotEmpty
        {
            get { return _isProductListNotEmpty; }
            set
            {
                if (_isProductListNotEmpty != value)
                {
                    _isProductListNotEmpty = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCustomerListNotEmpty;
        public bool IsCustomerListNotEmpty
        {
            get { return _isCustomerListNotEmpty; }
            set
            {
                if (_isCustomerListNotEmpty != value)
                {
                    _isCustomerListNotEmpty = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _foundCustomers;
        public bool FoundCustomers
        {
            get { return _foundCustomers; }
            set 
            {
                if (_foundCustomers != value)
                {
                    _foundCustomers = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _foundExactCustomer;
        public bool FoundExactCustomer
        {
            get { return _foundExactCustomer; }
            set 
            {
                if (_foundExactCustomer != value)
                {
                    _foundExactCustomer = value;
                    OnPropertyChanged();
                }
            }
        }
	    #endregion 

        #region Commands
        public RelayCommand<string> SearchCustomerCommand
        {
            get { return new RelayCommand<string>(searchCustomer); }
        }

        public RelayCommand<string> SearchProductItemCommand
        {
            get { return new RelayCommand<string>(searchProductItem); }
        }

        public RelayCommand<string> SearchProductCommand
        {
            get { return new RelayCommand<string>(searchProduct); }
        }

        public RelayCommand<string> SearchGroupCommand
        {
            get { return new RelayCommand<string>(searchGroup); }
        }

        public RelayCommand<int> SelectProductCommand
        {
            get { return new RelayCommand<int>(selectProduct); }
        }

        public RelayCommand<int> SelectCustomerCommand
        {
            get { return new RelayCommand<int>(selectCustomer); }
        }

        public RelayCommand<Customer> SaveCustomerCommand
        {
            get { return new RelayCommand<Customer>(saveCustomer); }
        }

        public RelayCommand<bool> ShowDetailsCommand
        {
            get { return new RelayCommand<bool>(showDetails); }
        }

        public RelayCommand<bool> CommitOrderCommand
        {
            get { return new RelayCommand<bool>(commitOrder); }
        }

        public RelayCommand<bool> CancelOrderCommand
        {
            get { return new RelayCommand<bool>(cancelOrder); }
        }

        public RelayCommand<Position> DeletePositionCommand
        {
            get { return new RelayCommand<Position>(deletePosition); }
        }

        public RelayCommand<Position> SetPositionCommand
        {
            get { return new RelayCommand<Position>(setPosition); }
        }

        public RelayCommand<int> IncrementCommand
        {
            get { return new RelayCommand<int>(incrementPosition); }
        }
        public RelayCommand<int> DecrementCommand
        {
            get { return new RelayCommand<int>(decrementPosition); }
        }


        #endregion

        #region Command Helpers
        /// <summary>
        /// Überschreibt den CurrentUser mit dem in der View aktuellen Customer
        /// </summary>
        /// <param name="customerID">zu suchender Customer durch Customer.Id</param>
        private void selectCustomer(int customerId)
        {
            // TODO: check if customerId is castable to int?!
            //int cid = int.Parse(customerId);
            var customers = CustomerSearchList.Where(c => c.Id == customerId);
            CurrentCustomer = customers.Count() == 1 ? customers.FirstOrDefault() : new Customer();

            //TODO: naming!!!
            FoundCustomers = false;
            FoundExactCustomer = true;
        }

        /// <summary>
        /// Definiert anhand des App.config Files, ab wievielen Zeichen die Kundensuche beginnt 
        /// und übergibt den searchString dem Model, wo alle zutreffenden Resultate zurückgeliefert werden.
        /// </summary>
        /// <param name="searchString">zu suchender String</param>
        private void searchCustomer(string searchString)
        {
            if (searchString.Length < ConfigWrapper.StartSearchAfterNumKeys_Customer) { IsCustomerListNotEmpty = false; return; }

            CustomerSearchList.Clear();
            List<Customer> customers = Data.SearchCustomer(searchString);
            if (customers.Count == 0)
            {
                IsCustomerListNotEmpty = false;
                return;
            }
            customers.ForEach(customer => CustomerSearchList.Add(customer));
            if (customers.Count > 1)
                FoundCustomers = true;
                IsCustomerListNotEmpty = true;
            if (customers.Count == 1)
                FoundExactCustomer = true;
                IsCustomerListNotEmpty = true;
        }

        /// <summary>
        /// Sucht anhand vom <paramref name="searchString" /> alle Produkte und Gruppen
        /// Füllt die Listen Products und Groups mit den gefundenen Resultaten
        /// </summary>
        /// <param name="searchString">zu suchender Suchtext</param>
        private void searchProductItem(string searchString)
        {
            // Wenn der String leer ist, gebe Standardresultat zurück
            if (searchString.Length == 0)
            {
                searchGroup("");
            }
            // Wenn zuwenige Zeichen eingegeben wurden, mach nichts
            if (searchString.Length < ConfigWrapper.StartSearchAfterNumKeys_Product) { IsProductListNotEmpty = false; return; }
            
            bwProduct.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                tmpProductList = Data.SearchProduct(searchString);

                // Wenn kein Resultat zurück kommt, mach nichts
                if (tmpProductList.Count == 0)
                    IsProductListNotEmpty = false;
                    return;

            });

            bwGroup.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                tmpProductGroupList = Data.SearchProductGroups(searchString);
                
                // Wenn kein Resultat zurück kommt, mach nichts
                if (tmpProductGroupList.Count == 0)
                    IsProductListNotEmpty = false;
                    return;
            });

            // what to do when worker completes its task (notify the user)
            bwProduct.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                ProductSearchList.Clear();
                tmpProductList.ForEach(product => ProductSearchList.Add(product));
                IsProductListNotEmpty = true;
            });

            bwGroup.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                GroupSearchList.Clear();
                tmpProductGroupList.ForEach(group => GroupSearchList.Add(group));
                IsProductListNotEmpty = true;
            });

            // Führe den Backgroundworker nur aus, wenn er nicht
            // bereits beschäftigt ist.
            if(!bwProduct.IsBusy) bwProduct.RunWorkerAsync();
            if(!bwGroup.IsBusy) bwGroup.RunWorkerAsync();
        }

        private void selectProduct(int productId)
        {
            refreshContent = false;

            if (bwProduct.IsBusy) { return; }

            bwProduct.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {

                tmpProductList = Data.SearchProduct(productId);
                if (tmpProductList.Count == 0)
                    return;

            });

            // what to do when worker completes its task (notify the user)
            bwProduct.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                // ProductSearchList.Clear();
                tmpProductList.ForEach(product => CurrentPositionList.Add(
                    new Position
                    {
                        Amount = 1,
                        Name = product.Name,
                        Product = product.Id,
                        Price = product.Price
                    }
                ));

                refreshContent = true;

                if (CurrentPositionList.Count < 1)
                    return;            
            });

            bwProduct.RunWorkerAsync();            

        }

        /// <summary>
        /// Übergibt den searchString dem Model, wo alle zutreffenden Resultate zurückgeliefert werden.
        /// </summary>
        /// <param name="searchString">zu suchender Suchtext</param>
        private void searchProduct(string searchString)
        {
            //if (searchString.Length < ConfigWrapper.StartSearchAfterNumKeys_Product) { return; }
            if (bwProduct.IsBusy) { return; }

            bwProduct.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
           {

                tmpProductList = Data.SearchProduct(searchString);
                if (tmpProductList.Count == 0)
                    return;
            });

            // what to do when worker completes its task (notify the user)
            bwProduct.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                if(refreshContent)
                ProductSearchList.Clear();
                
                tmpProductList.ForEach(product => ProductSearchList.Add(product));
            });

            bwProduct.RunWorkerAsync();


        }

        /// <summary>
        /// Definiert anhand des App.config Files, ab wievielen Zeichen die Produktesuche beginnt 
        /// und übergibt den searchString dem Model, wo alle zutreffenden Resultate zurückgeliefert werden.
        /// </summary>
        /// <param name="searchString">zu suchender Suchtext</param>
        public void searchGroup(string searchString)
        {
            // searchProduct(searchString);

            // if (searchString.Length < ConfigWrapper.StartSearchAfterNumKeys_Product) { return; }
            if (bwGroup.IsBusy) { return; }

            bwGroup.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {

                tmpProductGroupList = Data.SearchProductGroups(searchString);
                if (tmpProductGroupList.Count == 0)
                    return;

            });

            // what to do when worker completes its task (notify the user)
            bwGroup.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                GroupSearchList.Clear();
                tmpProductGroupList.ForEach(group => GroupSearchList.Add(group));
            });

            bwGroup.RunWorkerAsync();
        }

        /// <summary>
        /// Überprüft ob bereits eine Id im Order Object besteht, falls id = 0 ist dies ein neuer Order
        /// und wird in der Datenbank erstellt, als Rückgabewert wird die ID zurückgeliefert. 
        /// Ist die Id != 0 wird der Order gespeichert.
        /// </summary>
        private void showDetails(bool isTrue)
        {
            IsMainViewActive = false;
            IsDetailViewActive = true;

            SetCurrentPositionsToOrder();

            if (CurrentOrder.Id == 0)
            {
                CurrentOrder.Id = Data.CreateOrder();
            }

            Data.SetPositions(CurrentOrder);
        }

        /// <summary>
        /// Speichert Customer, Order und Positionen in der Datenbank
        /// </summary>
        private void commitOrder(bool isTrue)
        {
            SetCurrentPositionsToOrder();

            Data.SaveCustomer(CurrentCustomer);
            // TODO: Plenum CommitOrder & SetPositions Rückgabewert oder nicht?
            bool isSaved = Data.CommitOrder(CurrentOrder);
            Data.SetPositions(CurrentOrder);

            // return isSaved;
        }

        /// <summary>
        /// Löscht eine Position aus der aktuellen Bestellung
        /// </summary>
        /// <param name="pos">die zu löschende Position</param>
        private void deletePosition(Position pos)
        {
            CurrentPositionList.Remove(pos);
        }

        /// <summary>
        /// Ersetzt eine Position mit einer aktualisierten Version
        /// </summary>
        /// <param name="pos">die zu aktualisierende Version</param>
        private void setPosition(Position pos)
        {
            CurrentPositionList.Remove(pos);
            CurrentPositionList.Add(pos); 
        }

        private void incrementPosition(int id)
        {
            CurrentPositionList.Where(w => w.Id == id).ToList().ForEach(s => s.Amount++); 
        }

        private void decrementPosition(int id)
        {
            CurrentPositionList.Where(w => w.Id == id).ToList().ForEach(s => s.Amount--);
        }

        /// <summary>
        /// Maps the current selected position into the actual order
        /// </summary>
        private void SetCurrentPositionsToOrder()
        {

            CurrentOrder.PositionList.Clear();
            
            foreach (var item in CurrentPositionList)
                CurrentOrder.PositionList.Add(item);
        }

        /// <summary>
        /// Überprüft ob bereits eine Id im Customer Object besteht, 
        /// falls die id = 0 ist, wird ein neuer Customer in der Datenbank erstellt. 
        /// Ist die Id != 0 wird der Customer in der Datenbank gespeichert.
        /// </summary>
        private void saveCustomer()
        {
            if (CurrentCustomer.Id == 0)
            {
                CurrentCustomer.Id = Data.CreateCustomer(CurrentCustomer);
            }
            else
            {
                Data.SaveCustomer(CurrentCustomer);
            }
        }

        /// <summary>
        ///  Überprüft ob der aktuelle Order bereits in der Datenbank existiert, 
        ///  falls ja, wird der Order in der Datenbank gelöscht. 
        ///  Auf jedenfall wird der aktuelle Customer und der aktuelle Order zurückgesetzt.
        /// </summary>
        private void cancelOrder(bool isTrue)
        {
            if (CurrentOrder.Id != 0)
            {
                Data.CancleOrder(CurrentOrder.Id);
            }
            CurrentOrder = new Order();
            CurrentCustomer = new Customer();
        } 
        #endregion

        /// <summary>
        /// Konstruktor Klasse der MainViewModel
        /// </summary>
        public MainViewModel()
        {
            IsDetailViewActive = false;
            IsOrderCommitted = false;
            IsMainViewActive = true;

            FoundExactCustomer = true;

            CurrentCustomer = new Customer { FirstName = "Heiri", LastName = "Mega", EMail = "heiri.mega@yahoo.com", Mobile = "+41768888888" };
            
            CurrentOrder = new Order();
            CurrentOrder.PositionList = new List<Position>();
            Data = new Model.Model();

            // Backgroundworker für Multithreading bei Produktsuche
            bwProduct = new BackgroundWorker();
            bwGroup = new BackgroundWorker();

            tmpProductList = new List<Product>(); // Wird für schnelle Suche benötigt
            tmpProductGroupList = new List<ProductGroup>();

            // damit Gruppe vor ausgefüllt ist...
            // TODO: wie initial suchen?
            searchGroup("");
        }
    }
}
