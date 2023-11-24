using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Dialogue.Elements
{
    public class ChildsCache
    {
        private readonly int MaxCacheSize = 10;
        private Dictionary<string, (DialogueUIElement child, float time)> _cache = new Dictionary<string, (DialogueUIElement child, float time)>();


        public void AddId(string descendentId, DialogueUIElement child)
        {
            RemoveId(descendentId);
            _cache.Add(descendentId, (child, Time.realtimeSinceStartup));
            int count = _cache.Count;
            if (count > MaxCacheSize)
            {
                float minTime = _cache.Min(idChildTime => idChildTime.Value.time);
                _cache.Remove(_cache.First(idChildTime => idChildTime.Value.time == minTime).Key);
            }
        }

        public bool TryGetChild(string descendentId, out DialogueUIElement child)
        {
            if (_cache.ContainsKey(descendentId))
            {
                child = _cache[descendentId].child;
                AddId(descendentId, child);
                return true;
            }

            child = default;
            return false;
        }

        public void RemoveId(string descendentId)
        {
            if (_cache.ContainsKey(descendentId))
            {
                _cache.Remove(descendentId);
            }
        }
    }
}
