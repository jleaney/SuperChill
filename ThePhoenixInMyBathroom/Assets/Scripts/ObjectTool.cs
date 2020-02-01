using UnityEngine;

public class ObjectTool : Tool
{
	public GameObject[] Objects;
	public override void Use(Vector3 pos, Vector3 normal)
	{
		print("Use object");
		var prefab = Objects.GetRandom();
		var painting = GameManager.Instance.Painting;
		var obj = Instantiate(prefab, pos, prefab.transform.rotation);
		obj.transform.forward = normal;
		obj.transform.SetParent(painting.transform);
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
	}
}
