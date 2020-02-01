using UnityEngine;

public class Tape : MonoBehaviour
{
	public LineRenderer LineRenderer;

	private void Start()
	{
		LineRenderer.startColor = GameManager.Instance.SelectedColor;
		LineRenderer.endColor = GameManager.Instance.SelectedColor;
	}
}
