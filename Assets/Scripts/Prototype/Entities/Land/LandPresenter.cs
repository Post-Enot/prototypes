using IUP.Toolkits.BattleSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.BattleSystemPrototype
{
    public sealed class LandPresenter : CellEntityPresenter
    {
        [SerializeField] private Tile _landTile;
        [SerializeField] private Tile _borderTile;

        public override ICellEntity Entity => _land;

        private Land _land;

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate)
        {
            Init(battleArenaPresenter, eventBus, coordinate.x, coordinate.y);
        }

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            _land = new Land(x, y, battleArenaPresenter.BattleArena);
            Vector3Int tileCoordinate = battleArenaPresenter.CellCoordinateToTileCoordinate(x, y);
            tileCoordinate.z = 1;
            battleArenaPresenter.Tilemap.SetTile(tileCoordinate, _landTile);
            tileCoordinate.y -= 1;
            tileCoordinate.z = 0;
            battleArenaPresenter.Tilemap.SetTile(tileCoordinate, _borderTile);
        }
    }
}
