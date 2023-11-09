using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ConfirmWindowUI : MessageWindowUI
    {   
        private UnityEvent _onDeny = new UnityEvent();


        public void AddListenerOnDeny(UnityAction onDenyAction)
        {
            _onDeny.AddListener(onDenyAction);
        }

        public void OnDeny()
        {
            _onDeny.Invoke();
        }
    }
}
