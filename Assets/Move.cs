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

    void Update()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

        if (move != Vector2.zero)
        {
            Debug.Log(agent.isOnOffMeshLink);
            NavMeshHit hit;
            agent.FindClosestEdge(out hit);



            if (!OffMeshInProgress)
            {
                if (Vector3.Magnitude(hit.position - agent.transform.position) < 0.2f)
                {
                    // StartCoroutine(NormalSpeed(agent));
                    // agent.Raycast()


                    Debug.DrawLine(agent.transform.position, agent.transform.position + (hit.position - agent.transform.position).normalized * 10f, Color.red);
                }

                // return;
            }

            Vector3 direction = cameraForward * move.y + cameraTransform.right * move.x;
            transform.localRotation = Quaternion.LookRotation(direction);

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                // Vector3.Magnitude(hit.position - agent.transform.position)
                Debug.DrawLine(agent.transform.position, agent.transform.position + direction * Time.fixedDeltaTime * 5f * speed, Color.green, 10f);

                agent.Warp(agent.transform.position + direction * (Time.fixedDeltaTime * 10f * speed));

                // agent.Warp(Vector3.Lerp(Vector3.zero, direction * (Time.fixedDeltaTime * 5f * speed), smoothTime * Time.fixedDeltaTime));

            }
            else
                //Move to direction
                agent.Move(Vector3.Lerp(Vector3.zero, direction * (Time.fixedDeltaTime * speed), smoothTime * Time.fixedDeltaTime));

            // animator.SetFloat(idSpeed, move.magnitude);
        }
    }
    
    bool OffMeshInProgress = false;
    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshInProgress = true;
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }

        OffMeshInProgress = false;
        agent.CompleteOffMeshLink();

    }
}