using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponChoice {
	BiggerSaw,
	FasterSaw,
	StrongerSaw,
	NewSaw
}

public class WeaponSelection : MonoBehaviour
{
	[SerializeField] UIManager m_uiManager;

	public void ChooseBig()
	{
		m_uiManager.WeaponPlace(WeaponChoice.BiggerSaw);
	}

	public void ChooseFast()
	{
		m_uiManager.WeaponPlace(WeaponChoice.FasterSaw);
	}

	public void ChooseStrong()
	{
		m_uiManager.WeaponPlace(WeaponChoice.StrongerSaw);
	}

	public void ChooseNew()
	{
		m_uiManager.WeaponPlace(WeaponChoice.NewSaw);
	}
}
