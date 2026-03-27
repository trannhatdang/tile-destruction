using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum GameState {
	Playing,
	Paused,
	WeaponSelection,
	WeaponPlacement,
	Loading,
}

public class UIManager : MonoBehaviour
{
	[SerializeField] GameManager m_gameManager;
	[SerializeField] Button m_pauseButton;
	[SerializeField] GameObject m_weaponSelection;
	[SerializeField] GameObject m_weaponPlacement;
	[SerializeField] GameObject m_loadingScreen;

	private WeaponPlacement m_placement;
	private GameState m_oldState;
	private GameState m_state;

	public GameState State {
		get { return m_state; }
		set { m_state = value; }
	}

	void Start()
	{
		m_placement = m_weaponPlacement.GetComponent<WeaponPlacement>();
	}

	void Update()
	{
		m_weaponSelection.SetActive(m_state == GameState.WeaponSelection);
		m_weaponPlacement.SetActive(m_state == GameState.WeaponPlacement);
		m_loadingScreen.SetActive(m_state == GameState.Loading);

		if(m_state == GameState.Paused)
		{
			m_gameManager.Pause();
		}
	}

	public void Pause()
	{
		m_oldState = m_state;
		m_state = GameState.Paused;
	}

	public void Unpause()
	{
		m_state = m_oldState;
	}

	public void WeaponSelect()
	{
		m_state = GameState.WeaponSelection;
	}

	public void WeaponPlace(WeaponChoice choice)
	{
		m_state = GameState.WeaponPlacement;
		m_placement.Set(choice);
	}

	public void Resume()
	{
		m_state = GameState.Playing;
	}
}
