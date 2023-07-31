using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : MonoBehaviour
{
    public static bool stateActivated = false;
    [SerializeField]
    private GameObject go_ststeBase;
    void Start()
    {

    }

    void Update()
    {
        TryOpenState();
    }

    private void TryOpenState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateActivated = !stateActivated;

            if (stateActivated)
                OpenState();
            else
                CloseStare();
        }
    }

    private void OpenState()
    {
        go_ststeBase.SetActive(true);
    }

    private void CloseStare()
    {
        go_ststeBase.SetActive(false);
    }
}
