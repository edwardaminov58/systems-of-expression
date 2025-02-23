﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpExample : MonoBehaviour
{
    float lerpTime = 1f;
    float currentLerpTime;

    float moveDistance = 20f;

    Vector3 startPos;
    Vector3 endPos;

    protected void Start()
    {
        startPos = transform.position;
        endPos = transform.position + transform.up * moveDistance;
    }

    protected void Update()
    {
        //reset when we press spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLerpTime = 0f;
        }

        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float t = currentLerpTime / lerpTime;
        t = (Mathf.Sin(t * Mathf.PI * 0.5f))/10;
        transform.position = Vector3.Lerp(startPos, endPos, t);
    }
}