﻿using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		ShowHideCursor ();
		_rigidbody = GetComponent<Rigidbody> ();
		velocity = rotation = thrusterForce = Vector3.zero;
		cameraRotation = 0f;
		if (_camera != null)
			cameraHeight = _camera.transform.position.y;
	}

	void FixedUpdate ()
	{
		ShowHideCursor ();
		UpdatePosition ();
		UpdateRotation ();
	}

	//-----------------------------------------------------------------------------
	// Public Methods
	//-----------------------------------------------------------------------------

	public void Reset ()
	{
		Move (Vector3.zero);
		Rotate (Vector3.zero);
		if (_camera != null)
			_camera.transform.Rotate (Vector3.zero);
	}

	public void ApplyTrusterForce (Vector3 thrusterForce)
	{
		this.thrusterForce = thrusterForce;
	}

	public void Move (Vector3 velocity)
	{
		this.velocity = velocity;
	}

	public void Rotate (Vector3 rotation)
	{
		this.rotation = rotation;
	}

	public void RotateCamera (float cameraRotation)
	{
		this.cameraRotation = cameraRotation;
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void ShowHideCursor ()
	{
		Util.Input.HideCursor (hideCursor);
	}

	void UpdatePosition ()
	{
		Util.Rigidbody.Move (_rigidbody, velocity);
		Util.Rigidbody.AddForce (_rigidbody, thrusterForce, ForceMode.Impulse);

		if (_camera != null)
			_camera.transform.position = new Vector3 (
				_camera.transform.position.x,
				cameraHeight,
				_camera.transform.position.z
			);
	}

	void UpdateRotation ()
	{
		Util.Rigidbody.Rotate (_rigidbody, rotation);
		SetCameraRotation (cameraRotation);
	}

	void SetCameraRotation (float xRotation)
	{
		if (_camera == null)
			return;

		currentCameraRotation -= xRotation;
		currentCameraRotation = Mathf.Clamp (
			currentCameraRotation,
			-cameraRotationLimit,
			cameraRotationLimit
		);
		_camera.transform.localEulerAngles = new Vector3 (currentCameraRotation, 0f, 0f);		
	}


	//-----------------------------------------------------------------------------
	// Properties
	//-----------------------------------------------------------------------------

	public float CameraHeight {
		get { return cameraHeight; }
		set { cameraHeight = value; }
	}

	public float CameraRotationLimit {
		get { return cameraRotationLimit; }
		set { cameraRotationLimit = value; }
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private Camera _camera;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	[SerializeField]
	private bool hideCursor = true;

	private Rigidbody _rigidbody;

	private Vector3 velocity, rotation, thrusterForce;

	private float cameraRotation, currentCameraRotation, cameraHeight;

}
