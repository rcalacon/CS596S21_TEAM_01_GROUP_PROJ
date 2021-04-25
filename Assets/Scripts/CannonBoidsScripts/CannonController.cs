using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    float aimHeight;
    public GameObject cannonBall;
    public GameObject gameManager;
    public float forceAmount;

    // Start is called before the first frame update
    void Start()
    {
        aimHeight = -20f;
        forceAmount = 5000f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the Screen positions of the object
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector3 mouseOnScreen = (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        Vector3 cannonAim = new Vector3(mouseOnScreen.y * aimHeight, -angle, angle);
        Quaternion cannonRotation = Quaternion.Euler(cannonAim);
        transform.rotation = cannonRotation;

        //Fire Cannon
        if (Input.GetMouseButtonDown(0) && !gameManager.GetComponent<CannonBoidsManager>().IsPaused() && !gameManager.GetComponent<CannonBoidsManager>().IsEnded())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var firedCannonBall = Instantiate(cannonBall, transform.position, Quaternion.identity);
            firedCannonBall.GetComponent<Rigidbody>().AddForce((ray.direction) * forceAmount);
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
