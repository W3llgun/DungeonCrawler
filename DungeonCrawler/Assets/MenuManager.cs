using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public static MenuManager instance;
	public GameObject pause;
	public GameObject end;
	
	void Awake () {
		instance = this;
		pause.SetActive(false);
		end.SetActive(false);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			setPause(!pause.activeSelf);
		}
	}

	public void setPause(bool val)
	{
		if(val)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
		pause.SetActive(val);
	}

	public void setEnd(string display)
	{
		Time.timeScale = 0;
		end.SetActive(true);
		end.GetComponentInChildren<Text>().text = display;
	}

	public void quit()
	{
		Application.Quit();
	}

	public void retry()
	{
		Door.reloadCount = 0;
		Time.timeScale = 1;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player) DestroyImmediate(player);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}
}
