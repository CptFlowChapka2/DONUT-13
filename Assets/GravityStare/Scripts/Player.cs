using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded;
    public ForceReceiver thisForceReceiver;
    public Camera camera;
    private TagHandle handle;
    private Eyes _eyes;
    [SerializeField] private string[] tagsToSelect;
    
    private void Start()
    {
        handle=TagHandle.GetExistingTag("Ground");
        TagHandle[] tagHandles = new TagHandle[tagsToSelect.Length];
        for (int i = 0; i < tagsToSelect.Length; i++)
        {
            tagHandles[i] = TagHandle.GetExistingTag(tagsToSelect[i]);
        }
        _eyes = new Eyes(c:camera,t:transform,tagHandles);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int input =Vector3Int.CeilToInt(Input.mousePosition);
            var atrractor = _eyes.SelectAttractors(input);
           _eyes.SelectAttractors(input)?.AddOrToggleReceiver(thisForceReceiver,true);
        }
        
    }
}

class Eyes
{
    private Camera camera;
    private Transform parentTransform;
    private TagHandle[] valideTags;
    private int maxBufferValue;
    private int fixedPixelBuffer;

    
    
    public Eyes(Camera c , Transform t,TagHandle[] vt,int mbv =20,int fpb = 50)
    {
        camera = c;
        parentTransform = t;
        maxBufferValue = mbv;
        valideTags = vt;
        fixedPixelBuffer = fpb;
    }

    public ForceApplier SelectAttractors(Vector3Int input)
    {
        var hits = LookForAttractors();
        return  hits.ToList()
            .FindAll(x =>
            (new Vector2Int(x.posInSceenSpace.x, x.posInSceenSpace.y) - new Vector2Int(input.x, input.y)).magnitude<=x.rangeForValidation)
            .OrderBy(x=>x.posInSceenSpace.z)
            .FirstOrDefault()._forceApplier;
    }
    
    public HashSet<AttractorViewData> LookForAttractors()
    {
        RaycastHit[] hits = new RaycastHit[maxBufferValue];
        HashSet<AttractorViewData> hitViewData = new HashSet<AttractorViewData>(maxBufferValue);
        for (int i = 0; i < Physics.SphereCastNonAlloc(camera.transform.position,8,camera.transform.forward, hits); i++)
        {
            var raycastHit = hits[i];
            if (!valideTags.Contains(TagHandle.GetExistingTag(raycastHit.collider.tag))) continue;
            
            hitViewData.Add( CreateAttractorViewDataFromRaycastHit(raycastHit));
            
        }
        
        
        return hitViewData;
    }

    private AttractorViewData CreateAttractorViewDataFromRaycastHit(RaycastHit raycastHit)
    {
        AttractorViewData AVD = new AttractorViewData();
        AVD._forceApplier = raycastHit.collider.GetComponentInChildren<ForceApplier>();
        AVD.posInWorldSpace = AVD._forceApplier.transform.position;
        AVD.boundPosInWorldSpace = AVD._forceApplier.collider.bounds.max;
        
        AVD.posInSceenSpace =Vector3Int.CeilToInt(camera.WorldToScreenPoint(AVD.posInWorldSpace));
        AVD.boundPosInWorldSpace = Vector3Int.CeilToInt(camera.WorldToScreenPoint(AVD.boundPosInWorldSpace));

        AVD.rangeForValidation =
            Mathf.CeilToInt((AVD.posInSceenSpace - AVD.boundPosInScreenSpace).magnitude + fixedPixelBuffer);
        return AVD;
    }
}

struct AttractorViewData
{
    public ForceApplier _forceApplier;
    public Vector3 posInWorldSpace;
    public Vector3 boundPosInWorldSpace;
    public Vector3Int posInSceenSpace;
    public Vector3Int boundPosInScreenSpace;
    public int rangeForValidation;

}
