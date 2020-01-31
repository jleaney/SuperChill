using System;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	public Texture2D CursorSprite;

	private void OnMouseUpAsButton()
	{
		Cursor.SetCursor(CursorSprite, Vector2.zero, CursorMode.Auto);
		GameManager.Instance.SelectedTool = this;
	}

	public abstract void Use(Vector3 pos);
}