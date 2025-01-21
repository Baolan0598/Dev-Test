using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    string ball = "Ball";
    [SerializeField] Transform jammo;
    [SerializeField] Transform[] goals;

    public static GameController Instance;
    public bool isCameraAfterBall = false;
    public GameObject kickedBall = null;
    public Button kickBtn;
    public Button autoBtn;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this);
    }
    public void Kick(bool isAuto)
    {
        isCameraAfterBall = true;
        kickedBall = FindBall(isAuto);
        float time = 0.5f;
        Vector3 direction = FindClosestGoal(FindBall(isAuto)).position - FindBall(isAuto).transform.position;
        Vector3 velocity = new Vector3(direction.x / time,
                                       (direction.y / time),
                                       direction.z / time);
        FindBall(isAuto).GetComponent<Rigidbody>().velocity = velocity;
        kickBtn.enabled = false;
        autoBtn.enabled = false;
        StartCoroutine(EnableButton());
    }
    public GameObject FindBall(bool isAuto = false)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag(ball);
        GameObject res = null;
        if (!isAuto)
        {
            float min = Mathf.Infinity;
            foreach (GameObject obj in balls)
            {
                float distance = Vector3.Distance(jammo.position, obj.transform.position);
                if (distance < min)
                {
                    min = distance;
                    res = obj;
                }
            }
        }
        if (isAuto)
        {
            float max = -Mathf.Infinity;
            foreach (GameObject obj in balls)
            {
                float distance = Vector3.Distance(jammo.position, obj.transform.position);
                if (distance > max)
                {
                    max = distance;
                    res = obj;
                }
            }
        }
        return res;
    }
    Transform FindClosestGoal(GameObject ball)
    {
        Transform res = null;
        float min = Mathf.Infinity;
        foreach (Transform goal in goals)
        {
            float distance = Vector3.Distance(ball.transform.position, goal.position);
            if (distance < min)
            {
                min = distance;
                res = goal;
            }
        }
        return res;
    }
    IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(2.5f);
        kickBtn.enabled = true;
        autoBtn.enabled = true;
    }
    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
