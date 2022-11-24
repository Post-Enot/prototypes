using System;
using System.Collections.Generic;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleEventBus : IBattleEventBus
    {
        private readonly Dictionary<Type, List<Action<BattleEventContext>>> _events = new();

        public void RegisterEventCallback<T>(Action<T> callback) where T : BattleEventContext
        {
            Type callbackType = typeof(T);
            if (!_events.ContainsKey(callbackType))
            {
                _events.Add(callbackType, new List<Action<BattleEventContext>>());
            }
            _events[callbackType].Add((Action<BattleEventContext>)callback);
        }

        public bool UnregisterEventCallback<T>(Action<T> callback) where T : BattleEventContext
        {
            Type callbackType = typeof(T);
            if (_events.ContainsKey(callbackType))
            {
                return _events[callbackType].Remove((Action<BattleEventContext>)callback);
            }
            return false;
        }
    }
}
