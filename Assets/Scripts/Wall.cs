using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Material transParentMaterial;
    Material originalMaterial;
    PlayerContoller playerContoller;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial= GetComponent<MeshRenderer>().material;
        playerContoller = FindObjectOfType<PlayerContoller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerContoller.isRayHit)
        {
            if (playerContoller.camToPlayerRay.transform.gameObject.Equals(gameObject))
            {
                gameObject.GetComponent<MeshRenderer>().material = transParentMaterial;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
            }
        }
    }
}
