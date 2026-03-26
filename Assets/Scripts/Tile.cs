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
	private TileInfo m_tileInfo;
	
	[SerializeField] float m_hp = 100;

	private List<Tile> m_visitedTiles = new List<Tile>();

	private TileInfo tileInfo {
		get {
			if(m_tileInfo == null)
			{
				m_tileInfo = new TileInfo(
					m_col,
					m_pos
				);
			}

			return m_tileInfo;
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
		set {
			m_col = value;

			if(!m_spr)
			{
				m_spr = GetComponent<SpriteRenderer>();
			}
			m_spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
		}
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
				left.tag = "Tile";
				left.name = gameObject.name;
				left.transform.parent = m_isParent ? transform : transform.parent;

				var spr = left.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				left.AddComponent<Rigidbody2D>();
				left.AddComponent<BoxCollider2D>();

				var joint = left.AddComponent<FixedJoint2D>();
				joint.connectedBody = GetComponent<Rigidbody2D>();

				m_left = left.AddComponent<Tile>();
				m_left.TileObject = m_tileSO;
				m_left.m_pos = m_pos + new Vector2(-1, 0);
				m_left.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_left.m_pos);
				m_left.m_col = m_col;

				floodTilesWithNewTile(m_left);
				m_left.m_right = this;
			}

			return m_left;
		}
	}

	public Tile RightTile {
		get {
			if(!m_right)
			{
				GameObject right = new GameObject();
				right.tag = "Tile";
				right.name = gameObject.name;
				right.transform.parent = m_isParent ? transform : transform.parent;

				var spr = right.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				right.AddComponent<Rigidbody2D>();
				right.AddComponent<BoxCollider2D>();
				var joint = right.AddComponent<FixedJoint2D>();
				joint.connectedBody = GetComponent<Rigidbody2D>();

				m_right = right.AddComponent<Tile>();
				m_right.TileObject = m_tileSO;
				m_right.m_pos = m_pos + new Vector2(1, 0);
				m_right.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_right.m_pos);
				m_right.m_col = m_col;

				floodTilesWithNewTile(m_right);
				m_right.m_left = this;
			}

			return m_right;
		}
	}

	public Tile TopTile {
		get {
			if(!m_top)
			{
				GameObject top = new GameObject();
				top.tag = "Tile";
				top.name = gameObject.name;
				top.transform.parent = m_isParent ? transform : transform.parent;

				var spr = top.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				top.AddComponent<Rigidbody2D>();
				top.AddComponent<BoxCollider2D>();
				var joint = top.AddComponent<FixedJoint2D>();
				joint.connectedBody = GetComponent<Rigidbody2D>();

				m_top = top.AddComponent<Tile>();
				m_top.TileObject = m_tileSO;
				m_top.m_pos = m_pos + new Vector2(0, 1);
				m_top.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_top.m_pos);
				m_top.m_col = m_col;

				floodTilesWithNewTile(m_top);
				m_top.m_down = this;
			}

			return m_top;
		}
	}

	public Tile DownTile {
		get {
			if(!m_down)
			{
				GameObject down = new GameObject();
				down.tag = "Tile";
				down.name = gameObject.name;
				down.transform.parent = m_isParent ? transform : transform.parent;

				var spr = down.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				down.AddComponent<Rigidbody2D>();
				down.AddComponent<BoxCollider2D>();

				var joint = down.AddComponent<FixedJoint2D>();
				joint.connectedBody = GetComponent<Rigidbody2D>();

				m_down = down.AddComponent<Tile>();
				m_down.TileObject = m_tileSO;
				m_down.m_pos = m_pos + new Vector2(0, -1);
				m_down.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_down.m_pos);
				m_down.m_col = m_col;

				floodTilesWithNewTile(m_down);
				m_down.m_top = this;
			}

			return m_down;
		}
	}

	void floodTilesWithNewTile(Tile tile)
	{
		if(m_left)
		{
			m_left.receiveNewTile(tile);
		}

		if(m_right)
		{
			m_right.receiveNewTile(tile);
		}
		
		if(m_top)
		{
			m_top.receiveNewTile(tile);
		}

		if(m_down)
		{
			m_down.receiveNewTile(tile);
		}
	}

	void receiveNewTile(Tile tile)
	{
		if(m_visitedTiles.Contains(tile))
		{
			return;
		}

		m_visitedTiles.Add(tile);

		if(tile.m_pos.x == m_pos.x + 1 && tile.m_pos.y == m_pos.y)
		{
			m_right = tile;
			tile.m_left = this;
		}
		else if(tile.m_pos.x == m_pos.x - 1 && tile.m_pos.y == m_pos.y)
		{
			m_left = tile;
			tile.m_right = this;
		}
		else if(tile.m_pos.y == m_pos.y + 1 && tile.m_pos.x == m_pos.x)
		{
			m_top = tile;
			tile.m_down = this;
		}
		else if(tile.m_pos.y == m_pos.y - 1 && tile.m_pos.x == m_pos.x)
		{
			m_down = tile;
			tile.m_top = this;
		}
		floodTilesWithNewTile(tile);
	}

	void Start()
	{
		m_spr = gameObject.GetComponent<SpriteRenderer>();
		if(m_spr)
		{
			m_spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
		}

		m_joint = GetComponent<FixedJoint2D>();
		m_joint.enabled = !m_isParent;
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

	public void Remove()
	{

	}

	public Tile GetLeftTile()
	{
		return m_left;
	}

	public Tile GetRightTile()
	{
		return m_right;
	}

	public Tile GetTopTile()
	{
		return m_top;
	}

	public Tile GetDownTile()
	{
		return m_down;
	}
}
