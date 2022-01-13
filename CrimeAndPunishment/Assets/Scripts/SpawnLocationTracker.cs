using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLocationTracker : MonoBehaviour
{
	static SpawnLocationTracker instance;
	string previousScene = "Default";

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
		// Add a function to call when a new scene is laded
		SceneManager.sceneLoaded += OnSceneLoaded;
		OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{

		SceneChanger[] sceneChangers = FindObjectsOfType<SceneChanger>();
		foreach (SceneChanger sceneChanger in sceneChangers)
        {
			Debug.Log(sceneChanger.TargetScene);
			if (sceneChanger.TargetScene == previousScene)
            {
				FindObjectOfType<PlayerMovement>().transform.position = sceneChanger.transform.position;
				break;
            }
        }
	}

	public string PreviousScene
    {
		set { previousScene = value; }
    }
}
