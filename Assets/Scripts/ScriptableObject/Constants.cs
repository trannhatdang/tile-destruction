using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Constants", menuName = "ScriptableObjects/Constants")]
public class Constants : ScriptableObject
{
	// Start is called before the first frame update
	const string constantSO = "Constants";
	static Constants instance;
	public static Constants Instance {
		get {
			if (instance == null) {
				instance = Resources.Load<Constants>("Constants");
			}

		return instance;
		}
	}

	[SerializeField] private Sprite m_biggerSawSprite;
	[SerializeField] private Sprite m_strongerSawSprite;
	[SerializeField] private Sprite m_fasterSawSprite;
	[SerializeField] private Sprite m_newSawSprite;
	[SerializeField] private Sprite m_blueTileSprite;
	[SerializeField] private Sprite m_greenTileSprite;
	[SerializeField] private Sprite m_pinkTileSprite;
	[SerializeField] private Sprite m_redTileSprite;
	[SerializeField] private Sprite m_whiteTileSprite;
	[SerializeField] private Sprite m_yellowTileSprite;
	[SerializeField] private GameObject m_particles;
	[SerializeField] private int m_xp;

	public Sprite BLUE_TILE_SPRITE {
		get { return m_blueTileSprite; }
	}
	public Sprite GREEN_TILE_SPRITE {
		get { return m_greenTileSprite; }
	}
	public Sprite PINK_TILE_SPRITE {
		get { return m_pinkTileSprite; }
	}
	public Sprite RED_TILE_SPRITE {
		get { return m_redTileSprite; }
	}
	public Sprite WHITE_TILE_SPRITE {
		get { return m_whiteTileSprite; }
	}
	public Sprite YELLOW_TILE_SPRITE {
		get { return m_yellowTileSprite; }
	}
	public GameObject DEFAULT_PARTICLES {
		get { return m_particles; }
	}

	public int XP {
		get { return m_xp; }
	}
	
	public Sprite GetWeaponPlacementSprite(WeaponChoice choice)
	{
		switch(choice)
		{
			case WeaponChoice.BiggerSaw:
				return m_biggerSawSprite;
			case WeaponChoice.StrongerSaw:
				return m_strongerSawSprite;
			case WeaponChoice.FasterSaw:
				return m_fasterSawSprite;
			case WeaponChoice.NewSaw:
				return m_newSawSprite;
			default:
				return null;
		}
	}

	public Sprite GetColor(TileColor col)
	{
		switch(col)
		{
			case TileColor.BLUE:
				return m_blueTileSprite;
			case TileColor.GREEN:
				return m_greenTileSprite;
			case TileColor.PINK:
				return m_pinkTileSprite;
			case TileColor.RED:
				return m_redTileSprite;
			case TileColor.WHITE:
				return m_whiteTileSprite;
			case TileColor.YELLOW:
				return m_yellowTileSprite;
			default:
				return null;
		}

	}
}
