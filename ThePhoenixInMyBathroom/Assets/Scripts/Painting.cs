﻿using UnityEngine;

public class Painting : MonoBehaviour
{
	public bool LockedToWall { get; set; } // whether painting has been locked to the wall
	public bool LockedToEasel { get; set; } // whether painting has been locked to the Easel

	public MeshRenderer MeshRenderer;
	public Rigidbody Rigidbody;
	public Texture2D[] Masks;

	public Texture Texture
	{
		get => MeshRenderer.material.mainTexture;
		set => MeshRenderer.material.mainTexture = value;
	}

	private void Start()
	{
		var mask = Masks.GetRandom();
		var tex = new Texture2D(mask.width, mask.height);
		Graphics.CopyTexture(mask, tex);
		tex = tex.ScaleTexture(Texture.width, Texture.height);
		var newTex = new Texture2D(Texture.width, Texture.height);
		Graphics.CopyTexture(Texture, newTex);
		Texture = newTex.AddTextures(tex, new Vector2(0.5f, 0.5f));
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButton(0))
		{
			var camera = Camera.main;
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit))
				GameManager.Instance?.SelectedTool?.Hold(hit.point, hit.normal);
		}
	}

	private void OnMouseUpAsButton()
	{
		var camera = Camera.main;
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit))
			GameManager.Instance?.SelectedTool?.Use(hit.point, hit.normal);
	}
}
