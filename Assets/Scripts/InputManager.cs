using UnityEngine;
using System;

namespace ZapGames.TankGame
{
    public class InputManager : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES

        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PRIVATE VARIABLES

        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS
        public static Action<MovementDirection> OnMovementPress;
        public static Action OnPausePress;
        #endregion

        #region PRIVATE METHODS

        private void Update()
        {
            if (!PauseSystem.Paused)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) OnMovementPress?.Invoke(MovementDirection.Backward);

                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) OnMovementPress?.Invoke(MovementDirection.Forward);

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) OnMovementPress?.Invoke(MovementDirection.Left);

                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) OnMovementPress?.Invoke(MovementDirection.Right);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                OnPausePress?.Invoke();
            }
        }
        #endregion
        #endregion
    }
}
