using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleArenaPresenter : IBattleArenaPresenter
    {
        public BattleArenaPresenter(
            IBattleArena battleArena,
            int cellSizeInUnit,
            Transform entitiesRoot,
            Tilemap tilemap)
        {
            BattleArena = battleArena;
            CellSizeInUnit = cellSizeInUnit;
            EntitiesRoot = entitiesRoot;
            Tilemap = tilemap;
            tilemap.layoutGrid.cellSize = new Vector3(cellSizeInUnit, cellSizeInUnit);
            const float anchorMiddleShift = 0.5f;
            var tileAnchor = new Vector3()
            {
                x = BattleArena.Width % 2 == 0 ? anchorMiddleShift : 0,
                y = BattleArena.Height % 2 == 0 ? anchorMiddleShift : 0
            };
            tilemap.tileAnchor = tileAnchor;
        }

        public int Width => BattleArena.Width;
        public int Height => BattleArena.Height;
        public float CellSizeInUnit { get; }
        public Transform EntitiesRoot { get; }
        public Tilemap Tilemap { get; }
        public IBattleArena BattleArena { get; }

        public void SetEntityOnCell(ICellEntityPresenter entityPresenter, Vector2Int coordinate)
        {
            SetEntityOnCell(entityPresenter, coordinate.x, coordinate.y);
        }

        public void SetEntityOnCell(ICellEntityPresenter entityPresenter, int x, int y)
        {
            entityPresenter.Transform.parent = EntitiesRoot;
            entityPresenter.Transform.position = GetCellWorldPosition(x, y);
            if (!BattleArena[x, y].PutEntity(entityPresenter.Entity))
            {
                throw new System.InvalidOperationException("Сущность не может быть помещена в клетку по " +
                    $"координате ({x}, {y}), т.к. конфликтует с уже помещённой ранее в клетку сущностью.");
            }
        }

        public Vector3 GetCellLocalPosition(Vector2Int coordinate)
        {
            return GetCellLocalPosition(coordinate.x, coordinate.y);
        }

        public Vector3 GetCellLocalPosition(int x, int y)
        {
            return new Vector3(
                x: -(Width / 2f) + (CellSizeInUnit / 2f) + (x * CellSizeInUnit),
                y: -(Height / 2f) + (CellSizeInUnit / 2f) + (y * CellSizeInUnit));
        }

        public Vector3 GetCellWorldPosition(Vector2Int coordinate)
        {
            return GetCellWorldPosition(coordinate.x, coordinate.y);
        }

        public Vector3 GetCellWorldPosition(int x, int y)
        {
            Vector3 localPosition = GetCellLocalPosition(x, y);
            return EntitiesRoot.TransformPoint(localPosition);
        }

        public Vector3Int CellCoordinateToTileCoordinate(Vector2Int coordinate)
        {
            return CellCoordinateToTileCoordinate(coordinate.x, coordinate.y);
        }

        public Vector3Int CellCoordinateToTileCoordinate(int x, int y)
        {
            return new Vector3Int(
                x: -(Width / 2) + x,
                y: -(Height / 2) + y, // Прибавление единицы необходимо для правильного сопоставления клеток.
                z: 0);
        }
    }
}
