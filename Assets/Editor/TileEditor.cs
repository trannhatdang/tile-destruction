using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class TileEditor : EditorWindow
{
	GameObject m_selectedGB;
	void createGB()
	{
		m_selectedGB = new GameObject();
	}

	[MenuItem("Deng/Tile Editor")]
	public static void ShowExample()
	{
		TileEditor wnd = GetWindow<TileEditor>();
		wnd.titleContent = new GUIContent("TileEditor");
	}

	public void CreateGUI()
	{
		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;
		var splitView = new TwoPaneSplitView(0, 40, TwoPaneSplitViewOrientation.Vertical);
		root.Add(splitView);

		VisualElement topPane = new VisualElement();
		splitView.Add(topPane);
		VisualElement bottomPane = new VisualElement();
		splitView.Add(bottomPane);

		// VisualElements objects can contain other VisualElement following a tree hierarchy
		Label label = new Label("Hello World!");
		topPane.Add(label);

		// Create button
		Button button = new Button();
		button.name = "button";
		button.text = "Button";
		button.RegisterCallback<MouseUpEvent>((evt) => createGB());
		topPane.Add(button);
	}
}
