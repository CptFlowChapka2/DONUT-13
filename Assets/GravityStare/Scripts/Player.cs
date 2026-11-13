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

    private HashSet<AttractorViewData> seenAplier;
    public Vector3 normalOfClosestPointOnClosestSurface;
    
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
       
        if (Input.GetMouseButtonDown(1))
        {
            if(seenAplier is not null) foreach (var attractorViewData in seenAplier)
            {
                attractorViewData._forceApplier.meshRenderer.material.SetColor("_OL_Color", Color.indianRed);
            }
            Vector3Int input =Vector3Int.CeilToInt(Input.mousePosition);
            seenAplier = _eyes.AimForAttractor(input,seenAplier);
            var first = seenAplier.FirstOrDefault()._forceApplier;
            first?.meshRenderer.material.SetColor("_OL_Color",Color.green);
            first?.AddOrToggleReceiver(thisForceReceiver,true);
        }
        
    }

    private void FixedUpdate()
    {
        if (!isGrounded) return;
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f))
        {
            transform.up = hit.normal;
        }
        else
        {
            isGrounded = false;
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

    public HashSet<AttractorViewData> AimForAttractor(Vector3Int input,HashSet<AttractorViewData> set = null)
    {
        if (set is null) set = new HashSet<AttractorViewData>(maxBufferValue);
        set.Clear();
        set = LookForAttractors(input,set);
        foreach (var attractorViewData in set)
        {
            attractorViewData._forceApplier.meshRenderer.material.SetColor("_OL_Color", Color.royalBlue);
        }
        return set.ToList()
            .FindAll(x =>
                (new Vector2Int(x.posInSceenSpace.x, x.posInSceenSpace.y) - new Vector2Int(input.x, input.y))
                .magnitude <= x.rangeForValidation && x.boundPosInScreenSpace.z >= 0)
            .OrderBy(x => x.posInSceenSpace.z)
            .ToHashSet();
    }
    
    public HashSet<AttractorViewData> LookForAttractors(Vector3Int input,HashSet<AttractorViewData> set)
    {
        RaycastHit[] hits = new RaycastHit[maxBufferValue];
        HashSet<AttractorViewData> hitViewData = set;
        for (int i = 0; i < Physics.SphereCastNonAlloc(camera.transform.position,200,camera.transform.forward, hits); i++)
        {
            var raycastHit = hits[i];
            if (!valideTags.Contains(TagHandle.GetExistingTag(raycastHit.collider.tag))) continue;
            hitViewData.Add( CreateAttractorViewDataFromRaycastHit(raycastHit,input));
            
        }
        
        
        return hitViewData;
    }

    private AttractorViewData CreateAttractorViewDataFromRaycastHit(RaycastHit raycastHit,Vector3Int input)
    {
        AttractorViewData AVD = new AttractorViewData();
        AVD._forceApplier = raycastHit.collider.GetComponentInChildren<ForceApplier>();
        
        AVD.posInWorldSpace = AVD._forceApplier.transform.position;
        AVD.posInSceenSpace =Vector3Int.CeilToInt(camera.WorldToScreenPoint(AVD.posInWorldSpace));
        
        AVD.boundPosInWorldSpace = raycastHit.collider.ClosestPointOnBounds(camera.ScreenToWorldPoint(new Vector3(input.x,input.y,AVD.posInSceenSpace.z)));
        AVD.boundPosInScreenSpace = Vector3Int.CeilToInt(camera.WorldToScreenPoint(AVD.boundPosInWorldSpace));

        int pixelBuffer = Mathf.CeilToInt(fixedPixelBuffer/(AVD.posInSceenSpace - AVD.boundPosInScreenSpace).magnitude);
        AVD.rangeForValidation =
            Mathf.CeilToInt((AVD.posInSceenSpace - AVD.boundPosInScreenSpace).magnitude + pixelBuffer);
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
