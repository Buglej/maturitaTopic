using UnityEngine;

public class collectiblesFloat : MonoBehaviour
{
    private float initialY;
    [SerializeField] private float floatAmplitude = 0.5f; // Amplitude of the floating effect
    [SerializeField] private float floatFrequency = 1f; // Frequency of the floating effect

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply a sinusoidal movement to the object's position
        float newY = initialY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
