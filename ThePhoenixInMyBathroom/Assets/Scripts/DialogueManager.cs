using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Set up new dialogue sections by adding a string array (like the 'IntroDialogue' array below
    // Display dialogue can be called with the appropriate string array as an argument
    // this script should take care of the rest!
    // Player clicks Left Mouse Button to proceed through text

    public GameObject dialogueBox;
    private TextMeshProUGUI text;

    public bool AllowDialogue { get; set; }

    private int currentLine = 0;

    public bool DialogueActive { get; set; }

    public bool allowMouseInput = true;

    GameManager gameManager;

    private bool triggerEnd = false;

    private bool yesOrNo = false;

    public AudioClip dialogueSound;

    private void Start()
    {
        AllowDialogue = true;
        text = dialogueBox.transform.GetComponentInChildren<TextMeshProUGUI>();
        StartIntroDialogue();

        gameManager = FindObjectOfType<GameManager>();
    }

    public string[] IntroDialogue = new string[5]
    {
        "Ah, marvellous, you're here!!",
        "Just the person we needed to see.",
        "Our Van Goughs, Dalis, Picassos, and more have all been damaged.",
        "We need your unmatchable artistic talent to restore them to their original glory before we open tonight!",
        "I believe in you darling, you can do it!!"
    };

    public string[] FinishPaintingDialogue = new string[3]
    {
        "After we let the audience in you can't work on any more paintings",
        "Are you sure you're finished?",
        "*Y for yes, N for no! pop"
    };

    public string[] ExhibitionDialogue = new string[7]
    {
        "Alright, it's time. Let me just check on your work...",
        "Hmmm..",
        "Ooh...",
        "Ahh!",
        "Now THIS is art!",
        "We better let the hungry audience devour the art that you've so passionately baked.",
        "Well done. Who know, next year we may ask you to come back and restore the statue of David!"
    };

    public void DisplayDialogue(string[] dialogue)
    {
        dialogueBox.SetActive(true);
        UpdateText(dialogue);
        StartCoroutine(CheckInput(dialogue, false));
        DialogueActive = true;
    }

    private IEnumerator CheckInput(string[] dialogue, bool triggerEnd)
    {
        yield return new WaitForSeconds(0.01f);

        while(currentLine <= dialogue.Length && AllowDialogue && allowMouseInput)
        {
            if (Input.GetMouseButtonDown(0) && allowMouseInput)
            {
                UpdateText(dialogue);
                AudioManager.PlaySFXOneShot(dialogueSound);
            }

            yield return null;
        }

        if (triggerEnd)
        {
            gameManager.StartExhibition();
            EndDialogue();
        }

        if (allowMouseInput)
        {
            EndDialogue();
        }
    }

    private void UpdateText(string[] dialogue)
    {
        if (currentLine < dialogue.Length)
        {
            if (dialogue[currentLine].Contains("*"))
            {
                string[] splitText = dialogue[currentLine].Split('*');
                text.text = splitText[1];
                allowMouseInput = false;
                StartCoroutine(CheckCompleteExhibition());
            }

            else
            {
                text.text = dialogue[currentLine];
            }
        }
        
        currentLine++;
    }

    public void StartIntroDialogue()
    {
        DisplayDialogue(IntroDialogue);
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue End is being called");
        DialogueActive = false;
        // Untested!
        AllowDialogue = false;
        currentLine = 0;
        dialogueBox.SetActive(false); // hides dialogue box
        AllowDialogue = true;
    }

    private IEnumerator CheckCompleteExhibition()
    {
        while(!allowMouseInput)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (GameManager.inputEnabled)
                {
                    // Load ending sequence
                    gameManager.TriggerExhibition();
                    allowMouseInput = true;
                }
                
            }

            else if (Input.GetKeyDown(KeyCode.N))
            {
                if (GameManager.inputEnabled)
                {
                    EndDialogue();
                    allowMouseInput = true;
                }
            }

            yield return null;
        }
    }

    public void DisplayExhibitionDialogue()
    {
        dialogueBox.SetActive(true);
        UpdateText(ExhibitionDialogue);
        StartCoroutine(CheckInput(ExhibitionDialogue, true));
        DialogueActive = true;
    }
}
