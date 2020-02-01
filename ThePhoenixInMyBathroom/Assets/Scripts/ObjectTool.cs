using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectTool : Tool
{
	public GameObject[] Objects;
	public bool RandomiseScale = true;
	public ERotation Rotation = ERotation.Up;

	public override void Use(Vector3 pos, Vector3 normal)
	{
		var prefab = Objects.GetRandom();
		var painting = GameManager.Instance.Painting;
		var obj = Instantiate(prefab, pos, prefab.transform.rotation);
		SetRotation(normal, obj);
		obj.transform.SetParent(painting.transform);

		if (RandomiseScale)
			obj.transform.localScale *= Random.Range(0.9f, 1.1f);
	}

	private void SetRotation(Vector3 normal, GameObject obj)
	{
		switch (Rotation)
		{
			case ERotation.Up:
				obj.transform.up = normal;
				break;
			case ERotation.Down:
				obj.transform.up = -normal;
				break;
			case ERotation.Forward:
				obj.transform.forward = normal;
				break;
			case ERotation.Back:
				obj.transform.forward = -normal;
				break;
			case ERotation.Left:
				obj.transform.right = normal;
				break;
			case ERotation.Right:
				obj.transform.right = -normal;
				break;
		}

	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
	}
}
