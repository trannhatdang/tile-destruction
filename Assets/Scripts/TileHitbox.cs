using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHitbox : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] Tile m_tile;
	void Start()
	{
		m_tile = GetComponentInParent<Tile>();

		if(!m_tile)
		{
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(!m_tile)
		{
			Destroy(this.gameObject);
		}
	}

	void OnMouseDown()
	{
		Debug.Log("hi");
	}

	void OnMouseUpAsButton()
	{
		Debug.Log("hi");
		if(!m_tile)
		{
			Destroy(this.gameObject);
		}

		Hit();
	}

	public void Hit(float damage = 10.0f)
	{
		if(!m_tile)
		{
			Destroy(this.gameObject);
		}

		m_tile.Hit(damage);
	}
}
