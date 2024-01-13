using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public abstract class FactorySO<T> : ScriptableObject where T : class
    {
        protected readonly string EmptyString = "";
        private readonly int IdTypeSize = 2;
        private readonly int TypeIndex = 1;
        private readonly int IdIndex = 0;

        [SerializeField] private string _typesIDsFolder = "Types";
        [SerializeField] private string _typesIDsFile;
        [SerializeField] private string _typesSeparator = ";";
        [SerializeField] private string _idTypeSeparator = "=";

        private Dictionary<string, string> _typesID = new Dictionary<string, string>();


        public void Initialize()
        {
            _typesID.Clear();
            string path = System.IO.Path.Join(_typesIDsFolder, _typesIDsFile);
            TextAsset stringsTypes = Resources.Load<TextAsset>(path);
            if (stringsTypes is null)
            {
                Debug.LogError($"Can't get data from file {path}");
                return;
            }

            string[] types = stringsTypes.text.Split(_typesSeparator);
            foreach (string idType in types)
            {
                string[] separatedIdType = idType.Trim().Split(_idTypeSeparator);
                if (separatedIdType.Length != IdTypeSize)
                {
                    continue;
                }
                _typesID.Add(separatedIdType[IdIndex], separatedIdType[TypeIndex]);
            }
        }

        protected string GetTypeById(string id)
        {
            return _typesID.GetValueOrDefault(id, EmptyString);
        }

        public abstract bool TryGetObject(InitialData initial, out T obj);
    }
}
