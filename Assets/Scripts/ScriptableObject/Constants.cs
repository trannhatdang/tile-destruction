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
				instance = AssetDatabase.LoadAssetAtPath<Constants>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(constantSO)[0]));
			}

		return instance;
		}
	}

	[SerializeField] private string m_biggerSawSprite;
	[SerializeField] private string m_strongerSawSprite;
	[SerializeField] private string m_fasterSawSprite;
	[SerializeField] private string m_newSawSprite;
	[SerializeField] private string m_blueTileSprite;
	[SerializeField] private string m_greenTileSprite;
	[SerializeField] private string m_pinkTileSprite;
	[SerializeField] private string m_redTileSprite;
	[SerializeField] private string m_whiteTileSprite;
	[SerializeField] private string m_yellowTileSprite;
	[SerializeField] private int m_xp;

	public string BLUE_TILE_SPRITE {
		get { return m_blueTileSprite; }
	}
	public string GREEN_TILE_SPRITE {
		get { return m_greenTileSprite; }
	}
	public string PINK_TILE_SPRITE {
		get { return m_pinkTileSprite; }
	}
	public string RED_TILE_SPRITE {
		get { return m_redTileSprite; }
	}
	public string WHITE_TILE_SPRITE {
		get { return m_whiteTileSprite; }
	}
	public string YELLOW_TILE_SPRITE {
		get { return m_yellowTileSprite; }
	}

	public int XP {
		get { return m_xp; }
	}
	
	public string GetWeaponPlacementSprite(WeaponChoice choice)
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
				return "";
		}
	}

	public string GetColor(TileColor col)
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
				return "";
		}

	}
}
