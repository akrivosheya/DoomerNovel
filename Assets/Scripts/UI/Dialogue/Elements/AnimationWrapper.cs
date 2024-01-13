using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue.Elements
{
    public class AnimationWrapper : DialogueUIElement
    {
        public string CurrentAnimation { get; private set; }

        [SerializeField] private DialogueUIElement _child;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _notInterruptingTag = "notInterrupting";

        private readonly int _animationStateIndex = 3;


        public void StartAnimation(string animationState)
        {
            if (animationState == string.Empty)
            {
                _animator?.Play(0);
            }
            else
            {
                _animator?.Play(animationState);
            }
            
            CurrentAnimation = animationState;
        }

        public void Replay()
        { 
            _animator?.Play(0);
        }

        public override void Accept(DialogueSaverVisitor visitor)
        {
            visitor.VisitAnimationWrapper(this);
            _child.Accept(visitor);
        }

        public override void InterruptPresentation()
        {
            if (_animator is null)
            {
                Debug.Log("can't interrupt " + _child.ID + ": it doesn't have animator");
                return;
            }

            AnimatorStateInfo currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (currentStateInfo.tagHash == Animator.StringToHash(_notInterruptingTag))
            {
                return;
            }

            AnimatorStateInfo nextStateInfo = _animator.GetNextAnimatorStateInfo(0);
            _animator.Play(nextStateInfo.fullPathHash);
        }

        public override void Initialize(params string[] initParameters)
        {
            base.Initialize(initParameters);

            if (initParameters.Length > _animationStateIndex)
            {
                string animationState = initParameters[_animationStateIndex];
                StartAnimation(animationState);
            }
        }

        public override void SetChild(DialogueUIElement child)
        {
            transform.DetachChildren();
            _child = child;
            child.SetParent(this);
            child.transform.localScale = Vector3.one;
            child.transform.localPosition = Vector3.zero;

            _animator = child.GetComponent<Animator>();
            if (_animator is null)
            {
                Debug.Log(child.ID + " doesn't has component Animator");
            }
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
            _child?.SetActive(isActive);
        }

        public override Rect GetRect()
        {
            return (_child is null) ? Rect.zero : _child.GetRect();
        }

        public override void Clear()
        {
            _child?.Clear();
            Destroy(_child?.gameObject);
            _child = null;
        }

        public override void Remove(string id)
        {
            if (_child?.ID == id)
            {
                _child?.Clear();
                Destroy(_child?.gameObject);
            }
        }
    }
}
