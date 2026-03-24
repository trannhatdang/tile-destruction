using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTest : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnMouseDown()
	{
		Debug.Log("hi");
	}

	void OnMouseUpAsButton()
	{
		Debug.Log("hi");
	}

	public void Hit(float damage = 10.0f)
	{
	}
}
