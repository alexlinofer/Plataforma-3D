using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    private bool _endGame = false;

    public int currentLevel = 1;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter (Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
        }
    }


    private void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .1f).From().SetEase(Ease.OutBack);
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }

}
