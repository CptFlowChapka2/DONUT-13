using System;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private List<InteractableVolume> currentVolume=new List<InteractableVolume>();
    private bool testb=true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("button pressed");
            AskForCurrentVolume();
            var test = currentVolume.Find(x => x.allInteractables.Find(y => y.GetType() == typeof(OpenWall)))
                .allInteractables.Find(y => y.GetType() == typeof(OpenWall));
            if (test != null)
            {
                InteractionValues to = InteractionValues.Create();
                to.values.TryAdd(0, testb);
                var r =test.TryActivate("Open",to);
                Debug.Log(r.values[0] +"\n"+r.values[1]);
                testb = !testb;
            }
        }
    }

    private void AskForCurrentVolume()
    {
        currentVolume =
            God.Instance.allInteractableVolumes.FindAll(x => x.collider.bounds.Contains(transform.position));
    }
}
