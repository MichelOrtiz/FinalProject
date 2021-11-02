using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey.PowerUps
{
    [CreateAssetMenu(fileName = "HockeySpeedMod", menuName = "HockeyPowerUps/Speed Modifier")]
    public class HockeySpeedModifier : PowerUp
    {
        [Header("Additions")]
        [SerializeField] private float speedModifier;
         
        public override void StartAffect()
        {
            base.StartAffect();
            switch (affectedObject)
            {
                case AffectedObject.Player:
                    playerDisc.playerSpeed *= speedModifier;
                    break;
                case AffectedObject.AI:
                    aiDisc.aIMaxMovementSpeed *= speedModifier;
                    break;
                case AffectedObject.Puck:
                    puck.MaxSpeed *= speedModifier;
                    break;
            }
        }
        


        public override void StopAffect()
        {
            switch (affectedObject)
            {
                case AffectedObject.Player:
                    playerDisc.playerSpeed /= speedModifier;
                    break;
                case AffectedObject.AI:
                    aiDisc.aIMaxMovementSpeed /= speedModifier;
                    break;
                case AffectedObject.Puck:
                    puck.MaxSpeed /= speedModifier;
                    break;
            }
            base.StopAffect();
        }
    }
}