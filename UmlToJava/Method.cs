using System;

public class Method
{
    public string type { get; set; }
    public string[] args { get; set; }
    public string name { get; set; }
    public Method(string type, string name, string[] args)
	{
        this.type = type;
        this.name = name;
        this.args = args;
	}
}
