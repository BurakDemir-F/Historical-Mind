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
    }

    private void StartNewGame()
    {
        StartCoroutine(StartGameCor());
    }

    private IEnumerator StartGameCor()
    {
        var wait = new WaitForSeconds(2f);
        
        FadeManagerItemHolder.PlayFadeOut();
        yield return wait;
        LoadingScreen.Instance.PlayFadeIn();
        
        SceneManager.LoadScene(sceneBuildIndex:1);
        yield return wait;
    }
}