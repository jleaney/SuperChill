using UnityEngine;

public class PaintTool : Tool
{
	public Texture2D Brush;


	public override void Use(Vector3 pos)
	{
		var painting = GameManager.Instance.Painting;
		var localPos = painting.transform.InverseTransformPoint(pos);
		var final = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
		var newTex = new Texture2D(painting.Texture.width, painting.Texture.height);
		Graphics.CopyTexture(painting.Texture, newTex);
		painting.Texture = newTex.AddTextures(Brush, final);
	}

	public override void Hold(Vector3 pos)
	{
		Use(pos);
	}
}
