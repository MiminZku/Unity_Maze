using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed;
    float mouseX;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {       
        gameObject.transform.LookAt(target.transform.position);

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            mouseX = Input.GetAxis("Mouse X");
            gameObject.transform.RotateAround(target.transform.position, new Vector3(0, mouseX * rotateSpeed * Time.deltaTime, 0), 1f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}