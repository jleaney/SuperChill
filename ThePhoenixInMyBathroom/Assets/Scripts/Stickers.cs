using System.Linq;
using UnityEngine;

public class Stickers : Tool
{
	public Texture2D[] StickerTextures;

	public override void Use(Vector3 pos, Vector3 normal)
	{
		var sprite = StickerTextures.GetRandom();
		var painting = GameManager.Instance.Painting;
		var localPos = painting.transform.InverseTransformPoint(pos);
		var final = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
		var newTex = new Texture2D(painting.Texture.width, painting.Texture.height);
		Graphics.CopyTexture(painting.Texture, newTex);
		painting.Texture = newTex.AddTextures(sprite, final);
        AudioManager.PlayPlacementSound(); // not working!?
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{

	}
}
