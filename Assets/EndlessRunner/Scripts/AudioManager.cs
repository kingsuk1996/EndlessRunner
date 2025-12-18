using System;
using UnityEngine;

namespace EndlessRunner
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager Instance;

        private void Awake()
        {
            Instance = this;

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playAtAwake;
                s.source.mute = s.mute;
            }
        }

        public void Play(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Play();
        }

        public void Stop(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Stop();
        }

        public void Pause(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Pause();
        }

        public void PlayOneShot(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.PlayOneShot(s.clip);
        }

        public void Mute()
        {
            foreach (Sound s in sounds)
            {
                s.mute = true;
                s.source.mute = s.mute;
            }
        }

        public void UnMute()
        {
            foreach (Sound s in sounds)
            {
                s.mute = false;
                s.source.mute = s.mute;
            }
        }
    }
}