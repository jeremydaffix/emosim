

using System.Collections.Generic;

// an emotion is a set of perceptions
// and is viewed as "good" or "bad"
public class Emotion  {

    string name;
    List<Perception> perceptions = new List<Perception>(); // perceptions linked to the emotion
    int desirabilityScore; // 0 = neutral, -x = bad, +x= good

    public Emotion(string n, int score = 0)
    {
        name = n;
        DesirabilityScore = score;
    }


    public Emotion AddPerception(Perception p)
    {
        Perceptions.Add(p);

        return this;
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

    public int DesirabilityScore
    {
        get
        {
            return desirabilityScore;
        }

        set
        {
            desirabilityScore = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
