using System;

public class Clazz
{
    public String name { get; set; }
    public List<Parameter> parameters { get; set; }
    public List<Method> methods { get; set; }
    public Boolean isInterface { get; set; }
    public Boolean isEnum { get; set; }
    public List<String> enums { get; set; }
  
    public Clazz(String name, List<Parameter> parameters, List<Method> methods)
	{
        this.name = name;
        this.parameters = parameters;
        this.methods = methods;
        this.isEnum = false;
        this.isInterface = false;
	}
}
