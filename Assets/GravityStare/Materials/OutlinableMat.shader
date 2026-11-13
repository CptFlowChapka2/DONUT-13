// Upgrade NOTE: upgraded instancing buffer 'OutlinableMatOutline' to new syntax.

// Made with Amplify Shader Editor v1.9.9.12
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "OutlinableMat"
{
	Properties
	{
		_OutlineScale( "OutlineScale", Range( 0, 1 ) ) = 0
		_OL_Color( "OL_Color", Color ) = ( 1, 0, 0, 0 )
		_BaseColor( "BaseColor", Color ) = ( 1, 1, 1, 0 )
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		#pragma multi_compile_instancing
		
		
		struct Input
		{
			half filler;
		};
		UNITY_INSTANCING_BUFFER_START(OutlinableMatOutline)
		UNITY_DEFINE_INSTANCED_PROP(float4, _OL_Color)
#define _OL_Color_arr OutlinableMatOutline
			UNITY_DEFINE_INSTANCED_PROP(float, _OutlineScale)
#define _OutlineScale_arr OutlinableMatOutline
		UNITY_INSTANCING_BUFFER_END(OutlinableMatOutline)
		
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float _OutlineScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_OutlineScale_arr, _OutlineScale);
			float outlineVar = _OutlineScale_Instance;
			v.vertex.xyz += ( v.normal * outlineVar );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			float4 _OL_Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_OL_Color_arr, _OL_Color);
			o.Emission = _OL_Color_Instance.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.5
		#pragma multi_compile_instancing
		#define ASE_VERSION 19912
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float4 _BaseColor;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _BaseColor.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback Off
	CustomEditor "AmplifyShaderEditor.MaterialInspector"
}
/*ASEBEGIN
Version=19912
{"type":"AmplifyShaderEditor.ColorNode, AmplifyShaderEditor","id":26,"pos":[-312,-56],"params":["Inherit","False","InstancedProperty","_OL_Color","OL_Color","1","0","Create","True","0","0","0","False","0","False","Object","-1","","1,0,0,0","1,0,0,0","True","True","0","6","COLOR","0","FLOAT","1","FLOAT","2","FLOAT","3","FLOAT","4","FLOAT3","5"]}
{"type":"AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor","id":15,"pos":[-344,184],"params":["Inherit","False","InstancedProperty","_OutlineScale","OutlineScale","0","0","Create","True","0","0","0","False","0","False","Object","-1","","0","-0.5","0","1","0","1","FLOAT","0"]}
{"type":"AmplifyShaderEditor.OutlineNode, AmplifyShaderEditor","id":51,"pos":[40,120],"params":["Inherit","False","0","True","None","0","0","Front","True","True","True","True","0","False","","0","3","0","FLOAT3","0,0,0","False","2","FLOAT","0","False","1","FLOAT","0","False","1","FLOAT3","0"]}
{"type":"AmplifyShaderEditor.ColorNode, AmplifyShaderEditor","id":53,"pos":[-32,-280],"params":["Inherit","False","Property","_BaseColor","BaseColor","2","0","Create","True","0","0","0","False","0","False","Object","-1","","1,1,1,0","1,0,0,0","True","True","0","6","COLOR","0","FLOAT","1","FLOAT","2","FLOAT","3","FLOAT","4","FLOAT3","5"]}
{"type":"AmplifyShaderEditor.StandardSurfaceOutputNode, AmplifyShaderEditor","id":52,"pos":[416,-144],"params":["Float","False","True","-1","3","AmplifyShaderEditor.MaterialInspector","0","0","Standard","OutlinableMat","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","False","Back","0","False","","0","False","","False","0","False","","0","False","","False","0","0","False","","0","Opaque","0.5","True","True","0","False","Opaque","","Geometry","All","14","all","True","True","True","True","0","False","","False","0","False","","255","False","","255","False","","0","False","","0","False","","0","False","","0","False","","0","False","","0","False","","0","False","","0","False","","False","2","15","10","25","False","0.5","True","0","0","False","","0","False","","0","0","False","","0","False","","0","False","","0","False","","0","False","0","0,0,0,0","VertexOffset","True","False","Cylindrical","False","True","Relative","0","","-1","-1","-1","-1","0","False","0","0","False","","-1","0","False","","0","0","0","False","0.1","False","","0","False","","False","17","0","FLOAT3","0,0,0","False","1","FLOAT3","0,0,0","False","2","FLOAT3","0,0,0","False","3","FLOAT","0","False","4","FLOAT","0","False","5","FLOAT","0","False","6","FLOAT3","0,0,0","False","7","FLOAT3","0,0,0","False","8","FLOAT","0","False","9","FLOAT","0","False","10","FLOAT","0","False","13","FLOAT3","0,0,0","False","11","FLOAT3","0,0,0","False","12","FLOAT3","0,0,0","False","16","FLOAT4","0,0,0,0","False","14","FLOAT4","0,0,0,0","False","15","FLOAT3","0,0,0","False","0"]}
{"wire":[51,0,26,5]}
{"wire":[51,1,15,0]}
{"wire":[52,0,53,0]}
{"wire":[52,11,51,0]}
ASEEND*/
//CHKSM=B1E32A786815E760C0AC4E862672BB0F29AD2EC8