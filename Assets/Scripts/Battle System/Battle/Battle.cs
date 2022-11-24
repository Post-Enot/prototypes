using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.CellarMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.BattleSystemPrototype
{
    public sealed class Battle : MonoBehaviour, IBattle
    {
        [SerializeField] private EntitySpawner _entitySpawner;

        [Header("Temporary:")]
        [SerializeField] private CellarMapAsset _arenaPattern;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private float _minTurnDurationInSecond;

        public BattleEventBus BattleEventBus { get; private set; }

        private IBattleArenaGenerator _arenaGenerator;
        private IBattleLoop _battleLoop;

        private void Awake()
        {
            _arenaGenerator = new BattleArenaGenerator(_entitySpawner);
            BattleEventBus = new BattleEventBus();
            Init(new BattleContext(_arenaPattern, null));
        }

        public void Init(IBattleContext battleContext)
        {
            var rootObject = new GameObject();
            _battleLoop = new BattleLoop(_entitySpawner, this, _minTurnDurationInSecond);
            _arenaGenerator.Generate(battleContext.ArenaPattern, rootObject.transform, _tilemap);
            _battleLoop.StartIteration();
        }
    }
}
