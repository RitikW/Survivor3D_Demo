using EnemyScripts;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text totalScoreTxt;
    [SerializeField] private EnemyCoinManager manager;
    [SerializeField] private Toggle audio1, audio2;
    private int totalScore;

    private void OnEnable()
    {
        StaticHelper.OnGameOver += Init;
    }
    private void OnDisable()
    {
        StaticHelper.OnGameOver -= Init;
    }

    private void Init()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        Camera.main.GetComponent<AudioListener>().enabled = false;
        totalScore = manager.CoinsCollected + manager.CoinsCollected;
        totalScoreTxt.text = "Score: "+totalScore.ToString();
    }

    public void OnStart()
    {
        audio2.isOn = audio1.isOn;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
