using System;
using System.Collections.Generic;

public class Method
{
    public string type { get; set; }
    public List <Parameter> args { get; set; }
    public string name { get; set; }
  
    public Method(string type, string name, List<Parameter> args)
    {
        this.type = type;
        this.name = name;
        this.args = args;
    }
}