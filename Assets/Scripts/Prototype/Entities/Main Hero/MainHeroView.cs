using IUP.Toolkits.Direction2D;
using System;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    [RequireComponent(typeof(Animator))]
    public sealed class MainHeroView : MonoBehaviour
    {

        public event Action MoveAnimationEnded;

        private const string _mainHeroMove = "main-hero__move";
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            var clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == _mainHeroMove)
                {
                    AnimationEvent animationEvent = new()
                    {
                        time = clip.length,
                        functionName = nameof(InvokeAnimationEvent),
                        stringParameter = _mainHeroMove
                    };
                    clip.AddEvent(animationEvent);
                }
            }
        }

        public void InvokeAnimationEvent(string test)
        {
            switch (test)
            {
                case _mainHeroMove:
                    MoveAnimationEnded?.Invoke();
                    return;

                default:
                    throw new NotImplementedException();
            }
        }

        public void StartMoveAnimation(Direction direction)
        {
            _animator.SetTrigger("move");
        }
    }
}
