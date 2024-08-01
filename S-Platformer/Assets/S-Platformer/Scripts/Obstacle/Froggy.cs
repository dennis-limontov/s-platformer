using System.Collections;
using UnityEngine;

namespace SPlatformer
{
    public class Froggy : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _froggyView;

        [SerializeField]
        private float _kwaPause = 1.5f;

        private AudioSource _frogSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _froggyView.flipX = !_froggyView.flipX;
        }

        private void Start()
        {
            _frogSound = GetComponent<AudioSource>();
            StartCoroutine(Kwa());
        }

        private IEnumerator Kwa()
        {
            WaitForSeconds wfs = new WaitForSeconds(_kwaPause);
            while (true)
            {
                _frogSound.Play();
                yield return wfs;
            }
        }
    }
}