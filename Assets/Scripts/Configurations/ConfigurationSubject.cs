using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Configurations
{
    public abstract class ConfigurationSubject : MonoBehaviour
    {
        private UnityEvent _onConfigurationEvent = new UnityEvent();


        public void AddListener(UnityAction onConfigurationAction)
        {
            _onConfigurationEvent.AddListener(onConfigurationAction);
        }

        public void RemoveListener(UnityAction onConfigurationAction)
        {
            _onConfigurationEvent.RemoveListener(onConfigurationAction);
        }

        protected void OnConfigurationEvent()
        {
            _onConfigurationEvent.Invoke();
        }
    }
}
