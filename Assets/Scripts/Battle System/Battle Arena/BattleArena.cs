using IUP.Toolkits.Matrices;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleArena : IBattleArena
    {
        public BattleArena(int width, int height, IBattleEventBus eventBus)
        {
            _arena = new Matrix<ICell>(width, height);
            _arena.InitAllElements((int x, int y) => new Cell(this, x, y));
            _eventBus = eventBus;
            _eventBus.RegisterEventCallback(
                GeneralBattleEvents.BattleLoopIterationStarted,
                InitEntitiesSet);
            _eventBus.RegisterEventCallback<EntityDestroyedContext>(RemoveEntityFromSet);
        }

        public int Width => _arena.Width;
        public int Height => _arena.Height;
        public IReadOnlyCollection<ICellEntity> Entities => _entities;

        private readonly Matrix<ICell> _arena;
        private readonly HashSet<ICellEntity> _entities = new();
        private readonly IBattleEventBus _eventBus;

        public ICell this[Vector2Int coordinate] => this[coordinate.x, coordinate.y];

        public ICell this[int x, int y]
        {
            get
            {
                if (_arena.IsCoordinateInDefinitionDomain(x, y))
                {
                    return _arena[x, y];
                }
                else
                {
                    return InstantiateBeyondCell(x, y);
                }
            }
        }

        public void InitEntitiesSet()
        {
            void AddEntitiesFromCellOnSet(ref ICell cell)
            {
                foreach (ICellEntity entity in cell.Entities)
                {
                    bool isAdded = _entities.Add(entity);
                }
            }
            _arena.ForEachElements(AddEntitiesFromCellOnSet);
        }

        private void RemoveEntityFromSet(EntityDestroyedContext context)
        {
            _ = _entities.Remove(context.Entity);
        }

        /// <summary>
        /// Создаёт экземпляр специальной клетки, находящейся вне области определения матрицы боевой арены.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <returns></returns>
        private Cell InstantiateBeyondCell(int x, int y)
        {
            var beyondCell = new Cell(this, x, y);
            var beyond = new Beyond(x, y, this, beyondCell);
            beyondCell.PutEntity(beyond);
            return beyondCell;
        }
    }
}
