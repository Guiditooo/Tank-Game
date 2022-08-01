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

            if(cannonMovementCoroutine!=null) StopCoroutine(MoveCannon(actualTarget)); //Cancels actual movement

            cannonMovementCoroutine = MoveCannon(newTarget);
            StartCoroutine(cannonMovementCoroutine); //Starts a new corroutine with the new direction as target

            actualTarget = newTarget;

        }

        IEnumerator MoveCannon(Vector3 target) //Without clamping depression and elevation angles
        {
            Vector3 targetDirection = target - tankTransform.position;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, targetDirection.normalized);

            float targetAngle = Quaternion.Angle(transform.rotation, targetRotation);
            //Debug.Log("Posicion objetivo> " + target + " - ANGLE WITH CANNON POS> " + targetAngle);

            float auxAngle = transform.rotation.eulerAngles.y;

            Debug.Log("Empieza en " + auxAngle + " Y debe llegar a " + targetAngle);

            while (auxAngle < targetAngle)
            {
                
                transform.rotation = (target.x < tankTransform.position.x) ? Quaternion.Euler(0, -auxAngle, 0) : Quaternion.Euler(0, auxAngle, 0);
                auxAngle += Time.deltaTime * rotationSpeed;
                yield return null;
            }
            transform.rotation = (target.x < tankTransform.position.x) ? Quaternion.Euler(0, -targetAngle, 0) : Quaternion.Euler(0, targetAngle, 0);


            //Quaternion actualRotation = transform.rotation;
            //float actualRotation = transform.rotation.y;
            //while (t<1)
            //{
            //    transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(actualRotation, targetRotation.y, t), transform.rotation.z, transform.rotation.w);
            //    //transform.rotation = Quaternion.Lerp(actualRotation, targetRotation, t);
            //    t += Time.deltaTime * rotationSpeed;
            //    yield return null;
            //}
            //transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(actualRotation, targetRotation.y, t), transform.rotation.z, transform.rotation.w);
            //transform.rotation = Quaternion.Lerp(actualRotation, targetRotation, 1);

            TargetAligned?.Invoke(); // Once the cannon is aligned with the cannon, it is able to shoot.

        }

        IEnumerator MoveCannonY(Transform target) // Ill separate the movement of both X & Y axis.
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * rotationSpeed;
            }
            yield return null;
        }

        #endregion
        #endregion
    }
}
