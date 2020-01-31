using UnityEngine;

public class Sticker : MonoBehaviour
{
	public SpriteRenderer Renderer;
	public Sprite Sprite;
	
	public void SetSprite(Sprite sprite)
	{
		Renderer.sprite = sprite;
	}
}
