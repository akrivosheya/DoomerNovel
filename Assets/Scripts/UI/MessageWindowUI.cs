using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MessageWindowUI : MonoBehaviour
    {
        public string Message { set => _message.text = value; }

        private UnityEvent _onConfirm = new UnityEvent();

        private Text _message;


        public void AddListenerOnConfirm(UnityAction onConfirmAction)
        {
            _onConfirm.AddListener(onConfirmAction);
        }

        public void OnConfirm()
        {
            _onConfirm.Invoke();
        }
    }
}
