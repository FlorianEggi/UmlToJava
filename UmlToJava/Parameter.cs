using System;

public class Parameter
{
    public string type { get; set; }
    public string name { get; set; }
    public string value { get; set; }
    public Parameter(string type, string name)
    {
        this.type = type;
        this.name = name;
    }
    public Parameter(string type, string name, string value)
    {
        this.type = type;
        this.name = name;
        this.value = value;
    }
}
