using IUP.Toolkits.BattleSystem;
using System.Collections;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class BatPresenter : CellEntityPresenter, ITurnQueueMember
    {
        public override ICellEntity Entity => _bat;

        public int TurnPriority => 0;

        private Bat _bat;

        public override void Init(IBattleArenaPresenter battleArenaPresenter, IBattleEventBus eventBus, int x, int y)
        {
            Init(battleArenaPresenter, eventBus, new Vector2Int(x, y));
        }

        public override void Init(IBattleArenaPresenter battleArenaPresenter, IBattleEventBus eventBus, Vector2Int coordinate)
        {
            _bat = new Bat(coordinate, battleArenaPresenter.BattleArena);
            eventBus.RegisterEventCallback(GeneralBattleEvents.BattleLoopIterationStarted, _bat.AnalyzeBattleArena);
        }

        public IEnumerator MakeTurn()
        {
            yield return null;
        }
    }
}
