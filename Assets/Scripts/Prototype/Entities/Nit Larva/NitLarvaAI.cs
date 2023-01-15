using System.Collections.Generic;
using IUP.Toolkits.AI;
using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class NitLarvaAI
    {
        public class State : IAI_MovingState
        {
            public State(Vector2Int position, params Vector2Int[] destinationPoints)
            {
                Position = position;
                DestinationPoints = destinationPoints;
            }

            public Vector2Int Position { get; }
            public IReadOnlyCollection<Vector2Int> DestinationPoints { get; }
        }

        public NitLarvaAI(NitLarva model)
        {
            _model = model;
            _getCloseToEnemy = new GetCloseToEnemy<State>(
                cost: 1,
                InitMovingActions().ToArray());
        }

        private readonly NitLarva _model;
        private ICellEntity _targetEnemy;
        private readonly GetCloseToEnemy<State> _getCloseToEnemy;

        public void MakeOption()
        {
            var state = new State(_model.Coordinate, GetDestinationPoints().ToArray());
            if (_getCloseToEnemy.IsPossibleToMakeOption(state))
            {
                _getCloseToEnemy.MakeOption();
            }
        }

        public void AnalyzeBattleArena()
        {
            foreach (ICellEntity entity in _model.BattleArena.Entities)
            {
                if (entity.Tags.HasTag(EntitiesTags.MainHero))
                {
                    _targetEnemy = entity;
                }
            }
        }

        private bool IsPossibleToMoveOn(Direction direction, State state)
        {
            Vector2Int newCoordinate = state.Position + direction.ToVector2Int();
            return _model.BattleArena[newCoordinate].CanPutEntity(_model);
        }

        private State EvaluateMovingResult(Direction direction, State state)
        {
            Vector2Int newCoordinate = state.Position + direction.ToVector2Int();
            return new State(
                newCoordinate,
                GetDestinationPoints().ToArray());
        }

        private AI_MovingAction<State> CreateMovingAction(int cost, Direction direction)
        {
            return new AI_MovingAction<State>(
                cost,
                (State state) => IsPossibleToMoveOn(direction, state),
                (State state) => EvaluateMovingResult(direction, state),
                () => _model.MoveOn(direction));
        }

        private List<AI_MovingAction<State>> InitMovingActions()
        {
            return new List<AI_MovingAction<State>>()
            {
                CreateMovingAction(1, Direction.UpLeft),
                CreateMovingAction(1, Direction.UpRight),
                CreateMovingAction(1, Direction.DownLeft),
                CreateMovingAction(1, Direction.DownRight),
                CreateMovingAction(2, Direction.Up),
                CreateMovingAction(2, Direction.Down),
                CreateMovingAction(2, Direction.Left),
                CreateMovingAction(2, Direction.Right)
            };
        }

        private List<Vector2Int> GetDestinationPoints()
        {
            var destinationPoints = new List<Vector2Int>();
            void TryAddDestinationPoint(Direction direction)
            {
                Vector2Int destinationPoint = _targetEnemy.Cell.Coordinate + direction.ToVector2Int();
                if (_model.BattleArena[destinationPoint].CanPutEntity(_model))
                {
                    destinationPoints.Add(destinationPoint);
                }
            }
            TryAddDestinationPoint(Direction.UpLeft);
            TryAddDestinationPoint(Direction.UpRight);
            TryAddDestinationPoint(Direction.DownLeft);
            TryAddDestinationPoint(Direction.DownRight);
            TryAddDestinationPoint(Direction.Up);
            TryAddDestinationPoint(Direction.Down);
            TryAddDestinationPoint(Direction.Left);
            TryAddDestinationPoint(Direction.Right);
            return destinationPoints;
        }
    }
}
