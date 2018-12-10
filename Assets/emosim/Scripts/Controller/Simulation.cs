using UnityEngine;
using System.Collections;

/*
 
    Emo, Cogn
    

    Pas de cogn :
    - ne mange que les objets attirants, sans considération pour sa santé et ses besoins
    - ne se fie qu'à son intuition 1ère (odeur, vision)
    - si coincé dans un coin sans trucs bons, ne mangent rien
    - trop d'empathie envers animaux mignons
    - trop de comportements à risques avec trucs bons mais dangereux
    - foncent dans les obstacles lol

    Pas d'emo :
    - pas d'apprentissage des autres ni de sa propre xp
    - pas d'idée sur les éléments non connus
    - trop lent
    - pas de peur
    - flemme objets loin et / ou pas bons


    Emo = choix target, décision de fuite
    Cogn = évitement, satisfaction besoins urgents, connaissances objectives, distance target
  
  
    Inné, Acquis (+ Apprentissage)

    Pas d'inné :
    - comportements dangereux
    - pas d'instinct pour aller aux bons trucs
    - moins rapide

    Pas d'acquis :
    - ne remet pas en cause ses instincts
    - emo perd de son intérêt

    Inné = sensors + ms innés

    Immédiat = perceptions vue, odorat, goût
    Anticipation = remember object


    Affichages :
    Cartes de désirabilité selon perso sélectionné (cogn et emo)



    Difficultés :

    Fusion de perceptions de différentes sources



    TODO

    pb needs
    empathie / apprentissage autrui
    gros bug distances


    stats

    équilibrer
    voir si les scénarios marchent

    slides
    uml
    comments


    

*/


// this class contains all the parameters of the simulation
public class Simulation : MonoBehaviour
{
    public static Simulation Instance = null;


    public int TurnDuration = 60; // in frames, at 60fps : 60 = 1s per turn
    public int FrameCpt = 0;

    public int NbrPersons = 2; // number of persons
    public int NbrObstacles = 10; // number of obstacles
    public int NbrObjects = 20; // number of interactive objects (without obstacles and animals)
    public int NbrAnimals = 3; // number of animals

    // ratio emotional / cognitive
    public float EmotionalWeight = 1f;
    public float CognitiveWeight = 1f;

    public int Seed = 4242; // -1 if no seed

    public bool InnateEnabled = true; // innate (somatic markers loaded at the beginning + "sensors" (looksGood, smellsBad, looksTerrifying,...))

    public bool Playing = false; // currently playing or paused


    Person desirabilityView = null; // displays "desirability map" of a person, null if "standard map"


    public Simulation()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start()
    {
        //Instance = this;

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Playing) ++FrameCpt;
    }






    public Person DesirabilityView
    {
        get
        {
            return desirabilityView;
        }

        set
        {
            if (value == null) Environment.Instance.DisplayStandardMap(); // back to standard map
            desirabilityView = value;
        }
    }
}
