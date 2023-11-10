using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MenuElementsContainerUI : MonoBehaviour, IActivatingUIElement
    {
        [SerializeField] List<GameObject> _elements = new List<GameObject>();

        private List<IActivatingUIElement> _activatingUI = new List<IActivatingUIElement>();


        void Awake()
        {
            for (int i = 0; i < _elements.Count;)
            {
                if (_elements[i].TryGetComponent(out IActivatingUIElement element))
                {
                    _activatingUI.Add(element);
                    _elements.RemoveAt(i);
                }
                else 
                {
                    ++i;
                }
            }

            SetOtherElementsInterfaces();
        }

        public void SetActive(bool areActive)
        {
            foreach (IActivatingUIElement element in _activatingUI)
            {
                element.SetActive(areActive);
            }

            foreach (GameObject obj in _elements)
            {
                obj.SetActive(areActive);
            }

            SetActiveOtherElements(areActive);
        }

        protected virtual void SetActiveOtherElements(bool areActive)
        {

        }

        protected virtual void SetOtherElementsInterfaces()
        {
            
        }
    }
}
