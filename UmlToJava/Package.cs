using System;

public class Package
{
    public String name { get; set; }
    public List<Clazz> classes { get; set; }
    public Package(String name, List<Clazz> classes)
	{
        this.name = name;
        this.classes = classes;
	}
}
