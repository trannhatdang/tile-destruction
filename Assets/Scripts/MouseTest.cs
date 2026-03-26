using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTest : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] GameObject prefab;

	InputAction mouseAction;
	InputAction mousePosition;
	void Start()
	{
		mouseAction = InputSystem.actions.FindAction("Mouse");
		mousePosition = InputSystem.actions.FindAction("MousePosition");
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 mousePos = mousePosition.ReadValue<Vector2>();
		if(mouseAction.IsPressed())
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
			Deng.ObjectPoolManager.SpawnObject(prefab, pos, Quaternion.identity);
		}
	}

	void OnMouseDown()
	{
	}

	void OnMouseUpAsButton()
	{
		Debug.Log("hi");
	}
}
