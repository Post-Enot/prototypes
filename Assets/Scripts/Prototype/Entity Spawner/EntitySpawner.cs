using IUP.Toolkits.BattleSystem;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class EntitySpawner : AbstractEntitySpawner
    {
        [SerializeField] private GameObject _landEntityPrefab;
        [SerializeField] private GameObject _mainHeroEntityPrefab;
        [SerializeField] private GameObject _abyssEntityPrefab;

        public override ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate)
        {
            return SpawnEntityByMappingKey(mappingKey, battleArenaPresenter, eventBus, coordinate.x, coordinate.y);
        }

        public override ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            ICellEntityPresenter SpawnEntity(GameObject entityPrefab)
            {
                return this.SpawnEntity(entityPrefab, battleArenaPresenter, eventBus, x, y);
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
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            GameObject entityObject = Instantiate(entityPrefab);
            var entityPresenter = entityObject.GetComponent<ICellEntityPresenter>();
            entityPresenter.Init(battleArenaPresenter, eventBus, x, y);
            battleArenaPresenter.SetEntityOnCell(entityPresenter, x, y);
            if (entityPresenter is ITurnQueueMember member)
            {
                SpawnTurnQueueMemberContext context = new(member);
                eventBus.InvokeEventCallbacks(context);
            }
            return entityPresenter;
        }
    }
}
