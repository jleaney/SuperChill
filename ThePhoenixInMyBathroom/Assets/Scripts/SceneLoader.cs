using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public Image fadePanel;
    public float fadeSpeed;

    public void LoadSceneOnFade(int scene)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(2, fadeSpeed).OnComplete(()=>
            SceneManager.LoadScene(scene));
    }

    public void LoadSceneInstant(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
