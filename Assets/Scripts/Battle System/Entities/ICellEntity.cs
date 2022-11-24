using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public interface ICellEntity
    {
        public Vector2Int Coordinate { get; }
        public IReadonlyTagSet Tags { get; }
    }
}
