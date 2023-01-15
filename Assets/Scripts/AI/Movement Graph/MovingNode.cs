using IUP.Toolkits.Graphs;
using System.Collections;
using System.Collections.Generic;

namespace IUP.Toolkits.AI
{
    public sealed class MovingNode<TMovingState> : IGraphNode<TMovingState>
        where TMovingState : IAI_MovingState
    {
        public MovingNode(
            IReadOnlyCollection<IAI_MovingAction<TMovingState>> movingActions,
            TMovingState state,
            IAI_MovingAction<TMovingState> cause = null)
        {
            _movingActions = movingActions;
            Value = state;
            Cause = cause;
        }

        /// <summary>
        /// Текущее состояние перемещения.
        /// </summary>
        public TMovingState Value { get; }
        /// <summary>
        /// Действие передвижения, которое привело к перемещению в текущую позицию.
        /// </summary>
        public IAI_MovingAction<TMovingState> Cause { get; }
        /// <summary>
        /// Действия передвижения, доступные для совершения.
        /// </summary>
        public IReadOnlyCollection<IGraphEdge<TMovingState>> Edges
        {
            get
            {
                if (_edges is null)
                {
                    InitEdges();
                }
                return _edges;
            }
        }

        private List<MovingEdge<TMovingState>> _edges;
        private readonly IReadOnlyCollection<IAI_MovingAction<TMovingState>> _movingActions;

        private void InitEdges()
        {
            _edges = new List<MovingEdge<TMovingState>>();
            foreach (IAI_MovingAction<TMovingState> movingAction in _movingActions)
            {
                if (movingAction.IsPossibleToMakeAction(Value))
                {
                    TMovingState state = movingAction.EvaluateActionResult(Value);
                    var movingNode = new MovingNode<TMovingState>(_movingActions, state, movingAction);
                    var movingEdge = new MovingEdge<TMovingState>(movingAction, movingNode);
                    _edges.Add(movingEdge);
                }
            }
        }

        public IEnumerator<IGraphEdge<TMovingState>> GetEnumerator()
        {
            return Edges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Edges.GetEnumerator();
        }
    }
}
