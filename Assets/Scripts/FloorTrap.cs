using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    public bool isUp;
    public bool isAuto;
    // Start is called before the first frame update
    void Start()
    {
        if (isAuto)
        {
            if(isUp)
            {
                Down();
            }
            else
            {
                Up();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Up()
    {
        if (isUp) return;
        isUp = true;
        transform.position = transform.position + new Vector3(0, 1, 0);
        if (isAuto)
        {
            Invoke("Down", 1);
        }
    }
    public void Down()
    {
        if (!isUp) return;
        isUp = false;
        transform.position = transform.position + new Vector3(0, -1, 0);
        if(isAuto)
        {
            Invoke("Up", 1);
        }
    }
}
