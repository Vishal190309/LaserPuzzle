using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject circle;

    private Vector3 lastPosition;

    private bool placeObjectPreview;
    [SerializeField]
    private ObjectsDataSO data;
    [SerializeField]
    private InputManager inputManager;

    private PlacableObject currentSelectedObject;

    private List<Vector3> gridLocations = new List<Vector3>();
    private List<PlacableObject> placeObjectsLocations = new List<PlacableObject>();

    private void Start()
    {
        for(int i = -3; i<3; i++)
        {
            for (int j = -4; j<4; j++)
            {
                Vector3 gridCenterPosition = grid.GetCellCenterWorld(new Vector3Int(i, j));
                Instantiate(circle, gridCenterPosition, Quaternion.identity);
                gridLocations.Add(gridCenterPosition);
            }
        }
       
    }
    private void Update()
    {
        if (mouseIndicator.transform.position != GetSelectedMapPosition())
        {
            mouseIndicator.transform.position = GetSelectedMapPosition();
            if (placeObjectPreview)
            {
              
                currentSelectedObject.transform.position = GetSelectedMapPosition();
                currentSelectedObject.SetColorRed(!CanPlaceObject(currentSelectedObject.transform.position));
            }
        }
    }

    public void StartPlacingObject(int id)
    {
        StopPlacement();
        placeObjectPreview = true;

        currentSelectedObject = Instantiate(data.placementObjects[data.placementObjects.FindIndex(data => data.ID == id)].placementObject);
        currentSelectedObject.transform.position = mouseIndicator.transform.position;  
        currentSelectedObject.SetTransperentMaterial(true);
        inputManager.OnClicked += PlaceObject;
    }

    private void StopPlacement()
    {
       if(currentSelectedObject != null)
        {
            Destroy(currentSelectedObject.gameObject);
            currentSelectedObject = null;
            placeObjectPreview = false;
        }
    }

    public void PlaceObject()
    {
        if (inputManager.IsMouseOverGameObject())
            return;

        print("PlaceObjectCalled");
        if (currentSelectedObject != null)
        {
            if (CanPlaceObject(currentSelectedObject.gameObject.transform.position))
            {

                placeObjectsLocations.Add(currentSelectedObject);
                currentSelectedObject.SetTransperentMaterial(false);
                currentSelectedObject = null;
                placeObjectPreview = false;

            }
        }
        else
        {

        }

    }

    public bool CanPlaceObject(Vector3 location)
    {
        if (placeObjectsLocations.Count != 0)
        {
            foreach (var placeObject in placeObjectsLocations)
            {
                if(placeObject.transform.position == location)
                    return false;
            }
            return true;
        }
        return true;
    }

    public void RotateObject(bool bLeft)
    {
        if(currentSelectedObject != null)
        {
            currentSelectedObject.Rotate(bLeft);
        }
    }
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePos));

        if (gridLocations.Contains(gridPosition))
        {
           lastPosition = gridPosition;
        }
        return lastPosition;
    }
}
