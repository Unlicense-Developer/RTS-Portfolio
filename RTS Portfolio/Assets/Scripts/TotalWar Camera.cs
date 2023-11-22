using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TotalWarCamera : MonoBehaviour
{
    float speed = 1.5f;
	float zoomspeed = 100.0f;
	float rotatespeed = 10.0f;

	float maxHeight = 40.0f;
	float minHeight = 4.0f;

	// Use this for initialization
	void Start()
	{
		transform.position = new Vector3(0, 25.0f, 0);
		transform.rotation = Quaternion.Euler(new Vector3(42.0f, -135.0f, 0));
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y > maxHeight)
		{
			transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
		}

		if (transform.position.y < minHeight)
		{
			transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
		}

		float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
		float vsp = transform.position.y * speed * Input.GetAxis("Vertical");
		float scrollSp = Mathf.Log(transform.position.y) * -zoomspeed * Input.GetAxis("Mouse ScrollWheel");
		
		if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(new Vector3(0, -rotatespeed, 0) * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.E))
		{
			transform.Rotate(new Vector3(0, rotatespeed, 0) * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.Mouse1))
		{

			Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
			transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
		}

		Vector3 verticalMove = new Vector3(0, scrollSp, 0);
		Vector3 lateralMove = hsp * transform.right;
		Vector3 forwardMove = transform.forward;
		forwardMove.y = 0;
		forwardMove.Normalize();
		forwardMove *= vsp;

		Vector3 move = verticalMove + lateralMove + forwardMove;
		transform.position += move * Time.deltaTime;

	}
}
