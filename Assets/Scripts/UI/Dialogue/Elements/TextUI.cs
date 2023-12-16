using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogue.Elements
{
    public class TextUI : DialogueUIElement
    {
        [SerializeField] private Text _textUI;
        [SerializeField] private float _writingPauseSeconds = 0.25f;
        [SerializeField] private int _textIndex = 3;

        private string _text = string.Empty;
        private bool _isWriting = false;
        private bool _isInterrupted = false;


        public override void Initialize(params string[] initParameters)
        {
            base.Initialize(initParameters);

            if (initParameters.Length > _textIndex)
            {
                _textUI.text = initParameters[_textIndex];
            }
        }

        public override void SetActive(bool isActive)
        {
            _textUI.gameObject.SetActive(isActive);
        }

        public override Rect GetRect()
        {
            return _textUI.rectTransform.rect;
        }

        public override void InterruptPresentation()
        {
            if (_isWriting && !_isInterrupted)
            {
                _isInterrupted = true;
            }
        }

        public void SetText(string text)
        {
            if (_isWriting)
            {
                return;
            }
            _text = text;
        }

        public void StartWriting()
        {
            if (!_isWriting)
            {
                StartCoroutine(WriteText());
            }
        }

        private IEnumerator WriteText()
        {
            _isWriting = true;
            SendActivatedEvent();
            int currentLength = 0;
            for (; currentLength < _text.Length; ++currentLength)
            {
                if (_isInterrupted)
                {
                    _textUI.text = _text;
                    break;
                }

                _textUI.text = _text.Substring(0, currentLength);
                yield return new WaitForSeconds(_writingPauseSeconds);
            }

            _isInterrupted = false;
            _isWriting = false;
            SendUnactivatedEvent();
        }
    }
}
