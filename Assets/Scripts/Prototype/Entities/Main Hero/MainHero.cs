using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public class MainHero : ICellEntity, ICanPutEntityEventReceiver
    {
        public MainHero(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
            _battleArena = battleArena;
            _tags.AddTag(EntitiesTags.TakesUpSurface);
        }

        public MainHero(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
            _battleArena = battleArena;
            _tags.AddTag(EntitiesTags.TakesUpSurface);
        }

        public Vector2Int Coordinate { get; private set; }
        public IReadonlyTagSet Tags => _tags;

        private readonly IBattleArena _battleArena;
        private readonly TagSet _tags = new();

        public bool CanMoveOn(Direction direction)
        {
            var newCoordinate = Coordinate + direction.ToVector2Int();
            return _battleArena[newCoordinate].CanPutEntity(this);
        }

        public bool MoveOn(Direction direction)
        {
            var newCoordinate = Coordinate + direction.ToVector2Int();
            var cell = _battleArena[newCoordinate];
            if (cell.CanPutEntity(this))
            {
                _battleArena[Coordinate].RemoveEntity(this);
                cell.PutEntity(this);
                Coordinate = newCoordinate;
                return true;
            }
            return false;
        }

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return !entity.Tags.HasTag(EntitiesTags.TakesUpSurface);
        }
    }
}
