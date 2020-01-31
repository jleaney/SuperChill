using UnityEngine;
using Random = System.Random;

public static class Utilities
{
	public static T GetRandom<T>(this T[] array)
	{
		var rand = new Random();
		return array[rand.Next(0, array.Length)];
	}

	public static Texture2D AddTextures(this Texture2D main, Texture2D sticker, Vector2 pos)
	{
		int startX = (int)(main.width * pos.x) - (sticker.width / 2);
		int startY = (int)(main.height * pos.y) - (sticker.height / 2);

		for (int x = 0; x < sticker.width; x++)
		{
			if (x + startX > main.width)
				break;

			for (int y = 0; y < sticker.height; y++)
			{
				if (y + startY > main.height)
					break;

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
