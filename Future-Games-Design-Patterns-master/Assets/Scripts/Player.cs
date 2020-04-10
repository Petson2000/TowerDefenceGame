using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    public event Action<int> OnPlayerHealthChanged;

    public int Health
    {
        get => health;

        set
        {
            if (health != value)
            {
                health = value;
                OnPlayerHealthChanged?.Invoke(health);
            }
        }
    }

    public string name;
    public event Action<String> OnNameChanged;

    public string Name
    {
        get => name;

        set
        {
            if (name != value)
            {
                name = value;
                OnNameChanged?.Invoke(name);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
