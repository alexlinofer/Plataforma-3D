using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease ease = Ease.OutBack;
    private float _startScale;



    private void Start()
    {
        HideNotification();
        _startScale = notification.transform.localScale.x;
        
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, tweenDuration).SetEase(ease);

    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
