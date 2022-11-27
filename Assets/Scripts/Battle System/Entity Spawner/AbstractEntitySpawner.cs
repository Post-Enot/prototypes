using System;
using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    /// <summary>
    /// Абстрактный класс-основа для спавнера сущностей.
    /// </summary>
    public abstract class AbstractEntitySpawner : MonoBehaviour, IEntitySpawner
    {
        public abstract ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate);

        public abstract ICellEntityPresenter SpawnEntityByMappingKey(
            string mappingKey,
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y);
    }
}
