using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class AbyssPresenter : CellEntityPresenter
    {
        public override ICellEntity Entity => _abyss;

        private Abyss _abyss;

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate)
        {
            _abyss = new Abyss(coordinate, battleArenaPresenter.BattleArena);
        }

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            Init(battleArenaPresenter, eventBus, new Vector2Int(x, y));
        }
    }
}
