using System;
using System.Text;


public class HelperGameObject
{
	// Remove the (Clone) from the name
	public static string RemoveClone(string name){
		if (!name.Contains ("(Clone)"))
			return name;
		char[] name_array = name.ToCharArray();
		StringBuilder s = new StringBuilder();
		for (int k = 0; k < name_array.Length - 7; k++) {
			s.Append(name_array[k]);
		}
		return s.ToString();
	}
}
