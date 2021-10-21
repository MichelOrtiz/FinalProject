using UnityEngine;
using System;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey.PowerUps
{
    public abstract class PowerUp : ScriptableObject
    {
        public float duration;
        public string Name;

        [SerializeField] protected AffectedObject affectedObject;
        public AffectedObject Affected { get => affectedObject; }
        public enum AffectedObject
        {
            Player, AI, Puck
        }
        //[SerializeField] protected GameObject affected;

        protected AirHockeyPlayerMovement playerDisc;
        protected AIScript aiDisc;
        protected PuckScript puck;


        public Action StoppedAffect = delegate{};
        
        /*void Awake()
        {
            
        }*/
        /*protected void Start()
        {
            Invoke("StopAffect", duration);
            StartAffect();
        }*/

        /*protected void Update()
        {
            Affect();
        }*/
        public virtual void StartAffect()
        {
            switch (affectedObject)
            {
                case AffectedObject.Player:
                    playerDisc = FindObjectOfType<AirHockeyPlayerMovement>();// affected.GetComponent<AirHockeyPlayerMovement>();
                    break;
                case AffectedObject.AI:
                    aiDisc = FindObjectOfType<AIScript>();
                    break;
                case AffectedObject.Puck:
                    puck = FindObjectOfType<PuckScript>();
                    break;
            }
        }
        protected abstract void Affect();
        public virtual void StopAffect()
        {
            StoppedAffect?.Invoke();
            //Destroy(gameObject);
        }
    }
}