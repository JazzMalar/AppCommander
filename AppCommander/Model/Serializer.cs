using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO; 

namespace AppCommander.Model
{
    class Serializer
    {
        public static void SerializeToXML<T>(T obj, string path)
        {

            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(@"D:\TMP\blah.xml");
            ser.Serialize(textWriter, obj);
            textWriter.Close(); 
        }

        public static T DeSerializeFromXML<T>(T obj, string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(@"D:\TMP\blah.xml");
            T ret = (T)ser.Deserialize(textReader);
            textReader.Close();
            return ret;
        }
    }
}
