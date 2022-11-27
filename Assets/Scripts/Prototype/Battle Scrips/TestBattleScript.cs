using IUP.Toolkits.BattleSystem;

namespace IUP.BattleSystemPrototype
{
    public sealed class TestBattleScript : IBattleScript
    {
        public bool IsPerformed { get; private set; }

        private IBattleLoop _battleLoop;
        private IBattleEventBus _eventBus;
        private IBattleArenaPresenter _arenaPresenter;
        private IEntitySpawner _entitySpawner;

        public void Init(
            IBattleLoop battleLoop,
            IBattleEventBus eventBus,
            IBattleArenaPresenter arenaPresenter,
            IEntitySpawner entitySpawner)
        {
            _battleLoop = battleLoop;
            _eventBus = eventBus;
            _arenaPresenter = arenaPresenter;
            _entitySpawner = entitySpawner;
        }

        public void Start()
        {
            IsPerformed = true;
            _battleLoop.StartIteration();
        }
    }
}
