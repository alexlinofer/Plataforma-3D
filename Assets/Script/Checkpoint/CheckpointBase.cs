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

    IEnumerator CheckpointOffText()
    {
        yield return new WaitForSeconds(2f);
        image.DOFade(0f, 2f);
        text.DOFade(0, 2f);
    }
}
