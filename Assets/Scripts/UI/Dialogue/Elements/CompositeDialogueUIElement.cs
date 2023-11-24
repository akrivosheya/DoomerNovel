using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Factory;

namespace UI.Dialogue.Elements
{
    public abstract class CompositeDialogueUIElement : DialogueUIElement
    {
        protected FactorySO<DialogueUIElement> ElementsFactory { get => _factory; private set => _factory = value; }
        protected List<DialogueUIElement> Childs { get => _childs; }

        [SerializeField] private FactorySO<DialogueUIElement> _factory;
        [SerializeField] private List<DialogueUIElement> _childs = new List<DialogueUIElement>();
        private readonly ChildsCache _cache = new ChildsCache();


        public override void SetChild(DialogueUIElement newChild)
        {
            if (!Childs.Any(child => child.ID == newChild.ID))
            {
                Childs.Add(newChild);
                Vector3 position = newChild.transform.localPosition;
                newChild.SetParent(this);
                newChild.transform.localScale = Vector3.one;
                newChild.transform.localPosition = position;
            }
        }

        public override void Clear()
        {
            foreach (DialogueUIElement child in Childs)
            {
                child.Clear();
                Destroy(child.gameObject);
            }

            Childs.Clear();
        }

        public override bool TryGetChild(string id, out DialogueUIElement foundChild)
        {
            foundChild = default;
            int childIndex = Childs.FindIndex(child => child.ID == id);
            if (childIndex >= 0)
            {
                foundChild = Childs[childIndex];
                return true;
            }

            if (_cache.TryGetChild(id, out DialogueUIElement possibleContainer))
            {
                if (possibleContainer.TryGetChild(id, out foundChild))
                {
                    return true;
                }
                else
                {
                    _cache.RemoveId(id);
                }
            }

            foreach (DialogueUIElement child in Childs)
            {
                if (child.TryGetChild(id, out foundChild))
                {
                    _cache.AddId(id, child);
                    return true;
                }
            }

            return false;
        }

        public override void SetActive(bool isActive)
        {
            foreach (DialogueUIElement child in Childs)
            {
                child.SetActive(isActive);
            }
        }

        public override void InterruptPresentation()
        {
            foreach (DialogueUIElement child in Childs)
            {
                child.InterruptPresentation();
            }
        }

        public override Rect GetRect()
        {
            return Childs
                .Select(element => element.GetRect())
                .Aggregate((compositeRect, rect) =>
                    {
                        compositeRect.xMin = Mathf.Min(compositeRect.xMin, rect.xMin);
                        compositeRect.yMin = Mathf.Min(compositeRect.yMin, rect.yMin);
                        compositeRect.xMax = Mathf.Max(compositeRect.xMax, rect.xMax);
                        compositeRect.yMax = Mathf.Max(compositeRect.yMax, rect.yMax);
                        return compositeRect;
                    });
        }

        public override FactorySO<DialogueUIElement> GetFactory()
        {
            ElementsFactory ??= Parent.GetFactory();

            return ElementsFactory;
        }
    }
}
