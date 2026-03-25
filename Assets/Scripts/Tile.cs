using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] Tile m_left;
	[SerializeField] Tile m_right;
	[SerializeField] Tile m_top;
	[SerializeField] Tile m_bottom;
	[SerializeField] TileColor m_col;
	[SerializeField] FixedJoint2D m_joint;
	[SerializeField] SpriteRenderer m_spr;
	[SerializeField] TileSO m_tileSO;
	
	[SerializeField] float m_hp = 100;

	private bool m_isParent
	{
		get { return transform.parent == null; }
	}

	public TileSO TileInfo {
		get { return m_tileSO; }
		set { m_tileSO = value; }
	}

	public TileColor Color {
		get { return m_col; }
		set { m_col = value; }
	}

	void Start()
	{
		m_spr = gameObject.AddComponent<SpriteRenderer>();
		SetColor(m_col);

		gameObject.AddComponent<BoxCollider2D>();
		gameObject.AddComponent<Rigidbody2D>();
		m_joint = gameObject.AddComponent<FixedJoint2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(m_hp <= 0 && !m_isParent)
		{
			transform.parent = transform.parent.parent;
			m_joint.enabled = false;
		}
	}

	public void Hit(float damage)
	{
		m_hp -= damage;
	}

	void OnMouseUpAsButton()
	{
		Hit(25); //GameManager.PlayerDamage?
	}

	Tile GetLeft()
	{
		if(!m_left)
		{
			GameObject left = new GameObject();
			left.transform.parent = m_isParent ? transform : transform.parent;

			m_left = left.AddComponent<Tile>();
		}

		return m_left;
	}

	Tile GetRight()
	{
		if(!m_right)
		{
			GameObject right = new GameObject();
			right.transform.parent = m_isParent ? transform : transform.parent;

			m_right = right.AddComponent<Tile>();
		}

		return m_right;
	}

	Tile GetTop()
	{
		if(!m_top)
		{
			GameObject top = new GameObject();
			top.transform.parent = m_isParent ? transform : transform.parent;

			m_top = top.AddComponent<Tile>();
		}

		return m_top;
	}

	Tile GetDown()
	{
		if(!m_bottom)
		{
			GameObject bottom = new GameObject();
			bottom.transform.parent = m_isParent ? transform : transform.parent;

			m_bottom = bottom.AddComponent<Tile>();
		}

		return m_bottom;
	}
}
