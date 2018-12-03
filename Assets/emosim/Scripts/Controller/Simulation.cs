using UnityEngine;
using System.Collections;

public class Simulation : MonoBehaviour
{
    public static Simulation Instance = null;


    public int TurnDuration = 60; // in frames, at 60fps : 60 = 1s per turn
    public int TurnCpt = 0;






    // Use this for initialization
    void Start()
    {
        Instance = this;


        


       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ++TurnCpt;
    }
}
