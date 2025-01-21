using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 12f, 0f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 position = Vector3.zero;
            if (!GameController.Instance.isCameraAfterBall)
            {
                position = target.position + offset;
            } else
            {
                position = GameController.Instance.kickedBall.transform.position + offset;
            }
            transform.position = Vector3.Lerp(transform.position, position, smoothSpeed * Time.deltaTime);

        }
    }
}
