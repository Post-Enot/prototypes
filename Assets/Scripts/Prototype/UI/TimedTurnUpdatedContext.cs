using IUP.Toolkits.BattleSystem;

namespace IUP.BattleSystemPrototype
{
    public sealed class TimedTurnUpdatedContext : BattleEventContext
    {
        public TimedTurnUpdatedContext(float turnTimeLeft)
        {
            TurnTimeLeft = turnTimeLeft;
        }

        public float TurnTimeLeft { get; }
    }
}
