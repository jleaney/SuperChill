using UnityEngine;
using System.Collections.Generic;

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
}