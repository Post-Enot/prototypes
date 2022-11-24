using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public interface ICellEntityPresenter
    {
        public ICellEntity Entity { get; }
        public IBattleArenaPresenter BattleArenaPresenter { get; }
        public Transform Transform { get; }
        public Vector2Int Coordinate { get; }

        public void Init(IBattleArenaPresenter battleArenaPresenter, Vector2Int coordinate);

        public void Init(IBattleArenaPresenter battleArenaPresenter, int x, int y);
    }
}
