// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amencioglu/Transparency/Texture/Alpha01"
{
	Properties
	{
		_MainColor("MainColor", Color) = (0,0,0,0)
		_Texture("Texture", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _MainColor;
		uniform sampler2D _Texture;
		SamplerState sampler_Texture;
		uniform float4 _Texture_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_1_0 = _MainColor;
			o.Emission = temp_output_1_0.rgb;
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float4 tex2DNode18 = tex2D( _Texture, uv_Texture );
			o.Alpha = tex2DNode18.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
2795;207;1392;886;915.8815;392.0757;1;True;False
Node;AmplifyShaderEditor.ColorNode;1;-667.5,-264.7;Inherit;False;Property;_MainColor;MainColor;0;0;Create;True;0;0;False;0;False;0,0,0,0;0.9846833,0.4669811,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-440.1289,-52.10859;Inherit;True;Property;_Texture;Texture;1;0;Create;True;0;0;False;0;False;-1;None;3a4be7b272791b8479754e38b8bbadbb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-27.20341,-190.7172;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;19;220.9,-154.6;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amencioglu/Transparency/Texture/Alpha01;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;18;4
WireConnection;5;1;1;0
WireConnection;19;2;1;0
WireConnection;19;9;18;4
ASEEND*/
//CHKSM=3CD4B1F8D5D3311182DBFA465FDFB41144BEC088