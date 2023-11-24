using System.Collections.Generic;

namespace UI.Dialogue.Events
{
    public class Event
    {
        public EventTypes EventType { get; private set; }

        private Dictionary<string, string> _stringValues;
        private Dictionary<string, float> _floatValues;
        private Dictionary<string, bool> _boolValues;

        public enum EventTypes
        {
            Continue,
            Activated,
            Unctivated,
        }


        public Event(EventTypes eventType)
        {
            EventType = eventType;
        }

        public void SetStringValue(string key, string value)
        {
            _stringValues ??= new Dictionary<string, string>();
            _stringValues.Add(key, value);
        }

        public void SetFloatValue(string key, float value)
        {
            _floatValues ??= new Dictionary<string, float>();
            _floatValues.Add(key, value);
        }

        public void SetBoolValue(string key, bool value)
        {
            _boolValues ??= new Dictionary<string, bool>();
            _boolValues.Add(key, value);
        }

        public string GetStringValue(string key)
        {
            if (_stringValues is null || !_stringValues.ContainsKey(key))
            {
                return default;
            }

            return _stringValues[key];
        }

        public float GetFloatValue(string key)
        {
            if (_floatValues is null || !_floatValues.ContainsKey(key))
            {
                return default;
            }

            return _floatValues[key];
        }

        public bool GetBoolValue(string key)
        {
            if (_boolValues is null || !_boolValues.ContainsKey(key))
            {
                return default;
            }

            return _boolValues[key];
        }
    }
}
