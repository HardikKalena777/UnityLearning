using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool Interact;
		public bool Drop;
		public bool Use;
		public bool slotZero;
		public bool slotOne;
		public bool slotTwo;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private PlayerInputAsset inputAction;
        private void Awake()
        {
            inputAction = new PlayerInputAsset();
        }

        private void OnEnable()
        {
            inputAction.Enable();

			inputAction.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
			inputAction.Player.Move.canceled += ctx => move = Vector2.zero;

            inputAction.Player.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
			inputAction.Player.Look.canceled += ctx => look = Vector2.zero;

            inputAction.Player.Jump.performed += ctx => jump = ctx.ReadValueAsButton();
			inputAction.Player.Jump.canceled += ctx => jump = false;

            inputAction.Player.Sprint.performed += ctx => sprint = ctx.ReadValueAsButton();
			inputAction.Player.Sprint.canceled += ctx => sprint = false;

			inputAction.Player.Interact.performed += ctx => 
			{
                Interact = ctx.ReadValueAsButton();
            };
			inputAction.Player.Interact.canceled += ctx => Interact = false;

			inputAction.Player.Drop.performed += ctx =>
			{
                Drop = ctx.ReadValueAsButton();
            };
			inputAction.Player.Drop.canceled += ctx => Drop = false;

			inputAction.Player.Use.performed += ctx =>
			{
                Use = ctx.ReadValueAsButton();
            };
			inputAction.Player.Use.canceled += ctx => Use = false;

			inputAction.Player.SlotTwo.canceled += ctx => slotTwo = false;
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}