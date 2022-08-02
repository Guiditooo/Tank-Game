using System.Collections;
using UnityEngine;
using System;

namespace ZapGames.TankGame
{
    public class CannonMovement : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [SerializeField] [Range(-4,0)] private float depressionAngle = -1;
        [SerializeField] [Range(0, 15)] private float elevationAngle = 10;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform tankTransform;
        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PRIVATE VARIABLES
        bool movingCannon = false;
        Vector3 actualTarget;
        IEnumerator cannonMovementCoroutine = null;
        #endregion

        #region PUBLIC VARIABLES
        public Action TargetAligned;
        #endregion

        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            TankController.MoveCannon += AlignCannonWithTarget;
        }
        private void OnDestroy()
        {
            TankController.MoveCannon -= AlignCannonWithTarget;
        }
        private void AlignCannonWithTarget(Vector3 newTarget)
        {

            if(movingCannon) return; //Cancels actual movement

            StartCoroutine(MoveCannon(newTarget)); //Starts a new corroutine with the new direction as target

            actualTarget = newTarget;

        }

        IEnumerator MoveCannon(Vector3 target) //Without clamping depression and elevation angles
        {
            Vector3 targetDirection = target - transform.position;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, targetDirection.normalized);
            
            float targetAngleQ = Quaternion.Angle(transform.rotation, targetRotation);
            float targetAngleV = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

            Debug.Log("TargetAngleQ> " + targetAngleQ + " / TargetAngleV> " + targetAngleV);

            float startingAngle = tankTransform.rotation.eulerAngles.y;
            float auxAngle = 0;
            Quaternion auxRotation = transform.rotation;

            bool moveRight = ShouldMoveRight(transform.forward, target);

            Debug.Log("Forward del Canon> " + transform.forward);

            movingCannon = true;
            //float t = 0;



            while (auxAngle + startingAngle < targetAngleV + startingAngle)
            //while (t < 1)
            {
                //transform.rotation = (target.x < tankTransform.position.x) ? Quaternion.Euler(0, -auxAngle, 0) : Quaternion.Euler(0, auxAngle, 0);
                transform.rotation = moveRight ? Quaternion.Euler(0, -auxAngle - startingAngle, 0) : Quaternion.Euler(0, auxAngle + startingAngle, 0);
                //transform.rotation = Quaternion.RotateTowards(auxRotation, targetRotation, Time.deltaTime * rotationSpeed);
                //transform.rotation = Quaternion.AngleAxis(auxAngle, transform.up);
                //auxAngle += Time.deltaTime * rotationSpeed;
                //transform.rotation = Quaternion.Lerp(auxRotation, targetRotation, t);
                //t += Time.deltaTime/* * rotationSpeed*/;
                auxAngle += Time.deltaTime * rotationSpeed;

                yield return null;
            }

            //transform.rotation = (target.x < tankTransform.position.x) ? Quaternion.Euler(0, -targetAngle, 0) : Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = moveRight ? Quaternion.Euler(0, -targetAngleV - startingAngle, 0) : Quaternion.Euler(0, targetAngleV + startingAngle, 0);
            //transform.rotation = Quaternion.AngleAxis(targetAngle, transform.up);
            //transform.rotation = Quaternion.Lerp(auxRotation, targetRotation, 1);

            TargetAligned?.Invoke(); // Once the cannon is aligned with the cannon, it is able to shoot.

            StartCoroutine(ResetCannonPosition());

        }

        bool ShouldMoveRight(Vector3 vec, Vector3 p)
        {
            Vector3 newVec = vec - p;
            return Vector3.Cross(newVec, vec).y < 0;
        }

        IEnumerator ResetCannonPosition()
        {
            float t = 0;
            Quaternion auxRotation = transform.rotation;

            while (t < 1)
            {
                transform.rotation = Quaternion.Lerp(auxRotation, tankTransform.rotation, t);
                t += Time.deltaTime/* * rotationSpeed*/;
                yield return null;
            }

            transform.rotation = Quaternion.Lerp(auxRotation, tankTransform.rotation, 1);

            movingCannon = false;

            Debug.Log("Volvi a mi lugar");
        }

        //IEnumerator MoveCannonY(Transform target) // Ill separate the movement of both X & Y axis.
        //{
        //    float t = 0;
        //    while (t < 1)
        //    {
        //        t += Time.deltaTime * rotationSpeed;
        //    }
        //    yield return null;
        //}

        #endregion
        #endregion
    }
}
