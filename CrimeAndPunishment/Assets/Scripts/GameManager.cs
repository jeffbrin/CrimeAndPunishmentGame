using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
	public bool raskolnikovHasAxe;
	int murders = 0;
	bool hasMoney = false;

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
		FindObjectOfType<PlayerLogic>().hasAxe = raskolnikovHasAxe;
		if (SceneManager.GetActiveScene().name == "Pawnbroker_House")
		{
			murders = 0;
			hasMoney = false;
		}
	}

	public void SceneChange()
    {
		raskolnikovHasAxe = FindObjectOfType<PlayerLogic>().hasAxe;
		Debug.Log(raskolnikovHasAxe);
    }

	public void IndicateDeath()
    {
		murders += 1;
    }

	public void GetMoney()
    {
		hasMoney = true;
    }

	/// <summary>
	/// 
	/// </summary>
	/// <returns>1. Win, 2. Failed to get money, 3. Failed to kill Lizaveta, 4. Nothing</returns>
	public int InWinState()
    {
		if (!hasMoney && murders == 0)
			return 4;
		return hasMoney && murders >= 2 ? 1 : !hasMoney ? 2 : 3;
    }

	public void ShowEndGame(string message)
    {
		Image panel = GameObject.FindWithTag("Panel").GetComponent<Image>();
		panel.color = Color.black;
		panel.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }
}
