using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using AppCommander.Common.Log;
using AppCommander.Common.Config; 

namespace AppCommander.Model
{
    public class Serializer
    {
        /// <summary>
        /// Serializes an Object to the specified Path. 
        /// If the path cannot be written to, an exception
        /// is thrown and the error is written to the Log File
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">the object to serialize</param>
        /// <param name="path">the path to the XML File</param>
        public static void SerializeToXML<T>(T obj, string path)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                TextWriter textWriter = new StreamWriter(path);
                ser.Serialize(textWriter, obj);
                textWriter.Close();
            }
            catch (Exception e)
            {
                Logger.append(e.ToString(), Logger.ERROR);
                throw new ArgumentException("Could not Save, please consult Log at"+ConfigWrapper.LogDirectory);
            }
        }

        /// <summary>
        /// Deserializes an object from XML. 
        /// If the XML cannot be read, an exception is thrown
        /// and the error goes straight to the log file.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="path">path to the XML File</param>
        /// <returns></returns>
        public static T DeSerializeFromXML<T>(string path)
        {
            try { 
                XmlSerializer ser = new XmlSerializer(typeof(T));
                TextReader textReader = new StreamReader(path);
                T ret = (T)ser.Deserialize(textReader);
                textReader.Close();
                return ret;
            }
            catch (Exception e)
            {
                Logger.append(e.ToString(), Logger.ERROR );
                throw new ArgumentException("Invalid XML Format, please consult Log at"+ConfigWrapper.LogDirectory);
            }
        }

        /// <summary>
        /// Solution specific! 
        /// Deserializes an Appl by GUID from the specified XML
        /// If the XML cannot be read, throws an error and writes
        /// the error message to the log file.
        /// </summary>
        /// <param name="guid">GUID of the APPL</param>
        /// <param name="path">Path of the XML File</param>
        /// <returns>Appl or Null</returns>
        public static Appl DeSerializeByGUID(string guid, string path)
        {
            //TODO: Generic Version ? 
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<Appl>));
                TextReader textReader = new StreamReader(path);
                List<Appl> ret = (List<Appl>)ser.Deserialize(textReader);
                textReader.Close();
                return ret.FirstOrDefault(a => a.GUID == guid);
            }
            catch (Exception e)
            {
                Logger.append(e.ToString(), Logger.ERROR);
                throw new ArgumentException("Invalid XML Format, please consult Log at" + ConfigWrapper.LogDirectory);

            }
        }
    }
}
