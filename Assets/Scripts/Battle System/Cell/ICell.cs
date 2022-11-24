using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public interface ICell
    {
        public IReadOnlyCollection<ICellEntity> Entities { get; }
        public IBattleArena BattleArena { get; }
        public Vector2Int Coordinate { get; }

        public bool CanPutEntity(ICellEntity entity);

        public bool PutEntity(ICellEntity entity);

        public void PutEntityWithoutChecking(ICellEntity entity);

        public bool RemoveEntity(ICellEntity entity);
    }
}
