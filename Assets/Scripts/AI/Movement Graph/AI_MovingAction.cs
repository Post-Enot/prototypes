using System;
using UnityEngine;

namespace IUP.Toolkits.AI
{
    public sealed class AI_MovingAction<TMovingState> : IAI_MovingAction<TMovingState>
        where TMovingState : IAI_MovingState
    {
        public AI_MovingAction(
            int cost,
            Predicate<TMovingState> isPossibleToMakeAction,
            Func<TMovingState, TMovingState> evaluateActionResult,
            Action makeAction)
        {
            Cost = cost;
            _isPossibleToMakeAction = isPossibleToMakeAction;
            _evaluateActionResult = evaluateActionResult;
            _makeAction = makeAction;
        }

        public int Cost { get; }

        private readonly Func<TMovingState, TMovingState> _evaluateActionResult;
        private readonly Predicate<TMovingState> _isPossibleToMakeAction;
        private readonly Action _makeAction;

        public bool IsPossibleToMakeAction(TMovingState state)
        {
            return _isPossibleToMakeAction(state);
        }

        public TMovingState EvaluateActionResult(TMovingState state)
        {
            return _evaluateActionResult(state);
        }

        public void MakeAction()
        {
            _makeAction();
        }
    }
}
