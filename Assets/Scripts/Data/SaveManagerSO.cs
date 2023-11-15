using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="SaveManagerSO")]
    public class SaveManagerSO : ScriptableObject
    {
        public SaveData GetCurrentSaveData()
        {
            return new SaveData();
        }
    }
}
