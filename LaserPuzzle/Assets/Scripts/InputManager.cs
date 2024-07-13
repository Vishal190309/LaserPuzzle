using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private LayerMask placementLayerMask;

    private Vector3 lastPosition;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 viewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if(viewportPosition.x>=0 && viewportPosition.x<=1  && viewportPosition.y >=0 && viewportPosition.y <= 1){
            lastPosition = mousePos;
        }
        return lastPosition;
    }
}
