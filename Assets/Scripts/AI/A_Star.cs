using System;
using System.Collections.Generic;
using IUP.Toolkits.Graphs;
using UnityEngine;

namespace IUP.Toolkits.AI
{
    public static class A_Star<TState> where TState : IAI_MovingState
    {
        public static IPathNode<TState> NodeSelectionFunc(
            IReadOnlyCollection<IPathNode<TState>> reachedNodes)
        {
            double minCost = double.PositiveInfinity;
            IPathNode<TState> bestNode = null;
            foreach (IPathNode<TState> reachedNode in reachedNodes)
            {
                double estimatedCost = GetCostForNearestDestinationPoint(reachedNode);
                if (minCost > estimatedCost)
                {
                    minCost = estimatedCost;
                    bestNode = reachedNode;
                }
            }
            return bestNode;
        }

        public static IPathNode<TState> ChooseBestPathNode(
            IPathNode<TState> a,
            IPathNode<TState> b)
        {
            if ((a is null) && (b is null))
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a is null)
            {
                return b;
            }
            if (b is null)
            {
                return a;
            }
            double aCost = GetCostForNearestDestinationPoint(a);
            double bCost = GetCostForNearestDestinationPoint(b);
            return aCost >= bCost ? a : b;
        }

        public static bool IsPathFind(IPathNode<TState> pathNode)
        {
            foreach (Vector2Int destinationPoint in pathNode.GraphNode.Value.DestinationPoints)
            {
                if (pathNode.GraphNode.Value.Position == destinationPoint)
                {
                    return true;
                }
            }
            return false;
        }

        private static double GetCostForNearestDestinationPoint(IPathNode<TState> pathNode)
        {
            double minCost = double.PositiveInfinity;
            foreach (Vector2Int destinationPoint in pathNode.GraphNode.Value.DestinationPoints)
            {
                double cost =
                    Vector2Int.Distance(
                        pathNode.GraphNode.Value.Position,
                        destinationPoint)
                    + pathNode.Cost;
                if (minCost > cost)
                {
                    minCost = cost;
                }
            }
            return minCost;
        }
    }
}
