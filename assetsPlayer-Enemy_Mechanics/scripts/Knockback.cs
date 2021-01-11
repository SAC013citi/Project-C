﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                if(other.GetComponent<PlayerMovement>().currentState != PlayerState.defend)
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                }
                

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    Debug.Log("Heyy");
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (other.gameObject.CompareTag("player"))
                {
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.attack && other.GetComponent<PlayerMovement>().currentState != PlayerState.defend)
                    {
                        Debug.Log("Player");
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
} 
 