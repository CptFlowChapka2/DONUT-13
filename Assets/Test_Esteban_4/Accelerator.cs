using UnityEngine;

public class Accelerator : MonoBehaviour
{

    public float power = 10;
    private void OnTriggerStay(Collider other)
    {
        
        other.attachedRigidbody ?.AddRelativeForce(transform.forward*power,ForceMode.Force);
    }
}
