using System;

public class Clazz
{
    public String name { get; set; }
    public Parameter[] parameters { get; set; }
    public Method[] methods { get; set; }
    public Boolean isInterface { get; set; }
    public Clazz(String name, Parameter[] parameters, Method[] methods)
    {
        this.name = name;
        this.parameters = parameters;
        this.methods = methods;
    }
}