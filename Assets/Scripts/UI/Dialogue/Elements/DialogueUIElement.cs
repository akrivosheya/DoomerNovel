using System.Collections.Generic;
using UnityEngine;

using Factory;
using System.Linq;

namespace UI.Dialogue.Elements
{
    public abstract class DialogueUIElement : MonoBehaviour, IInitializable
    {
        public string ID { get => _id; private set => _id = value; }
        public DialogueUIElement Parent { get => _parent; protected set => _parent = value; }

        [SerializeField] private DialogueUIElement _parent;
        [SerializeField] private string _id;

        [SerializeField] private string _defaultId = "_";
        [SerializeField] private char _idsSeparator = '.';
        [SerializeField] private int _idIndex = 0;
        [SerializeField] private int _xPositionIndex = 1;
        [SerializeField] private int _yPositionIndex = 2;
        
        private readonly Events.Event.EventTypes _activatedEvent = Events.Event.EventTypes.Activated;
        private readonly Events.Event.EventTypes _unactivatedEvent = Events.Event.EventTypes.Unactivated;


        public void SetParent(DialogueUIElement parent)
        {
            Parent = parent;
            transform.SetParent(parent.transform);
        }

        public string GetFullId()
        {
            return (Parent is null) ? ID : Parent.GetFullId() + ID;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public abstract Rect GetRect();
        public abstract void Accept(DialogueSaverVisitor visitor);
        public virtual void InterruptPresentation() { }
        public virtual FactorySO<DialogueUIElement> GetFactory() => Parent.GetFactory();
        public virtual void SetActive(bool isActive) { }
        public virtual void SetChild(DialogueUIElement child) { }
        public virtual void Remove(string id) { }
        public virtual void Clear() { }
        public virtual void HandleEvent(Events.Event currentEvent) => Parent?.HandleEvent(currentEvent);
        
        public virtual void Initialize(params string[] initParameters)
        {
            ID  = GetStringParameter(initParameters, _idIndex);

            float xPosition = GetFloatParameter(initParameters, _xPositionIndex);
            float yPosition = GetFloatParameter(initParameters, _yPositionIndex);
            transform.localPosition = new Vector3(xPosition, yPosition);
        }
        
        public bool TryGetChild(string id, out DialogueUIElement child)
        {
            Stack<string> idsStack = new Stack<string>(id.Split(_idsSeparator).Reverse());
            return TryGetChild(idsStack, out child);
        }

        public virtual bool TryGetChild(Stack<string> ids, out DialogueUIElement child)
        {
            child = default;
            return false;
        }

        public void SendActivatedEvent()
        {
            Events.Event newEvent = new Events.Event(_activatedEvent);
            Parent?.HandleEvent(newEvent);
        }

        public void SendUnactivatedEvent()
        {
            Events.Event newEvent = new Events.Event(_unactivatedEvent);
            Parent?.HandleEvent(newEvent);
        }

        private float GetFloatParameter(string[] parameters, int index)
        {
            if (parameters.Length > index && float.TryParse(parameters[index], out float position))
            {
                return position;
            }

            return 0;
        }

        private string GetStringParameter(string[] parameters, int index)
        {
            if (parameters.Length > index)
            {
                return parameters[index];
            }

            return _defaultId;
        }

        protected bool TryGetChild(Stack<string> ids, out DialogueUIElement foundChild, DialogueUIElement possibleContainer)
        {
            string currentChildId = ids.Pop();
            if (ids.Count == 0)
            {
                foundChild = possibleContainer;
                return true;
            }
            else if (possibleContainer.TryGetChild(ids, out foundChild))
            {
                return true;
            }

            ids.Push(currentChildId);
            return false;
        }
    }
}
