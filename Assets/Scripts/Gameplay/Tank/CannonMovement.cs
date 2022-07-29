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
        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PRIVATE VARIABLES
        bool movingCannon = false;
        Vector3 actualTarget;
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
        private void AlignCannonWithTarget(Vector3 target)
        {

            if (movingCannon)
            {
                StopCoroutine(MoveCannon(actualTarget)); //Cancels actual movement
                movingCannon = false; 
            }

            StartCoroutine(MoveCannon(target)); //Starts a new corroutine with the new direction as target
            actualTarget = target;

        }

        IEnumerator MoveCannon(Vector3 target) //Without clamping depression and elevation angles
        {

            Quaternion targetRotation = Quaternion.FromToRotation(target,transform.position);

            float t = 0;

            movingCannon = true;

            while(t<1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
                t += Time.deltaTime;
            }

            yield return null;

            movingCannon = false;

            TargetAligned?.Invoke(); // Once the cannon is aligned with the cannon, it is able to shoot.

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
