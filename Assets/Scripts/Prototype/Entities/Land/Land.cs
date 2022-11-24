using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public class Land : ICellEntity
    {
        public Land(int x, int y)
        {
            Coordinate = new Vector2Int(x, y);
        }

        public Land(Vector2Int coordinate)
        {
            Coordinate = coordinate;
        }

        public Vector2Int Coordinate { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();
    }
}
