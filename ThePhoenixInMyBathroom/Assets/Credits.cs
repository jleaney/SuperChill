using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Credits : MonoBehaviour
{
	public Transform CreditsTarget;
	private Vector3 _startPos;
	private Quaternion _startRot;
	public GameObject MainUI;
	public GameObject CreditsUI;
	public Camera Camera;

	void Start()
	{
		_startPos = Camera.transform.position;
		_startRot = Camera.transform.rotation;
	}

	public void MoveToCredits()
	{
		MainUI.SetActive(false);
		Camera.transform.DOMove(CreditsTarget.transform.position, 2.0f).OnComplete(() => CreditsUI.SetActive(true));
		Camera.transform.DORotateQuaternion(CreditsTarget.transform.rotation, 2.0f);
	}

	public void MoveToRoot()
	{
		CreditsUI.SetActive(false);
		Camera.transform.DOMove(_startPos, 2.0f).OnComplete(() => MainUI.SetActive(true));
		Camera.transform.DORotateQuaternion(_startRot, 2.0f);
	}
}
