using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Environment
{
    static Environment instance = null; // singleton


    private Dictionary<string, InteractiveObject> interactiveObjects = new Dictionary<string, InteractiveObject>();

    /*apple, carrot, endive, // food
                   chanterelle, amanita, // champipis
                   snake, spider, pig, chicken, // animals
                   tree, brambles; // obstacles
                   */



    private Environment()
    {
        // objets interactifs

        InteractiveObjects["apple"] = new InteractiveObject("apple", InteractiveObject.TYPE_FOOD);
        InteractiveObjects["apple"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["apple"].NeedsSatisfied.Add("health", 1);

        InteractiveObjects["apple"].TriggerBySight.Add("looksGood");
        InteractiveObjects["apple"].TriggerBySmell.Add("smellsGood");
        InteractiveObjects["apple"].TriggerByTaste.Add("tastesGood");

        InteractiveObjects["apple"].TriggerByTaste.Add("pleasureHormones");
        InteractiveObjects["apple"].TriggerByTaste.Add("smilingFace");
        InteractiveObjects["apple"].TriggerByTaste.Add("happyPosture");


        InteractiveObjects["carrot"] = new InteractiveObject("carrot", InteractiveObject.TYPE_FOOD);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("health", 2);

        InteractiveObjects["carrot"].TriggerBySight.Add("looksBad");
        InteractiveObjects["carrot"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["carrot"].TriggerByTaste.Add("tastesNormal");


        InteractiveObjects["endive"] = new InteractiveObject("endive", InteractiveObject.TYPE_FOOD);
        InteractiveObjects["endive"].NeedsSatisfied.Add("satiety", 2);
        InteractiveObjects["endive"].NeedsSatisfied.Add("health", 3);

        InteractiveObjects["endive"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["endive"].TriggerBySmell.Add("smellsBad");
        InteractiveObjects["endive"].TriggerByTaste.Add("tastesBad");

        InteractiveObjects["endive"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["endive"].TriggerByTaste.Add("closedPosture");


        InteractiveObjects["chanterelle"] = new InteractiveObject("chanterelle", InteractiveObject.TYPE_MUSHROOM);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("health", 0);

        InteractiveObjects["chanterelle"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["chanterelle"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["chanterelle"].TriggerByTaste.Add("tastesGood");


        InteractiveObjects["amanita"] = new InteractiveObject("amanita", InteractiveObject.TYPE_MUSHROOM);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("health", -4);

        InteractiveObjects["amanita"].TriggerBySight.Add("looksGood");
        InteractiveObjects["amanita"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["amanita"].TriggerByTaste.Add("tastesNormal");

        InteractiveObjects["amanita"].TriggerByTaste.Add("wantToVomit");
        InteractiveObjects["amanita"].TriggerByTaste.Add("painHormones");
        InteractiveObjects["amanita"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["amanita"].TriggerByTaste.Add("closedPosture");
    }




    public static Environment Instance
    {
        get
        {
            if (instance == null) instance = new Environment();

            return instance;
        }

    }

    public Dictionary<string, InteractiveObject> InteractiveObjects
    {
        get
        {
            return interactiveObjects;
        }

        set
        {
            interactiveObjects = value;
        }
    }
}
