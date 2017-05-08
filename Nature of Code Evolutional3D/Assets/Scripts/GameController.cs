using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public float worldRange = 50;
    public GameObject Target;
    public Food food;
    public Poison poison;
    public Creature creature;

    public List<Food> foods;
    public List<Poison> poisons;
    public List<Creature> creatures;

    public int foodNum = 30;
    public int poisonNum = 30;
    public int creatureNum = 30;
	// Use this for initialization
	void Start () {
        foods = new List<Food>();
        poisons = new List<Poison>();
        creatures = new List<Creature>();

        for (int i = 0; i < creatureNum; i++)
        {
            Creature newCreature = (Creature)Instantiate(creature);
            newCreature.init();
            creatures.Add(newCreature);
        }

        for (int i = 0; i < foodNum; i++)
        {
            Food newFood = (Food)Instantiate(food);
            newFood.init();
            foods.Add(newFood);
        }
        for (int i = 0; i < poisonNum; i++)
        {
            Poison newPoison = (Poison)Instantiate(poison);
            newPoison.init();
            poisons.Add(newPoison);
        }

    }

    // Update is called once per frame
    

    void Update () {
        foreach(Creature c in creatures)
        {
            c.behaviors(foods, poisons);
        }

	}
}
