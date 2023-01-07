using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearUIs;
    public GameObject gameOverUIs;
    public GameObject bestRecordText;
    public Text timeText;

    public GameObject player;
    public GameObject key;
    public GameObject healPack;
    public GameObject[] keySpawnSpots;
    public GameObject[] healPackSpots;
    public AudioSource bgmAudioSource;
    public AudioSource soundEffectsSource;
    public AudioClip gameClearSound;
    public AudioClip gameOverSound;

    float surviveTime;
    bool isGameClear;
    bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameClear = false;
        isGameOver = false;

        int i = Random.Range(0, keySpawnSpots.Length);
        Instantiate(key, keySpawnSpots[i].transform.position, Quaternion.Euler(0,0,-90));

        /*i = Random.Range(0, healPackSpots.Length);*/
        for(int j = 0; j < healPackSpots.Length; j++)
        {
            /*if(j==i) continue;*/
            Instantiate(healPack, healPackSpots[j].transform.position, Quaternion.identity);
        }
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
        float bestRecord = PlayerPrefs.GetFloat("BestRecord");
        bestRecordText.GetComponent<Text>().text = "Best Record : " + string.Format("{0:N1}", bestRecord);
        bestRecordText.SetActive(true);
        gameOverUIs.SetActive(true);
        bgmAudioSource.Pause();
        soundEffectsSource.clip = gameOverSound;
        soundEffectsSource.Play();
    }
    public void GameClear()
    {
        isGameClear = true;
        float bestTime = PlayerPrefs.GetFloat("BestRecord");
        if (surviveTime < bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestRecord", bestTime);
        }
        bestRecordText.GetComponent<Text>().text = "Best Record : " + string.Format("{0:N1}", bestTime);
        bestRecordText.SetActive(true);
        gameClearUIs.SetActive(true);
        bgmAudioSource.Pause();
        soundEffectsSource.clip = gameClearSound;
        soundEffectsSource.Play();
    }
}
