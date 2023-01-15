using System;
using System.Collections;
using UnityEngine;
using IUP.Toolkits.Direction2D;

namespace IUP.BattleSystemPrototype
{
    public sealed class NitLarvaView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string _moveAnimationKey = "move";

        private const string _turnUpAnimationKey = "is turn up";
        private const string _turnDownAnimationKey = "is turn down";
        private const string _turnLeftAnimationKey = "is turn left";
        private const string _turnRightAnimationKey = "is turn right";

        private bool _isAnimationEnd;

        public IEnumerator StartMoveAnimation(Direction direction)
        {
            SetTurnDirectionAnimationFlag(direction);
            _animator.SetTrigger(_moveAnimationKey);
            yield return WaitAnimationEnd();
        }

        private IEnumerator WaitAnimationEnd()
        {
            _isAnimationEnd = false;
            while (!_isAnimationEnd)
            {
                yield return null;
            }
            _isAnimationEnd = false;
        }

        private void SetTurnDirectionAnimationFlag(Direction direction)
        {
            ResetTurnDirectionParams();
            switch (direction)
            {
                case Direction.Up:
                    _animator.SetBool(_turnUpAnimationKey, true);
                    break;

                case Direction.Down:
                    _animator.SetBool(_turnDownAnimationKey, true);
                    break;

                case Direction.Left:
                    _animator.SetBool(_turnLeftAnimationKey, true);
                    break;

                case Direction.Right:
                    _animator.SetBool(_turnRightAnimationKey, true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private void ResetTurnDirectionParams()
        {
            _animator.SetBool(_turnUpAnimationKey, false);
            _animator.SetBool(_turnDownAnimationKey, false);
            _animator.SetBool(_turnLeftAnimationKey, false);
            _animator.SetBool(_turnRightAnimationKey, false);
        }
        private void NotifyWhenAnimationEnds()
        {
            _isAnimationEnd = true;
        }
    }
}
