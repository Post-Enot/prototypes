using IUP.Toolkits.Graphs;

namespace IUP.Toolkits.AI
{
    public sealed class MovingEdge<TMovingState> : IGraphEdge<TMovingState> where TMovingState : IAI_MovingState
    {
        public MovingEdge(
            IAI_MovingAction<TMovingState> movingAction,
            MovingNode<TMovingState> direction)
        {
            MovingAction = movingAction;
            Direction = direction;
        }

        public int Cost => MovingAction.Cost;
        public IAI_MovingAction<TMovingState> MovingAction { get; }
        public IGraphNode<TMovingState> Direction { get; }
    }
}
