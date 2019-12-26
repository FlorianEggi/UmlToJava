using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlToJava
{
    class Parser
    {
        //Schreibt einen Parser, der das graphml-File einliest und in ein sinnvolles Format für die Weiterverarbeitung umwandelt.
        //Kontrolliert dabei die Korrektheit der Eingabe und leitet etwaige Fehler an die GUI weiter.
        private string xmlPath;
        public Parser(string xmlPath)
        {
            this.xmlPath = xmlPath;

        }

        public List<Package> parse()
        {
            List<Package> packages = new List<Package>();
            string[] lines = ReadXmlFile();
            for (int i = 0; i < lines.Length; i++)
            {
                if(lines[i].Contains("AttributeLabel")) Console.WriteLine(lines[i]);
            }
        }

        private string[] ReadXmlFile()
        {
            string[] lines = File.ReadAllLines(xmlPath);

            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);

                return lines;
            }
        }
    }
}
