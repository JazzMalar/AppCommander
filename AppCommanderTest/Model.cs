using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppCommander.ViewModel;
using AppCommander.Model;
using AppCommander.Common.Config;
using System.Collections.Generic;

namespace AppCommanderTest
{
    [TestClass]
    public class Model
    {

        private static MainViewModel _data;
        private static string _guid;
        private static Appl _appl;

        private static string _savedAppGuid;
        private static Appl _savedAppAppl;

        [ClassInitialize()]
        public static void InitializeModelTests(TestContext testContext)
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

        [TestMethod]
        public void ChangeXMLPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\\test.xml";
            ConfigWrapper.XMLPath = path;

            Assert.AreEqual(ConfigWrapper.XMLPath, path); 

        }

        [TestMethod]
        public void SerializeAppList()
        {
            List<Appl> appList = new List<Appl>(); 
            appList.Add(_savedAppAppl);

            Serializer.SerializeToXML<List<Appl>>(appList, ConfigWrapper.XMLPath); 

        }

        [TestMethod]
        public void DeserializeAppList()
        {
            List<Appl> appList = Serializer.DeSerializeFromXML<List<Appl>>(ConfigWrapper.XMLPath); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException)]
        public void DeserializeCorruptAppList()
        {
            Serializer.SerializeToXML<Appl>(_appl, ConfigWrapper.XMLPath);
            List<Appl> appList = Serializer.DeSerializeFromXML<List<Appl>>(ConfigWrapper.XMLPath); 
        }
    }
}
