using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cell;
    public float cooldown;
    private float t;
    
    void Update()
    {
        t += Time.deltaTime;
        if (t >= cooldown)
        {
            t = 0f;
            Instantiate(cell, transform.position, Random.rotation);
        }

    }
}
