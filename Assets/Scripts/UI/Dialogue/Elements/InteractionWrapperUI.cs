using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue.Elements
{
    public class InteractionWrapperUI : DialogueUIElement
    {
        [SerializeField] private DialogueUIElement _child;
        [SerializeField] private RectTransform _button;
        [SerializeField] private Events.Event.EventTypes _interactionEvent = Events.Event.EventTypes.Interacted;

        private readonly string _idKey = "id";
        private readonly int _eventIndex = 3;


        public void OnClick()
        {
            Events.Event newEvent = new Events.Event(_interactionEvent);
            newEvent.SetStringValue(_idKey, ID);
            Parent.HandleEvent(newEvent);
        }

        public override void Initialize(params string[] initParameters)
        {
            base.Initialize(initParameters);

            if (initParameters.Length > _eventIndex)
            {
                string eventString = initParameters[_eventIndex];
                if (Enum.TryParse(eventString, out Events.Event.EventTypes eventType))
                {
                    _interactionEvent = eventType;
                }
            }
        }

        public override void SetChild(DialogueUIElement child)
        {
            transform.DetachChildren();
            _child = child;
            child.SetParent(this);
            child.transform.localScale = Vector3.one;
            child.transform.localPosition = Vector3.zero;
            
            Rect childRect = child.GetRect();
            _button.transform.SetParent(transform);
            _button.transform.localPosition = Vector3.zero;
            _button.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childRect.width);
            _button.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childRect.height);
        }

        public override bool TryGetChild(Stack<string> ids, out DialogueUIElement child)
        {
            if (_child is null)
            {
                child = default;
                return false;
            }

            if (_child?.ID == ids.Peek())
            {
                if (TryGetChild(ids, out child, _child))
                {
                    return true;
                }
            }

            return _child.TryGetChild(ids, out child);
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
