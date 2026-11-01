using UnityEditor.Rendering;
using UnityEngine;

public class CaseScript : MonoBehaviour
{
    public bool mur = false;

    public bool Mur
    {
        get => mur;
        set
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
            mur = value;
        }
    }
}
