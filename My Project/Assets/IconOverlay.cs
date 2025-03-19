using UnityEngine;

public class IconOverlay : MonoBehaviour
{
    [SerializeField] private GameObject doubleJump;
    [SerializeField] private GameObject wallJump;
    private CollManagement collection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collection = GameObject.Find("Collection Obj").GetComponent<CollManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collection.collItems == 1)
        {
            wallJump.SetActive(true);
        }
        else if (collection.collItems == 2)
        {
            doubleJump.SetActive(true);
        }
    }
}
