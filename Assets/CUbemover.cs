using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUbemover : MonoBehaviour
{
    public float length = 1f;

    public float speed = 1f;

    // Update is called once per frame
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        while (true)
        {
            transform.Translate(speed * Vector3.right * Mathf.Sin(Time.time * length));
            yield return null;
        }
    }
}
