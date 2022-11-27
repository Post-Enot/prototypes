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
            CellarMapAsset arenaPattern,
            IBattleEventBus eventBus,
            Transform arenaRoot,
            Tilemap tilemap)
        {
            var battleArena = new BattleArena(arenaPattern.Map.Width, arenaPattern.Map.Height, eventBus);
            var battleArenaPresenter = new BattleArenaPresenter(battleArena, 1, arenaRoot, tilemap);
            for (int layerIndex = 0; layerIndex < arenaPattern.ViewLayers.LayersCount; layerIndex += 1)
            {
                for (int y = 0; y < arenaPattern.Map.Height; y += 1)
                {
                    for (int x = 0; x < arenaPattern.Map.Width; x += 1)
                    {
                        string mappingKey = arenaPattern.Map[layerIndex][x, y]?.TypeName;
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
