using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public class Beyond : ICellEntity, ICanPutEntityEventReceiver
    {
        public Beyond(int x, int y, IBattleArena battleArena, ICell cell)
        {
            Coordinate = new Vector2Int(x, y);
            BattleArena = battleArena;
            Cell = cell;
        }

        public Beyond(Vector2Int coordinate, IBattleArena battleArena, ICell cell)
        {
            Coordinate = coordinate;
            BattleArena = battleArena;
            Cell = cell;
        }

        public Vector2Int Coordinate { get; }
        public ICell Cell { get; }
        public IBattleArena BattleArena { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return false;
        }
    }
}
