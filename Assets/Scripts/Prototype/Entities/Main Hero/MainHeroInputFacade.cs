using IUP.BattleSystemPrototype.Input;
using IUP.Toolkits.Direction2D;
using System;
using UnityEngine.InputSystem;

namespace IUP.BattleSystemPrototype
{
    public sealed class MainHeroInputFacade
    {
        public MainHeroInputFacade()
        {
            _inputActions = new BattleArenaInputActions();
            _inputActions.MainHero.DirectionUp.started += InvokeMoveOnDirectionUpEvent;
            _inputActions.MainHero.DirectionDown.started += InvokeMoveOnDirectionDownEvent;
            _inputActions.MainHero.DirectionLeft.started += InvokeMoveOnDirectionLeftEvent;
            _inputActions.MainHero.DirectionRight.started += InvokeMoveOnDirectionRightEvent;
        }

        ~MainHeroInputFacade()
        {
            _inputActions.MainHero.DirectionUp.started -= InvokeMoveOnDirectionUpEvent;
            _inputActions.MainHero.DirectionDown.started -= InvokeMoveOnDirectionDownEvent;
            _inputActions.MainHero.DirectionLeft.started -= InvokeMoveOnDirectionLeftEvent;
            _inputActions.MainHero.DirectionRight.started -= InvokeMoveOnDirectionRightEvent;
        }

        public event Action<Direction> MovedOnDirection;

        private readonly BattleArenaInputActions _inputActions;

        public void Enable()
        {
            _inputActions.Enable();
        }

        public void Disable()
        {
            _inputActions.Disable();
        }

        private void InvokeMoveOnDirectionUpEvent(InputAction.CallbackContext context)
        {
            MovedOnDirection?.Invoke(Direction.Up);
        }

        private void InvokeMoveOnDirectionDownEvent(InputAction.CallbackContext context)
        {
            MovedOnDirection?.Invoke(Direction.Down);
        }

        private void InvokeMoveOnDirectionLeftEvent(InputAction.CallbackContext context)
        {
            MovedOnDirection?.Invoke(Direction.Left);
        }

        private void InvokeMoveOnDirectionRightEvent(InputAction.CallbackContext context)
        {
            MovedOnDirection?.Invoke(Direction.Right);
        }
    }
}
