using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jammo : MonoBehaviour
{
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float rotationSpeed = 10f;

    Animator animator;
    string isRunning = "isRunning";

    public static Jammo Instance;
    public bool isBallFound = false;
    [SerializeField] Button kickBtn;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 face = Vector3.zero;
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
            face = Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.forward;
            face += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.forward;
            face += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.forward;
            face += Vector3.right;
        }
        direction = direction.normalized;
        if (direction == Vector3.zero)
        {
            Run(direction, face, false);
            return;
        }
        Run(direction, face);
    }
    private void LateUpdate()
    {
        UpdateAttention();
    }
    void Run(Vector3 direction, Vector3 face, bool isActive = true)
    {
        if (isActive)
        {
            Face(face);
            animator.SetBool(isRunning, true);
            transform.Translate(direction * runSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool(isRunning, false);
        }
    }
    void Face(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    void UpdateAttention()
    {
        if (isBallFound)
        {
            kickBtn.gameObject.SetActive(true);
        }
        else kickBtn.gameObject.SetActive(false);
    }
}
