using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommander.Model
{
    public class Appl
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public byte Ranking { get; set; }
        public decimal Price { get; set; }
        public XMLVersion AppVersion { get; set; }
        public List<ValidPlattform> VPlattform { get; set; }        
        public Appl() { }
    }
}
