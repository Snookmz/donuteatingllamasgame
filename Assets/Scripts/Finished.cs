using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finished : MonoBehaviour
{

    public static Text finishedText;

    private void Start()
    {
        finishedText = gameObject.GetComponent<Text>();
        if (finishedText != null)
        {
            finishedText.gameObject.SetActive(false);
        }
        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log("Finished, getActiveScene: " + activeScene.name);
    }

    public static void ShowFinishedText()
    {
        if (finishedText != null)
        {
            finishedText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level_1");
    }

    public static IEnumerator LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(5);
    }
    
}