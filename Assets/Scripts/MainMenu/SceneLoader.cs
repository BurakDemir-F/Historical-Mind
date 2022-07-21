using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button newGame;

    private void Start()
    {
        newGame.onClick.AddListener(StartNewGame);
        print("start button on click function registered.");
    }

    private void StartNewGame()
    {
        print("clicked to start new game.");
        StartCoroutine(StartGameCor());
    }

    private IEnumerator StartGameCor()
    {
        print("Start game cor started");
        var wait = new WaitForSeconds(2f);
        print("waited for 2 seconds");
        FadeManagerItemHolder.PlayFadeOut();
        print("fade out called");
        yield return wait;
        print("waited for 2 seconds");
        LoadingScreen.Instance.PlayFadeIn();
        print("fade in called");
        print("load scene called");
        SceneManager.LoadScene(1);
        print("2 sec wait called with scene building");
        yield return wait;
    }
}