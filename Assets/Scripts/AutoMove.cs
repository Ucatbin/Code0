using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    void Update()
    {
        float x = Mathf.PingPong(Time.time, 2f);
        transform.position = new Vector2(x, transform.position.y);
    }
}