using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public LayerMask playerLayer;
    private Bandit Aggro;

    void Start()
    {
        Aggro = GetComponent<Bandit>();
    }    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("aggro");
            Aggro.aggro = true;
        }
    }

}
