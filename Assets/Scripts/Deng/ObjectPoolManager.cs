using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Deng
{
	public class ObjectPoolManager : MonoBehaviour
	{
		[SerializeField] bool m_addToDontDestroyOnLoad = false;

		private GameObject m_emptyHolder;

		private static GameObject m_particleSystemsEmpty;
		private static GameObject m_gameObjectsEmpty;
		private static GameObject m_soundFXEmpty;

		private static Dictionary<GameObject, ObjectPool<GameObject>> m_objectPools;
		private static Dictionary<GameObject, GameObject> m_cloneToPrefabMap;

		public enum PoolType
		{
			ParticleSystems,
			GameObjects,
			SoundFX
		}

		public static PoolType PoolingType;

		private void Awake()
		{
			m_objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
			m_cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

			SetupEmpties();
		}

		private void SetupEmpties()
		{
			m_emptyHolder = new GameObject("Object Pools");

			m_particleSystemsEmpty = new GameObject("Particle Effects");
			m_particleSystemsEmpty.transform.parent = m_emptyHolder.transform;

			m_gameObjectsEmpty = new GameObject("GameObjects");
			m_gameObjectsEmpty.transform.parent = m_emptyHolder.transform;

			m_soundFXEmpty = new GameObject("Sound FX");
			m_soundFXEmpty.transform.parent = m_emptyHolder.transform;

			if(m_addToDontDestroyOnLoad)
			{
				DontDestroyOnLoad(m_emptyHolder.transform.root);
			}
		}

		private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects)
		{
			ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
					createFunc: () => CreateObject(prefab, pos, rot, poolType),
					actionOnGet: OnGetObject,
					actionOnRelease: OnReleaseObject,
					actionOnDestroy: OnDestroyObject
					);

			m_objectPools.Add(prefab, pool);
		}

		private static void CreatePool(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObjects)
		{
			ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
					createFunc: () => CreateObject(prefab, parent, rot, poolType),
					actionOnGet: OnGetObject,
					actionOnRelease: OnReleaseObject,
					actionOnDestroy: OnDestroyObject
					);

			m_objectPools.Add(prefab, pool);
		}

		private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects)
		{
			prefab.SetActive(false); //avoid awake or on enable guaranteed

			GameObject obj = Instantiate(prefab, pos, rot);

			prefab.SetActive(true);

			GameObject parentObject = SetParentObject(poolType);
			obj.transform.parent = parentObject.transform;

			return obj;
		}

		private static GameObject CreateObject(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObjects)
		{
			prefab.SetActive(false); //avoid awake or on enable guaranteed

			GameObject obj = Instantiate(prefab, parent);
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = rot;
			obj.transform.localScale = Vector3.one;

			prefab.SetActive(true);

			return obj;
		}

		private static void OnGetObject(GameObject obj)
		{

		}

		private static void OnReleaseObject(GameObject obj)
		{
			obj.SetActive(false);
		}

		private static void OnDestroyObject(GameObject obj)
		{
			if(m_cloneToPrefabMap.ContainsKey(obj))
			{
				m_cloneToPrefabMap.Remove(obj);
			}
		}

		private static GameObject SetParentObject(PoolType poolType)
		{
			switch(poolType)
			{
				case PoolType.ParticleSystems:
					return m_particleSystemsEmpty;
				case PoolType.GameObjects:
					return m_gameObjectsEmpty;
				case PoolType.SoundFX:
					return m_soundFXEmpty;
				
				default:
					return null;
			}
		}

		private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T : Object
		{
			if(!m_objectPools.ContainsKey(objectToSpawn))
			{
				CreatePool(objectToSpawn, spawnPos, spawnRotation, poolType);
			}

			GameObject obj = m_objectPools[objectToSpawn].Get();

			if(obj != null)
			{
				if(!m_cloneToPrefabMap.ContainsKey(obj))
				{
					m_cloneToPrefabMap.Add(obj, objectToSpawn);
				}

				obj.transform.position = spawnPos;
				obj.transform.rotation = spawnRotation;
				obj.SetActive(true);

				if(typeof(T) == typeof(GameObject))
				{
					return obj as T;
				}

				T component = obj.GetComponent<T>();
				if(component == null)
				{
					Debug.LogError($"Object {objectToSpawn.name} doesn't have component of type {typeof(T)}");
				}

				return component;
			}

			return null;
		}

		private static T SpawnObject<T>(GameObject objectToSpawn, Transform parent, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T : Object
		{
			if(!m_objectPools.ContainsKey(objectToSpawn))
			{
				CreatePool(objectToSpawn, parent, spawnRotation, poolType);
			}

			GameObject obj = m_objectPools[objectToSpawn].Get();

			if(obj != null)
			{
				if(!m_cloneToPrefabMap.ContainsKey(obj))
				{
					m_cloneToPrefabMap.Add(obj, objectToSpawn);
				}

				obj.transform.parent = parent;
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localRotation = spawnRotation;
				obj.SetActive(true);

				if(typeof(T) == typeof(GameObject))
				{
					return obj as T;
				}

				T component = obj.GetComponent<T>();
				if(component == null)
				{
					Debug.LogError($"Object {objectToSpawn.name} doesn't have component of type {typeof(T)}");
				}

				return component;
			}

			return null;
		}

		public static T SpawnObject<T>(T typePrefab, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T: Component
		{
			return SpawnObject<T>(typePrefab.gameObject, spawnPos, spawnRotation, poolType);
		}

		public static T SpawnObject<T>(T typePrefab, Transform parent, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T: Component
		{
			return SpawnObject<T>(typePrefab.gameObject, parent, spawnRotation, poolType);
		}

		public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects)
		{
			return SpawnObject<GameObject>(objectToSpawn, spawnPos, spawnRotation, poolType);
		}

		public static GameObject SpawnObject(GameObject objectToSpawn, Transform parent, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects)
		{
			return SpawnObject<GameObject>(objectToSpawn, parent, spawnRotation, poolType);
		}

		public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObjects)
		{
			if(m_cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
			{
				GameObject parentObject = SetParentObject(poolType);

				if(obj.transform.parent != parentObject.transform)
				{
					obj.transform.SetParent(parentObject.transform);
				}

				if(m_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
				{
					pool.Release(obj);
				}
			}
			else
			{
				Debug.LogWarning("Trying to return an object that is not pooled: " + obj.name);
			}
		}
	}
}
