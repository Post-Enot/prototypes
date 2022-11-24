using IUP.Toolkits.CellarMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkits.BattleSystem
{
    public interface IBattleArenaGenerator
    {
        /// <summary>
        /// Ссылка на спавнер сущностей, используемый при генерации боевой арены.
        /// </summary>
        public IEntitySpawner EntitySpawner { get; }

        /// <summary>
        /// Генерирует боевую арену на основе паттерна.
        /// </summary>
        /// <param name="arenaPattern">Паттерн боевой арены.</param>
        /// <param name="arenaRoot">Корневой Transform иерархии объектов боевой арены.</param>
        /// <returns></returns>
        public IBattleArenaPresenter Generate(CellarMapAsset arenaPattern, Transform arenaRoot, Tilemap tilemap);
    }
}
