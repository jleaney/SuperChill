using UnityEngine;

public class PaintTool : Tool
{
	public Texture2D Brush;
	public Texture2D[] RotatedBrushes;
	private Texture2D _original;

	private void Start()
	{
		RotatedBrushes = new Texture2D[]
		{
			Brush.RotateTexture(false),
			Brush.RotateTexture(true),
			Brush.RotateTexture(true).RotateTexture(true)
		};

		GameManager.OnChangeColour += UpdateColours;
	}

	private void UpdateColours(Color colour)
	{
		Brush = Brush.Tint(colour);
		for (var i = 0; i < RotatedBrushes.Length; i++) RotatedBrushes[i] = RotatedBrushes[i].Tint(colour);
	}

	public override void Use(Vector3 pos, Vector3 normal)
	{
		var brush = RotatedBrushes.GetRandom();
		var painting = GameManager.Instance.Painting;
		var localPos = painting.transform.InverseTransformPoint(pos);
		var final = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
		painting.Texture = ((Texture2D)painting.Texture).AddTextures(brush, final);
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
		Use(pos, normal);
	}
}
