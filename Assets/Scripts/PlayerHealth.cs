using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Animator animator;
    private static readonly int IsHurt = Animator.StringToHash("IsHurt");
    public int health = 5;
    private Material _material;
    private bool _playerIsHurtingTakeNoDamage;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] public int orcDamage = 1;
    [SerializeField] public int eagleDamage = 2;

    [SerializeField] private float hurtTime = 1f;
    
    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleDamage(other);
    }

    private void HandleDamage(Collision2D other)
    {
         if (!_playerIsHurtingTakeNoDamage)
         {
             if (other.gameObject.CompareTag("EnemyEagle") || 
                 other.collider.CompareTag("EnemyBody") ||
                 other.gameObject.CompareTag("ObstacleSpike"))
             {
                 if (health > 0)
                 {
                     TakeDamage();
                 }
                 else
                 {
                     Death();
                 }
             }
             else if (other.gameObject.CompareTag("ObstaclePit"))
             {
                 Death();
             }
         }       
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        HandleDamage(other);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_playerIsHurtingTakeNoDamage)
        {
            if (other.gameObject.CompareTag("ObstacleSpike"))
            {
                TakeDamage();
            }
        }
    }   
    //
    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     // Debug.Log("PlayerHealth, OnCollisionStay2D, tag: " + other.gameObject.tag);
    //     if (!_playerIsHurtingTakeNoDamage)
    //     {
    //         if (other.gameObject.CompareTag("EnemyEagle") || other.gameObject.CompareTag("EnemyOrc") || other.gameObject.CompareTag("Robot"))
    //         {
    //             TakeDamage();
    //         } 
    //     }      
    // }

    private void TakeDamage()
    {
        health = health - orcDamage;
        UiHealth.UpdateHealth(health);
        _playerIsHurtingTakeNoDamage = true;
        StartCoroutine(StrobeColorOnHurt());
        Invoke(nameof(StopTheHurtAnimation), hurtTime);       
    }
    
    private void StopTheHurtAnimation()
    {
        // animator.SetBool(IsHurt, false);
        _playerIsHurtingTakeNoDamage = false;
        _spriteRenderer.color = Color.white;
    }

    private void Death()
    {
        Debug.Log("PlayerHealth, Death");
        animator.SetBool("IsDead", true);
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(hurtTime / 3);
        SceneManager.LoadScene("GameScene");
    }
    
    private IEnumerator StrobeColorOnHurt()
    {
        for (var i = 0; i < 3; i++)
        {
            if (i % 2 == 0)
            {
                _spriteRenderer.color = Color.red;
            }
            else
            {
                _spriteRenderer.color = Color.green;
            }
            yield return new WaitForSeconds(hurtTime / 3);
        }
    }
}