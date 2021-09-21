using UnityEngine.Audio;
using UnityEngine;

namespace FinalProject.Assets.Scripts.Utils.Sound
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;
        public bool loop;
        public AudioMixerGroup output; 


        [HideInInspector]
        public AudioSource source;
    }
}