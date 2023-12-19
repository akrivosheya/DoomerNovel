using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SavesListUI : MonoBehaviour
    {
        public bool SelectedCellIsEmpty { get => SelectedCellIndex < 0 || _savesList[SelectedCellIndex].IsEmpty; }
        public int SelectedCellIndex { get; private set; } = -1;

        [SerializeField] private SaveCellUI _saveCellPrefab;
        [SerializeField] private GameObject _listContainer;
        [SerializeField] private GameObject _mask;
        [SerializeField] private List<SaveCellUI> _savesList;
        [SerializeField] private Scrollbar _scrollbar;

        [SerializeField] private Vector3 _cellSize = new Vector3(1f, 1f, 1f);
        [SerializeField] private Vector3 _firstContainerPosition = new Vector3(0f, 150f);
        [SerializeField] private float _cellsOffset = 150f;


        public void Reset()
        {
            SelectedCellIndex = -1;
        }

        public void UpdateSelectedSave(string imageString, string text)
        {
            _savesList[SelectedCellIndex].Initialize(imageString, text);
        }

        public void SetEmpttySelectedCell()
        {
            _savesList[SelectedCellIndex].InitializeEmpty();
        }

        public void OnScrollbarMove(float value)
        {
            Vector3 newPosition = _firstContainerPosition + new Vector3(0, value * _cellsOffset * _savesList.Count);
            _listContainer.transform.localPosition = newPosition;
        }

        public void AddSaveCell(string imageString, string text)
        {
            if (!_listContainer.gameObject.activeSelf)
            {
                return;
            }

            SaveCellUI saveCell = Instantiate(_saveCellPrefab);
            saveCell.Initialize(imageString, text);
            AddSaveCell(saveCell);
        }

        public void AddSaveCell()
        {
            if (!_listContainer.gameObject.activeSelf)
            {
                return;
            }

            SaveCellUI saveCell = Instantiate(_saveCellPrefab);
            saveCell.InitializeEmpty();
            AddSaveCell(saveCell);
        }

        public void SetActive(bool isActive)
        {
            _mask.SetActive(isActive);
            _scrollbar.gameObject.SetActive(isActive);
            if (!isActive)
            {
                foreach (SaveCellUI saveCell in _savesList)
                {
                    Destroy(saveCell.gameObject);
                }
                _savesList.Clear();
            }
            else
            {
                _listContainer.transform.localPosition = _firstContainerPosition;
                _scrollbar.value = 0;
            }
        }

        private void OnClickSaveCell(int index)
        {
            if (index != SelectedCellIndex)
            {
                if (SelectedCellIndex >= 0)
                {
                    _savesList[SelectedCellIndex].SetSelected(false);
                }

                SelectedCellIndex = index;
                _savesList[SelectedCellIndex].SetSelected(true);
            }
        }

        private void AddSaveCell(SaveCellUI saveCell)
        {
            _savesList.Add(saveCell);
            int saveCellIndex = _savesList.Count - 1;
            saveCell.transform.SetParent(_listContainer.transform);
            saveCell.transform.localPosition = new Vector3(0, -_cellsOffset * saveCellIndex);
            saveCell.transform.localScale = _cellSize;
            saveCell.AddListenerOnClickCell(() => OnClickSaveCell(saveCellIndex));
        }
    }
}
