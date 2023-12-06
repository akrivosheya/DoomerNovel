using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogue.Elements
{
    public class TextUI : DialogueUIElement
    {
        [SerializeField] private Text _text;

        [SerializeField] private int _textIndex = 3;


        public override void Initialize(params string[] initParameters)
        {
            base.Initialize(initParameters);

            if (initParameters.Length > _textIndex)
            {
                _text.text = initParameters[_textIndex];
            }
        }

        public override void SetActive(bool isActive)
        {
            _text.gameObject.SetActive(isActive);
        }

        public override Rect GetRect()
        {
            return _text.rectTransform.rect;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
