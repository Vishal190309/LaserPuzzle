using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    [SerializeField]
    private float transperentAlpha = 0.9f;
    [SerializeField]
    private float redTransperentAlpha = 0.8f;
    [SerializeField]
    private float normalAplha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColorRed(bool bRed)
    {
        SpriteRenderer spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (bRed)
        {
            Color currentColor = spriteRender.color;
            currentColor.r = 100f;
            currentColor.g = 0f;
            currentColor.b = 0f;
            currentColor.a = redTransperentAlpha;
            spriteRender.color = currentColor;
        }
        else
        {
            Color currentColor = spriteRender.color;
            currentColor.r = 1f;
            currentColor.g = 1f;
            currentColor.b = 1f;
            currentColor.a = transperentAlpha;
            spriteRender.color = currentColor;
        }
       
    }
    
    public void SetTransperentMaterial(bool bTransperent)
    {
        SpriteRenderer spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (bTransperent)
        {
            
            Color currentColor = spriteRender.color;
            currentColor.a = transperentAlpha;
            spriteRender.color = currentColor;
        }
        else
        {
            Color currentColor = spriteRender.color;
            currentColor.a = normalAplha;
            spriteRender.color = currentColor;
        }
    }

    public void Rotate(bool bLeft)
    {
        gameObject.transform.Rotate(new Vector3(0, 0, bLeft ? 90 : 90));
       
    }
}
