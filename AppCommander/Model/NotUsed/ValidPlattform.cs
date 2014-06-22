using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCommander.Model
{
    public class ValidPlattform
    {

        public Plattform Plattform { get; set; }

        public XMLVersion MinVersion { get; set;}

        public XMLVersion MaxVersion { get; set; }

        public ValidPlattform() { }
    }
}
