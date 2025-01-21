using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ball : MonoBehaviour
{
    string jammo = "Jammo";
    string ball = "Ball";
    string goal = "Goal";
    [SerializeField] GameObject particlePrefab;
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindWithTag(jammo).GetComponent<Collider>(), true);
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindWithTag(ball).GetComponent<Collider>(), true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(goal))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            SpawnParticle(transform.position);
        }
    }
    void SpawnParticle(Vector3 position)
    {
        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        StartCoroutine(Restart(particle));
    }
    IEnumerator Restart(GameObject particle)
    {
        yield return new WaitForSeconds(2);
        Destroy(particle);
        Destroy(gameObject);
        GameController.Instance.isCameraAfterBall = false;
    }
}
