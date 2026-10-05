using System.Linq;
using UnityEngine;

public class RendererAndPhysic : MonoBehaviour
{
    public Entity thisEntity;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public Collider curentColider;

    public void ForcefullUpdateOfRenderParms()
    {
        Identity c_identity = thisEntity.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x=>x.name==thisEntity.id) ;
        Color c = (c_identity.blue, c_identity.red) switch
        {
            (false, false) => Color.white,
            (true, false) => Color.blue,
            (false, true) => Color.red,
            (true, true) => Color.blueViolet
        };

        meshRenderer.material.color = c;
    }
}
