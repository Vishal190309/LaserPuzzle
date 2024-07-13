using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject circle;
    [SerializeField] 
    private InputManager inputManager;

    private void Start()
    {
        for(int i = -10; i<10; i++)
        {
            for (int j = -10; j<10; j++)
            {
                Vector3 gridCenterPosition = grid.GetCellCenterWorld(new Vector3Int(i, j));

                Instantiate(circle, gridCenterPosition, Quaternion.identity);
            }
        }
       
    }
    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        
        mouseIndicator.transform.position = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
    }
}
