using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class Abyss : ICellEntity, ICanPutEntityEventReceiver
    {
        public Abyss(int x, int y)
        {
            Coordinate = new Vector2Int(x, y);
            _tags.AddTag(EntitiesTags.Surface);
        }

        public Abyss(Vector2Int coordinate)
        {
            Coordinate = coordinate;
            _tags.AddTag(EntitiesTags.Surface);
        }

        public Vector2Int Coordinate { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();

        public bool OnCanPutEntity(ICellEntity entity)
        {
            return entity.Tags.HasTag(EntitiesTags.Flying);
        }
    }
}
