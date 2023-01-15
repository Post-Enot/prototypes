using System.Collections.Generic;
using IUP.Toolkits.AI;
using IUP.Toolkits.Graphs;

namespace IUP.BattleSystemPrototype
{
    public sealed class GetCloseToEnemy<TMovingState> : IAI_Option<TMovingState>
        where TMovingState : IAI_MovingState
    {
        public GetCloseToEnemy(int cost, params IAI_MovingAction<TMovingState>[] movingActions)
        {
            Priority = cost;
            _movingActions = new List<IAI_MovingAction<TMovingState>>(movingActions);
        }

        public int Priority { get; }

        private readonly IReadOnlyCollection<IAI_MovingAction<TMovingState>> _movingActions;
        private Path<TMovingState> _path;

        public bool IsPossibleToMakeOption(TMovingState state)
        {
            var startNode = new MovingNode<TMovingState>(_movingActions, state);
            _path = Pathfinding.FindPath(
                startNode,
                A_Star<TMovingState>.IsPathFind,
                A_Star<TMovingState>.NodeSelectionFunc,
                A_Star<TMovingState>.ChooseBestPathNode);
            if (_path.PathNodes.Count == 0)
            {
                return false;
            }
            return A_Star<TMovingState>.IsPathFind(_path.PathNodes[_path.PathNodes.Count - 1]);
        }

        public void MakeOption()
        {
            (_path.PathNodes[0].GraphNode as MovingNode<TMovingState>).Cause.MakeAction();
        }
    }
}
