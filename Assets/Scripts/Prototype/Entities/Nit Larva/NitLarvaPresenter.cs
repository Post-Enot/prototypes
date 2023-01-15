using System.Collections;
using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class NitLarvaPresenter : CellEntityPresenter, ITurnQueueMember
    {
        public override ICellEntity Entity => _nitLarva;
        public IBattleArenaPresenter BattleArenaPresenter { get; private set; }

        public int TurnPriority => 10;

        private NitLarva _nitLarva;
        private NitLarvaAI _nitLarvaAI;

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            Init(battleArenaPresenter, eventBus, new Vector2Int(x, y));
        }

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate)
        {
            BattleArenaPresenter = battleArenaPresenter;
            _nitLarva = new NitLarva(coordinate, battleArenaPresenter.BattleArena);
            _nitLarvaAI = new NitLarvaAI(_nitLarva);
            eventBus.RegisterEventCallback(
                GeneralBattleEvents.BattleLoopIterationStarted,
                _nitLarvaAI.AnalyzeBattleArena);
        }

        public IEnumerator MakeTurn()
        {
            _nitLarvaAI.MakeOption();
            transform.position = BattleArenaPresenter.GetCellWorldPosition(_nitLarva.Coordinate);
            yield return null;
        }
    }
}
