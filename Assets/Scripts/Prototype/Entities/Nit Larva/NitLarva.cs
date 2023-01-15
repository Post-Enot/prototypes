using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class NitLarva : ICellEntity, ICanPutEntityEventReceiver
    {
        public NitLarva(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.TakesUpSurface);
        }

        public NitLarva(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.TakesUpSurface);
        }

        public Vector2Int Coordinate { get; private set; }
        public ICell Cell => BattleArena[Coordinate];
        public IBattleArena BattleArena { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return !entity.Tags.HasTag(EntitiesTags.TakesUpSurface);
        }

        public bool CanMoveOn(Direction direction)
        {
            Vector2Int newCoordinate = Coordinate + direction.ToVector2Int();
            return BattleArena[newCoordinate].CanPutEntity(this);
        }

        public void MoveOn(Direction direction)
        {
            Vector2Int newCoordinate = Coordinate + direction.ToVector2Int();
            Cell.RemoveEntity(this);
            BattleArena[newCoordinate].PutEntity(this);
            Coordinate = newCoordinate;
        }
    }
}
