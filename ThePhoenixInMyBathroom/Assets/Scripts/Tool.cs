using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	public Sprite Icon;

	private void OnMouseDown()
	{
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
		GameManager.Instance.ToolIcon.enabled = true;
		GameManager.Instance.ToolIcon.sprite = Icon;
		print("bam!");
	}

	public void Deselect()
	{
		GameManager.Instance.ToolIcon.enabled = false;
	}

	public abstract void Use(Vector3 pos, Vector3 normal);
	public abstract void Hold(Vector3 pos, Vector3 normal);
}