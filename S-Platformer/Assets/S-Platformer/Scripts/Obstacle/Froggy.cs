using UnityEngine;

namespace SPlatformer
{
    public class Froggy : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _froggyView;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _froggyView.flipX = !_froggyView.flipX;
        }
    }
}