using IUP.Toolkits.BattleSystem;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    [Serializable]
    public sealed class EntitySpawner : IEntitySpawner
    {
        [SerializeField] private GameObject _landEntityPrefab;
        [SerializeField] private GameObject _mainHeroEntityPrefab;
        [SerializeField] private GameObject _abyssEntityPrefab;

        public event Action<ICellEntityPresenter> EntitySpawned;

        public ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            Vector2Int coordinate)
        {
            return SpawnEntityByMappingKey(mappingKey, battleArenaPresenter, coordinate.x, coordinate.y);
        }

        public ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            int x,
            int y)
        {
            ICellEntityPresenter SpawnEntity(GameObject entityPrefab)
            {
                return this.SpawnEntity(entityPrefab, battleArenaPresenter, x, y);
            }

            return mappingKey switch
            {
                EntityMappingKeys.Land => SpawnEntity(_landEntityPrefab),
                EntityMappingKeys.MainHero => SpawnEntity(_mainHeroEntityPrefab),
                null => SpawnEntity(_abyssEntityPrefab),
                _ => throw new ArgumentException($"Ключ сопоставления {mappingKey}, переданный аргументом " +
                    $"{nameof(mappingKey)} не найден."),
            };
        }

        private ICellEntityPresenter SpawnEntity(
            GameObject entityPrefab,
            IBattleArenaPresenter battleArenaPresenter,
            int x,
            int y)
        {
            GameObject entityObject = UnityEngine.Object.Instantiate(entityPrefab);
            var entityPresenter = entityObject.GetComponent<ICellEntityPresenter>();
            entityPresenter.Init(battleArenaPresenter, x, y);
            battleArenaPresenter.SetEntityOnCell(entityPresenter, x, y);
            EntitySpawned?.Invoke(entityPresenter);
            return entityPresenter;
        }
    }
}
