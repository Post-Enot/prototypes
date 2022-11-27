using IUP.Toolkits.BattleSystem;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public class Land : ICellEntity
    {
        public Land(int x, int y, IBattleArena battleArena)
        {
            Coordinate = new Vector2Int(x, y);
        }

        public Land(Vector2Int coordinate, IBattleArena battleArena)
        {
            Coordinate = coordinate;
        }

        public Vector2Int Coordinate { get; }
        public ICell Cell => throw new NotImplementedException();
        public IBattleArena BattleArena => throw new NotImplementedException();
        public IReadonlyTagSet Tags => _tags;

        public event Action<ICellEntity> Destroyed;

        private readonly TagSet _tags = new();

        private void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
