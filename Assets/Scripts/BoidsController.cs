using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidsController : MonoBehaviour
{
    private List<Transform> _boids = new List<Transform>();
    // Flocking Target. Gets Updated On Editor Changes
    public Transform Prefab;
    public Transform Target;

    // Boids Fields
    public int BoidAmount;
    public float NeighborDistance;
    public float MaxVelocity;
    public float MaxRotationAngle;
    public Vector3 InitialVelocity;

    // Flocking Behavior
    public float CohesionStep;
    public float CohesionWeight;
    public float SeparationWeight;
    public float AlignmentWeight;
    public float ArrivalSlowingDistance;
    public float ArrivalMaxSpeed;

    private void Start()
    {
        BoidAmount = 30;
        NeighborDistance = 10f;
        MaxVelocity = 0.15f;
        MaxRotationAngle = 7f;

        CohesionStep = 100f;
        CohesionWeight = 0.05f;
        SeparationWeight = 60f;
        AlignmentWeight = 0.01f;
        ArrivalSlowingDistance = 2f;
        ArrivalMaxSpeed = 0.2f;

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

    private void UpdateBoids()
    {
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

    private void Update()
    {
        UpdateBoids();
    }

    private IEnumerator UpdateOnFrame()
    {
        while (true)
        {
            UpdateBoids();
            yield return new WaitForSeconds(0.5f);
        }
    }

}
