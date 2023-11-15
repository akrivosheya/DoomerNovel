using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Factory
{
    public class DefaultFactorySO<T> : FactorySO<T> where T: class
    {
        private Dictionary<string, T> _objects = new Dictionary<string, T>();

        public override bool TryGetObject(InitialData initial, out T obj)
        {
            obj = default;
            if(_objects.ContainsKey(initial.Type))
            {
                obj = _objects[initial.Type];
                return true;
            }

            string typeString = GetTypeById(initial.Type);
            if(typeString == EmptyString)
            {
                Debug.LogError($"Can't create {initial.Type}: this id doesn't exist.");
                return false;
            }

            Type type = Type.GetType(typeString);
            if(type is null)
            {
                Debug.LogError($"Can't create {typeString}: this type doesn't exist.");
                return false;
            }

            ConstructorInfo[] constructors = type.GetConstructors();
            T newObject = constructors[0].Invoke(initial.Parameters) as T;
            if(newObject is null)
            {
                Debug.LogError($"Can't create {type} with given parameters.");
                return false;
            }
            
            _objects[initial.Type] = newObject;
            obj = newObject;
            return true;
        }
    }
}
