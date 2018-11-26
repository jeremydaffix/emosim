using UnityEngine;
using System.Collections;

// a mental image is a memory of an object, a situation,...
public class MentalImage
{
    public const int TYPE_OBJECT = 0;
    public const int TYPE_PERSON = 1;
    public const int TYPE_NEED = 2;

    int type;
    InteractiveObject objectRemembered;
    Person personRemembered;
    Need needRemembered;

    public MentalImage(int type, InteractiveObject objectRemembered)
    {
        this.type = type;
        this.objectRemembered = objectRemembered;
    }

    // ... ?

    public InteractiveObject ObjectRemembered
    {
        get
        {
            return objectRemembered;
        }

        set
        {
            objectRemembered = value;
        }
    }

    public Person PersonRemembered
    {
        get
        {
            return personRemembered;
        }

        set
        {
            personRemembered = value;
        }
    }

    public Need NeedRemembered
    {
        get
        {
            return needRemembered;
        }

        set
        {
            needRemembered = value;
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
}
