using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public GameObject pooledObject;

	private List<GameObject> pooledObjects;


	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject> ();
	
	}

	public GameObject getPooledObject() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}

		GameObject obj = (GameObject)Instantiate(pooledObject);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}
