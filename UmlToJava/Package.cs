using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlToJava
{
    class Package
    {
        public string name { get; set; }
        public Clazz[] classes { get; set; }
        public Package(String name, Clazz[] classes)
        {
            this.name = name;
            this.classes = classes;
        }
    }
}
