using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.Meduf.Scripts
{
    public class WordGenerator : MonoBehaviour
    {
        private List<char> chars;

        void Awake()
        {
            chars = new List<char>();
            for (int i = 65; i < 91; i++)
            {
                var upper = (char) i;
                var lower = (char) (i + 32); 
                chars.Add(upper);
                chars.Add(lower);
            }
        }
    
        public string Generate(byte minLen, byte maxLen)
        {
            var length = RandomGenerator.NewRandom(minLen, maxLen);
            string word = string.Empty;
            //var charList = ScenesManagers.ArrayToList<char>(chars);
            for (int i = 0; i < length; i++)
            {
                word += RandomGenerator.RandomElement(chars);
            }
            return word;
        }
    }
}
