using System;

namespace MyLib;

public class TestAttribute : Attribute
{
    public string Name { get; set; }

    public TestAttribute(string name)
    {
        Name = name;
    }
}