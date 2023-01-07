using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] floorTrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.position += new Vector3(0, -0.09f, 0);
            foreach (GameObject obj in floorTrap)
            {
                FloorTrap fT = obj.GetComponent<FloorTrap>();
                if (fT.isUp)
                {
                    fT.Down();
                }
                else
                {
                    fT.Up();
                }
            }
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        transform.position += new Vector3(0, 0.09f, 0);
    }
}
