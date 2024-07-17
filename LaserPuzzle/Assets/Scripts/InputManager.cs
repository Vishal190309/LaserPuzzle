using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Action OnClicked,OnRightClick, OnExit,OnPause;
    public Action<bool> OnRotate;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetMouseButtonDown(1))
            OnRightClick?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnPause?.Invoke();
        if (Input.GetKeyDown(KeyCode.Q))
            OnRotate?.Invoke(true);
        if (Input.GetKeyDown(KeyCode.E))
            OnRotate?.Invoke(false);
    }

    public bool IsMouseOverGameObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
