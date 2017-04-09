using UnityEngine;
using System;

public class FirstPersonCamera : MonoBehaviour
{
	void Start ()
	{
		motor = GetComponent <PlayerMotor> ();
		cameraDistance = YDistanceBetween (middleBody.transform, _camera.transform);
		initialCameraRotationLimit = motor.CameraRotationLimit;
		initialCameraYPosition = CameraYPosition ();
	}

	void Update ()
	{
		Vector3 rotation = new Vector3 (0f, MouseMovementVariation ().x, 0f) * lookSensibility;
		motor.Rotate (rotation);

		motor.RotateCamera (MouseMovementVariation ().y * lookSensibility);

		motor.CameraHeight = YPosition (middleBody.transform) + cameraDistance;

		if (CameraYPosition () < initialCameraYPosition - 0.5f)
			motor.CameraRotationLimit = 40f;
		else
			motor.CameraRotationLimit = initialCameraRotationLimit;
	}

	Vector2 MouseMovementVariation ()
	{
		return Util.Input.NextMouseHorVerMovementVariation ();
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
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private float lookSensibility = 3f;

	[SerializeField]
	private GameObject middleBody;

	[SerializeField]
	private Camera _camera;

	private PlayerMotor motor;

	private float cameraDistance, initialCameraYPosition, initialCameraRotationLimit;
}