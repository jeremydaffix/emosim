
using System.Collections.Generic;

// a somatic marker links a mental image and a set of perceptions
public class SomaticMarker
{
    MentalImage mentalImage;
    List<Perception> perceptions = new List<Perception>();

    // acquis : weight 3
    // inné : weight 2
    // imitation : weight 1
    int weight;

    public SomaticMarker(MentalImage mentalImage, int w = 2)
    {
        this.mentalImage = mentalImage;
        weight = w;
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

    public int Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }
}
