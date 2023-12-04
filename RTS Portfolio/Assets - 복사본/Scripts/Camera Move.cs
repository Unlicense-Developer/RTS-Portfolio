using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float speed = 1.5f;
    float zoomSpeed = 50.0f;
    float rotateSpeed = 10.0f;

    float maxHeight = 40.0f;
    float minHeight = 4.0f;

    Vector2 p1;
    Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(320.0f, 25.0f, 320.0f);
        transform.rotation = Quaternion.Euler(new Vector3(42.0f, -135.0f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //Shift = 빠른 줌 모드
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.06f;
            zoomSpeed = 20.0f;
        }
        else
        {
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        //WASD, 마우스 휠 입력받기
        float hSpeed = transform.position.y * speed * Input.GetAxis("Horizontal");
        float vSpeed = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSpeed = Mathf.Log(transform.position.y) * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        //높이 제한
        scrollSpeed = AdjustHeight(scrollSpeed);

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = hSpeed * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vSpeed;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        // 카메라 움직이기
        transform.position += move * Time.deltaTime;

        GetCameraRotation();
    }
    float AdjustHeight(float scrollSpeed)
    {
        //최대, 최소 높이 제한
        if ((transform.position.y >= maxHeight) && (scrollSpeed > 0)) // 최대 높이
        {
            scrollSpeed = 0;
        }
        else if ((transform.position.y <= minHeight) && (scrollSpeed < 0)) // 최소 높이
        {
            scrollSpeed = 0;
        }

        //카메라 최대,최소 높이 보간
        if ((transform.position.y + scrollSpeed) > maxHeight)
        {
            scrollSpeed = transform.position.y - maxHeight;
        }
        else if ((transform.position.y + scrollSpeed) < minHeight)
        {
            scrollSpeed = minHeight - transform.position.y;
        }

        return scrollSpeed;
    }

    void GetCameraRotation()
    {
        if( Input.GetMouseButtonDown(1))
        {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            p1 = p2;
        }
    }
}
