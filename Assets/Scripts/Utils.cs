using UnityEditor;

public class Utils
{
	public static T LoadAsset<T>(string name)
		where T : UnityEngine.Object
	{
		var guid = AssetDatabase.FindAssets(name);

		if(guid.Length == 0)
		{
			return null;
		}

		return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid[0]));
	}
}
