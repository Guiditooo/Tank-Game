using System;
using UnityEngine;
//TO DO Move the "TankMovement" script to this one.
namespace ZapGames.TankGame
{
    public class TankController : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [SerializeField] private GameObject cannon;
        #endregion

        #region STATIC VARIABLES
        public static Action<Vector3> MoveCannon;
        #endregion

        #region PRIVATE VARIABLES
        private CannonMovement cannonMovement;
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
            InputManager.OnClicPress += SetTarget;
            cannonMovement = cannon.GetComponent<CannonMovement>();
            cannonMovement.TargetAligned += Shoot;
        }
        private void OnDestroy()
        {
            InputManager.OnClicPress -= SetTarget;
            cannonMovement.TargetAligned -= Shoot;
        }

        private void SetTarget(Vector3 target)
        {
            MoveCannon?.Invoke(target);
        }
        private void Shoot()
        {
            Debug.Log("SHOOT!");
        }
        #endregion
        #endregion
    }
}
