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

	private void OnMouseOver()
	{
		if (Input.GetMouseButton(0))
		{
			var camera = Camera.main;
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit))
				GameManager.Instance?.SelectedTool?.Hold(hit.point);
		}
	}

	private void OnMouseUpAsButton()
	{
		var camera = Camera.main;
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit))
			GameManager.Instance?.SelectedTool?.Use(hit.point);
	}
}
