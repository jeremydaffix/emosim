
using System.Collections.Generic;

// a somatic marker links a mental image and a set of perceptions
public class SomaticMarker
{
    MentalImage mentalImage;
    List<Perception> perceptions = new List<Perception>();

    // learnt : weight 3
    // innate : weight 2
    // empathy : weight 1
    // NOT USED YET
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
