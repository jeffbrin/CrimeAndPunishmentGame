using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
	public PlayerLogic lastRaskolnikovState = null;
	int murders = 0;

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

	public void ResetStage(string reason)
    {
		FindObjectOfType<SpawnLocationTracker>().SetRaskolnikovMessage(reason);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		if (lastRaskolnikovState != null) {
			PlayerLogic raskolnikovState = FindObjectOfType<PlayerLogic>();
			raskolnikovState = lastRaskolnikovState;
			murders = 0;
		}
    }

	public void SceneChange()
    {
		lastRaskolnikovState = FindObjectOfType<PlayerLogic>();
    }

	public void IndicateDeath()
    {
		murders += 1;
    }

}
