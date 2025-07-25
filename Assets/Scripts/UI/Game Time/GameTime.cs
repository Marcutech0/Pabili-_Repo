using UnityEngine;
using System;
using System.Collections;

public class GameTime : MonoBehaviour
{
    [SerializeField]
    private float dayLength;
    private TimeSpan currentTime;
    private float minuteLength => dayLength / 1440;

    private void Start()
    {
        StartCoroutine(AddMinute());   
    }

    private IEnumerator AddMinute()
    {
        currentTime += TimeSpan.FromMinutes(1);
        yield return new WaitForSeconds(minuteLength);
        StartCoroutine(AddMinute());
    }
   


    void Update()
    {
        
    }
}
