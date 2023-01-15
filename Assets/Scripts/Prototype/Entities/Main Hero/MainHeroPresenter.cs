using System.Collections;
using IUP.Toolkits.BattleSystem;
using IUP.Toolkits.Direction2D;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class MainHeroPresenter : CellEntityPresenter, ITurnQueueMember
    {
        public override ICellEntity Entity => _model;
        public int TurnPriority { get; private set; }
        /// <summary>
        /// Время в секундах на то, чтобы сделать ход.
        /// </summary>
        public float TimeInSecondsForMakeTurn { get; } = 1.5f;
        public Direction TurnDirection { get; private set; } = Direction.Down;
        public MainHeroView View => _view;

        private MainHeroInputFacade _inputFacade;
        private MainHeroView _view;
        private MainHero _model;
        private EntityTurns _entityTurns;
        private EntityTurn _waitTurn;
        private IBattleArenaPresenter _arenaPresenter;
        private IBattleEventBus _eventBus;

        private void Awake()
        {
            _waitTurn = new EntityTurn(() => FoveRoutine());
            _entityTurns = new EntityTurns(_waitTurn, turnsCount: 1);
            _entityTurns.TurnCompleted += DetermineNextTurn;
            _view = GetComponentInChildren<MainHeroView>();
        }

        private void OnEnable()
        {
            _inputFacade?.Enable();
        }

        private void OnDisable()
        {
            _inputFacade?.Disable();
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
            _model = new MainHero(x, y, _arenaPresenter.BattleArena);
            _inputFacade = new MainHeroInputFacade();
            _inputFacade.Enable();
            _inputFacade.MovedOnDirection += TryMoveOnDirection;
            TurnPriority = Random.Range(1, 10);
            _eventBus = eventBus;
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
            _eventBus.InvokeEventCallbacks(new TimedTurnStartedContext(TimeInSecondsForMakeTurn));
            float timeLeft;
            do
            {
                yield return null;
                timeLeft = Time.time - startTime;
                _eventBus.InvokeEventCallbacks(new TimedTurnUpdatedContext(
                    TimeInSecondsForMakeTurn - timeLeft));
            }
            while (timeLeft < TimeInSecondsForMakeTurn);
            _eventBus.InvokeEventCallbacks(BattleEvents.TimedTurnEnded);
            _entityTurns.TurnsLeft = 0;
        }

        private IEnumerator Move(Direction direction)
        {
            TurnDirection = direction;
            _model.MoveOn(direction);
            yield return _view.StartMoveAnimation(direction);
            _entityTurns.TurnsLeft -= 1;
        }

        private void TryMoveOnDirection(Direction direction)
        {
            if (_model.CanMoveOn(direction))
            {
                DisableInputHandling();
                EntityTurn moveTurn = new(() => Move(direction));
                _eventBus.InvokeEventCallbacks(BattleEvents.TimedTurnEnded);
                _entityTurns.SetTurnRoutine(moveTurn);
            }
            else
            {
                _view.StartAttackAnimation(direction);
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
