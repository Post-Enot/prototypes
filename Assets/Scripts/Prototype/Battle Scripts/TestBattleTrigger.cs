using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.CellarMaps;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class TestBattleTrigger : MonoBehaviour
    {
        [SerializeField] private CellarMapAsset _arenaPattern;
        [SerializeField] private Battle _battle;

        private void Start()
        {
            var battleScript = new TestBattleScript();
            var battleContext = new BattleContext(_arenaPattern, battleScript);
            _battle.Init(battleContext);
            _battle.StartBattleScript();
        }
    }
}
