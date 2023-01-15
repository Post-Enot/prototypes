using UnityEngine;
using System.Collections.Generic;

namespace IUP.Toolkits.AI
{
    public interface IAI_MovingState
    {
        public Vector2Int Position { get; }
        public IReadOnlyCollection<Vector2Int> DestinationPoints { get; }
    }
}
