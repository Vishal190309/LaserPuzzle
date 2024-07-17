using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserController : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private float laserSpeed = 5f;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private int currentVertexPosition = 0;
    private bool moveLaser = false;
    public void StartMovingLaser()
    {

        SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
        moveLineRenderer();
    }

    private void Update()
    {
        if(moveLaser)
        {
           
            Vector2 newPosition = (Vector2)lineRenderer.GetPosition(currentVertexPosition+1) + (direction*laserSpeed * Time.deltaTime);
            lineRenderer.SetPosition(currentVertexPosition + 1, newPosition);
            boxCollider.gameObject.transform.position = newPosition;
            boxCollider.enabled = true;


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveLaser = false;

        if (collision.gameObject.transform.parent)
        {
            PlacableObject mirror = collision.gameObject.transform.parent.GetComponent<PlacableObject>();

            if (mirror)
            {

                {
                    SoundManager.Instance.PlaySoundEffect(Sound.LASER_MIRROR);
                    mirror.enabled = false;
                    switch (mirror.GetMirrorType())
                    {
                        case MirrorType.Normal:
                            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
                            lineRenderer.positionCount += 1;
                            currentVertexPosition += 1;
                            lineRenderer.SetPosition(currentVertexPosition + 1, collision.contacts[0].point);
                            moveLaser = true;
                            break;
                        case MirrorType.Splitter:
                            Transform[] newLaserPosition = mirror.GetNewLaserLocation(collision.transform.position);
                            for (int i = 0; i < newLaserPosition.Length; i++)
                            {
                                LaserController controller = Instantiate(laser, newLaserPosition[i].position, Quaternion.identity).GetComponent<LaserController>();
                                controller.SetLaserStart(newLaserPosition[i].position);
                                controller.SetLaserDirection(newLaserPosition[i].transform.right);
                                controller.StartMovingLaser();
                            }
                            break;
                        case MirrorType.Both:
                            break;
                    }



                }


            }
            else
            {
                SoundManager.Instance.PlaySoundEffect(Sound.LASER_Wall);
                StartCoroutine(LevelLost());
            }
        }


    }


    IEnumerator LevelLost()
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetLaserStart(Vector2 startPoint)
    {
        currentVertexPosition = 0;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
    }

    public void SetLaserDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void moveLineRenderer()
    {
        moveLaser = true;
        /*RaycastHit2D ray = Physics2D.Raycast(lineRenderer.GetPosition(currentVertexPosition), direction,1000f,layerMask);
        Debug.DrawRay(lineRenderer.GetPosition(currentVertexPosition), direction * ray.distance,Color.black,100f);
        if (ray.collider!= null)
        {
            print(ray.collider.tag);
            lineRenderer.SetPosition(currentVertexPosition + 1, ray.point);
            if (ray.collider.CompareTag("Wall"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (ray.collider.CompareTag("Mirror"))
            {
                direction = Vector2.Reflect(direction, ray.normal).normalized;
                lineRenderer.positionCount += 1;
                currentVertexPosition += 1;
                moveLineRenderer();
            }
            
           
        }*/

        
    }

    
}



