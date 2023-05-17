using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fountain : MonoBehaviour
{
    Animator animator;
    public GameObject victory;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //gameover
        victory.SetActive(true);
        //
        animator.SetTrigger("folActivated");
    }
}