using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommander.Model
{
    public class Appl : IEquatable<Appl>, IComparable<Appl>
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public byte Ranking { get; set; }
        public decimal Price { get; set; }
        public XMLVersion AppVersion { get; set; }
        public List<ValidPlattform> VPlattform { get; set; }        
        public Appl() { }

        public int CompareTo(object obj)
        {
            if (!(obj is Appl))
            {
                throw new ArgumentException("Keine App");
            }

            Appl tmp = obj as Appl;

            return String.Compare(this.GUID, tmp.GUID); 

        }

        /// <summary>
        /// Validates equality of two Apps
        /// </summary>
        /// <param name="other">Appl Object</param>
        /// <returns>true if they are equal</returns>
        public bool Equals(Appl other)
        {
            return String.Equals(this.GUID, other.GUID);
        }

        public int CompareTo(Appl other)
        {
            return String.Compare(this.GUID, other.GUID); 
        }
    }
}
