using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isSwitchOn;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isSwitchOn = true;
        animator.SetBool("isActivated", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OffSwitch()
    {
        isSwitchOn = false;
        animator.SetBool("isActivated", false);
    }
    public void OnSwitch()
    {
        isSwitchOn = true;
        animator.SetBool("isActivated", true);
    }
}
