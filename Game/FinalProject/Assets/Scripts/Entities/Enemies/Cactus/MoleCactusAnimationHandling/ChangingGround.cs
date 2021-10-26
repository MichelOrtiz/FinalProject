using UnityEngine;

namespace FinalProject.Assets.Scripts.Entities.Enemies.Cactus.MoleCactusAnimationHandling
{
    public class ChangingGround : StateMachineBehaviour
    {
        [SerializeField] private Vector3 newPosition;
        [SerializeField] bool resetGroundPos;
        [SerializeField] bool enableCollider;
        private MoleCactus moleCactus;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            moleCactus = animator.transform.parent.GetComponent<MoleCactus>();
            moleCactus.canMove = false;
            animator.transform.parent.position += newPosition;
        }


        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            moleCactus.canMove = true;
            if (resetGroundPos) moleCactus.groundChecker.transform.position = moleCactus.GetPosition();
                //if (enableCollider) moleCactus.collisionHandler.gameObject.SetActive(true);
        }
    }
}