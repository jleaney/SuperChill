using UnityEngine;

public class TapeTool : Tool
{
	public Tape TapePrefab;
	private Tape _currentTape;

	public override void Use(Vector3 pos, Vector3 normal)
	{
		if (_currentTape == null)
		{
			var paintingTransform = GameManager.Instance.Painting.transform;
			_currentTape = Instantiate(TapePrefab, pos, TapePrefab.transform.rotation);
			_currentTape.transform.forward = -normal;
			_currentTape.transform.SetParent(paintingTransform);
			_currentTape.LineRenderer.enabled = false;
			_currentTape.LineRenderer.SetPosition(0, Vector3.zero);
			_currentTape.LineRenderer.useWorldSpace = false;
		}
		else
		{
			_currentTape.LineRenderer.SetPosition(1, _currentTape.transform.InverseTransformPoint(pos));
			_currentTape.LineRenderer.enabled = true;
			_currentTape = null;
		}
	}

	public override void Hold(Vector3 pos, Vector3 normal)
	{
	}
}
