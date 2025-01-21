using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attention : MonoBehaviour
{
    string ball = "Ball";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ball))
        {
            Jammo.Instance.isBallFound = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ball))
        {
            Jammo.Instance.isBallFound = false;
        }
    }
}
