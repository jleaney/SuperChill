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

    private void Start()
    {
        AllowDialogue = true;
        text = dialogueBox.transform.GetComponentInChildren<TextMeshProUGUI>();
        StartIntroDialogue();
    }

    public string[] IntroDialogue = new string[5]
    {
        "Ah, marvellous, you're here!!",
        "Just the person we needed to see.",
        "Our Van Goughs, Dalis, Picassos, and more have all been damaged.",
        "We need your unmatchable artistic talent to restore them to their original glory before we open tonight!",
        "I believe in you darling, you can do it!!"
    };

    public void DisplayDialogue(string[] dialogue)
    {
        dialogueBox.SetActive(true);
        UpdateText(dialogue);
        StartCoroutine(CheckInput(dialogue));
    }

    private IEnumerator CheckInput(string[] dialogue)
    {
        while(currentLine <= dialogue.Length && AllowDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpdateText(dialogue);
            }

            yield return null;
        }

        currentLine = 0;
        dialogueBox.SetActive(false); // hides dialogue box
    }

    private void UpdateText(string[] dialogue)
    {
        if (currentLine < dialogue.Length)
        {
            text.text = IntroDialogue[currentLine];
        }
        
        currentLine++;
    }

    public void StartIntroDialogue()
    {
        DisplayDialogue(IntroDialogue);
    }

    public void EndDialogue()
    {
        // Untested!
        AllowDialogue = false;
        currentLine = 0;
        AllowDialogue = true;
    }
}
