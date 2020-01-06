using System;
using System.Collections.Generic;
using System.IO;

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
            Package currentPack = null;
            Clazz currentClazz = null;
            List<Package> packages = new List<Package>();
            string[] lines = ReadXmlFile();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("fontStyle") || lines[i].Contains("plain") || !lines[i].Contains("hasText"))
                {
                    if (currentPack != null)
                    {
                        packages.Add(currentPack);
                    }
                    String s = lines[i].Substring(lines[i].IndexOf(">"));
                    String packName = s.Substring(0, s.IndexOf("<"));
                    packName.Replace("<", "");
                    currentPack = new Package(packName, new List<Clazz>());
                }
                if (lines[i].Contains("fontStyle") || lines[i].Contains("bold") || !lines[i].Contains("hasText"))
                {
                    if (currentClazz != null)
                    {
                        currentPack.classes.Add(currentClazz);
                    }
                    String s = lines[i].Substring(lines[i].IndexOf(">"));
                    String className = s.Substring(0, s.IndexOf("<"));
                    className.Replace("<", "");
                    currentClazz = new Clazz( className, new List < Parameter >(), new List < Method > ());
                }
                if (lines[i].Contains("enumeration"))
                {
                    currentClazz.isEnum = true;
                    currentClazz.enums = new List<String>();
                }
                if (lines[i].Contains("<y:AttributeLabel") || currentClazz.isEnum == true) 
                {
                    int counter = 0;
                    while (true)
                    {
                        if (lines[i + counter].StartsWith("</y:AttributeLabel")) break;
                            if (lines[i + counter].Contains("<y:AttributeLabel"))
                            {
                                lines[i + counter] = lines[i + counter].Substring(lines[i + counter].IndexOf(">"));
                            }
                            String[] sar = lines[i + counter].Split(",");
                        foreach (String s in sar)
                        {
                            if (s != "")
                            {
                                s.Replace("</y:AttributeLabel>", "");
                                currentClazz.enums.Add(s);
                            }
                        }
                        counter++;
                        if (lines[i].Contains("</y:AttributeLabel")) break;
                    }
                }
                else if (lines[i].Contains("<y:AttributeLabel"))
                {
                    int counter = 0;
                    while (true)
                    {
                        if (lines[i+counter].StartsWith("</y:AttributeLabel")) break;
                        if (lines[i + counter].Contains("="))
                        {
                            lines[i + counter].Replace("- &lt;u&gt;", "");
                            lines[i + counter].Replace("&lt;/u&gt;&lt;br&gt;", "");
                            lines[i + counter].Replace("- ", "");
                            lines[i + counter].Replace("&lt;html&gt;+ &lt;u&gt;", "");
                            lines[i + counter].Replace("&lt;/u&gt;&lt;/html&gt;", "");
                            if (lines[i + counter].Contains("<y:AttributeLabel"))
                            {
                                lines[i + counter] = lines[i + counter].Substring(lines[i + counter].IndexOf(">"));
                            }
                            String[] sar = lines[i + counter].Split(":");
                            if (sar[1].Contains("</y:AttributeLabel")) sar[1].Replace("</y:AttributeLabel>", "");
                            String[] x = sar[0].Split("=");
                            currentClazz.parameters.Add(new Parameter(sar[1], x[0], x[1]));
                        }
                        else if (lines[i+counter].Contains(":"))
                        {
                            lines[i + counter].Replace("- &lt;u&gt;", "");
                            lines[i + counter].Replace("&lt;/u&gt;&lt;br&gt;", "");
                            lines[i + counter].Replace("- ", "");
                            if (lines[i + counter].Contains("<y:AttributeLabel"))
                            {
                                lines[i + counter] = lines[i + counter].Substring(lines[i + counter].IndexOf(">"));
                            }
                            String[] sar = lines[i + counter].Split(":");
                            if(sar[1].Contains("</y:AttributeLabel")) sar[1].Replace("</y:AttributeLabel>", "");
                            currentClazz.parameters.Add(new Parameter(sar[1], sar[0]));
                        }
                        counter++;
                        if (lines[i].Contains("</y:AttributeLabel")) break;
                    }
                    
                };
                if (lines[i].Contains("<y:MethodLabel"))
                {
                    int counter = 0;
                    while (true)
                    {
                        if (lines[i + counter].StartsWith("</y:MethodLabel")) break;
                        if (lines[i + counter].Contains(":"))
                        {
                            lines[i + counter].Replace("&lt;html&gt;+ &lt;u&gt;", "");
                            lines[i + counter].Replace("&lt;/u&gt;&lt;/html&gt;", "");
                            lines[i + counter].Replace("+ ", "");
                            if (lines[i + counter].Contains("<y:MethodLabel"))
                            {
                                lines[i + counter] = lines[i + counter].Substring(lines[i + counter].IndexOf(">"));
                            }
                            String methName = lines[i + counter].Substring(0, lines[i + counter].IndexOf("("));
                            String returnType = lines[i + counter].Substring(lines[i + counter].LastIndexOf(":"));
                            if (returnType.Contains("</y:MethodLabel")) returnType.Replace("</y:MethodLabel>", "");
                            String args = lines[i + counter].Substring(lines[i + counter].IndexOf("("), lines[i + counter].IndexOf(")"));
                            String[] argsArr = args.Split(",");
                            List<Parameter> paraList = new List<Parameter>();
                            foreach (String x in argsArr)
                            {
                                String[] sArr = x.Split(":");
                                paraList.Add(new Parameter(sArr[1], sArr[0]));

                            }
                            currentClazz.methods.Add(new Method(returnType, methName, paraList));
                        }
                        else if (lines[i + counter].Contains("("))
                        {
                            lines[i + counter].Replace("&lt;html&gt;+ &lt;u&gt;", "");
                            lines[i + counter].Replace("&lt;/u&gt;&lt;/html&gt;", "");
                            lines[i + counter].Replace("+ ", "");
                            if (lines[i + counter].Contains("<y:MethodLabel"))
                            {
                                lines[i + counter] = lines[i + counter].Substring(lines[i + counter].IndexOf(">"));
                            }
                            String methodName = lines[i + counter].Substring(0, lines[i + counter].IndexOf("("));
                            methodName.Replace("()", "");
                            currentClazz.methods.Add(new Method(null, methodName, null));
                        }
                        counter++;
                        if (lines[i].Contains("</y:MethodLabel")) break;
                    }

                };
            }
            currentPack.classes.Add(currentClazz);
            packages.Add(currentPack);
            return packages;
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
