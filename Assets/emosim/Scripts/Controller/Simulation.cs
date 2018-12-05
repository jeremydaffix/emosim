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
    - pas d'apprentissage des autres
    - pas d'idée sur les éléments non connus
    - trop lent

  
  
    Inné, Acquis (+ Apprentissage)

    Pas d'inné :
    - comportements dangereux
    - pas d'instinct pour aller aux bons trucs
    - moins rapide

    Pas d'acquis :
    - ne remet pas en cause ses instincts
    - emo perd de son intérêt



    Affichages :
    Cartes de désirabilité selon perso sélectionné (cogn et emo)

*/


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
