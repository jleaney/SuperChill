﻿using UnityEngine;

public class GlitterTool : Tool
{
	public Texture2D Brush;
	public Texture2D[] RotatedBrushes;

	private void Start()
	{
		RotatedBrushes = new Texture2D[]
		{
			Brush.RotateTexture(false),
			Brush.RotateTexture(true),
			Brush.RotateTexture(true).RotateTexture(true)
		};
	}

	public override void Use(Vector3 pos, Vector3 normal)
	{
		var brush = RotatedBrushes.GetRandom();
		var painting = GameManager.Instance.Painting;
		var localPos = painting.transform.InverseTransformPoint(pos);
		var final = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
		var glitter = painting.MeshRenderer.material.GetTexture("_Mask");
		painting.MeshRenderer.material.SetTexture("_Mask", ((Texture2D)glitter).AddTextures(brush, final));
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
		Use(pos, normal);
	}
}
