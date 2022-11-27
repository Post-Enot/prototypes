using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class Bat : ICellEntity
    {
        public Bat(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.Flying, EntitiesTags.TakesUpSurface);
        }

        public Bat(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
            BattleArena = battleArena;
            _tags.AddTags(EntitiesTags.Flying, EntitiesTags.TakesUpSurface);
        }

        public Vector2Int Coordinate { get; private set; }
        public ICell Cell => BattleArena[Coordinate];
        public IBattleArena BattleArena { get; }
        public IReadonlyTagSet Tags => _tags;

        private readonly TagSet _tags = new();
        private MainHero _mainHero;

        public void AnalyzeBattleArena()
        {
            foreach (ICellEntity entity in BattleArena.Entities)
            {
                if (entity.Tags.HasTag(EntitiesTags.MainHero))
                {
                    _mainHero = entity as MainHero;
                }
            }
        }
    }
}
