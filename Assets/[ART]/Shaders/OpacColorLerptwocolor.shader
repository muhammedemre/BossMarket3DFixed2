
Shader "Amencioglu/Opac/Color/LerpTwoColor"
{
	Properties
	{
		_Mask("Mask", 2D) = "white" {}
		_Color1("Color 1", Color) = (0.3207547,0.1070996,0,0)
		_Color2("Color 2", Color) = (1,0.4555214,0.0990566,0)
		_Diffuse("Diffuse", Range( 0 , 1)) = 0
		_Emission("Emission", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Diffuse;
		uniform float _Emission;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 lerpResult103 = lerp( _Color1 , _Color2 , tex2D( _Mask, uv_Mask ));
			float4 lerpResult105 = lerp( float4( 0,0,0,0 ) , lerpResult103 , _Diffuse);
			o.Albedo = lerpResult105.rgb;
			float4 lerpResult106 = lerp( float4( 0,0,0,0 ) , lerpResult103 , _Emission);
			o.Emission = lerpResult106.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
7;6;1906;1005;596.9731;624.778;1;True;True
Node;AmplifyShaderEditor.ColorNode;101;-327.7728,-223.2132;Inherit;False;Property;_Color2;Color 2;2;0;Create;True;0;0;False;0;False;1,0.4555214,0.0990566,0;0.1748048,0.1023496,0.1886792,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;102;-318.7804,-432.9433;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;False;0.3207547,0.1070996,0,0;0.1927733,0.1262905,0.2075472,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;104;-407.6687,-10.58184;Inherit;True;Property;_Mask;Mask;0;0;Create;True;0;0;False;0;False;-1;None;a02fb0cf095f51e41a84b1bd61140f9c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;103;173.7802,-226.7825;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;107;55.02686,-497.778;Inherit;False;Property;_Diffuse;Diffuse;3;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;55.02686,-340.778;Inherit;False;Property;_Emission;Emission;4;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;105;378.0269,-597.778;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;106;496.0269,-108.778;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;94;964.8491,-342.657;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amencioglu/Opac/Color/LerpTwoColor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0.3490566,0.0722186,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;103;0;102;0
WireConnection;103;1;101;0
WireConnection;103;2;104;0
WireConnection;105;1;103;0
WireConnection;105;2;107;0
WireConnection;106;1;103;0
WireConnection;106;2;108;0
WireConnection;94;0;105;0
WireConnection;94;2;106;0
ASEEND*/
