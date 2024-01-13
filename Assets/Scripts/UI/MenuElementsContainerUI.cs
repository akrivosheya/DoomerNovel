using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Data;

namespace UI
{
    public class MenuElementsContainerUI : MonoBehaviour, IActivatingUIElement
    {
        [SerializeField] private GameObject _messageWindowPrefab;
        [SerializeField] private GameObject _confirmWindowPrefab;
        [SerializeField] private LanguageManagerSO _languageManager;
        [SerializeField] List<GameObject> _elements = new List<GameObject>();


        private GameObject _messageWindow;
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
                ConfigureMessageWindow(confirmWindowConfiguration, message, onConfirm);
                confirmWindowConfiguration.AddListenerOnDeny(onDeny);
            }
            else
            {
                DestroyObject(_confirmWindow);
            }
        }

        protected void ShowMessageWindow(string message, UnityAction onConfirm)
        {
            _messageWindow = Instantiate(_messageWindowPrefab);
            if (!(_messageWindow is null) && _messageWindow.TryGetComponent(out MessageWindowUI messageWindowConfiguration))
            {
                ConfigureMessageWindow(messageWindowConfiguration, message, onConfirm);
            }
            else
            {
                DestroyObject(_messageWindow);
            }
        }

        private void ConfigureMessageWindow(MessageWindowUI messageWindowConfiguration, string message, UnityAction onConfirm)
        {
                messageWindowConfiguration.transform.SetParent(transform);
                messageWindowConfiguration.transform.localScale = Vector3.one;
                messageWindowConfiguration.transform.localPosition = Vector3.zero;

                messageWindowConfiguration.Message = _languageManager.GetText(message);
                messageWindowConfiguration.AddListenerOnConfirm(onConfirm);
        }

        protected void DestroyWindow()
        {
            DestroyObject(_confirmWindow);
            DestroyObject(_messageWindow);
        }

        private void DestroyObject(GameObject gameObject)
        {
            if (gameObject is not null)
            {
                Destroy(gameObject);
                gameObject = null;
            }
        }
    }
}
