using IUP.BattleSystemPrototype;
using IUP.Toolkits.CellarMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class Battle : MonoBehaviour, IBattle
    {
        [Header("Temporary:")]
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private float _minTurnDurationInSecond;

        public IBattleScript BattleScrpt { get; private set; }

        private IBattleArenaGenerator _arenaGenerator;
        private IBattleLoop _battleLoop;
        private IEntitySpawner _entitySpawner;
        private IBattleArenaPresenter _arenaPresenter;
        private BattleEventBus _eventBus;
        private Transform _entitiesRoot;

        public void Init(IBattleContext battleContext)
        {
            _eventBus = new BattleEventBus();
            _entitySpawner = GetComponent<IEntitySpawner>();
            _arenaGenerator = new BattleArenaGenerator(_entitySpawner);
            _entitiesRoot = InstantiateEntitiesRoot();
            _battleLoop = new BattleLoop(_eventBus, this, _minTurnDurationInSecond);
            _arenaPresenter = _arenaGenerator.Generate(
                battleContext.ArenaPattern,
                _eventBus,
                _entitiesRoot,
                _tilemap);
            BattleScrpt = battleContext.BattleScript;
            BattleScrpt.Init(_battleLoop, _eventBus, _arenaPresenter, _entitySpawner);
        }

        private Transform InstantiateEntitiesRoot()
        {
            GameObject entitiesRootObject = new("Entities Root");
            entitiesRootObject.transform.parent = transform;
            return entitiesRootObject.transform;
        }
    }
}
