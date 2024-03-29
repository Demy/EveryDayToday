﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public HealthPoints hp;
    public Transform fill;

    private int max;

    private void Start()
    {
        hp.OnChange += Hp_OnChange;
        max = hp.max;
    }

    void Hp_OnChange(int value, int current)
    {
        fill.localScale = new Vector3((float)current / max, fill.localScale.y, fill.localScale.z);
    }

}
