using IUP.Toolkits.CoroutineShells;
using System.Collections;
using UnityEngine;

namespace IUP.Toolkits.BattleSystem
{
    public sealed class BattleLoop : IBattleLoop
    {
        public BattleLoop(
            IEntitySpawner entitySpawner,
            MonoBehaviour coroutinePerformer,
            float minTurnDurationInSecond)
        {
            _entitySpawner = entitySpawner;
            MinTurnDurationInSecond = minTurnDurationInSecond;
            _iterationRoutine = new UniqueCoroutine(coroutinePerformer, () => Iteration());
            _entitySpawner.EntitySpawned += HandleEntitySpawnedEvent;
        }

        ~BattleLoop()
        {
            _entitySpawner.EntitySpawned -= HandleEntitySpawnedEvent;
        }

        public bool IsIterating { get; private set; }
        public float MinTurnDurationInSecond { get; }

        private readonly TurnQueue _turnQueue = new();
        private readonly IEntitySpawner _entitySpawner;
        private readonly UniqueCoroutine _iterationRoutine;

        public void StartIteration()
        {
            IsIterating = true;
            if (_turnQueue.MembersCount == 0)
            {
                return;
            }
            _iterationRoutine.Start();
        }

        private IEnumerator Iteration()
        {
            while (_turnQueue.MembersCount != 0)
            {
                float turnStartTime = Time.time;
                yield return _turnQueue.CurrentMember.MakeTurn();
                _ = _turnQueue.MoveNext();
                float timeSpentPerTurn = Time.time - turnStartTime;
                float pauseDurationInSecond = MinTurnDurationInSecond - timeSpentPerTurn;
                if (pauseDurationInSecond > 0)
                {
                    yield return new WaitForSeconds(pauseDurationInSecond);
                }
            }
        }

        private void HandleEntitySpawnedEvent(ICellEntityPresenter entityPresenter)
        {
            if (entityPresenter == null)
            {
                return;
            }
            if (entityPresenter is ITurnQueueMember member)
            {
                _turnQueue.AddMember(member);
            }
            if (IsIterating && _turnQueue.MembersCount == 1 && !_iterationRoutine.IsPerformed)
            {
                _iterationRoutine.Start();
            }
        }
    }
}
