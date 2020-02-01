using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
	public Color SelectedColor { get; set; }

    public List<GameObject> paintings = new List<GameObject>();
    public Transform easelHoldTransform;

	public static event Action<Color> OnChangeColour; 
    public static GameState gameState;
	private int _next;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			SelectedColor = Random.ColorHSV();
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
}