using System;
public class GeneratorAttribute : Attribute 
{
	public string Name{ get; private set; }
	public GeneratorAttribute (string name)
	{
		this.Name = name;
	}
}


