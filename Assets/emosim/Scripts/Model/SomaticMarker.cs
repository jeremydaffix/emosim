
using System.Collections.Generic;

// a somatic marker links a mental image and a set of perceptions
public class SomaticMarker
{
    MentalImage mentalImage;
    List<Perception> perceptions = new List<Perception>();

    public SomaticMarker(MentalImage mentalImage)
    {
        this.mentalImage = mentalImage;
    }

    public MentalImage MentalImage
    {
        get
        {
            return mentalImage;
        }

        set
        {
            mentalImage = value;
        }
    }

    public List<Perception> Perceptions
    {
        get
        {
            return perceptions;
        }

        set
        {
            perceptions = value;
        }
    }
}
