using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace UI
{
    public class ConfirmWindowUI : MessageWindowUI
    {   
        private UnityEvent _onDeny = new UnityEvent();


        public void AddListenerOnDeny(UnityAction onDenyAction)
        {
            _onDeny.RemoveAllListeners();
            _onDeny.AddListener(onDenyAction);
        }

        public void OnDeny()
        {
            _onDeny.Invoke();
        }
    }
}
