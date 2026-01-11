using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 movementVector;
     Vector3 startPosition;
     Vector3 endPosition; 
     float movementFactor; //0 for not moved, 1 for fully moved


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed , 1f);
        transform.position = Vector3.Lerp(startPosition , endPosition , movementFactor);
    }
}
