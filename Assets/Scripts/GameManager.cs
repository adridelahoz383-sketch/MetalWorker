using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOberText;
    public Button RestartButton;
    public GameObject titleScreen;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0,targets.Count);
        Instantiate(targets[index]);

        
        }

    }

    public void UpdateScore(int scoreToAdd)
    {   
        score  += scoreToAdd;
        scoreText.text = "Score: "+ score;
    }

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
        gameOberText.gameObject.SetActive(true);
        isGameActive = false;

    }
    
    public  void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty )
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        spawnRate  /= difficulty; 
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
}
