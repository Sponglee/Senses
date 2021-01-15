using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float smoothTime = 0.05f;
    static int idSpeed = Animator.StringToHash("Speed");

    Transform cameraTransform;
    Animator animator;
    NavMeshAgent agent;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        // animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

        if (move != Vector2.zero)
        {
            Vector3 direction = cameraForward * move.y + cameraTransform.right * move.x;
            transform.localRotation = Quaternion.LookRotation(direction);

            //Move to direction
            agent.Move(Vector3.Lerp(Vector3.zero, direction * (Time.fixedDeltaTime * speed), smoothTime * Time.fixedDeltaTime));
        }

        // animator.SetFloat(idSpeed, move.magnitude);
    }
}