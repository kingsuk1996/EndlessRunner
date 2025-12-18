using UnityEngine;
using System;

namespace EndlessRunner
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;


        public float volume = 1;

        [Range(-3f, 3f)]
        public float pitch = 1;

        public bool loop = false;

        public bool playAtAwake = false;

        public bool mute = false;

        [HideInInspector]
        public AudioSource source;
    }

    [Serializable]
    public class UIScreensInstanceModel
    {
        public UiScreen _screenId;
        public UiScreens _screenInstance;
    }

    [Serializable]
    public class GameData
    {
        public int lastScore;
        public int bestScore;

        public GameData()
        {
            lastScore = 0;
            bestScore = 0;
        }
    }
}
