using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppCommander.Common.Config;
using AppCommander.ViewModel;
using AppCommander.Model;

namespace AppCommanderTest
{
    [TestClass]
    public class View
    {
        private static MainViewModel _data;
        private static string _guid;
        private static Appl _appl;

        private static string _savedAppGuid;
        private static Appl _savedAppAppl;

        [ClassInitialize()]
        public static void InitializeViewTests(TestContext testContext)
        {
            ConfigWrapper.XMLPath = String.Empty;
            _data = new MainViewModel();
            _guid = Guid.NewGuid().ToString();
            _appl = new Appl() { GUID = _guid, Name = "Test App", Price = 20, IsCompAndroid = true, AndroidVersion = new XMLVersion("1.0.0") };

            _savedAppGuid = Guid.NewGuid().ToString();
            _savedAppAppl = new Appl() { GUID = _savedAppGuid, Name = "Test App 2", Price = 20, IsCompIphone = true, IphoneVersion = new XMLVersion("1.1.1") };
            _data.CmdSaveApp.Execute(_savedAppAppl);
            _data.SelectedApp = _savedAppAppl; 
        }

        /// <summary>
        /// Tests if an App can be saved
        /// </summary>
        [TestMethod]
        public void SaveApp()
        {
            _data.CmdSaveApp.Execute(_appl);
            Appl compare = Serializer.DeSerializeByGUID(_guid, ConfigWrapper.XMLPath);
            Assert.IsTrue(_appl.Equals(compare));
        }

        /// <summary>
        /// Tests the ability to add new Apps
        /// </summary>
        [TestMethod]
        public void AddApp()
        {
            _data.CmdAddApp.Execute();
            Assert.IsTrue(
                (_data.SelectedApp.Name == null) &&
                (_data.SelectedApp.GUID != _savedAppGuid)
            ); 
        }
        
        /// <summary>
        /// Tests if the focus of an app can be changed
        /// (happens when you click on one in the catalog)
        /// </summary>
        [TestMethod]
        public void SetApp()
        {
            _data.SelectedApp = null;
            _data.CmdSetApp.Execute(_savedAppGuid);

            Assert.IsTrue(_data.SelectedApp.Equals(_savedAppAppl)); 

        }

        /// <summary>
        /// Tests the ability to remove Apps
        /// </summary>
        [TestMethod]
        public void RemoveApp()
        {
            Appl local = _appl;
            local.Name = "Test 3";
            local.GUID = Guid.NewGuid().ToString(); 
            _data.CmdSaveApp.Execute(local);
            _data.CmdRemoveApp.Execute(local);
            _data.CmdSetApp.Execute(local.GUID);

            Assert.IsNull(_data.SelectedApp); 

        }


    }
}
