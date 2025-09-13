using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SweetDeal.Source.Player.IKFX
{
    public class RigWeightControll : MonoBehaviour
    {
        [SerializeField] private TwoBoneIKConstraint leftHandIKConstraint;
        [SerializeField] private TwoBoneIKConstraint rightHandIKConstraint;
        [SerializeField] private float speedTurnHand;
        
        [SerializeField] private HandTargetIntersection leftHandTargetIntersection;
        [SerializeField] private HandTargetIntersection rightHandTargetIntersection;

        [SerializeField] private Transform leftTarget;
        [SerializeField] private Transform rightTarget;

        private float leftHandProgress;
        private float rightHandProgress;
        
        private float leftTargetProgress;
        private float rightTargetProgress;

        private void OnEnable()
        {
            leftHandTargetIntersection.OnIntersect += ActivateLeftHandIK;
            rightHandTargetIntersection.OnIntersect += ActivateRightHandIK;
            leftHandTargetIntersection.OnRelease += DeactivateLeftHandIK;
            rightHandTargetIntersection.OnRelease += DeactivateRightHandIK;
        }

        private void OnDisable()
        {
            leftHandTargetIntersection.OnIntersect -= ActivateLeftHandIK;
            rightHandTargetIntersection.OnIntersect -= ActivateRightHandIK;
            leftHandTargetIntersection.OnRelease -= DeactivateLeftHandIK;
            rightHandTargetIntersection.OnRelease -= DeactivateRightHandIK;
        }

        private void Update()
        {
            leftHandProgress = Mathf.Lerp(leftHandProgress, leftTargetProgress, Time.deltaTime * speedTurnHand);
            rightHandProgress = Mathf.Lerp(rightHandProgress, rightTargetProgress, Time.deltaTime * speedTurnHand);
            
            leftHandIKConstraint.weight = leftHandProgress;
            rightHandIKConstraint.weight = rightHandProgress;

            if (leftHandTargetIntersection.Position != null)
            {
                leftTarget.position = leftHandTargetIntersection.Position.Value;
                leftTarget.forward = leftHandTargetIntersection.Normal.Value;
            }
            if (rightHandTargetIntersection.Position != null)
            {
                rightTarget.position = rightHandTargetIntersection.Position.Value;
                rightTarget.forward = rightHandTargetIntersection.Normal.Value;
            }
        }

        void ActivateLeftHandIK(Vector3 position)
        {
            leftTarget.position = position;
            leftTargetProgress = 1;
        }

        void ActivateRightHandIK(Vector3 position)
        {
            rightTarget.position = position;
            rightTargetProgress = 1;
        }

        void DeactivateLeftHandIK()
        {
            leftTargetProgress = 0;
        }

        void DeactivateRightHandIK()
        {
            rightTargetProgress = 0;
        }
    }
}