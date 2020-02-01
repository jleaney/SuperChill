using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	public Texture2D CursorSprite;

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
	}

	public void Deselect()
	{
	}

	public abstract void Use(Vector3 pos, Vector3 normal);
	public abstract void Hold(Vector3 pos, Vector3 normal);
}