using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] floorTrap;
    public AudioClip pressSound;
    public AudioClip releaseSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.clip = pressSound;
        audioSource.Play();
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
        audioSource.clip = releaseSound;
        audioSource.Play();
        transform.position += new Vector3(0, 0.09f, 0);
    }
}
