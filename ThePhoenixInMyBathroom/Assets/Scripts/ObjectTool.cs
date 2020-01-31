using UnityEngine;

public class ObjectTool : Tool
{
	public GameObject[] Objects;
	public override void Use(Vector3 pos)
	{
		var prefab = Objects.GetRandom();
		var painting = GameManager.Instance.Painting;
		var obj = Instantiate(prefab, pos, prefab.transform.rotation);
	}
}
