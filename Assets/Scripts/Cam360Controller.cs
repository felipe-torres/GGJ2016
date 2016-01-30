using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//// <summary>
/// Camera controlled by gyroscope, vr and or panning
/// </summary>
public class Cam360Controller : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	private Vector3 dragOrigin;
	private Vector3 dragDelta;
	public bool DragInertia = true;



	void Awake()
	{
		Input.gyro.enabled = true;
	}
	// Update is called once per frame
	void Update ()
	{
		UpdateCamera();
	}

	//// <summary>
	/// Updates the camera's transform
	/// </summary>
	private void UpdateCamera()
	{
		if (Input.gyro.enabled)
		{
			// Use Gyo sensor to rotate camera, gyro is rotated x degrees
			Gyroscope gyro = Input.gyro;

			// Create a parent object containing the camera
			GameObject camParent = new GameObject ("CamParent");
			camParent.transform.position = transform.position;
			transform.parent = camParent.transform;

			// Rotate the parent object by 90 degrees around the x axis
			camParent.transform.Rotate(Vector3.right, 90);

			transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w);
			//transform.localRotation = gyro.attitude;
		}
		else
		{
			// Use mouse panning
			PanRotation();
		}
	}

	//// <summary>
	/// Rotation with mouse pointer (when in desktop)
	/// </summary>
	private void MouseRotation()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	//// <summary>
	/// Rotation with mouse drag (when in desktop), or with touch drag, when in mobile
	/// </summary>
	private void PanRotation()
	{

		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			dragDelta = dragOrigin - Input.mousePosition;

			dragOrigin = Input.mousePosition;

			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + dragDelta.x * sensitivityX;

				rotationY += dragDelta.y * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, dragDelta.x * sensitivityX, 0);
			}
			else
			{
				rotationY += dragDelta.y * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
		else
		{
			if (DragInertia)
			{
				// Apply inertia
				dragDelta = Vector3.Lerp(dragDelta, Vector3.zero, Time.deltaTime*10f);
				float rotationX = transform.localEulerAngles.y + dragDelta.x * sensitivityX;

				rotationY += dragDelta.y * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
		}

	}
}
