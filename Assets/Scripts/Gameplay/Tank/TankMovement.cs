using UnityEngine;

namespace ZapGames.TankGame
{
    public class TankMovement : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [SerializeField] float maxSpeed;
        [SerializeField] float rotationSpeed;
        #endregion

        #region STATIC VARIABLES
        #endregion

        #region PRIVATE VARIABLES
        private Rigidbody rb;
        private bool floorHitted = false;
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
        private void Start()
        {
            InputManager.OnMovementPress += Move;
        }
        private void OnDestroy()
        {
            InputManager.OnMovementPress -= Move;
        }

        private void Move(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Backward:
                        Brake();
                    break;
                case MovementDirection.Forward:
                        Accelerate();
                    break;
                case MovementDirection.Left:
                        TurnLeft();
                    break;
                case MovementDirection.Right:
                default:
                        TurnRight();
                    break;
            }
        }

        private void TurnLeft()
        {
            //Debug.LogError("Mover Izq");
            Vector3 actualRotation = -transform.up * rotationSpeed * Time.deltaTime;
            if(rb.angularVelocity.magnitude < actualRotation.magnitude)
                rb.AddTorque(actualRotation, ForceMode.Force);
        }
        private void TurnRight()
        {
            //Debug.LogError("Mover Der");
            Vector3 actualRotation = transform.up * rotationSpeed * Time.deltaTime;
            if (rb.angularVelocity.magnitude < actualRotation.magnitude)
                rb.AddTorque(actualRotation, ForceMode.Force);
        }
        private void Accelerate()
        {
            Vector3 actualSpeed = transform.forward * maxSpeed * Time.deltaTime;
            //Debug.LogError("Mover adelante " + rb.velocity.magnitude + " " + actualSpeed.magnitude);
            if (rb.velocity.magnitude < actualSpeed.magnitude)
                rb.AddForce(actualSpeed/*, ForceMode.Acceleration*/);
        }
        private void Brake()
        {
            //Debug.LogError("Mover atras");
            Vector3 actualSpeed = -transform.forward * maxSpeed/ 2 * Time.deltaTime;
            if (rb.velocity.magnitude < actualSpeed.magnitude)
                rb.AddForce(actualSpeed, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (floorHitted) return;
            if (collision.collider.tag == "Floor")
            {
                floorHitted = true;
                //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                
            }
        }

        #endregion
        #endregion
    }
}
