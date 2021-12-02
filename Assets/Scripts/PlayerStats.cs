using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    
    public static int Money;
    public static int lives;
    public int startLives =30;
    public int startMoney=100;

    private float countdown=1f;
    void Start()
    {
        Money = startMoney;
        lives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
         if (countdown <= 0f)
        {
            StartCoroutine(GameOver());
            countdown = 1f;
        }
        countdown -= Time.deltaTime;
    }
    IEnumerator GameOver(){
        if(lives<=0){
            SceneManager.LoadScene("GameOverScreen");
        }
        yield return new WaitForSeconds(0f);
    }
}
