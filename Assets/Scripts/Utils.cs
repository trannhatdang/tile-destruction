using UnityEditor;

public class Utils
{
	public static T LoadAsset<T>(string name)
		where T : UnityEngine.Object
	{
		return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(name)[0]));
	}
}
