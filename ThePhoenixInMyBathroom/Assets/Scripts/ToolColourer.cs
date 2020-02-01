using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToolColourer : MonoBehaviour
{
	private MeshRenderer _renderer;

	private void Start()
	{
		_renderer = GetComponent<MeshRenderer>();
		GameManager.OnChangeColour += UpdateColour;
	}

	private void UpdateColour(Color colour)
	{
		var mat = _renderer.materials.FirstOrDefault(x => x.name.Contains("colour"));
		if (mat == null)
			return;

		mat.SetColor("_BaseColor", colour);
	}
}
