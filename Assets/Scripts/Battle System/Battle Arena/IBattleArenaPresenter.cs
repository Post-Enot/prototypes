using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkits.BattleSystem
{
    public interface IBattleArenaPresenter
    {
        /// <summary>
        /// Размер стороны клетки в юнитах (ед. измерения Unity).
        /// </summary>
        public float CellSizeInUnit { get; }
        /// <summary>
        /// Ширина боевой арены в клетках.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота боевой арены в клетках.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Корневой Transform иерархии сущностей боевой арены.
        /// </summary>
        public Transform EntitiesRoot { get; }
        public Tilemap Tilemap { get; }
        /// <summary>
        /// Ссылка на модель боевой арены.
        /// </summary>
        public IBattleArena BattleArena { get; }

        /// <summary>
        /// Устанавливает сущность на клетку по переданным координатам.
        /// </summary>
        /// <param name="entityPresenter">Представление сущности.</param>
        /// <param name="coordinate">Координаты клетки.</param>
        public void SetEntityOnCell(ICellEntityPresenter entityPresenter, Vector2Int coordinate);

        /// <summary>
        /// Устанавливает сущность на клетку по переданным координатам.
        /// </summary>
        /// <param name="entityPresenter">Представление сущности.</param>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        public void SetEntityOnCell(ICellEntityPresenter entityPresenter, int x, int y);

        /// <summary>
        /// Возвращает локальную позицию клетки относительно Root.
        /// </summary>
        /// <param name="coordinate">Координата клетки.</param>
        /// <returns>Локальная позиция клетки относительно Root.</returns>
        public Vector3 GetCellLocalPosition(Vector2Int coordinate);

        /// <summary>
        /// Возвращает мировую позицию клетки.
        /// </summary>
        /// <param name="coordinate">Координата клетки.</param>
        /// <returns>Мировая позиция клетки.</returns>
        public Vector3 GetCellWorldPosition(Vector2Int coordinate);

        public Vector3Int CellCoordinateToTileCoordinate(Vector2Int coordinate);

        public Vector3Int CellCoordinateToTileCoordinate(int x, int y);
    }
}
