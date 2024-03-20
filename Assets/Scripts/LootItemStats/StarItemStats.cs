using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItemStats : MonoBehaviour
{
    public int pointsAdded = 3;
    public static StarItemStats instance;
    private void Awake()
    {
        instance = this;
    }
}
