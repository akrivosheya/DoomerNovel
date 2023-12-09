using System.Collections.Generic;

namespace UI.Dialogue.Events
{
    public class Event
    {
        public EventTypes EventType { get; private set; }

        private Dictionary<string, string> _stringValues;
        private Dictionary<string, float> _floatValues;
        private Dictionary<string, int> _intValues;
        private Dictionary<string, bool> _boolValues;

        public enum EventTypes
        {
            Continue,
            Activated,
            Unactivated,
            Interacted,       
            BeginChoice,
            EndChoice,
            SpeedUp,
            SpeedDown,
            Exit,
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

        public void SetIntValue(string key, int value)
        {
            _intValues ??= new Dictionary<string, int>();
            _intValues.Add(key, value);
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

        public int GetIntValue(string key)
        {
            if (_intValues is null || !_intValues.ContainsKey(key))
            {
                return default;
            }

            return _intValues[key];
        }

        public bool GetBoolValue(string key)
        {
            if (_boolValues is null || !_boolValues.ContainsKey(key))
            {
                return default;
            }

            return _boolValues[key];
        }

        public bool HasStringValue(string key)
        {
            return _stringValues is not null && _stringValues.ContainsKey(key);
        }

        public bool HasFloatValue(string key)
        {
            return _floatValues is not null && _floatValues.ContainsKey(key);
        }

        public bool HasIntValue(string key)
        {
            return _intValues is not null && _intValues.ContainsKey(key);
        }

        public bool HasBoolValue(string key)
        {
            return _boolValues is not null && _boolValues.ContainsKey(key);
        }
    }
}
