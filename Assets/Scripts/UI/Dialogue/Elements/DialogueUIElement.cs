using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public abstract class DialogueUIElement : MonoBehaviour, IInitializable
    {
        public string ID { get => _id; private set => _id = value; }
        public DialogueUIElement Parent { get => _parent; protected set => _parent = value; }

        [SerializeField] private DialogueUIElement _parent;
        [SerializeField] private string _id;

        [SerializeField] private string _defaultId = "_";
        [SerializeField] private int _idIndex = 0;
        [SerializeField] private int _xPositionIndex = 1;
        [SerializeField] private int _yPositionIndex = 2;


        public void SetParent(DialogueUIElement parent)
        {
            Parent = parent;
            transform.SetParent(parent.transform);
        }
        
        public abstract Rect GetRect();
        public virtual void InterruptPresentation() { }
        public virtual FactorySO<DialogueUIElement> GetFactory() => Parent.GetFactory();
        public virtual void SetActive(bool isActive) { }
        public virtual void SetChild(DialogueUIElement child) { }
        public virtual void Clear() { }
        
        public virtual void Initialize(params string[] initParameters)
        {
            ID  = GetStringParameter(initParameters, _idIndex);

            float xPosition = GetFloatParameter(initParameters, _xPositionIndex);
            float yPosition = GetFloatParameter(initParameters, _yPositionIndex);
            transform.localPosition = new Vector3(xPosition, yPosition);
        }

        public virtual bool TryGetChild(string id, out DialogueUIElement child)
        {
            child = default;
            return false;
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
    }
}
