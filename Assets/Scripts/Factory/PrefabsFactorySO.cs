using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class PrefabsFactorySO<T> : FactorySO<T> where T: class, IInitializable
    {
        [SerializeField] private string _prefabsFolder = "Prefabs";

        private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();


        public override bool TryGetObject(InitialData initial, out T component)
        {
            component = default;
            GameObject obj;
            if(_prefabs.ContainsKey(initial.Type))
            {
                obj = Instantiate(_prefabs[initial.Type]);
                return obj.TryGetComponent(out component);
            }

            string prefabFile = GetTypeById(initial.Type);
            if(prefabFile == EmptyString)
            {
                Debug.LogError($"Can't instantiate {initial.Type}: this id doesn't exist.");
                return false;
            }

            obj = Resources.Load<GameObject>(System.IO.Path.Join(_prefabsFolder, prefabFile));
            if(obj is null)
            {
                Debug.LogError($"Can't instantiate {prefabFile}: this prefab doesn't exist.");
                return false;
            }

            GameObject newObject = Instantiate(obj);
            if (!newObject.TryGetComponent(out component))
            {
                Debug.LogError($"{prefabFile} doesn't have component {typeof(T)}");
                Destroy(newObject);
                return false;
            }

            try
            {
                component.Initialize(initial.Parameters);
            }
            catch (InitializationException ex)
            {
                Debug.LogError($"Can't initialize {prefabFile}: {ex.Message}");
                Destroy(newObject);
                return false;
            }

            return true;
        }
    }
}
