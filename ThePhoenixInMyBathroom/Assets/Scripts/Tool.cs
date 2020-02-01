﻿using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	public Texture2D CursorSprite;

    public AudioClip[] placementSounds;

	private void OnMouseDown()
	{
		print("Mouse Down!");
		Cursor.SetCursor(CursorSprite, Vector2.zero, CursorMode.Auto);
		var selectedTool = GameManager.Instance.SelectedTool;
		if (selectedTool != this)
		{
			selectedTool?.Deselect();
			GameManager.Instance.SelectedTool = this;
			Select();
		}
		else
		{
			Deselect();
			GameManager.Instance.SelectedTool = null;
		}
	}

	public void Select()
	{
		var material = GetComponent<MeshRenderer>().material;
		var color = material.GetColor("_BaseColor");
		color.a = 0.5f;
		material.SetColor("_BaseColor", color);
	}

	public void Deselect()
	{
		var material = GetComponent<MeshRenderer>().material;
		var color = material.GetColor("_BaseColor");
		color.a = 1.0f;
		material.SetColor("_BaseColor", color);
	}

	public abstract void Use(Vector3 pos, Vector3 normal);
	public abstract void Hold(Vector3 pos, Vector3 normal);
}