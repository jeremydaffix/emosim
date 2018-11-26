using UnityEngine;
using System.Collections;

// a primary need has to be satisfied
// if not, we die :(
public class Need
{
    string name;
    int currentScore;

    public Need(string _name, int _currentScore = 5)
    {
        Name = _name;
        CurrentScore = _currentScore;
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

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
        }
    }
}
