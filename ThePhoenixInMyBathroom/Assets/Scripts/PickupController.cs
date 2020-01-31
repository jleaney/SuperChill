using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private GameObject heldObject;

    public Transform holdTransform; // transform for the painting to snap to while being held

    private bool holdingPainting = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Painting")
                {
                    CheckHoldPainting(hit.transform.gameObject);
                }
            }
        }
    }

    private void CheckHoldPainting(GameObject painting)
    {
        if (heldObject == null)
        {
            heldObject = painting;
            heldObject.transform.parent = holdTransform;
            heldObject.transform.position = holdTransform.position;
            heldObject.transform.rotation = holdTransform.rotation;
        }
    }
}
