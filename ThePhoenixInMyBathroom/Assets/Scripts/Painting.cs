using UnityEngine;

public class Painting : MonoBehaviour
{
	public MeshRenderer MeshRenderer;
	public Rigidbody Rigidbody;

	public Texture Texture
	{
		get => MeshRenderer.material.mainTexture;
		set => MeshRenderer.material.mainTexture = value;
	}

	private void OnMouseUpAsButton()
	{
		var camera = Camera.main;
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit))
		{
			if (GameManager.Instance.SelectedTool != null)
				GameManager.Instance.SelectedTool.Use(hit.point);
		}
	}
}
