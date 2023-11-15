using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MessageWindowUI : MonoBehaviour
    {
        public string Message { set => _message.text = value; }

        [SerializeField] private Text _message;

        private UnityEvent _onConfirm = new UnityEvent();


        public void AddListenerOnConfirm(UnityAction onConfirmAction)
        {
            _onConfirm.RemoveAllListeners();
            _onConfirm.AddListener(onConfirmAction);
        }

        public void OnConfirm()
        {
            _onConfirm.Invoke();
        }
    }
}
