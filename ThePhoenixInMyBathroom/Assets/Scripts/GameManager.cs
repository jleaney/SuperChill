using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Painting Painting;
	public Tool SelectedTool { get; set; }

    public List<GameObject> paintings = new List<GameObject>();
    public Transform easelHoldTransform;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

    public GameObject SpawnPainting()
    {
        int paintingToSpawn = Random.Range(0, paintings.Count);

        return Instantiate(paintings[paintingToSpawn], easelHoldTransform.position, easelHoldTransform.rotation);
    }
}