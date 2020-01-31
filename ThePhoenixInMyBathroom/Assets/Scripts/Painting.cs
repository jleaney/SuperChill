﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class Painting : MonoBehaviour
{
	public MeshRenderer MeshRenderer;
	public Texture Texture
	{
		get => MeshRenderer.material.mainTexture;
		set => MeshRenderer.material.mainTexture = value;
	}

	public void Start()
	{

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
