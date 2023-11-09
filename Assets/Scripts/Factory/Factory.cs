using System;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Factory<T> : ScriptableObject where T : class
    {
        private Dictionary<string, string> _classesID = new Dictionary<string, string>();
        private Dictionary<string, T> _classes = new Dictionary<string, T>();


        public bool TryGetObject(string objectName, string[] parameters, out T obj)
        {
            if(_classes.ContainsKey(objectName))
            {
                obj = _classes[objectName];
                return true;
            }
            if(!_classesID.ContainsKey(objectName))
            {
                Debug.LogError("Can't create " + objectName + ": this id doesn't exist.");
                obj = default(T);
                return false;
            }
            var objectType = _classesID[objectName];
            var type = Type.GetType(objectType);
            if(type is null)
            {
                Debug.LogError("Can't create " + objectType + ": this type doesn't exist.");
                obj = default(T);
                return false;
            }
            var constructors = type.GetConstructors();
            var newObject = constructors[0].Invoke(parameters) as T;
            if(newObject is null)
            {
                Debug.LogError("Can't create " + objectType + " with given parameters.");
                obj = default(T);
                return false;
            }
            _classes[objectName] = newObject;
            obj = newObject;
            return true;
        }
    }
}
