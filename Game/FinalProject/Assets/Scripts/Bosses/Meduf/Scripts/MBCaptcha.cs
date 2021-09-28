
using FinalProject.Assets.Scripts.Utils.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.Meduf.Scripts
{
    [RequireComponent(typeof(WordGenerator))]
    public class MBCaptcha : MasterMinigame
    {
        [SerializeField] private WordGenerator wordGenerator;
        [SerializeField] private Text wordText;
        [SerializeField] private TMPro.TMP_InputField inputField;
        [SerializeField] private Button button;
        private string word;


        [Header("WordLength")]
        [SerializeField] private byte minLen;
        [SerializeField] private byte maxLen;

        [Header("Words")]
        [SerializeField] private byte nWords;
        private byte index;
        [SerializeField] private byte maxAttempts;
        private byte attempts;
        
        [SerializeField] private Image[] attemptImages;

        void Awake()
        {
            wordGenerator = GetComponent<WordGenerator>();
            word = wordGenerator.Generate(minLen, maxLen);
            wordText.text = word;

            inputField.onSubmit.AddListener(input_Submit);
            ResetInput();
        }

        void Start()
        {
            PlayerManager.instance.inputs.enabled = false;
            button.onClick.AddListener(button_Click);
        }

        void NextWord()
        {
            ResetInput();
            word = wordGenerator.Generate(minLen, maxLen);
            wordText.text = word;
        }

        void ResetInput()
        {
            inputField.text = string.Empty;
            inputField.ActivateInputField();
        }

        public void button_Click()
        {
            Debug.Log("button click");
            input_Submit(inputField.text);
        }

        public void input_Submit(string text)
        {
            if (text.Equals(word))
            {
                if (index < nWords-1)
                {
                    index++;
                    NextWord();
                }
                else
                {
                    OnWinMinigame();
                }
            }
            else
            {
                if (attempts < maxAttempts)
                {
                    attemptImages[attempts].enabled = true;
                    attempts++;
                    AudioManager.instance.Play("WrongAnswer");
                    ResetInput();
                }
                else
                {
                    attemptImages[maxAttempts].enabled = true;
                    AudioManager.instance.Play("WrongAnswer");
                    OnLoseMinigame();
                }
            }
        }

        void OnDestroy()
        {
            PlayerManager.instance.inputs.enabled = true;  
        }
    }

}