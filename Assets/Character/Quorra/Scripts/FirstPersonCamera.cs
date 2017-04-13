using UnityEngine;
using System;

[RequireComponent (typeof(PlayerMotor))]
public class FirstPersonCamera : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		InitializeMotor ();
		InitializeCamera ();
	}

	void FixedUpdate ()
	{
		UpdateCameraHorizontalRotation ();
		UpdateCameraVerticalRotation ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void UpdateCameraHorizontalRotation ()
	{
		Vector3 rotation = new Vector3 (0f, MouseMovementVariation ().x, 0f) * lookSensibility;
		motor.Rotate (rotation);
	}

	void UpdateCameraVerticalRotation ()
	{
		motor.RotateCamera (MouseMovementVariation ().y * lookSensibility);
		UpdateCameraVerticalClamp ();
	}

	void UpdateCameraVerticalClamp ()
	{
		motor.CameraHeight = YPosition (middleBody.transform) + cameraDistance;
		if (CameraYPosition () < initialCameraYPosition - 0.1f)
			motor.CameraRotationLimit = crouchCameraVerticalLimit;
		else
			motor.CameraRotationLimit = initialCameraRotationLimit;
	}

	Vector2 MouseMovementVariation ()
	{
		return Util.Input.MouseMovementDelta ();
	}

	float YPosition (Transform _transform)
	{
		return _transform.position.y;
	}

	float YDistanceBetween (Transform tA, Transform tB)
	{
		return Math.Abs (YPosition (tA) - YPosition (tB));
	}

	float CameraYPosition ()
	{
		return YPosition (_camera.transform);
	}

	//-----------------------------------------------------------------------------
	// Initialization Methods
	//-----------------------------------------------------------------------------

	void InitializeMotor ()
	{
		motor = GetComponent<PlayerMotor> ();
		motor.RotateCamera (0f);
	}

	void InitializeCamera ()
	{
		cameraDistance = YDistanceBetween (middleBody.transform, _camera.transform);
		initialCameraRotationLimit = motor.CameraRotationLimit;
		initialCameraYPosition = CameraYPosition ();
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private float lookSensibility = 3f;

	[SerializeField]
	private float crouchCameraVerticalLimit = 40f;

	[SerializeField]
	private GameObject middleBody;

	[SerializeField]
	private Camera _camera;

	private PlayerMotor motor;

	private float cameraDistance, initialCameraYPosition, initialCameraRotationLimit;
}