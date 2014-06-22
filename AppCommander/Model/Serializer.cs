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
        public static void SerializeToXML<T>(T obj, string path)
        {

            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(path);
            ser.Serialize(textWriter, obj);
            textWriter.Close(); 
        }

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
                throw new ArgumentException("Invalid XML Format!");
            }
        }

        public static Appl DeSerializeByGUID(string guid, string path)
        {
            //TODO: Generic Version ? 
            XmlSerializer ser = new XmlSerializer(typeof(List<Appl>));
            TextReader textReader = new StreamReader(path);
            List<Appl> ret = (List<Appl>)ser.Deserialize(textReader);
            textReader.Close(); 

            return ret.FirstOrDefault(a => a.GUID == guid);
        }
    }
}
