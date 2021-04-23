using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextHole : MonoBehaviour
{
    public Transform nextSpawn;
    private void OnTriggerEnter(Collider other)
    {   
        other.gameObject.transform.position = nextSpawn.position;
    }

}
