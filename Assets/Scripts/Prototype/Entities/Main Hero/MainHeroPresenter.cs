using IUP.BattleSystemPrototype.Input;
using IUP.Toolkits;
using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using System;
using System.Collections;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class MainHeroPresenter : CellEntityPresenter, ITurnQueueMember
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

        public override ICellEntity Entity => _mainHero;
        public int TurnPriority { get; private set; }
        /// <summary>
        /// Время в секундах на то, чтобы сделать ход.
        /// </summary>
        public float TimeInSecondsForMakeTurn { get; } = 3f;
        public Direction TurnDirection { get; private set; } = Direction.Down;

        public event Action<Direction> MoveStarted;

        private InputFacade _inputFacade;
        private InputActions _inputActions;
        private MainHero _mainHero;
        private EntityTurns _entityTurns;
        private EntityTurn _waitTurn;
        private IBattleArenaPresenter _arenaPresenter;

        private void Awake()
        {
            _waitTurn = new EntityTurn(() => FoveRoutine());
            _entityTurns = new EntityTurns(_waitTurn, turnsCount: 1);
            _entityTurns.TurnCompleted += DetermineNextTurn;
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

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            Vector2Int coordinate)
        {
            Init(battleArenaPresenter, eventBus, coordinate.x, coordinate.y);
        }

        public override void Init(
            IBattleArenaPresenter battleArenaPresenter,
            IBattleEventBus eventBus,
            int x,
            int y)
        {
            _arenaPresenter = battleArenaPresenter;
            _mainHero = new MainHero(x, y, _arenaPresenter.BattleArena);
            TurnPriority = UnityEngine.Random.Range(1, 10);
        }

        public IEnumerator MakeTurn()
        {
            EnableInputHandling();
            yield return _entityTurns.MakeTurn();
        }

        private void DetermineNextTurn(int turnsLeft)
        {
            if (turnsLeft > 0)
            {
                _entityTurns.ReturnToFoveRoutine();
            }
        }

        private IEnumerator FoveRoutine()
        {
            float startTime = Time.time;
            float timeLeft;
            do
            {
                yield return null;
                timeLeft = Time.time - startTime;
            }
            while (timeLeft < TimeInSecondsForMakeTurn);
            _entityTurns.TurnsLeft = 0;
        }

        private IEnumerator Move(Direction direction)
        {
            Vector2Int startPosition = Entity.Coordinate;
            TurnDirection = direction;
            _mainHero.MoveOn(direction);
            MoveStarted?.Invoke(direction);
            Vector2Int finalPosition = startPosition + direction.ToVector2Int();
            yield return YieldInstructions.Move(
                transform,
                _arenaPresenter.GetCellWorldPosition(startPosition),
                _arenaPresenter.GetCellWorldPosition(finalPosition),
                _curve,
                _movingDurationInSeconds);
            _entityTurns.TurnsLeft -= 1;
        }

        private void TryMoveOnDirection(Direction direction)
        {
            if (_mainHero.CanMoveOn(direction))
            {
                DisableInputHandling();
                EntityTurn moveTurn = new(() => Move(direction));
                _entityTurns.SetTurnRoutine(moveTurn);
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
