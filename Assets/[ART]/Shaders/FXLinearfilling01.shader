
Shader "Amencioglu/FX/Linearfilling01"
{
	Properties
	{
		_FirstColor("FirstColor", Color) = (0,0,0,0)
		_SecondColor("Second Color", Color) = (1,1,1,0)
		_StrokeColor("StrokeColor", Color) = (0.507209,0.7414559,0.7735849,0)
		_Process("Process", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Background+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float4 _FirstColor;
		uniform float4 _SecondColor;
		uniform float _Process;
		uniform float4 _StrokeColor;


		struct Gradient
		{
			int type;
			int colorsLength;
			int alphasLength;
			float4 colors[8];
			float2 alphas[8];
		};


		Gradient NewGradient(int type, int colorsLength, int alphasLength, 
		float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
		float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
		{
			Gradient g;
			g.type = type;
			g.colorsLength = colorsLength;
			g.alphasLength = alphasLength;
			g.colors[ 0 ] = colors0;
			g.colors[ 1 ] = colors1;
			g.colors[ 2 ] = colors2;
			g.colors[ 3 ] = colors3;
			g.colors[ 4 ] = colors4;
			g.colors[ 5 ] = colors5;
			g.colors[ 6 ] = colors6;
			g.colors[ 7 ] = colors7;
			g.alphas[ 0 ] = alphas0;
			g.alphas[ 1 ] = alphas1;
			g.alphas[ 2 ] = alphas2;
			g.alphas[ 3 ] = alphas3;
			g.alphas[ 4 ] = alphas4;
			g.alphas[ 5 ] = alphas5;
			g.alphas[ 6 ] = alphas6;
			g.alphas[ 7 ] = alphas7;
			return g;
		}


		float4 SampleGradient( Gradient gradient, float time )
		{
			float3 color = gradient.colors[0].rgb;
			UNITY_UNROLL
			for (int c = 1; c < 8; c++)
			{
			float colorPos = saturate((time - gradient.colors[c-1].w) / (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1);
			color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
			}
			#ifndef UNITY_COLORSPACE_GAMMA
			color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
			#endif
			float alpha = gradient.alphas[0].x;
			UNITY_UNROLL
			for (int a = 1; a < 8; a++)
			{
			float alphaPos = saturate((time - gradient.alphas[a-1].y) / (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1);
			alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
			}
			return float4(color, alpha);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			Gradient gradient75 = NewGradient( 0, 2, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float4 temp_cast_0 = (_Process).xxxx;
			float4 lerpResult82 = lerp( _FirstColor , _SecondColor , step( SampleGradient( gradient75, i.uv_texcoord.x ) , temp_cast_0 ));
			float4 lerpResult87 = lerp( lerpResult82 , ( _StrokeColor * i.vertexColor ) , i.vertexColor);
			o.Emission = lerpResult87.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
1013;73;1014;888;896.0269;-941.0901;1.544655;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;77;-1050.673,1527.241;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GradientNode;75;-988.9509,1351.215;Inherit;False;0;2;2;0,0,0,0;1,1,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-431.5046,1647.418;Inherit;False;Property;_Process;Process;4;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GradientSampleNode;76;-680.3391,1442.373;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;80;-72.8004,1416.456;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;68;-568.1495,607.3229;Inherit;False;Property;_FirstColor;FirstColor;1;0;Create;True;0;0;False;0;False;0,0,0,0;0.5137255,0.5534198,0.6705883,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;69;-577.3241,769.142;Inherit;False;Property;_SecondColor;Second Color;2;0;Create;True;0;0;False;0;False;1,1,1,0;0.8867924,0.6318907,0.0543788,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;84;-368.3467,1823.33;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;86;-374.7654,2077.041;Inherit;False;Property;_StrokeColor;StrokeColor;3;0;Create;True;0;0;False;0;False;0.507209,0.7414559,0.7735849,0;0.8867924,0.6318907,0.0543788,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;82;237.4055,1349.934;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;15.20595,2067.757;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;87;489.0005,1563.439;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;752.6844,944.5447;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amencioglu/FX/Linearfilling01;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Background;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;76;0;75;0
WireConnection;76;1;77;1
WireConnection;80;0;76;0
WireConnection;80;1;81;0
WireConnection;82;0;68;0
WireConnection;82;1;69;0
WireConnection;82;2;80;0
WireConnection;85;0;86;0
WireConnection;85;1;84;0
WireConnection;87;0;82;0
WireConnection;87;1;85;0
WireConnection;87;2;84;0
WireConnection;0;2;87;0
ASEEND*/
