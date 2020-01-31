using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private GameObject heldObject;

    public Transform holdTransform; // transform for the painting to snap to while being held

    private bool holdingPainting = false;

    public Transform[] snapPoints;
    public float minSnapDistance;
    private bool snapped = false; // true when painting is snapped to a point, but not yet confirmed to stay there



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

            if (!heldObject.GetComponent<Painting>().LockedToWall)
            {
                heldObject.transform.parent = holdTransform;
                heldObject.transform.position = holdTransform.position;
                heldObject.transform.rotation = holdTransform.rotation;

                StartCoroutine(CheckSnapPoint());
            }

            else
            {
                heldObject = null;
            }
        }
    }

    private IEnumerator CheckSnapPoint()
    {
        while(heldObject != null)
        {
            string snapTag = ""; // gameobject tag

            foreach (Transform t in snapPoints)
            {
                // painting snaps to snap point
                if (Vector3.Distance(transform.position, t.position) < minSnapDistance)
                {
                    heldObject.transform.parent = t;
                    heldObject.transform.position = t.position;
                    heldObject.transform.rotation = t.rotation;
                    snapped = true;
                    snapTag = t.tag;

                    break;
                }

                else
                {
                    heldObject.transform.parent = holdTransform;
                    heldObject.transform.position = holdTransform.position;
                    heldObject.transform.rotation = holdTransform.rotation;
                    snapped = false;
                    snapTag = "";
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (snapped)
                {
                    ConfirmSnap(snapTag);
                }
            }

            yield return null;
        }
    }

    private void ConfirmSnap(string snapTag)
    {
        heldObject.transform.parent = null;
        
        if (snapTag == "Wall Point")
        {
            heldObject.GetComponent<Painting>().LockedToWall = true;
            // check player wants to lock their painting to the wall
            // Lock painting to wall
        }

        else
        {
            // Do something when snapped to easel
            // lock to easel and start painting process
        }

        snapped = false;
        heldObject = null;
    }
}
