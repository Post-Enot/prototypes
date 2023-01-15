using Cinemachine;
using IUP.Toolkits.BattleSystem;
using UnityEngine;

namespace IUP.BattleSystemPrototype
{
    public sealed class CameraBinder : MonoBehaviour
    {
        [SerializeField] private Battle _battle;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public void BindCamera()
        {
            var mainHeroView = _battle.ArenaPresenter.EntitiesRoot.GetComponentInChildren<MainHeroView>();
            _cinemachineVirtualCamera.Follow = mainHeroView.transform;
        }
    }
}
