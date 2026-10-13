using UnityEngine;

public class SwordMover : MonoBehaviour
{
    public Transform pointParent;
    private int index;
   
    void Update()
    {
        // transform.position = Vector3.Slerp(transform.position, pointParent.GetChild(index).position, 1f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, pointParent.GetChild(index).position, 1f * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, pointParent.GetChild(index).position) <= 0.1f)
            index = index >= pointParent.childCount - 1 ? 0 : index + 1;

    }
}
