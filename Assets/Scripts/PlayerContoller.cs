using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Animator animator;
    public GameObject cam;
    public GameObject interactionText;
    public GameObject keyIcon;
    public int keyCount;
    public int hp = 3;
    public GameObject[] heartIcons;

    public RaycastHit camToPlayerRay;
    public bool isRayHit;
    //public Rigidbody rb;

    float h, v;
    public bool canMove = true;
    bool isHurt = false;
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
        // 마우스 클릭 이벤트
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
                            if(keyCount == 0)
                            {
                                keyIcon.SetActive(false);
                            }
                            obj.GetComponent<Chest>().OpenLock();
                        }
                    }
                    else
                    {
                        obj.GetComponent<Chest>().OpenChest();
                        GameManager gameManager = FindObjectOfType<GameManager>();
                        gameManager.GameClear();
                    }
                }
                if (obj.tag == "Switch")
                {
                    if (obj.GetComponent<Switch>().isSwitchOn)
                    {
                        obj.GetComponent<Switch>().OffSwitch();
                    }
                    else
                    {
                        obj.GetComponent<Switch>().OnSwitch();
                    }
                }
                if(obj.tag == "FakeWall")
                {
                    obj.GetComponent<Rigidbody>().AddForce(-obj.transform.forward,ForceMode.Impulse);
                }
            }
        }

        // 캐릭터랑 카메라 사이 광선
        isRayHit = Physics.Raycast(cam.transform.position, transform.position - cam.transform.position, out camToPlayerRay);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key" || other.tag == "Heart")
        {
            interactionText.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key" || other.tag == "Heart")
        {
            if (Input.GetKey(KeyCode.F) && canMove)
            {
                Debug.Log("gathering...");
                canMove = false;
                animator.SetTrigger("gathering");
                Invoke("LetMove", 2);

                if(other.tag == "Key")
                {
                    keyCount++;
                    keyIcon.SetActive(true);
                }
                else
                {
                    Heal();
                }
                Destroy(other.gameObject);
                interactionText.SetActive(false);
            }
        }
        if (other.tag == "Hurt" && hp > 0)
        {
            Hurt();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Key" || other.tag == "Heart")
        {
            interactionText.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hurt" && hp > 0)
        {
            Hurt();
        }
    }
    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        moveDirection = transform.position - new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
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
            animator.SetBool("isMove", true);
            lookRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

    }
    void LetMove()
    {
        Debug.Log("LetMove");
        canMove = true;
    }

    void Hurt()
    {
        if (isHurt) return;
        isHurt = true;
        canMove = false;
        GetComponent<Rigidbody>().AddForce((-gameObject.transform.forward + Vector3.up) * 20, ForceMode.Impulse);
        hp--;
        heartIcons[hp].SetActive(false);
        if (hp == 0)
        {
            animator.SetTrigger("die");
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();
        }
        else
        {
            animator.SetTrigger("getHit");
            Invoke("LetMove", 0.5f);
            Invoke("EndHurt", 1f);
        }
    }
    void EndHurt()
    {
        isHurt = false;
    }

    void Heal()
    {
        if (hp < 3)
        {
            heartIcons[hp++].SetActive(true);
        }

    }
}
