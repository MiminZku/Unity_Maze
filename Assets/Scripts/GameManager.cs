using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearUIs;
    public GameObject gameOverUIs;
    public Text besrRecordText;
    public Text timeText;

    public GameObject player;
    public GameObject key;
    public GameObject[] keySpawnSpots;
    public GameObject[] enemys;
    float surviveTime;
    bool isGameClear;
    bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameClear = false;
        isGameOver = false;

        int i = Random.Range(0, 3);
        Instantiate(key, keySpawnSpots[i].transform.position, Quaternion.Euler(0,0,-90));
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameClear || isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        else
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + string.Format("{0:N1}", surviveTime);
        }

    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverUIs.SetActive(true);
        besrRecordText.text = "Best Record : " + string.Format("{0:N1}", PlayerPrefs.GetFloat("BestRecord"));
    }
    public void GameClear()
    {
        isGameClear = true;
        gameClearUIs.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestRecord");
        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestRecord", bestTime);
        }
        besrRecordText.text = "Best Record : " + string.Format("{0:N1}", bestTime);
    }
}
