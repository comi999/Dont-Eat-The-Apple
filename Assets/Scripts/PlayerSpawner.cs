﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        PlayerController.Instance.transform.SetPositionAndRotation( transform.position, transform.rotation );
    }
}