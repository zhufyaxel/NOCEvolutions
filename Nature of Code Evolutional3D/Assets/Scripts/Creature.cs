using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {
    public Vector3 acceleration = new Vector3(0, 0, 0);
    public Vector3 velocity = new Vector3(0, 0, 0);

    public float maxSpeed = 15.0f;
    public float maxForce = 7.5f;
    public float d = 50;
    public float Range = 6.0f;

    public Transform RangeCircle;
    public float[] DNA;

    public float health = 1.0f;
    // Use this for initialization
    public void init()
    {
        DNA = new float[2];
        DNA[0] = Random.Range(-5.0f, 5.0f);
        DNA[1] = Random.Range(-5.0f, 5.0f);
    }


    void Start() {

        // velocity.x = Random.Range(-2.0f, 2.0f);
        // velocity.y = Random.Range(-2.0f, 2.0f);
        // velocity.z = Random.Range(-2.0f, 2.0f);

    }

    // Update is called once per frame
    void Update() {
        velocity += (acceleration * Time.deltaTime);
        if (velocity.magnitude >= maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        transform.position += velocity * Time.deltaTime;
        transform.LookAt(velocity + transform.position);
        Bounce();
        acceleration.Set(0, 0, 0);
        RangeCircle.localScale = new Vector3(Range, Range, Range);
        health -= (-0.01f *Time.deltaTime);
    }

    public void behaviors(List<Food>foods, List<Poison> poisons)
    {
        int foodIndex = findNearestFood(foods);
        int poisonIndex = findNearestPoison(poisons);
        if (foodIndex != -1 && poisonIndex != -1)
        {

            Food targetFood = foods[foodIndex];
            Poison targetPoison = poisons[poisonIndex];
            float foodDis = Vector3.Distance(targetFood.gameObject.transform.position, transform.position);
            float poisonDis = Vector3.Distance(targetPoison.gameObject.transform.position, transform.position);

            combinedForces(targetFood.gameObject.transform, targetPoison.gameObject.transform);
            if (foodDis < Range)
            {
                Destroy(foods[foodIndex].gameObject);
                foods.RemoveAt(foodIndex);
            }
            if (poisonDis < Range)
            {
                Destroy(poisons[poisonIndex].gameObject);
                poisons.RemoveAt(poisonIndex);
            }
        }
    }

    public void combinedForces(Transform targetFood, Transform targetPoison)
    {
        Vector3 steerFood = seek(targetFood);
        Vector3 steerPoison = seek(targetPoison);

        steerFood *= DNA[0];
        steerPoison *= DNA[1];

        applyForce(steerFood + steerPoison);
    }

    public Vector3 seek(Transform target)
    {
        Vector3 desired = target.position - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - velocity;
        steer.Normalize();
        steer *= maxForce;
        return steer;
    }

    public void applyForce(Vector3 force)
    {
        acceleration += force;
    }

    public void Bounce()
    {
        if ((transform.position.x > d / 2 && velocity.x > 0) || (transform.position.x < -d / 2 && velocity.x < 0))
        {
            velocity.x *= -1;
        }
        if ((transform.position.y > d && velocity.y > 0) || (transform.position.y < 0 && velocity.y < 0))
        {
            velocity.y *= -1;
        }
        if ((transform.position.z > d / 2 && velocity.z > 0) || (transform.position.z < -d / 2 && velocity.z < 0))
        {
            velocity.z *= -1;
        }


    }
    public int findNearestFood(List<Food> foods)
    {
        float record = 99999;
        float dis = 99999;
        int targetIndex = -1;
        for (int i = 0; i < foods.Count; i++)
        {
            dis = Vector3.Distance(foods[i].transform.position, transform.position);
            if (dis < record)
            {
                record = dis;
                targetIndex = i;
                //Debug.Log(targetIndex);
            }

        }
        return targetIndex;
    }

    public int findNearestPoison(List<Poison> poisons)
    {
        float record = 99999;
        float dis = 99999;
        int targetIndex = -1;
        for (int i = 0; i < poisons.Count; i++)
        {
            dis = Vector3.Distance(poisons[i].transform.position, transform.position);
            if (dis < record)
            {
                record = dis;
                targetIndex = i;
            }

        }
        return targetIndex;
    }


   




}
