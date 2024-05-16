using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine()); 
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.sreenTint;

        screenTint.Tint();
        yield return new WaitForSeconds(2f);

        screenTint.UnTint();
        yield return new WaitForSeconds(2f);

        yield return null;
    }
}
