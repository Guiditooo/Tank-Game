using UnityEngine;

namespace ZapGames.TankGame
{
    public class Chaser : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [SerializeField] private Transform target;
        [SerializeField] private float chasingSpeed = 260;
        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PRIVATE VARIABLES
        private Rigidbody rb;
        private bool startingImpulse = false;
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
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.tag == "Floor")
            {
                Vector3 auxDirection = target.transform.position - transform.position;
                rb.AddForce(auxDirection.normalized * Time.fixedDeltaTime * chasingSpeed);
                Debug.Log("EMPUJO!");
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (startingImpulse) return;

            startingImpulse = true;

            if (collision.collider.tag == "Floor")
            {
                Debug.Log("IMPULSO!");
                Vector3 auxDirection = target.transform.position - transform.position;
                auxDirection.Normalize();
                rb.AddForce(auxDirection * Time.fixedDeltaTime * chasingSpeed * 3, ForceMode.Impulse);
            }
        }
        #endregion
        #endregion
    }
}
