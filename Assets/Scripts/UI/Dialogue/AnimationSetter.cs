using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UI.Dialogue.Elements;

namespace UI.Dialogue
{
    public class AnimationSetter : IDialoguePresenterBehaviour
    {
        private readonly int ElementIdIndex = 0;
        private readonly int AnimationIndex = 1;

        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= ElementIdIndex)
            {
                Debug.Log($"parameters doesn't have element id parameter: its length is {parameters.Length}");
                return;
            }

            if (!presenter.TryGetElement(parameters[ElementIdIndex], out DialogueUIElement element))
            {
                Debug.LogError($"can't get element {parameters[ElementIdIndex]}");
                return;
            }

            if (element is AnimationWrapper animationWrapper)
            {
                string animation = (parameters.Length <= AnimationIndex) ? string.Empty : parameters[AnimationIndex];
                animationWrapper.StartAnimation(animation);
            }
            else
            {
                Debug.LogError($"{parameters[ElementIdIndex]} isn't AnimationWrapper element");
            }
        }
    }
}
