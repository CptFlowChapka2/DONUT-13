using System.Linq;
using UnityEngine;

public class RendererAndPhysic : MonoBehaviour
{
    public Entity thisEntity;
    public GameObject appearance;

    public void ForcefullUpdateOfRenderParms()
    {
        Identity id =
            thisEntity.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x =>
                x.name == thisEntity.id);
        
        DestroyImmediate(appearance.gameObject);
        appearance = Instantiate(id.appearence, transform, false);
        appearance.transform.localPosition = Vector3.zero;
    }
}
