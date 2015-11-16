using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetweenMin;
	public float distanceBetweenMax;
	public ObjectPooler theObjectPool;
	//public GameObject[] thePlatforms;

	private float platformWidth;
	private float distanceBetween;
	private float[] platformWidths;
	private float lastPlacedPlatformWidth;

    public ObjectPooler[] theObjectPools;


	private int platformSelector;

	// Use this for initialization
	void Start () {

		platformWidths = new float[theObjectPools.Length];

		for (int i = 0; i < platformWidths.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}

		lastPlacedPlatformWidth = 7;

	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

			platformSelector = Random.Range(0, theObjectPools.Length);

			float distanceToMove = platformWidths[platformSelector]/2 + lastPlacedPlatformWidth/2 + distanceBetween - (float)0.01;

			transform.position = new Vector3(transform.position.x + distanceToMove,
			                                 transform.position.y, transform.position.z);


			//Instantiate(thePlatforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = theObjectPools[platformSelector].getPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			lastPlacedPlatformWidth = platformWidths[platformSelector];
		}
	
	}
}
