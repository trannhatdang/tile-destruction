using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] Transform m_outPos;
	[SerializeField] Transform m_inPos;
	[SerializeField] TMP_Text m_text;

	void Start()
	{
		m_text = GetComponentInChildren<TMP_Text>();
	}

	public void MoveIn()
	{
		m_text.text = "Loading Next Level...";
		transform.position = m_inPos.position;

		transform.DOMove(new Vector3(-1, 0, 0), 1f).SetUpdate(true);
	}

	public void MoveOut()
	{
		m_text.text = "Loading Next Level...";
		transform.DOMove(m_outPos.position, 0.5f).SetUpdate(true);
	}

	public void SetActive(bool val)
	{
		m_text.text = "Loading Next Level...";
		gameObject.SetActive(val);
	}
}
