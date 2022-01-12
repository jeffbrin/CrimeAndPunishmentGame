using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationTracker : MonoBehaviour
{
	static SpawnLocationTracker instance;
	string previousScene;

	// make this a singleton
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

    private void Start()
    {
        // Look for a SceneChanger with the ComingFrom property that matches the previous scene
    }
}
