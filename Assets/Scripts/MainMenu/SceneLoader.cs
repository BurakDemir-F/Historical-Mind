using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button newGame;

    private void Start()
    {
        newGame.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
