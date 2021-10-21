using UnityEngine;
using System.Collections.Generic;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey.PowerUps
{
    [RequireComponent(typeof(CollisionHandler))]
    public class PowerUpActivator : MonoBehaviour
    {
        [SerializeField] private string puckTag;

        [SerializeField] private List<PowerUp> playerPowerUps;
        [SerializeField] private List<PowerUp> aiPowerUps;

        private PowerUp instantiated;
        private CollisionHandler collisionHandler;


        void Awake()
        {
            collisionHandler = GetComponent<CollisionHandler>();
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        }

        void collisionHandler_EnterContact(GameObject contact)
        {
            if (contact.tag == puckTag)
            {
                PowerUp powerUp = null;

                var lastTag = contact.GetComponent<PuckScript>().lastTag;
                if (lastTag == "Player")
                {
                    powerUp = RandomGenerator.RandomElement<PowerUp>(playerPowerUps);
                }
                else if (lastTag == "Enemy")
                {
                    powerUp = RandomGenerator.RandomElement<PowerUp>(aiPowerUps);
                }

                if (powerUp == null)
                {
                    Debug.Log("PowerUp null: last puck collider tag: " + lastTag);
                    return;
                }


                SetEnabled(false);
                instantiated = Instantiate(powerUp);

                instantiated.StoppedAffect += Enable;

                instantiated.StartAffect();
                Invoke("StopPowerUp", instantiated.duration);
                
                
                Debug.Log("Power Up start: " + instantiated);
            }
        }

        void StopPowerUp()
        {
            Debug.Log("Power Up stop: " + instantiated);

            instantiated.StopAffect();
        }

        void Enable()
        {
            SetEnabled(true);
        }

        void SetEnabled(bool value)
        {
            GetComponent<SpriteRenderer>().enabled = value;
            GetComponent<Collider2D>().enabled = value;
        }

    }
}