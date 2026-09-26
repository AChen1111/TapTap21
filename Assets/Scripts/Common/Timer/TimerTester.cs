using System.Collections;
using AChen.Log;
using Common.Timer;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerTester : MonoBehaviour
{
    void Start()
    {
        // StartCoroutine(func());
        TimerUtil.StartTimer("time2",5);
    }
    IEnumerator func()
    {
        TimerUtil.StartTimer("time1",4,()=>ALog.Log("Ciallo!"));
        ALog.Log("Start timer.");
        yield return new WaitForSeconds(3);
        ALog.Log(TimerUtil.HasTimer("time1").ToString());
        yield return new WaitForSeconds(3);
        TimerUtil.DeleteTimer("time1");
        ALog.Log(TimerUtil.HasTimer("time1").ToString());
    }
    void Update()
    {
        ALog.Log(TimerUtil.GetTimerElapsed("time2").ToString());
    }
}
