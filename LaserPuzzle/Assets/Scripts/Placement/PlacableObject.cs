using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    [SerializeField]
    private float transperentAlpha = 0.9f;
    [SerializeField]
    private float redTransperentAlpha = 0.8f;
    [SerializeField]
    private float normalAplha = 1f;
    [SerializeField]
    private MirrorType mirrorType;
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
        gameObject.transform.Rotate(new Vector3(0, 0, bLeft ? 90 : -90));
       
    }

    public MirrorType GetMirrorType()
    {
        return mirrorType;
    }

    public Transform[] GetNewLaserLocation(Vector3 hitLocation)
    {
        switch (mirrorType)
        {
            case MirrorType.Normal:
                return new Transform[0];
            case MirrorType.Splitter:
                Transform[] transforms = new Transform[2];
                int currentPosition = 0;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child  = transform.GetChild(i);

                    if (child.transform.position != hitLocation && child.CompareTag("Mirror"))
                    {
                        transforms[currentPosition] = child.transform;
                        currentPosition++;
                    }
                }
                return transforms;
             case MirrorType.Both:
                return new Transform[0];
                
        }
        return new Transform[0];
    }

     
}

[Serializable]
public enum MirrorType
{
    Normal,
    Splitter,
    Both
}
