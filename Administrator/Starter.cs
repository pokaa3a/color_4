using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Debug.Log("Before first Scene loaded");

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        // Debug.Log("After first Scene loaded");
        Administrator.Instance.Initialize();
        Map.Instance.InitializeMap();
        CharacterManager.Instance.SummonCharacters();
        UIManager.Instance.InitiateUIObjects();
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("RuntimeMethodLoad: After first Scene loaded");

    }
}
