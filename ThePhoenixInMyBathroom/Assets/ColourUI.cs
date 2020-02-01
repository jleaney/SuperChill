using UnityEngine;
using UnityEngine.UI;

public class ColourUI : MonoBehaviour
{
	public Image Previous;
	public Image Current;
	public Image Next;

	private void Start()
	{
		GameManager.OnChangeColour += UpdateColours;
	}

	private void UpdateColours(Color colour)
	{
		var prev = GameManager.Instance.PreviousColor;
		var next = GameManager.Instance.NextColor;
		Previous.color = new Color(prev.r, prev.g, prev.b, 0.5f);
		Next.color = new Color(next.r, next.g, next.b, 0.5f);
		Current.color = colour;
	}
}
