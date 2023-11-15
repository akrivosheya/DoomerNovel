using UnityEngine;

namespace UI.Dialogue.Elements
{
    public class InteractionWrapperUI : DialogueUIElement
    {
        [SerializeField] private DialogueUIElement _child;
        [SerializeField] private RectTransform _button;

        [SerializeField] private string _event;
        [SerializeField] private int _eventIndex = 3;


        public override void Initialize(params string[] initParameters)
        {
            base.Initialize();

            if (initParameters.Length > _eventIndex)
            {
                _event = initParameters[_eventIndex];
            }
        }

        public override void SetChild(DialogueUIElement child)
        {
            _child = child;
            child.SetParent(this);
            child.transform.localScale = Vector3.one;
            
            Rect childRect = child.GetRect();
            _button.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childRect.width);
            _button.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childRect.height);
            _button.position = child.transform.position;
        }

        public override bool TryGetChild(string id, out DialogueUIElement child)
        {
            if (_child is null)
            {
                child = default;
                return false;
            }

            if (_child?.ID == id)
            {
                child = _child;
                return true;
            }

            return _child.TryGetChild(id, out child);
        }

        public override void SetActive(bool isActive)
        {
            _child.SetActive(isActive);
        }

        public override Rect GetRect()
        {
            return _button.rect;
        }
    }
}
