using System;

namespace IUP.Toolkits.BattleSystem
{
    public interface IBattleEventBus
    {
        public void RegisterEventCallback<T>(Action<T> callback) where T : BattleEventContext;

        public bool UnregisterEventCallback<T>(Action<T> callback) where T : BattleEventContext;
    }
}
