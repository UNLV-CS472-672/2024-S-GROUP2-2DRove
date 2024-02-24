using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    private GameObject[] gameObjects;
    public Rigidbody2D rb;
    public Transform self;
    public Animator m_Animator;
    public BaseState(EState key)
    {
        StateKey = key;
    }
    
    public EState StateKey { get; private set; }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    public abstract void OnTriggerExit(Collider other);
    public void updateID()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Ghoul");
        foreach (GameObject ghoul in gameObjects)
        {
            rb = ghoul.GetComponent<Rigidbody2D>();
            self = ghoul.GetComponent<Transform>();
            m_Animator = ghoul.GetComponent<Animator>();
        }
    }
}
