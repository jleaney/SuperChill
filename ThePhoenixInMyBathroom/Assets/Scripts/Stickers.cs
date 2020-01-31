using System.Linq;
using UnityEngine;

public class Stickers : Tool
{
	public Texture2D[] StickerTextures;
	public Sticker StickerPrefab;

	public override void Use(Vector3 pos)
	{
		var sprite = StickerTextures.GetRandom();
		var painting = GameManager.Instance.Painting;
		var localPos = painting.transform.InverseTransformPoint(pos);
		Vector2 final = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
		var newTex = new Texture2D(painting.Texture.width, painting.Texture.height);
		Graphics.CopyTexture(painting.Texture, newTex);
		painting.Texture = CombineTextures(newTex, sprite, final);
	}

	public Texture2D CombineTextures(Texture2D main, Texture2D sticker, Vector2 pos)
	{

		int startX = (int)(main.width * pos.x);
		int startY = (int)(main.height * pos.y);

		for (int x = 0; x < sticker.width; x++)
		{
			for (int y = 0; y < sticker.height; y++)
			{
				Color mainColor = main.GetPixel(x + startX, y + startY);
				Color stickerColor = sticker.GetPixel(x, y);
				Color finalColor = Color.Lerp(mainColor, stickerColor, stickerColor.a / 1.0f);
				main.SetPixel(x + startX, y + startY, finalColor);
			}
		}

		main.Apply();
		return main;
	}
}
