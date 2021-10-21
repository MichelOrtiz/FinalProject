using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey.PowerUps
{
    [CreateAssetMenu(fileName = "HockeyFreeze", menuName = "HockeyPowerUps/Freeze")]
    public class HockeyFreeze : PowerUp
    {
        public override void StartAffect()
        {
            base.StartAffect();
            switch (affectedObject)
            {
                case AffectedObject.Player:
                    playerDisc.enabled = false;
                    break;
                case AffectedObject.AI:
                    aiDisc.enabled = false;
                    break;
                case AffectedObject.Puck:
                    puck.enabled = false;
                    break;
            }
        }

        protected override void Affect()
        {
            return;
        }

        public override void StopAffect()
        {
            switch (affectedObject)
            {
                case AffectedObject.Player:
                    playerDisc.enabled = true;
                    break;
                case AffectedObject.AI:
                    aiDisc.enabled = true;
                    break;
                case AffectedObject.Puck:
                    puck.enabled = true;
                    break;
            }
            base.StopAffect();
        }
    }
}