using IUP.Toolkits.CoroutineShells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineShellTest : MonoBehaviour
{
    private CoroutineShell _shell;

    void Start()
    {
        _shell = new CoroutineShell(this, Routine);
        StartRoutine();
    }

    private void StartRoutine()
    {
        _shell.StartAnyway();
    }

    private IEnumerator Routine()
    {
        Debug.Log("start routine");
        for (int i = 0; i < 10; i += 1)
        {
            yield return null;
            Debug.Log(i);
        }
        StartRoutine();
        Debug.Log("end routine");
    }
}
