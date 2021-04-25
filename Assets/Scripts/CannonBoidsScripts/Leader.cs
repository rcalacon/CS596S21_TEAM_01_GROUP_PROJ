using UnityEngine;

public class Leader : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 newPosition;

    void Start()
    {
        InvokeRepeating("SetRandomPos", 0, 1);
    }

    void SetRandomPos()
    {
        newPosition = new Vector3(50, Random.Range(0f, 10f), Random.Range(-25f, 25f));
    }

    // Update is called once per frame
    private void Update()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, step);
    }
}
