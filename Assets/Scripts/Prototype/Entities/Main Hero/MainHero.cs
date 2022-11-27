using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public class MainHero : ICellEntity, ICanPutEntityEventReceiver
    {
        public MainHero(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.TakesUpSurface, EntitiesTags.MainHero);
        }

        public MainHero(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.TakesUpSurface, EntitiesTags.MainHero);
        }

        public Vector2Int Coordinate { get; private set; }
        public ICell Cell => BattleArena[Coordinate];
        public IBattleArena BattleArena { get; }
        public IReadonlyTagSet Tags => _tags;

        public event Action<ICellEntity> Destroyed;

        private readonly TagSet _tags = new();

        public bool CanMoveOn(Direction direction)
        {
            var newCoordinate = Coordinate + direction.ToVector2Int();
            return BattleArena[newCoordinate].CanPutEntity(this);
        }

        public void MoveOn(Direction direction)
        {
            var newCoordinate = Coordinate + direction.ToVector2Int();
            Cell.RemoveEntity(this);
            BattleArena[newCoordinate].PutEntity(this);
            Coordinate = newCoordinate;
        }

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return !entity.Tags.HasTag(EntitiesTags.TakesUpSurface);
        }

        private void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
