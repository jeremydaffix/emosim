using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class contains an interactive object model
// used by one or many instances (see class InteractiveObjectInstance)
// an interactive object triggers some perceptions
// which can then be translated to emotions
// it can also satisfy some needs
public class InteractiveObject

{
    public const int TYPE_FOOD = 0;
    public const int TYPE_ANIMAL = 1;
    //public const int TYPE_MUSHROOM = 2;
    public const int TYPE_OBSTACLE = 2;

    Sprite sprite;


    string objectName;
    int type;
    
    List<string> triggerBySight = new List<string>(); // perceptions triggered when seen
    List<string> triggerBySmell = new List<string>(); // perceptions triggered when smell
    List<string> triggerByTaste = new List<string>(); // percetions triggered when tasted / eaten

    Dictionary<string, float> needsSatisfied = new Dictionary<string, float>(); // needs satisfied when eaten




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
