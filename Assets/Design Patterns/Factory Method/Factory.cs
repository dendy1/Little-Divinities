using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    private Dictionary<string, Type> _elements;
    
    public Factory()
    {
        var elementTypes = Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType =>
            myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));
        
        _elements = new Dictionary<string, Type>();

        foreach (var type in elementTypes)
        {
            var temp = Activator.CreateInstance(type) as T;
            _elements.Add(temp.name, type);
        }
    }

    public T GetElement(string name)
    {
        if (_elements.ContainsKey(name))
        {
            Type type = _elements[name];
            var element = Activator.CreateInstance(type) as T;
            return element;
        }

        return null;
    }
}
