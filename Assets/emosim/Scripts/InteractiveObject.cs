using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// an interactive object triggers some perceptions
// which can then be translated to emotions
// it can also satisfy some needs
public class InteractiveObject : MonoBehaviour
{
    string objectName;

    List<Perception> triggerBySight = new List<Perception>();
    List<Perception> triggerBySmell = new List<Perception>();
    List<Perception> triggerByTaste = new List<Perception>();

    Dictionary<Need, int> needsSatisfied = new Dictionary<Need, int>();


    float detectableFrom = 5.0f;


    public InteractiveObject(string n)
    {
        this.ObjectName = n;
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

    public List<Perception> TriggerBySight
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

    public List<Perception> TriggerBySmell
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

    public List<Perception> TriggerByTaste
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

    public Dictionary<Need, int> NeedsSatisfied
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
}
