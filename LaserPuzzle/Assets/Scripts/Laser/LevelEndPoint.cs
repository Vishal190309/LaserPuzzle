using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LevelController levelController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<LaserController>())
        {
            levelController.increaseNoOfLaserEndPoints();
        }
    }
}
