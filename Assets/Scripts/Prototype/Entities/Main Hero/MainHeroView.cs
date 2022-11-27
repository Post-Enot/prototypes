using IUP.Toolkits.Direction2D;
using System.Data;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    [RequireComponent(typeof(Animator))]
    public sealed class MainHeroView : MonoBehaviour
    {
        private Animator _animator;
        private MainHeroPresenter _mainHero;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainHero = GetComponent<MainHeroPresenter>();
            _mainHero.MoveStarted += StartMoveAnimation;
        }

        public void StartMoveAnimation(Direction direction)
        {
            SetTurnDirection(direction);
            _animator.SetTrigger("move");
        }

        private void SetTurnDirection(Direction direction)
        {
            ResetTurnDirectionParams();
            switch (direction)
            {
                case Direction.Up:
                    _animator.SetBool("is turn up", true);
                    break;

                case Direction.Down:
                    _animator.SetBool("is turn down", true);
                    break;

                case Direction.Left:
                    _animator.SetBool("is turn left", true);
                    break;

                case Direction.Right:
                    _animator.SetBool("is turn right", true);
                    break;

                default:
                    throw new System.ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private void ResetTurnDirectionParams()
        {
            _animator.SetBool("is turn up", false);
            _animator.SetBool("is turn down", false);
            _animator.SetBool("is turn left", false);
            _animator.SetBool("is turn right", false);
        }
    }
}
