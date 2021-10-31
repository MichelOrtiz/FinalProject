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
            instance = this;
            /*if (instance == null)
            {
                instance = this;
                PlayMainTheme();
            }
            else
            {
                instance.sounds = sounds;
                instance.PlayMainTheme();
                Destroy(gameObject);
                return;
            }*/

            //DontDestroyOnLoad(gameObject);

            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.output;
            }    
            PlayMainTheme();
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

        public void PlayMainTheme()
        {
            StopAll();
            Play("Theme");
        }

        public void StopAll()
        {
            sounds.ForEach(s => s.source.Stop());
        }
    }
}