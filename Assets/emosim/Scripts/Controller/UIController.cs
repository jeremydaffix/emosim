using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject CanvasNewSimu, CanvasPlay, CanvasObject, CanvasPerson, CanvasStats;
    public Text PersonsValue, ObjectsValue, AnimalsValue, ObstaclesValue, EmoValue, CognValue;
    public Slider PersonsSlider, ObjectsSlider, AnimalsSlider, ObstaclesSlider, EmoSlider, CognSlider;
    public InputField seedField;
    public Toggle innateToggle;


    public Slider SpeedSlider;
    public Text SpeedLabel;
    public GameObject PlayButton, PauseButton;



	// Use this for initialization
	void Start () {

        CanvasPlay.gameObject.SetActive(false);

        RefreshParamsValues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void RefreshParamsValues()
    {
        PersonsValue.text = ((int)PersonsSlider.value).ToString();
        ObjectsValue.text = ((int)ObjectsSlider.value).ToString();
        AnimalsValue.text = ((int)AnimalsSlider.value).ToString();
        ObstaclesValue.text = ((int)ObstaclesSlider.value).ToString();
        EmoValue.text = ((int)(EmoSlider.value * 100f)).ToString() + "%";
        CognValue.text = ((int)(CognSlider.value * 100f)).ToString() + "%";
    }


    public void StartSimu()
    {
        Simulation.Instance.FrameCpt = 0;

        Simulation.Instance.NbrPersons = (int)PersonsSlider.value;
        Simulation.Instance.NbrObjects = (int)ObjectsSlider.value;
        Simulation.Instance.NbrAnimals = (int)AnimalsSlider.value;
        Simulation.Instance.NbrObstacles = (int)ObstaclesSlider.value;

        Simulation.Instance.EmotionalWeight = EmoSlider.value;
        Simulation.Instance.CognitiveWeight = CognSlider.value;

        Simulation.Instance.InnateEnabled = innateToggle.isOn;

        if (!seedField.text.Equals("")) Simulation.Instance.Seed = int.Parse(seedField.text);
        else Simulation.Instance.Seed = 4242;

        GetComponent<Environment>().CreateEnvironment();

        CanvasNewSimu.gameObject.SetActive(false);
        CanvasPlay.gameObject.SetActive(true);

        Simulation.Instance.Playing = true;
        RefreshPlayValues();
        
    }



    public void RefreshPlayValues()
    {
        SpeedLabel.text = (int)SpeedSlider.value + " turn/s";

        Simulation.Instance.TurnDuration = (int)(60f / SpeedSlider.value);

        PlayButton.SetActive(!Simulation.Instance.Playing);
        PauseButton.SetActive(Simulation.Instance.Playing);
    }

    public void PlaySimu()
    {
        Simulation.Instance.Playing = true;
        RefreshPlayValues();
    }

    public void PauseSimu()
    {
        Simulation.Instance.Playing = false;
        RefreshPlayValues();
    }

    public void StopSimu()
    {
        Simulation.Instance.Playing = false;

        Environment.Instance.DestroyEnvironment();

        CanvasNewSimu.gameObject.SetActive(true);
        CanvasPlay.gameObject.SetActive(false);

        RefreshParamsValues();
    }
}
