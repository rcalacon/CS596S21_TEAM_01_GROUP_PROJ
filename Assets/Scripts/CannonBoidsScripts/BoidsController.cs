using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidsController : MonoBehaviour
{
    private static List<Transform> _boids = new List<Transform>();
    // Flocking Target. Gets Updated On Editor Changes
    public static Transform DragonPrefab;
    public static GameObject DragonGameObject;
    public static Transform ChickenPrefab;
    public static GameObject ChickenGameObject;
    public static Transform CondorPrefab;
    public static GameObject CondorGameObject;
    public static Transform Prefab;
    public static Transform Target;
    public static GameObject TargetGameObject;
    public GameObject gameManager;

    // Boids Fields
    public static int BoidAmount = 30;
    public float NeighborDistance;
    public static float MaxVelocity;
    public float MaxRotationAngle;
    public Vector3 InitialVelocity;

    // Flocking Behavior
    public float CohesionStep;
    public static float CohesionWeight;
    public static float SeparationWeight;
    public float AlignmentWeight;
    public float ArrivalSlowingDistance;
    public float ArrivalMaxSpeed;

    // Levels
    // Level Two
    private int lTwoBoidAmount = 60;
    private float lTwoMaxVelocity = 0.20f;
    private float lTwoSeparationWeight = 100f;
    private float lTwoCohesionWeight = 50f;

    // Level Three
    private int lThreeBoidAmount = 90;
    private float lThreeMaxVelocity = 0.30f;
    private float lThreeSeparationWeight = 200f;
    private float lThreeCohesionWeight = 100f;

    private void Start()
    {
        TargetGameObject = (GameObject)Instantiate(Resources.Load("Leader"));
        Target = TargetGameObject.transform;
        DragonGameObject = GameObject.Find("Dragon");
        ChickenGameObject = GameObject.Find("Chicken");
        CondorGameObject = GameObject.Find("Condor");
        DragonPrefab = DragonGameObject.transform;
        ChickenPrefab = ChickenGameObject.transform;
        CondorPrefab = CondorGameObject.transform;
        Prefab = ChickenPrefab;
        BoidAmount = 30;
        NeighborDistance = 10f;
        MaxVelocity = 0.10f;
        MaxRotationAngle = 7f;

        CohesionStep = 100f;
        CohesionWeight = 0.05f;
        SeparationWeight = 60f;
        AlignmentWeight = 0.01f;
        ArrivalSlowingDistance = 2f;
        ArrivalMaxSpeed = 0.2f;

        CreateBoids();
    }

    public void CreateBoids()
    {
        for (var boidIndex = 0; boidIndex < BoidAmount; boidIndex++)
        {
            var position = new Vector3(30, Random.Range(0f, 2.0f), Random.Range(-1.0f, 1.0f));
            var transform = Instantiate(Prefab, position, Quaternion.identity);
            transform.GetComponent<Boid>().ApplyFlockUpdates(position, InitialVelocity);
            _boids.Add(transform);
        }


        for (var boidIndex = 0; boidIndex < _boids.Count; boidIndex++)
        {
            var boid = _boids[boidIndex].GetComponent<Boid>();
            boid.UpdateNeighbors(_boids, NeighborDistance);
        }
    }

    public void ClearBoids()
    {
        for (var boidIndex = 0; boidIndex < _boids.Count; boidIndex++)
        {
            var boid = _boids[boidIndex];
            if(boid != null)
            {
                Destroy(boid.gameObject);
            }
        }
        _boids = new List<Transform>();
    }

    public void SetLevel(int level)
    {
        switch(level)
        {
            case 2:
                print("Starting Level Two");
                Prefab = CondorPrefab;
                BoidAmount = lTwoBoidAmount;
                MaxVelocity = lTwoMaxVelocity;
                SeparationWeight = lTwoSeparationWeight;
                CohesionWeight = lTwoCohesionWeight;
                break;
            case 3:
                print("Starting Level Three");
                Prefab = DragonPrefab;
                BoidAmount = lThreeBoidAmount;
                MaxVelocity = lThreeMaxVelocity;
                SeparationWeight = lThreeSeparationWeight;
                CohesionWeight = lThreeCohesionWeight;
                break;
            // Game Over
            case -1:
                MaxVelocity = 0.00001f;
                break;
        }
    }

    void UpdateBoids()
    {
        print(BoidAmount);
        print(MaxVelocity);
        print(SeparationWeight);
        print(CohesionWeight);
        for (var boidIndex = 0; boidIndex < _boids.Count; boidIndex++)
        {
            var boid = _boids[boidIndex];

            if (boid == null) continue;

            var boidComponent = boid.GetComponent<Boid>();
            // Update its neighbors within a distance
            boidComponent.UpdateNeighbors(_boids, NeighborDistance);
            // Steering Behaviors
            var cohesionVector = boidComponent.Cohesion(CohesionStep, CohesionWeight);
            var separationVector = boidComponent.Separation(SeparationWeight);
            var alignmentVector = boidComponent.Alignment(AlignmentWeight);
            var seekVector = boidComponent.Seek(Target);
            var socializeVector = boidComponent.Socialize(_boids);
            var arrivalVector = boidComponent.Arrival(Target, ArrivalSlowingDistance, ArrivalMaxSpeed);
            // Update Boid's Position and Velocity
            var velocity = boidComponent.Velocity + cohesionVector + separationVector + alignmentVector + seekVector + socializeVector + arrivalVector;
            velocity = boidComponent.LimitVelocity(velocity, MaxVelocity);
            velocity = boidComponent.LimitRotation(velocity, MaxRotationAngle, MaxVelocity);
            var position = boidComponent.Position + velocity;
            boidComponent.ApplyFlockUpdates(position, velocity);
        }
    }

    void Update()
    {
        UpdateBoids();
    }

    IEnumerator UpdateOnFrame()
    {
        while (true)
        {
            UpdateBoids();
            yield return new WaitForSeconds(0.5f);
        }
    }

}
