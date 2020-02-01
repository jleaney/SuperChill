using UnityEngine;

public class ObjectTool : Tool
{
	public GameObject[] Objects;
	public bool RandomiseScale = true;

	public override void Use(Vector3 pos, Vector3 normal)
	{
		var prefab = Objects.GetRandom();
		var painting = GameManager.Instance.Painting;
		var obj = Instantiate(prefab, pos, prefab.transform.rotation);
		obj.transform.up = normal;
		obj.transform.SetParent(painting.transform);

		if (RandomiseScale)
			obj.transform.localScale *= Random.Range(0.9f, 1.1f);
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
	}
}
