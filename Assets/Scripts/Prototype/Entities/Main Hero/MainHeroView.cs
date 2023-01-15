using System;
using System.Collections;
using Cinemachine;
using IUP.Toolkits.Direction2D;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class MainHeroView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _moveEffectPrefab;

        private const string _moveAnimationKey = "move";
        private const string _attackAnimationKey = "attack";
        private const string _withdrawalAnimationKey = "withdrawal";
        private const string _swordStrikeAnimationKey = "sword strike";

        private const string _turnUpAnimationKey = "is turn up";
        private const string _turnDownAnimationKey = "is turn down";
        private const string _turnLeftAnimationKey = "is turn left";
        private const string _turnRightAnimationKey = "is turn right";

        private bool _isAnimationEnd;
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public IEnumerator StartMoveAnimation(Direction direction)
        {
            Instantiate(
                _moveEffectPrefab,
                transform.position,
                transform.rotation,
                transform.transform.parent.parent);
            SetTurnDirectionAnimationFlag(direction);
            _animator.SetTrigger(_moveAnimationKey);
            yield return WaitAnimationEnd();
        }

        public void StartAttackAnimation(Direction direction)
        {
            SetTurnDirectionAnimationFlag(direction);
            _animator.SetTrigger(_attackAnimationKey);
            _impulseSource.GenerateImpulse();
            //yield return WaitAnimationEnd();
        }

        public IEnumerator StartWithdrawalAnimation(Direction direction)
        {
            SetTurnDirectionAnimationFlag(direction);
            _animator.SetTrigger(_withdrawalAnimationKey);
            yield return WaitAnimationEnd();
        }

        public IEnumerator StartSwordStrikeAnimation(Direction direction)
        {
            SetTurnDirectionAnimationFlag(direction);
            _animator.SetTrigger(_swordStrikeAnimationKey);
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
