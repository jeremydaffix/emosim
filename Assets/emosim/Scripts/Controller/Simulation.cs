using UnityEngine;
using System.Collections;

/*
 
    Emo, Cogn (needs?)
    

    Pas de cogn :
    - ne mange que les saloperies
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
    - flemme objets loin (à compenser de toute manière avec cogn needs urgents)


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



    Immédiat = perceptions vue, odorat, goût
    Anticipation = remember object


    Affichages :
    Cartes de désirabilité selon perso sélectionné (cogn et emo)



    Difficultés :

    Fusion de perceptions de différentes sources




    TODO

    peur
    empathie
    needs
    animaux
    champipis
    truc fat

    params
    stats

    meilleur évitement obstacles

    si satiety < x alors augmenter score aliment selon remplissage besoin satiety
    si health < x alors réduire score aliment selon impact besoin health

    si target depuis trop longtemps on en change ?


    choses en cours
    map
    randomwalk after obstacle
    needs

*/


public class Simulation : MonoBehaviour
{
    public static Simulation Instance = null;


    public int TurnDuration = 60; // in frames, at 60fps : 60 = 1s per turn
    public int FrameCpt = 0;

    public int NbrPersons = 2;
    public int NbrObstacles = 10;
    public int NbrObjects = 20;

    public float EmotionalWeight = 1f;
    public float CognitiveWeight = 1f;

    public int Seed = 4242; // -1 if no seed


    Person desirabilityView = null;

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
        ++FrameCpt;
    }
}
