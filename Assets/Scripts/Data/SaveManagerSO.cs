using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName="SaveManagerSO")]
    public class SaveManagerSO : ScriptableObject
    {
        [SerializeField] private string _savesCommonName = "save";
        [SerializeField] private string _notExistingFileMessage = "notExistMessage";
        [SerializeField] private string _fileErrorMessage = "fileErrorMessage";
        [SerializeField] private string _successDeletingMessage = "successDeletingMessage";
        [SerializeField] private int _savesCount = 10;

        private SaveData _currentSave;
        private int _saveIndexToSave;


        public void SaveDialogue(SaveData saveData)
        {
            TrySaveSaveData(MakeSaveName(_saveIndexToSave), saveData);
            _currentSave = saveData;
        }

        public SaveData GetCurrentSaveData()
        {
            return _currentSave;
        }

        public void ResetSaveData()
        {
            _currentSave = SaveData.EmptyData;
        }

        public void SetCurrentSaveIndex(int newIndex)
        {
            _saveIndexToSave = Mathf.Clamp(newIndex, 0, _savesCount - 1);
        }

        public List<SaveData> GetSavesList()
        {
            List<SaveData> savesList = new List<SaveData>();
            for (int i = 0; i < _savesCount; ++i)
            {
                if (TryLoadSaveData(MakeSaveName(i), out SaveDataDTO save))
                {
                    savesList.Add(new SaveData(save));
                }
                else
                {
                    savesList.Add(SaveData.EmptyData);
                }
            }

            return savesList;
        }

        public void DeleteSaveData(int saveIndex, out string message)
        {
            string path = Path.Combine(Application.persistentDataPath, MakeSaveName(saveIndex));
            if (!File.Exists(path))
            {
                Debug.LogError($"File {path} doesn't exist");
                message = _notExistingFileMessage;
                return;
            }

            try
            {
                File.Delete(path);
            }
            catch (IOException ex)
            {
                Debug.LogError($"Can't delete file {path}: {ex.Message}");
                message = _fileErrorMessage;
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.LogError($"Can't delete file {path}: {ex.Message}");
                message = _fileErrorMessage;
                return;
            }

            message = _successDeletingMessage;
        }

        public bool TryLoadCurrentSaveData(int saveIndex)
        {
            if (TryLoadSaveData(MakeSaveName(saveIndex), out SaveDataDTO currentSaveDTO))
            {
                _currentSave = new SaveData(currentSaveDTO);
                return true;
            }

            return false;
        }

        private bool TryLoadSaveData(string saveFile, out SaveDataDTO save)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFile);
            save = default;
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveDataDTO));
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    save = (SaveDataDTO)serializer.Deserialize(stream);
                    return true;
                }
            }

            return false;
        }

        private bool TrySaveSaveData(string saveFile, SaveData save)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFile);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveDataDTO));
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                SaveDataDTO saveDTO = new SaveDataDTO()
                {
                    SaveImage=save.SaveImage,
                    SaveInformation=save.SaveInformation,
                    SceneState=save.SceneState,
                    DialogueState=save.DialogueState
                };
                serializer.Serialize(stream, saveDTO);
            }

            return true;
        }

        private string MakeSaveName(int saveIndex)
        {
            return $"{_savesCommonName}{saveIndex}";
        }
    }
}
