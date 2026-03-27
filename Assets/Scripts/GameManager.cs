using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerAttack m_playerAttack;
	[SerializeField] UIManager m_uiManager;
	[SerializeField] Transform m_spawnPosition;
	[SerializeField] LevelSO m_levelInfo;
	[SerializeField] Abyss m_abyss;
	[SerializeField] TopLimit m_topLimit;
	[SerializeField] float m_timeSinceLastDrop;
	[SerializeField] int m_objIndex;
	[SerializeField] int m_xp;
	[SerializeField] int m_currLevel = 1;

	private List<TileObjectSO> m_objects;
	private List<float> m_timings;

	private List<GameObject> m_spawnedObjects;

	void Start()
	{
		m_spawnedObjects = new List<GameObject>();
		m_objects = m_levelInfo.Objects;
		m_timings = m_levelInfo.Timings;

		m_abyss.onTileEnter += onTileEnter;

		//LOADING SCREEN LOGIC HERE
		for(int i = 0; i < m_objects.Count; ++i)
		{
			var gb = Instantiate(m_objects[i].Prefab, m_spawnPosition.position, Quaternion.identity, transform);
			gb.SetActive(false);
			m_spawnedObjects.Add(gb);
		}

		m_uiManager.WeaponSelect(true, WeaponChoice.NewSaw, WeaponChoice.NewSaw);
	}
	
	void Update()
	{
		if(m_objIndex >= m_objects.Count) m_objIndex = 0;

		m_timeSinceLastDrop += Time.deltaTime;
		
		if(m_timeSinceLastDrop > m_timings[m_objIndex] && !m_topLimit.IsHittingLimit)
		{
			m_spawnedObjects[m_objIndex++].SetActive(true);
			m_timeSinceLastDrop = 0.0f;
		}

		if(m_xp > m_levelInfo.PassLevelXP)
		{
			nextLevel();
		}

		if(m_xp > m_levelInfo.LevelUpXP * m_currLevel)
		{
			m_uiManager.WeaponSelect();
			m_currLevel++;
		}

	}

	void onTileEnter(Tile tile)
	{
		m_xp += Constants.Instance.XP;
		Destroy(tile.gameObject);
	}

	void nextLevel()
	{

	}

	public void Pause()
	{
		m_playerAttack.Pause();
		Time.timeScale = 0.0f;
	}

	public void Unpause()
	{
		m_playerAttack.Unpause();
		Time.timeScale = 1.0f;
	}
}
