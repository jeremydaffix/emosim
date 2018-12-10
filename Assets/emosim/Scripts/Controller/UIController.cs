using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


// controller for the graphical interface
public class UIController : MonoBehaviour {


    // UI elements

    public GameObject CanvasNewSimu, CanvasPlay, CanvasObject, CanvasPerson, CanvasStats;
    public Text PersonsValue, ObjectsValue, AnimalsValue, ObstaclesValue, EmoValue, CognValue;
    public Slider PersonsSlider, ObjectsSlider, AnimalsSlider, ObstaclesSlider, EmoSlider, CognSlider;
    public InputField seedField;
    public Toggle innateToggle;


    public Slider SpeedSlider;
    public Text SpeedLabel;
    public GameObject PlayButton, PauseButton;

    public TextMeshProUGUI PersonTMP, ObjectTMP;


    public static UIController Instance;


    Person displayedPerson = null; // person displayed after the user clicks it
    InteractiveObjectInstance displayedObject = null; // object displayed after the user clicks it




    // Use this for initialization
    void Start () {

        Instance = this;

        CanvasPlay.gameObject.SetActive(false);
        CanvasPerson.gameObject.SetActive(false);
        CanvasObject.gameObject.SetActive(false);

        RefreshParamsValues();
	}
	
	// Update is called once per frame
	void Update () {

        DisplayPerson();
        DisplayObject();
	}


    // update the values of the labels in the parameters view
    public void RefreshParamsValues()
    {
        PersonsValue.text = ((int)PersonsSlider.value).ToString();
        ObjectsValue.text = ((int)ObjectsSlider.value).ToString();
        AnimalsValue.text = ((int)AnimalsSlider.value).ToString();
        ObstaclesValue.text = ((int)ObstaclesSlider.value).ToString();
        EmoValue.text = ((int)(EmoSlider.value * 100f)).ToString() + "%";
        CognValue.text = ((int)(CognSlider.value * 100f)).ToString() + "%";
    }



    // configure and start a simulation
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
        CanvasPerson.gameObject.SetActive(false);
        CanvasObject.gameObject.SetActive(false);

        Simulation.Instance.Playing = true;
        RefreshPlayValues();
        
    }



    // update the labels of the play view
    public void RefreshPlayValues()
    {
        SpeedLabel.text = (int)SpeedSlider.value + " turn/s";

        Simulation.Instance.TurnDuration = (int)(60f / SpeedSlider.value);

        PlayButton.SetActive(!Simulation.Instance.Playing);
        PauseButton.SetActive(Simulation.Instance.Playing);
    }


    // continue the simulation
    public void PlaySimu()
    {
        Simulation.Instance.Playing = true;
        RefreshPlayValues();
    }


    // pause the simulation
    public void PauseSimu()
    {
        Simulation.Instance.Playing = false;
        RefreshPlayValues();
    }


    // stop the simulation and go back to the configuration view
    public void StopSimu()
    {
        Simulation.Instance.Playing = false;

        Environment.Instance.DestroyEnvironment();

        CanvasNewSimu.gameObject.SetActive(true);
        CanvasPlay.gameObject.SetActive(false);
        CanvasPerson.gameObject.SetActive(false);
        CanvasObject.gameObject.SetActive(false);

        RefreshParamsValues();
    }



    // display data about a person
    public void DisplayPerson()
    {
        if (DisplayedPerson != null)
        {
            CanvasPerson.gameObject.SetActive(true);

            // map
            // needs
            // emotions
            // perceptions
            // nbr SM

            string txt = "";

            txt += "<b>Somatic Memory: </b>\n  " + DisplayedPerson.EmotionalMachine.SomaticMemory.Count + " markers\n";

            txt += "<b>Needs ( / 10): </b>\n";
            foreach(KeyValuePair<string, Need> kvp in DisplayedPerson.CognitiveMachine.Needs)
            {
                txt += "  " + kvp.Key + " = " + kvp.Value.CurrentScore.ToString("0.0") + "\n";
            }

            txt += "<b>Main Emotions: </b>\n  " + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(0).Key.Name + " (" + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(0).Value + ")\n";
            if (DisplayedPerson.EmotionalMachine.LastEmotions.Count > 1) txt += "  " + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(1).Key.Name + " (" + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(1).Value + ")\n";
            if (DisplayedPerson.EmotionalMachine.LastEmotions.Count > 2) txt += "  " + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(2).Key.Name + " (" + DisplayedPerson.EmotionalMachine.LastEmotions.ElementAt(2).Value + ")\n";


            txt += "<b>Perceptions: </b>\n";
            foreach (KeyValuePair<string, Perception> kvp in DisplayedPerson.EmotionalMachine.Perceptions)
            {
                if (kvp.Value.Organ.State == kvp.Value.State) // if the organ is in the current perception's state
                {
                    txt += "  " + kvp.Value.Organ.Name + " = " + kvp.Key + "\n";
                }
            }


            PersonTMP.text = txt;
        }

        else
        {
            CanvasPerson.gameObject.SetActive(false);
        }
    }


    // display data about an interactive object
    public void DisplayObject()
    {
        if (DisplayedObject != null)
        {
            CanvasObject.gameObject.SetActive(true);

            string txt = "";

            txt += "<b>Name: </b>\n";
            txt += "  " + DisplayedObject.InteractiveObjectName + "\n";

            txt += "<b>Needs Satisfied: </b>\n";
            foreach (KeyValuePair<string, float> kvp in DisplayedObject.InteractiveObject.NeedsSatisfied)
            {
                txt += "  " + kvp.Key + " -> " + kvp.Value.ToString("0.0") + "\n";
            }

            txt += "<b>Sight: </b>\n";
            foreach (string trigger in DisplayedObject.InteractiveObject.TriggerBySight)
            {
                txt += "  " + trigger + "\n";
            }

            txt += "<b>Smell: </b>\n";
            foreach (string trigger in DisplayedObject.InteractiveObject.TriggerBySmell)
            {
                txt += "  " + trigger + "\n";
            }

            txt += "<b>Taste: </b>\n";
            foreach (string trigger in DisplayedObject.InteractiveObject.TriggerByTaste)
            {
                txt += "  " + trigger + "\n";
            }

            ObjectTMP.text = txt;
        }

        else
        {
            CanvasObject.gameObject.SetActive(false);
        }
    }


    // close the display person view
    public void ClosePerson()
    {
        Environment.Instance.DisplayStandardMap();
        DisplayedPerson = null;
        Simulation.Instance.DesirabilityView = null;
    }


    // close the display object view
    public void CloseObject()
    {
        DisplayedObject = null;
    }







    public InteractiveObjectInstance DisplayedObject
    {
        get
        {
            return displayedObject;
        }

        set
        {
            displayedObject = value;
        }
    }

    public Person DisplayedPerson
    {
        get
        {
            return displayedPerson;
        }

        set
        {
            displayedPerson = value;
        }
    }
}
