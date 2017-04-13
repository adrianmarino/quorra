using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Characters.Quorra
{
	[RequireComponent (typeof(ThirdPersonCharacter))]
	public class QuorraThirdPersonUserControl : MonoBehaviour
	{
		//-----------------------------------------------------------------------------
		// Engine Methods
		//-----------------------------------------------------------------------------

		// the world-relative desired move direction, calculated from the camForward
		// and user input.
		private void Start ()
		{
			InitializeCamera ();
			InitializeCharacter ();
		}

		private void Update ()
		{
			if (!jump)
				jump = Util.Input.GetJumpButtonDown ();
		}

		// Fixed update is called in sync with physics
		private void FixedUpdate ()
		{
			// read inputs
			float h = Util.Input.HorizontalMovementDelta ();
			float v = Util.Input.VerticalMovementDelta ();
			bool crouch = Input.GetKey (KeyCode.C);

			// Adjust vertical speed when walk or run(Press LeftShift)...

			v = v < 0 ? 0 : v;

			// v *= GetSpeedDelta (h, v);
			// h *= GetSpeedDelta (h, v);

			// calculate move direction to pass to character
			if (_camera != null) {
				// calculate camera relative direction to move:
				cameraForward = Vector3.Scale (_camera.forward, new Vector3 (1, 0, 1)).normalized;

				move = v * cameraForward + h * _camera.right;
			} else {
				// we use world-relative directions in the case of no main camera
				move = v * Vector3.forward + h * Vector3.right;
			}

#if !MOBILE_INPUT
			move *= 0.5f;

			// Run speed multiplier
			if (Input.GetKey (KeyCode.LeftShift))
				move *= 2f;
#endif

			// pass all parameters to the character control script
			move = Vector3.ClampMagnitude (move, 1);
			character.Move (move, crouch, jump);
			jump = false;
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		static float GetSpeedDelta (float h, float v)
		{
			if (v != 0 && h != 0)
				return Input.GetKey (KeyCode.LeftShift) ? 0.91f : 0.75f;
			else
				return 1f;
		}

		void InitializeCharacter ()
		{
			// get the third person character ( this should never be null due to require component )
			character = GetComponent<ThirdPersonCharacter> ();
		}

		void InitializeCamera ()
		{
			// get the transform of the main camera
			if (Camera.main != null)
				_camera = Camera.main.transform;
			else {
				Debug.LogWarning ("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		private ThirdPersonCharacter character;

		// A reference to the ThirdPersonCharacter on the object
		private Transform _camera;

		// A reference to the main camera in the scenes transform
		private Vector3 cameraForward;

		// The current forward direction of the camera
		private Vector3 move;

		private bool jump;
	}
}
