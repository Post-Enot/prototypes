using IUP.Toolkits.CellarMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleArenaGenerator : IBattleArenaGenerator
    {
        public BattleArenaGenerator(IEntitySpawner entitySpawner)
        {
            EntitySpawner = entitySpawner;
        }

        public IEntitySpawner EntitySpawner { get; }

        public IBattleArenaPresenter Generate(
            IReadOnlyCellarMap arenaPattern,
            IBattleEventBus eventBus,
            Transform arenaRoot,
            Tilemap tilemap)
        {
            var battleArena = new BattleArena(arenaPattern.Width, arenaPattern.Height, eventBus);
            var battleArenaPresenter = new BattleArenaPresenter(battleArena, 1, arenaRoot, tilemap);
            for (int layerIndex = 0; layerIndex < arenaPattern.LayersCount; layerIndex += 1)
            {
                for (int y = 0; y < arenaPattern.Height; y += 1)
                {
                    for (int x = 0; x < arenaPattern.Width; x += 1)
                    {
                        string mappingKey = arenaPattern[layerIndex][x, y].MappingKey;
                        // Пустые клетки расставляются только по первому (0) слою.
                        if (mappingKey == null && layerIndex != 0)
                        {
                            continue;
                        }
                        _ = EntitySpawner.SpawnEntityByMappingKey(
                            mappingKey, 
                            battleArenaPresenter,
                            eventBus,
                            x,
                            y);
                    }
                }
            }
            return battleArenaPresenter;
        }
    }
}
