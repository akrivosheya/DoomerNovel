using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Data;

namespace UI
{
    public class SavesUI : MenuElementsContainerUI
    {
        [SerializeField] private SavesListUI _savesList;
        [SerializeField] private SaveManagerSO _saveManager;
        [SerializeField] private string _deleteMessage = "deleteMessage";
        [SerializeField] private string _emptySaveMessage = "emptySaveMessage";
        [SerializeField] private string _continueMessage = "continueMessage";
        [SerializeField] private string _continueErrorMessage = "continueErrorMessage";
        [SerializeField] private string _saveMessage = "saveMessage";
        [SerializeField] private string _notSelectedSaveMessage = "notSelectedSaveMessage";

        private UnityEvent _onBackSaves = new UnityEvent();
        private UnityEvent _onContinue = new UnityEvent();
        private UnityEvent _onSave = new UnityEvent();


        public void UpdateSelectedSave()
        {
            SaveData currentSave = _saveManager.GetCurrentSaveData();
            _savesList.UpdateSelectedSave(currentSave.SaveImage, currentSave.SaveInformation);
        }

        public void OnClickDelete()
        {
            if (_savesList.SelectedCellIsEmpty)
            {
                return;
            }

            ShowConfirmWindow(_deleteMessage, OnConfirmDelete, DestroyWindow);
        }

        public void OnConfirmDelete()
        {
            _saveManager.DeleteSaveData(_savesList.SelectedCellIndex, out string message);
            _savesList.SetEmpttySelectedCell();
            DestroyWindow();
            ShowMessageWindow(message, DestroyWindow);
        }

        public void OnClickBack()
        {
            _onBackSaves?.Invoke();
        }

        public void OnClickContinue()
        {
            if (_savesList.SelectedCellIsEmpty)
            {
                ShowMessageWindow(_emptySaveMessage, DestroyWindow);
                return;
            }

            ShowConfirmWindow(_continueMessage, OnConfirmContinue, DestroyWindow);
        }

        public void OnClickSave()
        {
            if (_savesList.SelectedCellIndex < 0)
            {
                ShowMessageWindow(_notSelectedSaveMessage, DestroyWindow);
                return;
            }

            ShowConfirmWindow(_saveMessage, OnConfirmSave, DestroyWindow);
        }

        public void AddListenerOnBackSaves(UnityAction onBackAction)
        {
            _onBackSaves.RemoveAllListeners();
            _onBackSaves.AddListener(onBackAction);
        }

        public void RemoveListenerOnBackSaves(UnityAction onBackAction)
        {
            _onBackSaves.RemoveListener(onBackAction);
        }

        public void AddListenerOnContinue(UnityAction onContinueAction)
        {
            _onContinue.RemoveAllListeners();
            _onContinue.AddListener(onContinueAction);
        }

        public void RemoveListenerOnContinue(UnityAction onContinueAction)
        {
            _onContinue.RemoveListener(onContinueAction);
        }

        public void AddListenerOnSave(UnityAction onSaveAction)
        {
            _onSave.RemoveAllListeners();
            _onSave.AddListener(onSaveAction);
        }

        public void RemoveListenerOnSave(UnityAction onSaveAction)
        {
            _onSave.RemoveListener(onSaveAction);
        }

        protected override void SetActiveOtherElements(bool areActive)
        {
            _savesList.SetActive(areActive);
            if (areActive)
            {
                List<SaveData> savesList = _saveManager.GetSavesList();
                foreach (SaveData save in savesList)
                {
                    if (save.IsEmpty)
                    {
                        _savesList.AddSaveCell();
                    }
                    else
                    {
                        _savesList.AddSaveCell(save.SaveImage, save.SaveInformation);
                    }
                }
            }
            _savesList.Reset();
        }

        private void OnConfirmContinue()
        {
            DestroyWindow();
            if (!_saveManager.TryLoadCurrentSaveData(_savesList.SelectedCellIndex))
            {
                ShowMessageWindow(_continueErrorMessage, DestroyWindow);
                return;
            }

            _onContinue?.Invoke();
        }

        private void OnConfirmSave()
        {
            DestroyWindow();
            _saveManager.SetCurrentSaveIndex(_savesList.SelectedCellIndex);
            _onSave?.Invoke();
        }
    }
}
