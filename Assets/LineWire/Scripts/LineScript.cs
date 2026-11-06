using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;
using Object = System.Object;

public class LineScript : MonoBehaviour
{
    [SerializeField] private float timerToNextPoint = 0.1f;
     private float timerToNextPointActual ;
     private int startingPos = -1;

    public SplineAnimate cam;

    public SplineContainer splineContainer;
    public SplineContainer CamsplineContainer;
    public SplineAnimate visual;

    private void Start()
    {
        timerToNextPointActual = timerToNextPoint;
    }

    void Update()
    {
        if (timerToNextPointActual > 0)
        {
            timerToNextPointActual -= Time.deltaTime;
        }

        if (!(timerToNextPointActual <= 0)) return;
        
        
        startingPos++;
        timerToNextPointActual = timerToNextPoint;
        splineContainer.Spline.Add(splineContainer.Spline[^1].Position +(float3)((Vector3.forward / 5) + new Vector3(Input.GetAxis("Horizontal") / 5, Input.GetAxis("Vertical") / 5 , 0)),TangentMode.AutoSmooth);
        CamsplineContainer.Spline.Add(CamsplineContainer.Spline[^1].Position +(float3)((Vector3.forward / 5)),TangentMode.Linear);
        if (startingPos >20&& !cam.IsPlaying)
        {
            cam.Play();
        }
        if (startingPos >0&& !visual.IsPlaying)
        {
            visual.Play();
        }
        
        
        
    }
}