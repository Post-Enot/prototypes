using IUP.Toolkits.BattleSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.BattleSystemPrototype
{
    public sealed class LandPresenter : MonoBehaviour, ICellEntityPresenter
    {
        [SerializeField] private Tile _landTile;
        [SerializeField] private RuleTile _borderTile;

        public ICellEntity Entity { get; private set; }
        public IBattleArenaPresenter BattleArenaPresenter { get; private set; }
        public Transform Transform => transform;
        public Vector2Int Coordinate => Entity.Coordinate;

        public void Init(IBattleArenaPresenter battleArenaPresenter, Vector2Int coordinate)
        {
            Init(battleArenaPresenter, coordinate.x, coordinate.y);
        }

        public void Init(IBattleArenaPresenter battleArenaPresenter, int x, int y)
        {
            Entity = new Land(x, y);
            BattleArenaPresenter = battleArenaPresenter;
            Vector3Int tileCoordinate = battleArenaPresenter.CellCoordinateToTileCoordinate(x, y);
            tileCoordinate.z = 1;
            battleArenaPresenter.Tilemap.SetTile(tileCoordinate, _landTile);
            tileCoordinate.y -= 1;
            tileCoordinate.z = 0;
            battleArenaPresenter.Tilemap.SetTile(tileCoordinate, _borderTile);
        }
    }
}
