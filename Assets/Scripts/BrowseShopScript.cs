using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseShopScript : MonoBehaviour
{
    BoxCollider trigger;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
           // trigger = GameObject.Find("Persistant Objects").GetComponent<Canvas>();
        }
    }
}
