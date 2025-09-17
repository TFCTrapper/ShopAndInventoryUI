using System.Collections;
using DI;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Inject] private IUIManager _uiManager;

    private void Awake()
    {
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        ProjectContext.Container.Inject(this);
        _uiManager.Test();
    }
}
