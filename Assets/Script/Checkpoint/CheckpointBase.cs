using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;
    public Image image;
    public TextMeshProUGUI text;
    public float checkpointBrightness = 2f;
    public Animator uiAnimator;
    public string animatorTrigger = "NextLevel";

    private bool checkpointActivated = false;
    private string checkpointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
        if (uiAnimator != null)
        {
            AnimateUI();
        }
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        Color emissionColor = meshRenderer.material.GetColor("_EmissionColor");

        emissionColor = Color.white;
        emissionColor *= checkpointBrightness;
        meshRenderer.material.SetColor("_EmissionColor", emissionColor);

        //meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        Color emissionColor = meshRenderer.material.GetColor("_EmissionColor");

        emissionColor = Color.grey;
        meshRenderer.material.SetColor("_EmissionColor", emissionColor);


        //meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        CheckpointManager.Instance.SaveCheckpoint(key);

        checkpointActivated = true;
        CheckpointOnText();
    }

    private void CheckpointOnText()
    {
        image.DOFade(1f, 2f);
        text.DOFade(1f, 2f);
        StartCoroutine(CheckpointOffText());
    }

    private void AnimateUI()
    {
        // O objetivo dessa função é vincular a animação de carregar a próxima fase, caso seja o último checkpoint da fase
        uiAnimator.SetTrigger(animatorTrigger);
    }

    IEnumerator CheckpointOffText()
    {
        yield return new WaitForSeconds(2f);
        image.DOFade(0f, 2f);
        text.DOFade(0, 2f);
    }
}
