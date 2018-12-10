using UnityEngine;
using System.Collections;

// a primary need has to be satisfied
// if not, we die :(
public class Need
{
    string name;
    float currentScore; // score / 10
    float decreaseByTurn; // how much we decrease the satisfaction (score) of this need each turn

    public Need(string _name, float _currentScore = 5, float _decreaseByTurn = 0f)
    {
        Name = _name;
        CurrentScore = _currentScore;
        DecreaseByTurn = _decreaseByTurn;
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

    public float CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
            if (currentScore > 10f) currentScore = 10f; // 10 max
            else if (currentScore < 0f) currentScore = 0f; // 0 min
        }
    }

    public float DecreaseByTurn
    {
        get
        {
            return decreaseByTurn;
        }

        set
        {
            decreaseByTurn = value;
        }
    }
}
