﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player") {
            Debug.Log("Enter");
            transform.parent.GetComponent<Zombie>().follow = true;
        }
    }
}
