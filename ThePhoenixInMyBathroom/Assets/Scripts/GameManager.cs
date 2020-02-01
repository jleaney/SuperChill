using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public Color[] ColourPalette;

	public Color SelectedColor => ColourPalette[_curColorIndex];

	public Color PreviousColor => _curColorIndex <= 0 ? ColourPalette[ColourPalette.Length - 1] : ColourPalette[_curColorIndex - 1];

	public Color NextColor => _curColorIndex >= ColourPalette.Length ? ColourPalette[0] : ColourPalette[_curColorIndex + 1];

	public List<GameObject> paintings = new List<GameObject>();
    public Transform easelHoldTransform;

	public static event Action<Color> OnChangeColour; 
    public static GameState gameState;
	private int _next;
	private int _curColorIndex;

    public static bool inputEnabled = true;

    public GameObject player;
    public Transform playerOrigin; // transform postion where player starts game and enters for exhibition
    public AudioClip successSting; // sound played when exhibition starts

    public Image fadePanel;
    public float fadeSpeed = 2;

    public DialogueManager dialogueManager;

	public Image ToolIcon;

    public ParticleSystem confetti;

    public GameObject paintingTools;

    public int menuScene = 0;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		_curColorIndex = 0;
		OnChangeColour?.Invoke(SelectedColor);

        gameState = GameState.Painting;
	}

    private void Update()
    {
		if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
		{
			++_curColorIndex;
			if (_curColorIndex >= ColourPalette.Length)
				_curColorIndex = 0;

			OnChangeColour?.Invoke(SelectedColor);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
		{
			--_curColorIndex;
			if (_curColorIndex < 0)
				_curColorIndex = ColourPalette.Length - 1;

			OnChangeColour?.Invoke(SelectedColor);
		}

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
        var paintingToSpawn = paintings[_next];
		++_next;
		if (_next >= paintings.Count)
			_next = 0;

        return Instantiate(paintingToSpawn, easelHoldTransform.position, easelHoldTransform.rotation);
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

        paintingTools.SetActive(false);
        player.GetComponent<PickupController>().currentPainting.SetActive(false);
    }

    public void StartExhibition()
    {
        Debug.Log("Exhibition starting!");
        confetti.Play(); // confetti raining from the ceiling
        AudioManager.PlaySFXOneShot(successSting);
        fadePanel.DOFade(0, fadeSpeed).OnComplete(() =>
            fadePanel.gameObject.SetActive(false));
        inputEnabled = true;

        StartCoroutine(player.GetComponent<PickupController>().AllowExit());

        
    }

    public void ExitToMenu()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, fadeSpeed).OnComplete(() =>
            SceneManager.LoadScene(menuScene));
    }
    
}