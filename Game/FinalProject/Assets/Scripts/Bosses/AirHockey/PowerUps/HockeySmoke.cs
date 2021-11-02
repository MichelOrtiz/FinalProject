using UnityEngine;
namespace FinalProject.Assets.Scripts.Bosses.AirHockey.PowerUps
{
    [CreateAssetMenu(fileName = "HockeySmoke", menuName = "HockeyPowerUps/Smoke")]
    public class HockeySmoke : PowerUp
    {
        [Header("Additions")]
        [SerializeField] private GameObject smoke;
        private GameObject instantiated;

        [SerializeField] private float affectedOffsetMod;

        // positions of both players, to instantiate the smoke in the corresponding one
        [SerializeField] private Vector2 position;


        public override void StartAffect()
        {
            base.StartAffect();
            if (affectedObject == AffectedObject.Player)
            {
                playerDisc.offset *= affectedOffsetMod;
            }
            else if (affectedObject == AffectedObject.AI)
            {
                aiDisc.PUoffset *= affectedOffsetMod;
            }
            instantiated = Instantiate(smoke, position, smoke.transform.rotation);
            
        }
        


        public override void StopAffect()
        {
            Destroy(instantiated);
            if (affectedObject == AffectedObject.Player)
            {
                playerDisc.offset /= affectedOffsetMod;
            }
            else if (affectedObject == AffectedObject.AI)
            {
                aiDisc.PUoffset /= affectedOffsetMod;
            }

            base.StopAffect();
        }
    }
}