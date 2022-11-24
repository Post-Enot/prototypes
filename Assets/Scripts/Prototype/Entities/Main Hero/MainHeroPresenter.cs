using IUP.BattleSystemPrototype.Input;
using IUP.Toolkits;
using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using System;
using System.Collections;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class MainHeroPresenter : MonoBehaviour, ICellEntityPresenter, ITurnQueueMember
    {
        [SerializeField] private float _movingDurationInSeconds;
        [SerializeField] private AnimationCurve _curve;

        public sealed class InputFacade
        {
            public InputFacade(InputActions inputActions)
            {
                _inputActions = inputActions;
                _inputActions.MainHeroBattleArenaControl.DirectionUp.started += DirectionUp_started;
                _inputActions.MainHeroBattleArenaControl.DirectionDown.started += DirectionDown_started;
                _inputActions.MainHeroBattleArenaControl.DirectionLeft.started += DirectionLeft_started;
                _inputActions.MainHeroBattleArenaControl.DirectionRight.started += DirectionRight_started;
            }

            ~InputFacade()
            {
                _inputActions.MainHeroBattleArenaControl.DirectionUp.started -= DirectionUp_started;
                _inputActions.MainHeroBattleArenaControl.DirectionDown.started -= DirectionDown_started;
                _inputActions.MainHeroBattleArenaControl.DirectionLeft.started -= DirectionLeft_started;
                _inputActions.MainHeroBattleArenaControl.DirectionRight.started -= DirectionRight_started;
            }

            public event Action<Direction> MovedOnDirection;

            private readonly InputActions _inputActions;

            private void DirectionRight_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
            {
                MovedOnDirection?.Invoke(Direction.Right);
            }

            private void DirectionLeft_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
            {
                MovedOnDirection?.Invoke(Direction.Left);
            }

            private void DirectionDown_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
            {
                MovedOnDirection?.Invoke(Direction.Down);
            }

            private void DirectionUp_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
            {
                MovedOnDirection?.Invoke(Direction.Up);
            }
        }

        public ICellEntity Entity => _mainHero;
        public IBattleArenaPresenter BattleArenaPresenter { get; private set; }
        public Transform Transform => transform;
        public Vector2Int Coordinate => Entity.Coordinate;
        public int TurnPriority { get; private set; }
        /// <summary>
        /// Время в секундах на то, чтобы сделать ход.
        /// </summary>
        public float TimeInSecondsForMakeTurn { get; } = 3f;

        private InputFacade _inputFacade;
        private InputActions _inputActions;
        private MainHero _mainHero;
        private IEnumerator _currentAction;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputFacade = new InputFacade(_inputActions);
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void Init(IBattleArenaPresenter battleArenaPresenter, Vector2Int coordinate)
        {
            Init(battleArenaPresenter, coordinate.x, coordinate.y);
        }

        public void Init(IBattleArenaPresenter battleArenaPresenter, int x, int y)
        {
            BattleArenaPresenter = battleArenaPresenter;
            _mainHero = new MainHero(x, y, battleArenaPresenter.BattleArena);
            TurnPriority = UnityEngine.Random.Range(1, 10);
        }

        public IEnumerator MakeTurn()
        {
            EnableInputHandling();
            _currentAction = WaitTurn();
            while (_currentAction.MoveNext())
            {
                yield return _currentAction.Current;
            }
        }

        private IEnumerator Move(Direction direction)
        {
            Vector2Int startPosition = Entity.Coordinate;
            if (_mainHero.MoveOn(direction))
            {
                Vector2Int finalPosition = startPosition + direction.ToVector2Int();
                yield return YieldInstructions.Move(
                    transform,
                    BattleArenaPresenter.GetCellWorldPosition(startPosition),
                    BattleArenaPresenter.GetCellWorldPosition(finalPosition),
                    _curve,
                    _movingDurationInSeconds);
            }
        }

        private IEnumerator WaitTurn()
        {
            float startTime = Time.time;
            float timeLeft;
            do
            {
                yield return null;
                timeLeft = Time.time - startTime;
            }
            while (timeLeft < TimeInSecondsForMakeTurn);
        }

        private void TryMoveOnDirection(Direction direction)
        {
            if (_mainHero.CanMoveOn(direction))
            {
                DisableInputHandling();
                _currentAction = Move(direction);
            }
        }

        private void EnableInputHandling()
        {
            _inputFacade.MovedOnDirection += TryMoveOnDirection;
        }

        private void DisableInputHandling()
        {
            _inputFacade.MovedOnDirection -= TryMoveOnDirection;
        }
    }
}
