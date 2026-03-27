using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponSelectionButton : MonoBehaviour
{
	[SerializeField] UIManager m_uiManager;
	[SerializeField] Button m_btn;
	[SerializeField] Image m_img;
	[SerializeField] WeaponChoice m_choice;

	void Start()
	{
		m_btn = GetComponent<Button>();
	}

	void OnEnable()
	{
		m_choice = (WeaponChoice)(Random.Range(0, 4));

		if(m_img == null)
		{
			m_img = GetComponent<Image>();

		}

		m_img.sprite = Constants.Instance.GetWeaponPlacementSprite(m_choice);
	}

	public void OnClick()
	{
		m_uiManager.WeaponPlace(m_choice);
	}

	public void SetChoice(WeaponChoice choice)
	{
		m_choice = choice;
		m_img.sprite = Constants.Instance.GetWeaponPlacementSprite(m_choice);
	}
}
