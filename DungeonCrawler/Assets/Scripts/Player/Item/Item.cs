using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum ItemType
{
    NONE,
    LIFE,
    SCORE,
    END,
}

[Serializable]
public struct Pickup
{
    public ItemType type;
    public int value;

    public Pickup(ItemType t, int v)
    {
        type = t;
        value = v;
    }
}

public class Item : MonoBehaviour {

    public Pickup action;
    public bool destroy;
    [SerializeField]
    List<Sprite> sprites;
    
    public Pickup Collect()
    {
        if (destroy)
        {
            return new Pickup(ItemType.NONE, 0);
        }
        destroy = true;
        Destroy(gameObject, Time.deltaTime);
        return action;
    }

    public void SetValue(int index, int value)
    {
        action.value = value;
        if (sprites == null || sprites.Count <= index)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
