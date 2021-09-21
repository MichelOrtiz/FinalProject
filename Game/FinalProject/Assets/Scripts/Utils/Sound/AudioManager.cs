using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

namespace FinalProject.Assets.Scripts.Utils.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public List<Sound> sounds;
        public static AudioManager instance;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.output;
            }    
        }

        public void Play(string name)
        {
            var sound = sounds.Find( s => s.name == name);
            if (sound != null)
            {
                sound.source.Play();
            }
            else
            {
                Debug.LogWarning("Sound \"" +  name + "\" not found: check the name is correct");
            }
        }
    }
}