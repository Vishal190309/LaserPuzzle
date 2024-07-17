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

    private Vector3 lastPosition = new Vector3(0.5f,0.5f,0.5f);

    [SerializeField]
    private List<Vector2> NoOfPlacableObjectsAvailable = new List<Vector2>();

    private bool placeObjectPreview;
    [SerializeField]
    private ObjectsDataSO data;

    private int currentObjectId = -1;
    private PlacableObject currentSelectedObject;

    private List<Vector3> gridLocations = new List<Vector3>();

    public List<Transform> placeObjectsLocations = new List<Transform>();

    private void Start()
    {
       
        InputManager.Instance.OnRightClick += RemoveObject;
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
        
        if (NoOfPlacableObjectsAvailable[id].y > 0)
        {
            StopPlacement();
            currentObjectId = id;
            SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
           
            placeObjectPreview = true;

            currentSelectedObject = Instantiate(data.placementObjects[data.placementObjects.FindIndex(data => data.ID == id)].placementObject);
            currentSelectedObject.transform.position = mouseIndicator.transform.position;
            currentSelectedObject.SetTransperentMaterial(true);
            InputManager.Instance.OnClicked += PlaceObject;
            InputManager.Instance.OnRotate += RotateObject;
        }
    }

    private void StopPlacement()
    {
       if(currentSelectedObject != null)
        {
            InputManager.Instance.OnClicked -= PlaceObject;
            InputManager.Instance.OnRotate -= RotateObject;
            Destroy(currentSelectedObject.gameObject);
            currentSelectedObject = null;
            currentObjectId = -1;
            placeObjectPreview = false;
        }
    }

    public void PlaceObject()
    {
        if (InputManager.Instance.IsMouseOverGameObject())
            return;

        if (currentSelectedObject != null)
        {
            if (CanPlaceObject(currentSelectedObject.gameObject.transform.position))
            {
               
                Vector2 currentObjectCount = NoOfPlacableObjectsAvailable[currentObjectId];
                currentObjectCount.y -= 1;
                NoOfPlacableObjectsAvailable[currentObjectId] = currentObjectCount;
                LevelManager.Instance.OnObjectCountUpdated?.Invoke(currentObjectId);
                SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
                InputManager.Instance.OnClicked -= PlaceObject;
                InputManager.Instance.OnRotate -= RotateObject;
                placeObjectsLocations.Add(currentSelectedObject.transform);
                currentSelectedObject.SetTransperentMaterial(false);
                currentSelectedObject = null;
                currentObjectId = -1;
                placeObjectPreview = false;

            }
        }

    }

    private void RemoveObject()
    {

        Vector3 mouseCellPosition = GetSelectedMapPosition();
        foreach (Transform transform in placeObjectsLocations)
        {
            
            if(mouseCellPosition == grid.GetCellCenterWorld(grid.WorldToCell(transform.position)))
            {
                SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
                PlacableObject placableObject = transform.gameObject.GetComponent<PlacableObject>();
                Vector2 currentObjectCount = NoOfPlacableObjectsAvailable[placableObject.GetId()];
                currentObjectCount.y += 1;
                NoOfPlacableObjectsAvailable[placableObject.GetId()] = currentObjectCount;
                LevelManager.Instance.OnObjectCountUpdated?.Invoke(placableObject.GetId());
                Destroy(placableObject.gameObject);
                placeObjectsLocations.Remove(placableObject.transform);
                break;
            }
        }
    }

    public bool CanPlaceObject(Vector3 location)
    {
        if (placeObjectsLocations.Count != 0)
        {
            foreach (var placeObject in placeObjectsLocations)
            {
                if(placeObject.position == location)
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
            SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
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

    public int GetAvailabelObjectCount(int id)
    {
        return (int)NoOfPlacableObjectsAvailable[id].y;
    }
}
