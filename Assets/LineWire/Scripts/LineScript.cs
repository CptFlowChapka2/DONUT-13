using UnityEngine;

public class LineScript : MonoBehaviour
{
    [SerializeField] private float timerToNextPoint = 0.1f;
    public bool spawnNextPoint;
    private bool enableSpawn = true;

    public Camera cam;

    void Update()
    {
        if (timerToNextPoint > 0)
        {
            timerToNextPoint -= Time.deltaTime;
        }

        if (timerToNextPoint <= 0 && enableSpawn)
        {
            enableSpawn = false;

            spawnNextPoint = true;
        }

        if (spawnNextPoint)
        {
            spawnNextPoint = false;
            GameObject nextPoint = Instantiate(this.gameObject, transform.position + (Vector3.forward / 5) + new Vector3(Input.GetAxis("Horizontal") / 5, Input.GetAxis("Vertical") / 5 , 0), Quaternion.identity);
            cam.transform.position = new Vector3(0, 1.2f, nextPoint.transform.position.z - 3.5f);
        }
    }
}