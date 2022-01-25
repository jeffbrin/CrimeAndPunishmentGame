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
	List<string> loadedScenes = new List<string>();

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

	public void RemoveThoughtTexts()
    {
		// If this scene has been visited and it's not the last scene to be visited
		if (loadedScenes.Contains(SceneManager.GetActiveScene().name) && loadedScenes[loadedScenes.Count - 1] != SceneManager.GetActiveScene().name)
		{
			TextPopup[] texts = FindObjectsOfType<TextPopup>();
			foreach (TextPopup text in texts)
				Destroy(text.gameObject);
		}
		else if (SceneManager.GetActiveScene().name == "Pawnbroker_House" && murders > 0)
        {
			TextPopup[] texts = FindObjectsOfType<TextPopup>();
			foreach (TextPopup text in texts)
				Destroy(text.gameObject);
			foreach (PawnBrokerBehaviour pb in FindObjectsOfType<PawnBrokerBehaviour>())
				Destroy(pb.gameObject);
            foreach(MoneyCollection mc in FindObjectsOfType<MoneyCollection>())
				Destroy(mc.gameObject);
			Destroy(FindObjectOfType<AlyonaConvo>().gameObject);
			loadedScenes.Add(SceneManager.GetActiveScene().name);
		}
		else
			loadedScenes.Add(SceneManager.GetActiveScene().name);

    }
}
