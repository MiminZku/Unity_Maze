using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.transform.position + new Vector3(0f, 1.5f, -2f);
        gameObject.transform.LookAt(target.transform.position + new Vector3(0,1f,0));
    }
}
