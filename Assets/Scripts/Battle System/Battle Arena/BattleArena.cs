using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleArena : IBattleArena
    {
        public BattleArena(int width, int height)
        {
            _arena = new Matrix<ICell>(width, height);
            _arena.InitAllElements((int x, int y) => new Cell(this, x, y));
        }

        public int Width => _arena.Width;
        public int Height => _arena.Height;

        private readonly Matrix<ICell> _arena;

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

        /// <summary>
        /// Создаёт экземпляр специальной клетки, находящейся вне области определения матрицы боевой арены.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <returns></returns>
        private Cell InstantiateBeyondCell(int x, int y)
        {
            var beyondCell = new Cell(this, x, y);
            var beyond = new Beyond(x, y);
            beyondCell.PutEntity(beyond);
            return beyondCell;
        }
    }
}
