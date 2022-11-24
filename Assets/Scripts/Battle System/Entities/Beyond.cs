using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public class Beyond : ICellEntity, ICanPutEntityEventReceiver
    {
        public Beyond(int x, int y)
        {
            Coordinate = new Vector2Int(x, y);
        }

        public Beyond(Vector2Int coordinate)
        {
            Coordinate = coordinate;
        }

        public Vector2Int Coordinate { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return false;
        }
    }
}
