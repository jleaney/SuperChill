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
    public Transform easel; // Easel where the painting sits
    public bool leftEaselArea = false;
    public float minEaselDistance = 7;

    public GameObject currentPainting;
    public bool CurrentlyPainting { get; set; } // whether painting is currently happening

    public float minPickupDistance = 5;

    // Update is called once per frame
    void Update()
    {
        // Checks if painting ISN'T being restored, and that the player is within the minimum distance to pick the painting up
        if (!CurrentlyPainting && Vector3.Distance(transform.position, currentPainting.transform.position) < minPickupDistance)
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

                heldObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shake");
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

            if (!leftEaselArea)
            {
                if (Vector3.Distance(transform.position, easel.position) > minEaselDistance)
                {
                    leftEaselArea = true;
                }
            }

            foreach (Transform t in snapPoints)
            {
                // painting snaps to snap point
                if (Vector3.Distance(transform.position, t.position) < minSnapDistance)
                {
                    snapTag = t.tag;

                    if (snapTag == "Easel")
                    {
                        if (leftEaselArea)
                        {
                            heldObject.transform.parent = t;
                            heldObject.transform.position = t.position;
                            heldObject.transform.rotation = t.rotation;
                            snapped = true;
                            heldObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StopShake");

                            break;
                        }
                    }

                    else
                    {
                        heldObject.transform.parent = t;
                        heldObject.transform.position = t.position;
                        heldObject.transform.rotation = t.rotation;
                        snapped = true;
                        heldObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StopShake");

                        break;
                    }
                }

                else
                {
                    snapped = false;

                }
            }

            if (!snapped)
            {
                // set to hold transform

                Animator anim = heldObject.transform.GetChild(0).GetComponent<Animator>();

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Painting Shake"))
                {
                    anim.SetTrigger("Shake");
                }

                heldObject.transform.parent = holdTransform;
                heldObject.transform.position = holdTransform.position;
                heldObject.transform.rotation = holdTransform.rotation;
                
                snapTag = "";
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
        leftEaselArea = false; // resets checking if have left easel area
        
        if (snapTag == "Wall Point")
        {
            // check player wants to lock their painting to the wall
            heldObject.GetComponent<Painting>().LockedToWall = true;
            
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
