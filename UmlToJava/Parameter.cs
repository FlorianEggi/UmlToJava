using System;

public class Parameter
{
    public string type { get; set; }
    public string name { get; set; }
    public Parameter(string type, string name)
    {
        this.type = type;
        this.name = name;
    }
}