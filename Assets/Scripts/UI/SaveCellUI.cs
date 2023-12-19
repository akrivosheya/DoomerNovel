using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI
{
    public class SaveCellUI : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }

        [SerializeField] private Image _selectedBorder;
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private string _spritesFolder = "Sprites";

        private UnityEvent _onClickCell = new UnityEvent();


        public void OnClick()
        {
            _onClickCell?.Invoke();
        }

        public void Clear()
        {
            IsEmpty = true;
            _image.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
            _selectedBorder.gameObject.SetActive(false);
        }
        
        public void InitializeEmpty()
        {
            IsEmpty = true;
            _image.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
            _selectedBorder.gameObject.SetActive(false);
        }

        public void Initialize(string imageString, string text)
        {
            IsEmpty = false;
            _image.gameObject.SetActive(true);
            _text.gameObject.SetActive(true);
            _selectedBorder.gameObject.SetActive(false);
            _image.sprite = Resources.Load<Sprite>(System.IO.Path.Join(_spritesFolder, imageString));
            _text.text = text;
        }

        public void AddListenerOnClickCell(UnityAction onClickAction)
        {
            _onClickCell.RemoveAllListeners();
            _onClickCell.AddListener(onClickAction);
        }

        public void SetSelected(bool isSelected)
        {
            _selectedBorder.gameObject.SetActive(isSelected);
        }
    }
}
