using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Key : MonoBehaviour
{
    bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(200 * Time.deltaTime, 0 , 0);
        if(isUp)
        {
            transform.Translate(-0.5f*Time.deltaTime, 0, 0);
            if(transform.position.y > 1)
            {
                isUp= false;
            }
        }
        else
        {
            transform.Translate(0.5f*Time.deltaTime, 0, 0);
            if(transform.position.y < 0.5)
            {
                isUp = true;
            }
        }
    }
}
