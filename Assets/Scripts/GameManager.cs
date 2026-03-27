using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerAttack m_playerAttack;
	[SerializeField] UIManager m_uiManager;
	[SerializeField] Transform m_spawnPosition;
	[SerializeField] LevelSO m_levelInfo;
	[SerializeField] Abyss m_abyss;
	[SerializeField] TopLimit m_topLimit;
	[SerializeField] GameObject m_sawSlots;
	[SerializeField] GameObject m_platforms;
	[SerializeField] Slider m_levelUpSlider;
	[SerializeField] Slider m_passLevelSlider;
	[SerializeField] float m_timeSinceLastDrop;
	[SerializeField] int m_objIndex;
	[SerializeField] int m_xp;
	[SerializeField] int m_currLevel = 1;
	[SerializeField] int m_k = 4;
	bool m_sceneLoading = false;

	private List<TileObjectSO> m_objects;
	private List<float> m_timings;

	private List<GameObject> m_spawnedObjects;

	async void Start()
	{
		m_spawnedObjects = new List<GameObject>();
		m_objects = m_levelInfo.Objects;
		m_timings = m_levelInfo.Timings;

		m_abyss.onTileEnter += onTileEnter;

		Pause();
		//LOADING SCREEN LOGIC HERE
		
		for(int k = 0; k < m_k; ++k)
		{
			for(int i = 0; i < m_objects.Count; ++i)
			{
				var gb = Instantiate(m_objects[i].Prefab, m_spawnPosition.position, Quaternion.identity, transform);
				gb.SetActive(false);
				m_spawnedObjects.Add(gb);
			}
		}

		await UniTask.Delay(TimeSpan.FromSeconds(1), true);
		m_uiManager.LoadingScreen(false);
		await UniTask.Delay(TimeSpan.FromSeconds(1f), true);
		m_sawSlots.SetActive(true);
		if(m_platforms)
		{
			m_platforms.SetActive(true);
		}
		m_levelUpSlider.gameObject.SetActive(true);
		m_passLevelSlider.gameObject.SetActive(true);
		Unpause();

		m_uiManager.WeaponSelect(true, WeaponChoice.NewSaw, WeaponChoice.NewSaw);
	}
	
	async void Update()
	{
		if(m_sceneLoading) return;

		m_levelUpSlider.value = ((m_xp * 1.0f) / m_levelInfo.LevelUpXP);
		m_passLevelSlider.value = ((m_xp * 1.0f) / m_levelInfo.PassLevelXP);

		if(m_xp >= m_levelInfo.PassLevelXP)
		{
			m_sawSlots.SetActive(false);
			m_uiManager.LoadingScreen(true);
			await UniTask.Delay(TimeSpan.FromSeconds(3), true);
			m_sceneLoading = true;
			NextLevel();
		};

		if(Time.timeScale != 0.0f && m_objIndex < 4 * m_timings.Count)
		{
			m_timeSinceLastDrop += Time.deltaTime;
			
			if(m_timeSinceLastDrop > m_timings[m_objIndex % m_timings.Count] && !m_topLimit.IsHittingLimit)
			{
				m_spawnedObjects[m_objIndex++].SetActive(true);
				m_timeSinceLastDrop = 0.0f;
			}
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

	void NextLevel()
	{
		StartCoroutine(LoadSceneAsync());
	}

	IEnumerator LoadSceneAsync()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
		    yield return null;
		}
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
