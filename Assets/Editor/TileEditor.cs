using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class TileEditor : EditorWindow
{
	private VisualElement m_bottomPane;

	private Tile m_selectedTile;
	private TileColor m_currColor;

	private Button createGBButton;
	private List<Button> dirButtons;

	private Rect m_bottomPaneRect;

	void createGB()
	{
		GameObject selectedGB = new GameObject();
		m_selectedTile = selectedGB.AddComponent<Tile>();
		m_selectedTile.SetColor(m_currColor);
	}

	void save()
	{

	}

	void load()
	{

	}

	[MenuItem("Deng/Tile Editor")]
	public static void ShowWindow()
	{
		TileEditor wnd = GetWindow<TileEditor>();
		wnd.titleContent = new GUIContent("TileEditor");
	}

	public void CreateGUI()
	{
		dirButtons = new List<Button>(4);

		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;
		var splitView = new TwoPaneSplitView(0, 40, TwoPaneSplitViewOrientation.Vertical);
		root.Add(splitView);

		VisualElement topPane = new VisualElement();
		splitView.Add(topPane);
		m_bottomPane = new VisualElement();
		splitView.Add(m_bottomPane);

		// Create Save button
		Button saveButton = new Button();
		saveButton.name = "Save";
		saveButton.text = "Save";
		saveButton.RegisterCallback<MouseUpEvent>((evt) => save());
		topPane.Add(saveButton);

		// Create Load button
		Button loadButton = new Button();
		loadButton.name = "Load";
		loadButton.text = "Load";
		loadButton.RegisterCallback<MouseUpEvent>((evt) => load());
		topPane.Add(loadButton);

		Button createGBButton = new Button();
		createGBButton.name = "Create Game Object";
		createGBButton.text = "Create Object";
		createGBButton.RegisterCallback<MouseUpEvent>((evt) => createGB());

		createGBButton.style.top = 50;
		m_bottomPane.Add(createGBButton);

		for(int i = 0; i < 4; ++i)
		{
			dirButtons.Add(new Button());
			dirButtons[i].style.width = 20;
		}

		dirButtons[0].name = "Up";
		dirButtons[0].text = "↑";
		dirButtons[0].style.top = 50;
		dirButtons[0].style.left = 55;

		dirButtons[1].name = "Down";
		dirButtons[1].text = "↓";
		dirButtons[1].style.top = 60;
		dirButtons[1].style.left = 55;

		dirButtons[2].name = "Left";
		dirButtons[2].text = "←";
		dirButtons[2].style.top = 55;
		dirButtons[2].style.left = 50;

		dirButtons[3].name = "Right";
		dirButtons[3].text = "→";
		dirButtons[3].style.top = 55;
		dirButtons[3].style.left = 60;

		foreach(Button btn in dirButtons)
		{
			m_bottomPane.Add(btn);
		}
	}

	void Update()
	{
		if(Selection.count == 1 && Selection.activeGameObject.GetComponent<Tile>())
		{
			m_selectedTile = Selection.activeGameObject.GetComponent<Tile>();
		}

		foreach(Button btn in dirButtons)
		{
			btn.visible = m_selectedTile;
		}

		createGBButton.visible = !m_selectedTile;
		m_bottomPaneRect = m_bottomPane.layout;
	}
}
