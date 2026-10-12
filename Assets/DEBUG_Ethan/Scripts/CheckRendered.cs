using UnityEngine;

public class CheckRendered : MonoBehaviour
{
    public Renderer selfRenderer;
    
    void Start()
    {
        // selfRenderer = GetComponent<Renderer>();
    }

    
    void Update()
    {
        if (selfRenderer.isVisible)
        {
            Vector3 ptOnScreen = Camera.current.WorldToScreenPoint(selfRenderer.bounds.center);
            
            // if(ptOnScreen.z < 0)
            if((ptOnScreen.x < 0) || (ptOnScreen.x > Screen.width) || (ptOnScreen.y < 0) || (ptOnScreen.y > Screen.height))
                Debug.Log("i'm here, i'm late !!!");
        }

        else
        {
            Debug.Log("... ...... ... ....");
        }
        
    }
}
