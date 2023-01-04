using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float moveSpeed = 10f;
    float h, v;
    public bool isMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (h * v != 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
        Debug.Log(string.Format("{0} , {1}",h,v));
        gameObject.transform.Translate(new Vector3(h, 0, v)*moveSpeed*Time.deltaTime);

    }
    
    
}
