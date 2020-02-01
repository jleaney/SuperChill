using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PickupController : MonoBehaviour
{
	private GameObject heldObject;

	public Transform holdTransform; // transform for the painting to snap to while being held

	private bool holdingPainting = false;

	public Transform wallPoints;
	private List<Transform> snapPoints = new List<Transform>();
	public float minSnapDistance;
	private bool snapped = false; // true when painting is snapped to a point, but not yet confirmed to stay there
	public Transform easel; // Easel where the painting sits
	public bool leftEaselArea = false;
	public float minEaselDistance = 7;

	public GameObject currentPainting;
	public bool CurrentlyPainting { get; set; } // whether painting is currently happening

	public float minPickupDistance = 5;

	public AudioClip pickupSound;
	public AudioClip snapSound;
	public AudioClip confirmSnapSound;

	private GameManager gameManager;

	public DialogueManager dialogueManager;

	private bool allowExit;

	private void Start()
	{
		foreach (Transform child in wallPoints)
		{
			snapPoints.Add(child);
		}

		gameManager = FindObjectOfType<GameManager>();
		currentPainting = gameManager.SpawnPainting();
		GameManager.Instance.Painting = currentPainting.GetComponent<Painting>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.gameState == GameState.Painting)
		{
			// Checks if painting ISN'T being restored, and that the player is within the minimum distance to pick the painting up
			if (!CurrentlyPainting && Vector3.Distance(transform.position, currentPainting.transform.position) < minPickupDistance)
			{
				if (Input.GetMouseButtonDown(0) && GameManager.Instance.SelectedTool == null)
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

			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "Door")
					{
						if (!dialogueManager.DialogueActive)
							dialogueManager.DisplayDialogue(dialogueManager.FinishPaintingDialogue);
					}
				}
			}
		}

		else if (GameManager.gameState == GameState.Exhibition)
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "Door" && allowExit)
					{
						gameManager.ExitToMenu();
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

				AudioManager.PlaySFXOneShot(pickupSound);
			}

			else
			{
				heldObject = null;
			}
		}
	}

	private IEnumerator CheckSnapPoint()
	{
		while (heldObject != null)
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
				var dot = Vector3.Dot(Camera.main.transform.forward, (t.position - transform.position).normalized);
				var intersectWall = !Physics.Linecast(t.position, transform.position, LayerMask.GetMask("Environment"));

				if (Vector3.Distance(transform.position, t.position) < minSnapDistance && dot > 0.9f)
				{
					snapTag = t.tag;

					if (!snapped)
					{
						//AudioManager.PlaySFXOneShot(snapSound);
					}

					if (snapTag == "Easel")
					{
						if (leftEaselArea)
						{
							heldObject.transform.parent = t;
							heldObject.transform.DOMove(t.position, 0.2f);
							heldObject.transform.DORotateQuaternion(t.rotation, 0.1f);
							snapped = true;
							heldObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StopShake");

							break;
						}
					}

					else
					{
						heldObject.transform.parent = t;
						heldObject.transform.DOMove(t.position, 0.2f);
						heldObject.transform.DORotateQuaternion(t.rotation, 0.1f);
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
				heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdTransform.position, Time.smoothDeltaTime*10);
				heldObject.transform.rotation = Quaternion.Lerp(heldObject.transform.rotation, holdTransform.rotation, Time.smoothDeltaTime * 10);

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

		Debug.Log("No longer holder item!");
	}

	private void ConfirmSnap(string snapTag)
	{
		Transform snapPoint = heldObject.transform.parent;
		heldObject.transform.parent = null;
		snapPoints.Remove(snapPoint); // removes snapPoint from list of points that can be used
		leftEaselArea = false; // resets checking if have left easel area

		AudioManager.PlaySFXOneShot(confirmSnapSound);

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

		currentPainting = gameManager.SpawnPainting();

	}

	public IEnumerator AllowExit()
	{
		yield return new WaitForSeconds(5);
		allowExit = true;
	}
}
