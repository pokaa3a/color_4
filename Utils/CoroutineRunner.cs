using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference:
// https://gist.github.com/LotteMakesStuff/d179d28f29bc9bb499dc5260e0146154

public class CoroutineRunner : MonoBehaviour
{
    public static void RunCoroutine(IEnumerator coroutine)
    {
        GameObject thisObj = new GameObject("CoroutineRunner");
        DontDestroyOnLoad(thisObj);

        CoroutineRunner runner = thisObj.AddComponent<CoroutineRunner>();
        runner.StartCoroutine(runner.MonitorRunning(coroutine));
    }

    IEnumerator MonitorRunning(IEnumerator coroutine)
    {
        while (coroutine.MoveNext())
        {
            yield return coroutine.Current;
        }
        Destroy(gameObject);
    }
}
