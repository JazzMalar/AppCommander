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
using AppCommander.ViewModel;
using AppCommander.Common.Converters;
using AppCommander.Common.Config;

namespace AppCommander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel = new MainViewModel();
        public MainViewModel ViewModel
        {
            get { return _viewModel; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// I wasn't able to figure out a way to do this
        /// "MVVM" conformly. So I open the File Dialog
        /// using an event and call the ICommand to properly
        /// Update the ViewModel / Model
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                (this.DataContext as MainViewModel).CmdSavePicture.Execute(filename);

                NameToImagePathConverter con = new NameToImagePathConverter();

                // Needed so the user can see the new chosen Image
                ApplPic.Source = (ImageSource)con.Convert(filename, null, "", System.Globalization.CultureInfo.CurrentCulture);

            }
        }

        /// <summary>
        /// I wasn't able to figure out a way to do this
        /// "MVVM" conformly. So I open the File Dialog
        /// using an event and call the ICommand to properly
        /// Update the ViewModel / Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSaveDialog(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                ConfigWrapper.XMLPath = dlg.FileName;

                (this.DataContext as MainViewModel).CmdSave.Execute();

            }
        }

        /// <summary>
        /// I wasn't able to figure out a way to do this
        /// "MVVM" conformly. So I open the File Dialog
        /// using an event and call the ICommand to properly
        /// Update the ViewModel / Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLoadDialog(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                ConfigWrapper.XMLPath = dlg.FileName;

                (this.DataContext as MainViewModel).CmdLoad.Execute();

            }
        }
    }
}
