using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float maxX = 1.8f;



        var newPos = transform.position;
        newPos.x = (Mathf.PingPong(Time.time, 2) - 1) * maxX;
        transform.position = newPos;
    }
}

