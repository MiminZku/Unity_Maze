using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator chestAnimator;
    public bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenLock()
    {
        isLocked = false;
        transform.GetChild(0).gameObject.SetActive(isLocked);
    }
    public void OpenChest()
    {
        chestAnimator.SetBool("isLocked", isLocked);
        Invoke("StopAnimation", 1.5f);
    }

    void StopAnimation()
    {
        chestAnimator.speed = 0;
    }
}
