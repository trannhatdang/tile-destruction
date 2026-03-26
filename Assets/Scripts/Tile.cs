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
	[SerializeField] FixedJoint2D m_leftJoint;
	[SerializeField] FixedJoint2D m_rightJoint;
	[SerializeField] FixedJoint2D m_topJoint;
	[SerializeField] FixedJoint2D m_downJoint;
	[SerializeField] Vector2 m_pos;
	[SerializeField] bool m_isParent;

	[SerializeField] TileColor m_col;
	[SerializeField] SpriteRenderer m_spr;
	[SerializeField] TileObjectSO m_tileSO;
	[SerializeField] Rigidbody2D m_rb;
	
	[SerializeField] float m_hp = 100;

	private List<Tile> m_visitedTiles = new List<Tile>();

	public FixedJoint2D LeftJoint {
		get { return m_leftJoint; }
	}

	public FixedJoint2D RightJoint {
		get { return m_rightJoint; }
	}

	public FixedJoint2D TopJoint {
		get { return m_topJoint; }
	}

	public FixedJoint2D DownJoint {
		get { return m_downJoint; }
	}

	private TileInfo m_tileInfo;
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

	// public bool IsParent
	// {
	// 	get { return m_isParent; }
	// 	set { m_isParent = value; }
	// }

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
				left.transform.parent = transform.parent;

				var spr = left.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				var rb = left.AddComponent<Rigidbody2D>();
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.angularDrag = 0;
				left.AddComponent<BoxCollider2D>();

				m_left = left.AddComponent<Tile>();
				m_left.TileObject = m_tileSO;
				m_left.m_pos = m_pos + new Vector2(-1, 0);
				m_left.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_left.m_pos);
				m_left.m_col = m_col;

				m_left.InitializeJoints();

				receiveNewTile(m_left);
				m_left.m_right = this;
				m_left.m_rightJoint.connectedBody = GetComponent<Rigidbody2D>();
				m_left.m_rightJoint.enabled = true;
			}

			return m_left;
		}
		set { m_left = value; }
	}

	public Tile RightTile {
		get {
			if(!m_right)
			{
				GameObject right = new GameObject();
				right.tag = "Tile";
				right.name = gameObject.name;
				right.transform.parent = transform.parent;

				var spr = right.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				var rb = right.AddComponent<Rigidbody2D>();
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.angularDrag = 0;
				right.AddComponent<BoxCollider2D>();

				m_right = right.AddComponent<Tile>();
				m_right.TileObject = m_tileSO;
				m_right.m_pos = m_pos + new Vector2(1, 0);
				m_right.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_right.m_pos);
				m_right.m_col = m_col;

				m_right.InitializeJoints();

				receiveNewTile(m_right);
				m_right.m_left = this;
				m_right.m_leftJoint.connectedBody = GetComponent<Rigidbody2D>();
				m_right.m_leftJoint.enabled = true;
			}

			return m_right;
		}
		set { m_right = value; }
	}

	public Tile TopTile {
		get {
			if(!m_top)
			{
				GameObject top = new GameObject();
				top.tag = "Tile";
				top.name = gameObject.name;
				top.transform.parent = transform.parent;

				var spr = top.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				var rb = top.AddComponent<Rigidbody2D>();
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.angularDrag = 0;
				top.AddComponent<BoxCollider2D>();

				m_top = top.AddComponent<Tile>();
				m_top.TileObject = m_tileSO;
				m_top.m_pos = m_pos + new Vector2(0, 1);
				m_top.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_top.m_pos);
				m_top.m_col = m_col;

				m_top.InitializeJoints();

				receiveNewTile(m_top);
				m_top.m_down = this;
				m_top.m_downJoint.connectedBody = GetComponent<Rigidbody2D>();
				m_top.m_downJoint.enabled = true;
			}

			return m_top;
		}
		set { m_top = value; }
	}

	public Tile DownTile {
		get {
			if(!m_down)
			{
				GameObject down = new GameObject();
				down.tag = "Tile";
				down.name = gameObject.name;
				down.transform.parent = transform.parent;

				var spr = down.AddComponent<SpriteRenderer>();
				spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(m_col));
				var rb = down.AddComponent<Rigidbody2D>();
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				rb.angularDrag = 0;
				down.AddComponent<BoxCollider2D>();

				m_down = down.AddComponent<Tile>();
				m_down.TileObject = m_tileSO;
				m_down.m_pos = m_pos + new Vector2(0, -1);
				m_down.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), m_down.m_pos);
				m_down.m_col = m_col;

				m_down.InitializeJoints();

				floodTilesWithNewTile(m_down);
				m_down.m_top = this;
				m_down.m_topJoint.connectedBody = GetComponent<Rigidbody2D>();
				m_down.m_topJoint.enabled = true;
			}

			return m_down;
		}
		set { m_down = value; }
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
			m_rightJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
			m_rightJoint.enabled = true;
			tile.m_left = this;
			tile.m_leftJoint.connectedBody = GetComponent<Rigidbody2D>();
			tile.m_leftJoint.enabled = true;
		}
		else if(tile.m_pos.x == m_pos.x - 1 && tile.m_pos.y == m_pos.y)
		{
			m_left = tile;
			m_leftJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
			m_leftJoint.enabled = true;
			tile.m_right = this;
			tile.m_rightJoint.connectedBody = GetComponent<Rigidbody2D>();
			tile.m_rightJoint.enabled = true;
		}
		else if(tile.m_pos.y == m_pos.y + 1 && tile.m_pos.x == m_pos.x)
		{
			m_top = tile;
			m_topJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
			m_topJoint.enabled = true;
			tile.m_down = this;
			tile.m_downJoint.connectedBody = GetComponent<Rigidbody2D>();
			tile.m_downJoint.enabled = true;
		}
		else if(tile.m_pos.y == m_pos.y - 1 && tile.m_pos.x == m_pos.x)
		{
			m_down = tile;
			m_downJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
			m_downJoint.enabled = true;
			tile.m_top = this;
			tile.m_topJoint.connectedBody = GetComponent<Rigidbody2D>();
			tile.m_topJoint.enabled = true;
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
		m_rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(m_hp <= 0)
		{
			SetJoints(false);
		}
	}

	void OnMouseUpAsButton()
	{
		Hit(25); //GameManager.PlayerDamage?
	}

	public void InitializeJoints()
	{
		if(!m_leftJoint)
		{
			m_leftJoint = gameObject.AddComponent<FixedJoint2D>();
		}

		if(!m_rightJoint)
		{
			m_rightJoint = gameObject.AddComponent<FixedJoint2D>();
		}

		if(!m_topJoint)
		{
			m_topJoint = gameObject.AddComponent<FixedJoint2D>();
		}

		if(!m_downJoint)
		{
			m_downJoint = gameObject.AddComponent<FixedJoint2D>();
		}
		SetJoints(false);
	}

	public void SetJoints(bool val)
	{
		if(m_leftJoint)
		{
			m_leftJoint.enabled = val;
		}

		if(m_rightJoint)
		{
			m_rightJoint.enabled = val;
		}

		if(m_topJoint)
		{
			m_topJoint.enabled = val;
		}

		if(m_downJoint)
		{
			m_downJoint.enabled = val;
		}
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
