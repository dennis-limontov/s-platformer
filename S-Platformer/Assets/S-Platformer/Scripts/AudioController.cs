using UnityEngine;

namespace SPlatformer
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Play(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }

        public void PlayLoop(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void SetSoundVolume(float volume)
        {
            if ((volume >= 0f) && (volume <= 1f))
            {
                _audioSource.volume = volume;
            }
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}