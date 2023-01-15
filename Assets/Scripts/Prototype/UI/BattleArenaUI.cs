using IUP.Toolkits.BattleSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace IUP.BattleSystemPrototype
{
    public sealed class BattleArenaUI : MonoBehaviour
    {
        [SerializeField] private Battle _battle;

        private UIDocument _uiDocument;
        private ProgressBar _timeBar;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _timeBar = _uiDocument.rootVisualElement.Q<ProgressBar>("time-bar");
            HideTimer();
        }

        public void SubscribeToBattleEvents()
        {
            _battle.EventBus.RegisterEventCallback<TimedTurnStartedContext>(HandleStartTimedTurn);
            _battle.EventBus.RegisterEventCallback<TimedTurnUpdatedContext>(HandleUpdateTimedTurn);
            _battle.EventBus.RegisterEventCallback(BattleEvents.TimedTurnEnded, HandleEndTimedTurn);
        }

        private void HandleStartTimedTurn(TimedTurnStartedContext context)
        {
            _timeBar.highValue = context.TurnDuration;
            _timeBar.value = context.TurnDuration;
            ShowTimer();
        }

        private void HandleUpdateTimedTurn(TimedTurnUpdatedContext context)
        {
            _timeBar.value = context.TurnTimeLeft;
        }

        private void HandleEndTimedTurn()
        {
            HideTimer();
        }

        private void ShowTimer()
        {
            _timeBar.visible = true;
        }

        private void HideTimer()
        {
            _timeBar.visible = false;
        }
    }
}
