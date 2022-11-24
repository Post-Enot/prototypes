using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class AbyssPresenter : MonoBehaviour, ICellEntityPresenter
    {
        public ICellEntity Entity { get; private set; }
        public IBattleArenaPresenter BattleArenaPresenter { get; private set; }
        public Transform Transform => transform;
        public Vector2Int Coordinate => Entity.Coordinate;

        public void Init(IBattleArenaPresenter battleArenaPresenter, Vector2Int coordinate)
        {
            BattleArenaPresenter = battleArenaPresenter;
            Entity = new Abyss(coordinate);
        }

        public void Init(IBattleArenaPresenter battleArenaPresenter, int x, int y)
        {
            Init(battleArenaPresenter, new Vector2Int(x, y));
        }
    }
}
