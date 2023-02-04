using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 0.2f;
    public float movementTime = 5f;
    public float panBoarderThickness = 20f;
    public Vector2 panLimit;
    public float scrollSpeed = 0.2f;
    public Vector2 scrollLimit;
    public float rotationAmount = 5f;

    public int level = 0;

    public Vector3 newPosition;
    public Quaternion newRotation;

    int[] layerMask = new int[] { 1 << 8, 3 << 8, 7 << 8, 15 << 8};

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            if (Physics.Raycast(newPosition + transform.forward * panSpeed * movementTime, Vector3.down, Mathf.Infinity, layerMask[level]))
            {
                newPosition += transform.forward * panSpeed;
            }
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
        {
            if (Physics.Raycast(newPosition - transform.forward * panSpeed * movementTime, Vector3.down, Mathf.Infinity, layerMask[level]))
            {
                newPosition -= transform.forward * panSpeed;
            }
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            if (Physics.Raycast(newPosition + transform.right * panSpeed * movementTime, Vector3.down, Mathf.Infinity, layerMask[level]))
            {
                newPosition += transform.right * panSpeed;
            }
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            if (Physics.Raycast(newPosition - transform.right * panSpeed * movementTime, Vector3.down, Mathf.Infinity, layerMask[level]))
            {
                newPosition -= transform.right * panSpeed;
            }
        }

        if (Input.GetKey("q"))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey("e"))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //newPosition.y -= scroll * scrollSpeed * 100f;

        //if (Physics.Raycast(newPosition + scroll * transform.forward * 150f * scrollSpeed * movementTime, Vector3.down, Mathf.Infinity, layerMask))
        //{
        //    newPosition += scroll * transform.forward * 150f * scrollSpeed;
        //}

        //newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
        //newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.y, panLimit.y);
        newPosition.y = Mathf.Clamp(newPosition.y, scrollLimit.x, scrollLimit.y);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}
