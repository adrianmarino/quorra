using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Util
{
	public class Input
	{
		public static void HideCursor (bool value)
		{
			if (value)
				HideCursor ();
			else
				ShowCursor ();
		}

		public static void HideCursor ()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}


		public static void ShowCursor ()
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public static Vector2 KeyboardMovementDelta ()
		{
			return new Vector2 (
				HorizontalMovementDelta (), 
				VerticalMovementDelta ()
			);
		}

		public static Vector2 MouseMovementDelta ()
		{
			return new Vector2 (
				MouseHorizontalMovementDelta (), 
				MouseVerticalMovementDelta ()
			);
		}

		public static float MouseHorizontalMovementDelta ()
		{
			return MovementDelta ("Mouse X");
		}

		public static float MouseVerticalMovementDelta ()
		{
			return MovementDelta ("Mouse Y");
		}

		public static float HorizontalMovementDelta ()
		{
			return MovementDelta ("Horizontal");
		}

		public static float VerticalMovementDelta ()
		{
			return MovementDelta ("Vertical");
		}

		public static float MovementDelta (string axisName)
		{
			return CrossPlatformInputManager.GetAxisRaw (axisName);
		}

		public static bool GetJumpButtonDown ()
		{
			return CrossPlatformInputManager.GetButtonDown ("Jump");
		}

		public static bool GetJumpButtonUp ()
		{
			return CrossPlatformInputManager.GetButtonUp ("Jump");
		}

		public static bool GetFireButton ()
		{
			return CrossPlatformInputManager.GetButton ("Fire1");
		}

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Input ()
		{
		}
	}
}
