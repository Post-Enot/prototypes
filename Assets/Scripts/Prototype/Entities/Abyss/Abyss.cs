using IUP.Toolkits.BattleSystem;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class Abyss : ICellEntity, ICanPutEntityEventReceiver
    {
        public Abyss(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
            BattleArena = battleArena;
            _tags.AddTag(EntitiesTags.Surface);
        }

        public Abyss(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
            BattleArena = battleArena;
            _tags.AddTag(EntitiesTags.Surface);
        }

        public Vector2Int Coordinate { get; }
        public ICell Cell => BattleArena[Coordinate];
        public IBattleArena BattleArena { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();

        public event Action<ICellEntity> Destroyed;

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return entity.Tags.HasTag(EntitiesTags.Flying);
        }

        private void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
