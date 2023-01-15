using IUP.Toolkits.BattleSystem;

namespace IUP.BattleSystemPrototype
{
    public sealed class TimedTurnStartedContext : BattleEventContext
    {
        public TimedTurnStartedContext(float turnDuration)
        {
            TurnDuration = turnDuration;
        }

        public float TurnDuration { get; }
    }
}
