using System.Collections;
using UnityEngine;

namespace IUP.Toolkits
{
    public static class YieldInstructions
    {
        public static IEnumerator Move(
            Transform transform,
            Vector3 startPosition,
            Vector3 finalPosition,
            AnimationCurve curve,
            float durationInSeconds)
        {
            float startTime = Time.time;
            float timeLeft;
            Vector3 distance = finalPosition - startPosition;
            do
            {
                yield return null;
                timeLeft = Time.time - startTime;
                float normalizedTime = timeLeft / durationInSeconds;
                float curveFactor = curve.Evaluate(normalizedTime);
                transform.position = startPosition + (distance * curveFactor);
            }
            while (timeLeft < durationInSeconds);
            transform.position = finalPosition;
        }
    }
}
