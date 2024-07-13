using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Action OnClicked, OnExit;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
    }

    public bool IsMouseOverGameObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
