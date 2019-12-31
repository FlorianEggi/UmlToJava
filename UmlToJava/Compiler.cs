using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlToJava
{
    class Compiler
    {
        string actpath;
        public void Compile(Package[] packages, string path)
        {
            foreach (var package in packages)
            {
                string pathstring = Path.Combine(path, package.name);
                actpath = pathstring;
                foreach (var clazz in package.classes)
                {
                    string classPath = $@"{actpath} + \ + {clazz.name} +.java";
                    //string pathstring2 = System.IO.Path.Combine(actpath, clazz.name);
                    if (clazz.isInterface)
                    {
                        TextWriter tw = new StreamWriter(classPath);
                        tw.WriteLine($"interface {clazz.name}");
                        tw.WriteLine("{");
                        foreach (var method in clazz.methods)
                        {
                            tw.WriteLine($"public {method.type} {method.name}({method.args});");
                        }
                        tw.WriteLine("}");
                    }
                    else
                    {
                        TextWriter tw = new StreamWriter(classPath);
                        tw.WriteLine($"public class {clazz.name}");
                        tw.WriteLine("{");
                        foreach (var parameter in clazz.parameters)
                        {
                            tw.WriteLine($"{ parameter.type} {parameter.name}");
                        }
                        foreach (var method in clazz.methods)
                        {
                            tw.WriteLine($"public {method.type} {method.name}({method.args})");
                            tw.WriteLine("{");
                            tw.WriteLine("}");
                            tw.WriteLine();
                        }
                        tw.WriteLine("}");
                    }
                }
            }
        }
    }
}
