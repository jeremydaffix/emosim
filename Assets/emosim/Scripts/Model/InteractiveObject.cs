using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// an interactive object triggers some perceptions
// which can then be translated to emotions
// it can also satisfy some needs
public class InteractiveObject

{
    // + tard : faire une factory !!!

    public const int TYPE_FOOD = 0;
    public const int TYPE_ANIMAL = 1;
    public const int TYPE_MUSHROOM = 2;
    public const int TYPE_OBSTACLE = 3;

    Sprite sprite;


    string objectName;
    int type;
    
    List<string> triggerBySight = new List<string>();
    List<string> triggerBySmell = new List<string>();
    List<string> triggerByTaste = new List<string>();

    Dictionary<string, float> needsSatisfied = new Dictionary<string, float>();


    float detectableFrom = 5.0f;


    public InteractiveObject(string n, int t, Sprite spr = null)
    {
        ObjectName = n;
        Type = t;
        Sprite = spr;
    }





    public string ObjectName
    {
        get
        {
            return objectName;
        }

        set
        {
            objectName = value;
        }
    }

    public float DetectableFrom
    {
        get
        {
            return detectableFrom;
        }

        set
        {
            detectableFrom = value;
        }
    }
    

    public int Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public List<string> TriggerBySight
    {
        get
        {
            return triggerBySight;
        }

        set
        {
            triggerBySight = value;
        }
    }

    public List<string> TriggerBySmell
    {
        get
        {
            return triggerBySmell;
        }

        set
        {
            triggerBySmell = value;
        }
    }

    public List<string> TriggerByTaste
    {
        get
        {
            return triggerByTaste;
        }

        set
        {
            triggerByTaste = value;
        }
    }

    public Dictionary<string, float> NeedsSatisfied
    {
        get
        {
            return needsSatisfied;
        }

        set
        {
            needsSatisfied = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }
}
