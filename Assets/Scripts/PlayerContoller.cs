using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Animator animator;
    public GameObject camera;
    public int keyCount;
    //public Rigidbody rb;

    float h, v;
    bool canMove = true;
    Vector3 moveDirection;
    Vector3 movement;
    Quaternion lookRotation;
    // Start is called before the first frame update
    void Start()
    {
        lookRotation= transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        moveDirection = transform.position - new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
        movement = moveDirection * v + Vector3.Cross(Vector3.up, moveDirection) * h;
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        //rb.MovePosition(transform.position + movement);
        //rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        if (canMove)
        {
            transform.position += movement;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }
        if (h != 0 || v != 0)
        {
            animator.SetBool("isMove",true);
            lookRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.transform.gameObject;
                if(obj.tag == "Chest")
                {
                    if (obj.GetComponent<Chest>().isLocked)
                    {
                        if(keyCount > 0)
                        {
                            keyCount--;
                            obj.GetComponent<Chest>().OpenLock();
                        }
                    }
                    else
                    {
                        obj.GetComponent<Chest>().OpenChest();
                    }
                }
            }

        }
    }
    void LetMove()
    {
        canMove = true;
    }
 
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Key")
        {
            if (Input.GetKeyDown(KeyCode.F) && canMove)
            {
                canMove = false;
                animator.SetTrigger("gathering");
                Invoke("LetMove", 2);
               
                keyCount++;
                Destroy(other.gameObject);                
            }
        }
    }

}
