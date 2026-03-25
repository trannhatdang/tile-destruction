using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] Tile m_left;
	[SerializeField] Tile m_right;
	[SerializeField] Tile m_top;
	[SerializeField] Tile m_down;
	[SerializeField] Vector2 m_pos;

	[SerializeField] TileColor m_col;
	[SerializeField] FixedJoint2D m_joint;
	[SerializeField] SpriteRenderer m_spr;
	[SerializeField] TileObjectSO m_tileSO;
	
	[SerializeField] float m_hp = 100;

	private TileInfo tileInfo {
		get {
			return new TileInfo(
				m_left ? m_left.tileInfo : null,
				m_right ? m_right.tileInfo : null,
				m_top ? m_top.tileInfo : null,
				m_down ? m_down.tileInfo : null,
				m_pos
			);
		}
	}

	private bool m_isParent
	{
		get { return transform.parent == null; }
	}

	public TileObjectSO TileObject {
		get { return m_tileSO; }
		set { m_tileSO = value; }
	}

	public TileColor Color {
		get { return m_col; }
		set { m_col = value; }
	}

	public Vector2 Position {
		get { return m_pos; }
		set { m_pos = value; }
	}

	public Tile LeftTile {
		get {
			if(!m_left)
			{
				GameObject left = new GameObject();
				left.transform.parent = m_isParent ? transform : transform.parent;

				left.AddComponent<SpriteRenderer>();
				left.AddComponent<Rigidbody2D>();
				left.AddComponent<BoxCollider2D>();
				left.AddComponent<RelativeJoint2D>();

				m_left = left.AddComponent<Tile>();
			}

			return m_left;
		}
	}

	public Tile RightTile {
		get {
			if(!m_right)
			{
				GameObject right = new GameObject();
				right.transform.parent = m_isParent ? transform : transform.parent;

				right.AddComponent<SpriteRenderer>();
				right.AddComponent<Rigidbody2D>();
				right.AddComponent<BoxCollider2D>();
				right.AddComponent<RelativeJoint2D>();

				m_right = right.AddComponent<Tile>();
			}

			return m_right;
		}
	}

	public Tile TopTile {
		get {
			if(!m_top)
			{
				GameObject top = new GameObject();
				top.transform.parent = m_isParent ? transform : transform.parent;

				top.AddComponent<SpriteRenderer>();
				top.AddComponent<Rigidbody2D>();
				top.AddComponent<BoxCollider2D>();
				top.AddComponent<RelativeJoint2D>();

				m_top = top.AddComponent<Tile>();
			}

			return m_top;
		}
	}

	public Tile DownTile {
		get {
			if(!m_down)
			{
				GameObject down = new GameObject();
				down.transform.parent = m_isParent ? transform : transform.parent;

				down.AddComponent<SpriteRenderer>();
				down.AddComponent<Rigidbody2D>();
				down.AddComponent<BoxCollider2D>();
				down.AddComponent<RelativeJoint2D>();

				m_down = down.AddComponent<Tile>();
			}

			return m_down;
		}
	}

	void Start()
	{
		m_spr = gameObject.GetComponent<SpriteRenderer>();
		if(m_spr)
		{
			m_spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
		}
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

	void OnMouseUpAsButton()
	{
		Hit(25); //GameManager.PlayerDamage?
	}

	public void Hit(float damage)
	{
		m_hp -= damage;
	}

	public void SaveTile()
	{
		if(!m_isParent)
		{
			transform.parent.GetComponent<Tile>().SaveTile();
			return;
		}

		m_tileSO.HeadTile = new TileInfo(
			m_left ? m_left.tileInfo : null,
			m_right ? m_right.tileInfo : null,
			m_top ? m_top.tileInfo : null,
			m_down ? m_down.tileInfo : null,
			m_pos
		);

	}
}
