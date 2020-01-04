using System;

public class Package
{
    public String name { get; set; }
    public Clazz[] classes { get; set; }
    public Package(String name, Clazz[] classes)
	{
        this.name = name;
        this.classes = classes;
	}
}
