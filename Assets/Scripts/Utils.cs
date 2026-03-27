using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Utils
{
	private static bool cameraMoving = false;
	private static bool cameraZooming = false;
	public static T LoadAsset<T>(string name)
		where T : UnityEngine.Object
	{
		return Resources.Load<T>(name);
	}

	public static void ZoomCamera()
	{
		if(cameraZooming) return;

		cameraZooming = true;
		var size = Camera.main.orthographicSize;
		Camera.main.DOOrthoSize(4.1f, 0.05f).OnComplete(resetCameraZoom);
	}

	public static void ShakeCamera()
	{
		if(cameraMoving) return;
		Debug.Log(cameraMoving);

		cameraMoving = true;
		Camera.main.DOShakePosition(0.01f, 0.1f, 1, 10).OnComplete(resetCamera);
	}

	static void resetCamera()
	{
		cameraMoving = false;
	}

	static void resetCameraZoom()
	{
		cameraZooming = false;
		Camera.main.DOOrthoSize(4f, 0.05f);
	}
}
