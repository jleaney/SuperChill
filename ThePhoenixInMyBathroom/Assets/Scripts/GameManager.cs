using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    Painting,
    Exhibition
}

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Painting Painting;
	public Tool SelectedTool { get; set; }

    public List<GameObject> paintings = new List<GameObject>();
    public Transform easelHoldTransform;

    public static GameState gameState;

    public static bool inputEnabled = true;

    public GameObject player;
    public Transform playerOrigin; // transform postion where player starts game and enters for exhibition
    public AudioClip successSting; // sound played when exhibition starts

    public Image fadePanel;
    public float fadeSpeed = 2;

    public DialogueManager dialogueManager;

    public ParticleSystem confetti;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameState = GameState.Menu;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameState = GameState.Painting;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameState = GameState.Exhibition;
        }
    }

    public GameObject SpawnPainting()
    {
        int paintingToSpawn = Random.Range(0, paintings.Count);

        return Instantiate(paintings[paintingToSpawn], easelHoldTransform.position, easelHoldTransform.rotation);
    }

    public void TriggerExhibition()
    {
        inputEnabled = false;
        gameState = GameState.Exhibition;
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, fadeSpeed).OnComplete(ResetPlayerPosition);
    }

    private void ResetPlayerPosition()
    {
        AudioManager.StartExhibition();

        player.transform.position = playerOrigin.position;
        player.transform.rotation = playerOrigin.rotation;

        // play exhibition dialogue
        dialogueManager.DisplayExhibitionDialogue();
    }

    public void StartExhibition()
    {
        Debug.Log("Exhibition starting!");
        confetti.Play(); // confetti raining from the ceiling
        AudioManager.PlaySFXOneShot(successSting);
        fadePanel.DOFade(0, fadeSpeed).OnComplete(() =>
            fadePanel.gameObject.SetActive(false));
        inputEnabled = true;
        
    }
}