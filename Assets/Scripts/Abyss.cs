using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviour
{
	public Action<Tile> onTileEnter;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.CompareTag("Tile"))
		{
			return;
		}

		onTileEnter(other.GetComponent<Tile>());
	}
}
