using IUP.Toolkits.Direction2D;
using IUP.Toolkits.Graphs;
using IUP.Toolkits.Matrices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    public class TestNode : IGraphNode<Vector2Int>
    {
        public TestNode(
            Matrix<Vector2Int> matrix,
            Vector2Int value,
            Dictionary<Vector2Int, TestNode> testNodeByCoordinate)
        {
            _testNodeByCoordinate = testNodeByCoordinate;
            _testNodeByCoordinate.Add(value, this);
            _matrix = matrix;
            Value = value;
            var edges = new List<IGraphEdge<Vector2Int>>();
            TryCreateNewEdgeByDirection(ref edges, Direction.Up);
            TryCreateNewEdgeByDirection(ref edges, Direction.Down);
            TryCreateNewEdgeByDirection(ref edges, Direction.Left);
            TryCreateNewEdgeByDirection(ref edges, Direction.Right);
            _edges = edges;
        }

        public Vector2Int Value { get; }
        public IReadOnlyCollection<IGraphEdge<Vector2Int>> Edges => _edges;

        private readonly Matrix<Vector2Int> _matrix;
        private readonly IReadOnlyCollection<IGraphEdge<Vector2Int>> _edges;
        private readonly Dictionary<Vector2Int, TestNode> _testNodeByCoordinate;

        private void TryCreateNewEdgeByDirection(
            ref List<IGraphEdge<Vector2Int>> edges,
            Direction direction)
        {
            Vector2Int newValue = Value + direction.ToVector2Int();
            if (_matrix.IsCoordinateInDefinitionDomain(newValue))
            {
                TestNode newNode;
                if (!_testNodeByCoordinate.ContainsKey(newValue))
                {
                    newNode = new(_matrix, newValue, _testNodeByCoordinate);
                }
                else
                {
                    newNode = _testNodeByCoordinate[newValue];
                }
                GraphEdge<Vector2Int> newEdge = new(newNode, 1);
                edges.Add(newEdge);
            }
        }

        public IEnumerator<IGraphEdge<Vector2Int>> GetEnumerator()
        {
            return _edges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _edges.GetEnumerator();
        }
    }

    private void Awake()
    {
        //var matrix = new Matrix<Vector2Int>(10, 10);
        //matrix.InitAllElements((int x, int y) => new Vector2Int(x, y));
        //var startNode = new TestNode(matrix, Vector2Int.zero, new());
        //var finalPosition = new Vector2Int(9, 9);
        //A_StarVector2Int A_Star = new A_StarVector2Int(finalPosition);
        //var path = Pathfinding.FindPath(
        //    startNode,
        //    (IPathNode<Vector2Int> pathNode) => pathNode.GraphNode.Value == finalPosition,
        //    A_Star.NodeSelectionFunc);
        //foreach (var item in path.Nodes)
        //{
        //    Debug.Log(item.Value);
        //}
    }
}
