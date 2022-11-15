
Shader "Amencioglu/FX/Desaturate01"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Opacity("Opacity", Range( 0 , 1)) = 0
		_DissolvetoSaturate("DissolvetoSaturate", Range( 0 , 1)) = 0
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass{ }
		CGPROGRAM
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
		};

		uniform float _Opacity;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _TextureSample3;
		uniform float4 _TextureSample3_ST;
		uniform float _DissolvetoSaturate;
		uniform float _Cutoff = 0.5;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 screenColor110 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_grabScreenPos.xy/ase_grabScreenPos.w);
			float3 desaturateInitialColor109 = ( _Opacity * screenColor110 ).rgb;
			float desaturateDot109 = dot( desaturateInitialColor109, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar109 = lerp( desaturateInitialColor109, desaturateDot109.xxx, 1.0 );
			o.Emission = desaturateVar109;
			o.Alpha = 1;
			float2 uv_TextureSample3 = i.uv_texcoord * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
			float lerpResult175 = lerp( -0.5 , 0.5 , _DissolvetoSaturate);
			float4 ahmet167 = ( tex2D( _TextureSample3, uv_TextureSample3 ) + lerpResult175 );
			clip( ahmet167.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
