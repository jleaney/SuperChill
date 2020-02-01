using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comments : MonoBehaviour
{
    public GameObject chatBubble;
    public Sprite[] emojis;

    private bool showingComment = false;

    private Vector3 chatBubblePos;
    private Quaternion chatBubbleRot;

    public float minCommentDistance = 3;

    private void Start()
    {
        chatBubblePos = chatBubble.transform.localPosition;
        chatBubbleRot = chatBubble.transform.localRotation;
    }

    private void Update()
    {
        if (GameManager.gameState == GameState.Exhibition)
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Painting")
                {
                    if (!showingComment && Vector3.Distance(transform.position, hit.transform.position) < minCommentDistance)
                    {
                        showingComment = true;
                        ShowComment(hit.transform);
                    }
                }

                else
                {
                    HideComment();
                }
            }
        }
    }

    private void ShowComment(Transform painting)
    {
        // Move comment to relevant transform
        chatBubble.transform.parent = painting;
        chatBubble.transform.localPosition = chatBubblePos;
        chatBubble.transform.localRotation = chatBubbleRot;
        
        // Set random Emojis
        foreach (Transform child in chatBubble.transform.GetChild(0))
        {
            Sprite randomImage = emojis[Random.Range(0, emojis.Length)];
            child.GetComponent<Image>().sprite = randomImage;
        }

        // Turn on chat bubble
        // Insert animation
        chatBubble.SetActive(true);

    }

    private void HideComment()
    {
        showingComment = false;
        // Insert animatino
        chatBubble.SetActive(false);

    }
}
