using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Data;

namespace UI
{
    public class MenuElementsContainerUI : MonoBehaviour, IActivatingUIElement
    {
        [SerializeField] private GameObject _confirmWindowPrefab;
        [SerializeField] private LanguageManagerSO _languageManager;
        [SerializeField] List<GameObject> _elements = new List<GameObject>();


        private GameObject _confirmWindow;
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

        protected virtual void SetActiveOtherElements(bool areActive) { }
        protected virtual void SetOtherElementsInterfaces() { }

        protected void ShowConfirmWindow(string message, UnityAction onConfirm, UnityAction onDeny)
        {
            _confirmWindow = Instantiate(_confirmWindowPrefab);
            if (!(_confirmWindow is null) && _confirmWindow.TryGetComponent(out ConfirmWindowUI confirmWindowConfiguration))
            {
                _confirmWindow.transform.SetParent(transform);
                _confirmWindow.transform.localScale = Vector3.one;
                _confirmWindow.transform.localPosition = Vector3.zero;

                confirmWindowConfiguration.Message = _languageManager.GetText(message);
                confirmWindowConfiguration.AddListenerOnConfirm(onConfirm);
                confirmWindowConfiguration.AddListenerOnDeny(onDeny);
            }
        }

        protected void DestroyWindow()
        {
            Destroy(_confirmWindow);
            _confirmWindow = null;
        }
    }
}
