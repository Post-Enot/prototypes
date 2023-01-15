using IUP.Toolkits.BattleSystem;

namespace IUP.BattleSystemPrototype
{
    public sealed class TestBattleScript : IBattleScript
    {
        public bool IsPerformed { get; private set; }

        private IBattleLoop _battleLoop;

        public void Init(
            IBattleLoop battleLoop,
            IBattleEventBus eventBus,
            IBattleArenaPresenter arenaPresenter,
            IEntitySpawner entitySpawner)
        {
            _battleLoop = battleLoop;
        }

        public void Start()
        {
            IsPerformed = true;
            _battleLoop.StartIteration();
        }
    }
}
