using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
	// private VisualElement m_topPane;
	// private VisualElement m_bottomPane;
	// private VisualElement m_customizePane;
	// private Label m_noLevelLabel;
	// private Button m_addRowButton;
	// private ObjectField m_levelObjectField;
	//
	// private List<VisualElement> m_rows;
	// private List<ObjectField> m_objs;
	// private List<FloatField> m_timings;
	//
	// private LevelSO m_selectedLevel;
	//
	// [MenuItem("Deng/Level Editor")]
	// public static void ShowWindow()
	// {
	// 	LevelEditor wnd = GetWindow<LevelEditor>();
	// 	wnd.titleContent = new GUIContent("Level Editor");
	// }
	//
	// void changeSelectedLevel()
	// {
	// 	m_selectedLevel = (LevelSO)m_levelObjectField.value;
	//
	// 	m_noLevelLabel.visible = (m_selectedLevel == null);
	// 	m_customizePane.visible = (m_selectedLevel != null);
	// 	m_addRowButton.visible = (m_selectedLevel != null);
	//
	// 	if(m_selectedLevel != null)
	// 	{
	// 		m_selectedLevel.objectsChanged += objectsInfoChanged;
	// 		objectsInfoChanged();
	// 	};
	// }
	//
	// void objectsInfoChanged()
	// {
	// 	m_customizePane.Clear();
	// 	var objs = m_selectedLevel.Objects;
	// 	int size = objs.Count;
	//
	// 	m_rows = new List<VisualElement>();
	// 	m_objs = new List<ObjectField>();
	// 	m_timings = new List<FloatField>();
	//
	// 	for(int i = 0; i < size; ++i)
	// 	{
	// 		m_rows.Add(new VisualElement());
	// 		m_objs.Add(new ObjectField());
	// 		m_objs[i].objectType = typeof(TileObjectSO);
	// 		m_timings.Add(new FloatField());
	//
	// 		Debug.Log($"{m_rows.Count}, {m_objs.Count}, {m_timings.Count}");
	// 		Debug.Log(i);
	//
	// 		m_objs[i].RegisterValueChangedCallback((evt) => { m_selectedLevel.SetObject((TileObjectSO)m_objs[i].value, m_timings[i].value, i); });
	// 		m_timings[i].RegisterValueChangedCallback((evt) => { m_selectedLevel.SetObject((TileObjectSO)m_objs[i].value, m_timings[i].value, i); });
	//
	// 		m_rows[i].Add(m_objs[i]);
	// 		m_rows[i].Add(m_timings[i]);
	//
	// 		m_customizePane.Add(m_rows[i]);
	// 	}
	// }
	//
	// void addRow()
	// {
	// 	m_selectedLevel.AddNewObject();
	// }
	//
	// public void CreateGUI()
	// {
	// 	VisualElement root = rootVisualElement;
	//
	// 	var splitView = new TwoPaneSplitView(0, 40, TwoPaneSplitViewOrientation.Vertical);
	// 	root.Add(splitView);
	//
	// 	m_topPane = new VisualElement();
	// 	splitView.Add(m_topPane);
	// 	m_bottomPane = new VisualElement();
	// 	splitView.Add(m_bottomPane);
	//
	// 	m_levelObjectField = new ObjectField("Level: ");
	// 	m_levelObjectField.objectType = typeof(LevelSO);
	// 	m_levelObjectField.RegisterValueChangedCallback((evt) => changeSelectedLevel());
	// 	m_topPane.Add(m_levelObjectField);
	//
	// 	m_noLevelLabel = new Label();
	// 	m_noLevelLabel.text = "Choose a level to customize";
	// 	m_bottomPane.Add(m_noLevelLabel);
	//
	// 	m_customizePane = new VisualElement();
	// 	m_customizePane.style.flexDirection = FlexDirection.Column;
	//
	// 	m_bottomPane.Add(m_customizePane);
	//
	// 	m_addRowButton = new Button();
	// 	m_addRowButton.text = "Add new Row";
	// 	m_addRowButton.RegisterCallback<MouseUpEvent>((evt) => addRow());
	// 	m_addRowButton.visible = false;
	//
	// 	m_bottomPane.Add(m_addRowButton);
	// }
	//
	// void Update()
	// {
	// 	Rect bottomPaneRect = m_bottomPane.layout;
	//
	// 	m_noLevelLabel.style.top = bottomPaneRect.height / 2 - 50;
	// 	m_noLevelLabel.style.left = bottomPaneRect.width / 2 - 60;
	// }
}
