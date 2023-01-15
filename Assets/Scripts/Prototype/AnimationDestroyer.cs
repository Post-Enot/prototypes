using UnityEngine;

namespace IUP.Toolkits
{
    public class AnimationDestroyer : MonoBehaviour
    {
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}
