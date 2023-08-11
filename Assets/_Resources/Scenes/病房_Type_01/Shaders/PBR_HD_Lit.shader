// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PBR/2020/PBR_HD_Lit"
{
	Properties
	{
		[Enum(UnityEngine.Rendering.CullMode)]_Culling("Culling", Float) = 2
		[Toggle]_CutOff("CutOff", Float) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Color("Color", Color) = (1,1,1,1)
		[NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
		[NoScaleOffset]_MM("MG(M)(AO)(BM)(SR)", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[Enum(Smothness,0,Roughness,1)]_RSType("Smothness(Roughness)", Float) = 1
		_SRMin("Smothness(Roughness) Min", Range( 0 , 1)) = 0
		_SRMax("Smothness(Roughness) Max", Range( 0 , 1)) = 1
		_AO("AO", Range( 0 , 1)) = 0
		[NoScaleOffset][Normal]_BumpMap("Normal", 2D) = "bump" {}
		_NormalInt("Normal Int", Range( 0 , 3)) = 1
		[Toggle]_Emissive("Emissive", Float) = 0
		[HDR]_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		[HDR][NoScaleOffset]_EmissiveMap("Emissive Map", 2D) = "white" {}
		_Tiling("Tiling", Vector) = (1,1,0,0)
		_Offset("Offset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull [_Culling]
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
		};

		uniform float _Culling;
		uniform sampler2D _BumpMap;
		uniform float2 _Tiling;
		uniform float2 _Offset;
		uniform float _NormalInt;
		uniform float4 _Color;
		uniform sampler2D _Albedo;
		uniform float4 _EmissiveColor;
		uniform sampler2D _EmissiveMap;
		uniform float _Emissive;
		uniform sampler2D _MM;
		uniform float _Metallic;
		uniform float _SRMin;
		uniform float _SRMax;
		uniform float _RSType;
		uniform float _AO;
		uniform float _CutOff;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord109 = i.uv_texcoord * _Tiling + _Offset;
			float3 break235 = UnpackScaleNormal( tex2D( _BumpMap, uv_TexCoord109 ), _NormalInt );
			float4 appendResult237 = (float4(break235.x , break235.y , ( break235.z * i.ASEVFace ) , 0.0));
			o.Normal = appendResult237.xyz;
			float4 tex2DNode106 = tex2D( _Albedo, uv_TexCoord109 );
			o.Albedo = ( _Color * tex2DNode106 ).rgb;
			o.Emission = ( _EmissiveColor * tex2D( _EmissiveMap, uv_TexCoord109 ) * _Emissive ).rgb;
			float4 tex2DNode132 = tex2D( _MM, uv_TexCoord109 );
			o.Metallic = ( tex2DNode132.r * _Metallic );
			float clampResult146 = clamp( tex2DNode132.a , _SRMin , _SRMax );
			float lerpResult223 = lerp( clampResult146 , ( 1.0 - clampResult146 ) , _RSType);
			o.Smoothness = lerpResult223;
			float lerpResult174 = lerp( 1.0 , tex2DNode132.g , _AO);
			o.Occlusion = lerpResult174;
			o.Alpha = 1;
			float lerpResult232 = lerp( 1.0 , ( _Color.a * tex2DNode106.a ) , _CutOff);
			clip( lerpResult232 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Legacy Shaders/Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;915.2;2128;733;-307.2147;447.3426;1.711932;True;False
Node;AmplifyShaderEditor.CommentaryNode;196;-1685.903,-1356.295;Inherit;False;1854.341;1934.403;Main Tex;24;132;109;122;115;113;114;130;174;223;102;106;209;126;107;117;108;111;222;116;146;145;110;227;231;Main Tex;1,0.4103774,0.9015171,1;0;0
Node;AmplifyShaderEditor.Vector2Node;115;-1613.433,-867.9559;Float;False;Property;_Tiling;Tiling;16;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;122;-1619.452,-695.5003;Inherit;False;Property;_Offset;Offset;17;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;109;-1369.119,-782.3006;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;107;-1033.165,259.386;Float;False;Property;_NormalInt;Normal Int;12;0;Create;True;0;0;0;False;0;False;1;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-724.0844,56.55165;Float;False;Property;_SRMax;Smothness(Roughness) Max;9;0;Create;False;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;145;-726.702,-25.74644;Float;False;Property;_SRMin;Smothness(Roughness) Min;8;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;132;-1178.427,-255.0018;Inherit;True;Property;_MM;MG(M)(AO)(BM)(SR);5;1;[NoScaleOffset];Create;False;0;0;0;False;0;False;-1;None;0ff10421776ccc148b34e4a5b97ffffe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;130;-697.1232,177.6434;Inherit;True;Property;_BumpMap;Normal;11;2;[NoScaleOffset];[Normal];Create;False;0;0;0;False;0;False;-1;None;bb2ee93853ec5a8429cb8d7d9f02f0b6;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;126;-649.5029,-1262.295;Inherit;False;Property;_Color;Color;3;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FaceVariableNode;234;2654.447,771.122;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;146;-387.0598,-62.16954;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;106;-736.303,-1080.194;Inherit;True;Property;_Albedo;Albedo;4;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;22a2bc840d17c374cb760259f0753734;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;235;2631.822,620.343;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;116;-720.6993,-181.2843;Float;False;Property;_AO;AO;10;0;Create;True;0;0;0;False;0;False;0;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;236;2887.822,738.343;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;102;-656.8082,-821.7891;Inherit;False;Property;_EmissiveColor;Emissive Color;14;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;198;329.9583,660.1043;Inherit;False;580.0068;308.6493;Detail Mask Control;3;118;148;129;Detail Mask Control;0.1251335,0.7169812,0.1411825,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;231;-334.1854,-982.368;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;197;-1706.252,1230.95;Inherit;False;1897.5;1923.528;Detail Tex;26;175;119;152;163;151;120;153;211;149;112;160;150;157;159;155;165;156;220;221;127;217;125;103;226;228;224;Detail Tex;0.3160377,0.8603439,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-719.6364,-311.8798;Float;False;Property;_Metallic;Metallic;6;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;209;-357.7745,-625.8589;Inherit;False;Property;_Emissive;Emissive;13;1;[Toggle];Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;117;-219.4753,76.87758;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;108;-744.0334,-616.0385;Inherit;True;Property;_EmissiveMap;Emissive Map;15;2;[HDR];[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;233;2484.32,-165.7913;Inherit;False;Property;_CutOff;CutOff;1;1;[Toggle];Create;True;0;2;Off;0;On;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;199;2037.578,289.0625;Inherit;False;614.7139;1207.434;Combine;6;168;167;166;121;154;104;Combine;0.6987083,1,0.07075471,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;222;-312.8113,164.6912;Inherit;False;Property;_RSType;Smothness(Roughness);7;1;[Enum];Create;False;0;2;Smothness;0;Roughness;1;0;True;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;207;2160,-240;Inherit;False;225.3333;165;Cull Mode;1;205;Cull Mode;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;154;2426.983,550.6176;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;256;1411.365,2517.074;Inherit;False;Property;_UVCLM;UV CLM;37;1;[Enum];Create;True;0;2;UV1;0;UV3;1;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;174;-410.6355,-214.8536;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;152;-313.4433,1420.837;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;239;1986.237,2473.153;Inherit;False;Property;_CLMColor;CLM Color;39;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;254;1339.365,2382.074;Inherit;False;2;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;244;2982.963,2370.782;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-323.232,-785.9893;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;247;3354.923,2356.671;Inherit;False;CLM;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;251;2984.182,2206.121;Inherit;False;Property;_UseCustomLightmap;Use Custom Lightmap;36;1;[Toggle];Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;168;2428.472,1290.461;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;167;2422.82,1096.612;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;253;1337.185,2217.594;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;150;-722.9769,1978.396;Inherit;True;Property;_DEmissiveMap;(D)Emissive Map;25;2;[HDR];[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;260;2619.273,2059.853;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;258;2725.279,948.767;Inherit;False;Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;246;2440.621,2174.139;Inherit;False;245;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;227;-397.1863,-346.7249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;237;3048.148,614.08;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;104;2425.075,370.0756;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;249;2831.574,294.6605;Inherit;False;247;CLM;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-1016.368,2969.207;Float;False;Property;_DNormalInt;(D)Normal Int;33;0;Create;True;0;0;0;False;0;False;1;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;259;2398.273,2014.853;Inherit;False;258;Metallic;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;243;2343.237,2512.153;Inherit;False;Property;_CLMIntensity;CLM Intensity;38;0;Create;True;0;0;0;False;0;False;0;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;241;2829.019,2298.127;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;240;2308.237,2362.153;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;248;3044.767,325.775;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;252;3174.787,2277.855;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;205;2224,-192;Float;False;Property;_Culling;Culling;0;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;232;2666.233,-226.2088;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;224;-77.13187,2550.587;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;121;2428.389,721.3535;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FogAndAmbientColorsNode;242;2605.802,2406.892;Inherit;False;UNITY_LIGHTMODEL_AMBIENT;0;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;155;-638.6351,2496.398;Float;False;Property;_DRMin;(D)Smothness(Roughness) Min;29;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;149;-635.7518,1772.645;Inherit;False;Property;_DEmissiveColor;(D)Emissive Color;24;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;119;-652.3248,2898.334;Inherit;True;Property;_DNormal;(D)Normal;32;2;[NoScaleOffset];[Normal];Create;True;0;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;156;-638.0176,2596.696;Float;False;Property;_DRMax;(D)Smothness(Roughness) Max;30;0;Create;False;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;125;-1626.916,1497.744;Inherit;False;Property;_DOffset;(D)Offset;35;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;740.9651,837.7536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;148;472.8407,824.1803;Inherit;False;Property;_DM;Use Detail Mask;18;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;165;-988.0428,2261.027;Inherit;True;Property;_DMG;(D)MG;26;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;163;-310.9196,2657.951;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;220;-1015.504,1432.731;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;217;-1330.55,1317.396;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;127;-1329.744,1457.968;Inherit;False;3;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;103;-1627.404,1315.651;Float;False;Property;_DTilling;(D)Tilling;34;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;245;2748.768,176.7701;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;238;1966.378,2264.745;Inherit;True;Property;_CLM;CLM;40;2;[HDR];[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;151;-302.1756,1808.445;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;129;400.0644,710.1043;Float;False;Property;_DInt;D Int;20;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;175;-326.297,2315.427;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;166;2423.498,905.6366;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;226;-256.0415,2752.343;Inherit;False;Property;_DRSType;(D)Smothness(Roughness);28;1;[Enum];Create;False;0;2;Smothness;0;Roughness;1;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;228;-327.2385,2193.193;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;157;-655.5577,2373.906;Float;False;Property;_DAO;(D)AO;31;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;159;-653.4946,2201.31;Float;False;Property;_DMetallic;(D)Metallic;27;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;120;-712.8951,1502.124;Inherit;True;Property;_DBaseColor;(D)Base Color;22;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;153;-694.7673,1288.95;Inherit;False;Property;_DColor;(D)Color;21;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;255;1785.366,2326.074;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;223;-35.49866,29.0015;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;160;-309.5197,2511.737;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-345.3032,-1172.494;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;211;-352.1289,1993.404;Inherit;False;Property;_DEmissive;(D)Emissive;23;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;221;-1244.49,1607.696;Inherit;False;Property;_UV;UV;19;1;[Enum];Create;True;0;2;UV1;0;UV4;1;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;204;3550.976,398.7869;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;PBR/2020/PBR_HD_Lit;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;Legacy Shaders/Diffuse;2;-1;-1;-1;0;False;0;0;True;205;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;109;0;115;0
WireConnection;109;1;122;0
WireConnection;132;1;109;0
WireConnection;130;1;109;0
WireConnection;130;5;107;0
WireConnection;146;0;132;4
WireConnection;146;1;145;0
WireConnection;146;2;110;0
WireConnection;106;1;109;0
WireConnection;235;0;130;0
WireConnection;236;0;235;2
WireConnection;236;1;234;0
WireConnection;231;0;126;4
WireConnection;231;1;106;4
WireConnection;117;0;146;0
WireConnection;108;1;109;0
WireConnection;154;1;151;0
WireConnection;154;2;118;0
WireConnection;174;1;132;2
WireConnection;174;2;116;0
WireConnection;152;0;153;0
WireConnection;152;1;120;0
WireConnection;244;0;241;0
WireConnection;244;1;242;0
WireConnection;244;2;243;0
WireConnection;114;0;102;0
WireConnection;114;1;108;0
WireConnection;114;2;209;0
WireConnection;247;0;252;0
WireConnection;168;1;175;0
WireConnection;168;2;118;0
WireConnection;167;1;224;0
WireConnection;167;2;118;0
WireConnection;150;1;220;0
WireConnection;260;0;259;0
WireConnection;258;0;166;0
WireConnection;227;0;132;1
WireConnection;227;1;111;0
WireConnection;237;0;235;0
WireConnection;237;1;235;1
WireConnection;237;2;236;0
WireConnection;104;1;152;0
WireConnection;104;2;118;0
WireConnection;241;0;246;0
WireConnection;241;1;240;0
WireConnection;241;2;260;0
WireConnection;240;0;238;0
WireConnection;240;1;239;0
WireConnection;248;0;249;0
WireConnection;248;1;154;0
WireConnection;252;0;251;0
WireConnection;252;1;244;0
WireConnection;232;1;231;0
WireConnection;232;2;233;0
WireConnection;224;0;160;0
WireConnection;224;1;163;0
WireConnection;224;2;226;0
WireConnection;121;1;119;0
WireConnection;121;2;118;0
WireConnection;119;1;220;0
WireConnection;119;5;112;0
WireConnection;118;0;148;0
WireConnection;118;1;129;0
WireConnection;148;1;132;3
WireConnection;165;1;220;0
WireConnection;163;0;160;0
WireConnection;220;0;217;0
WireConnection;220;1;127;0
WireConnection;220;2;221;0
WireConnection;217;0;103;0
WireConnection;217;1;125;0
WireConnection;127;0;103;0
WireConnection;127;1;125;0
WireConnection;245;0;104;0
WireConnection;238;1;255;0
WireConnection;151;0;149;0
WireConnection;151;1;150;0
WireConnection;151;2;211;0
WireConnection;175;1;165;2
WireConnection;175;2;157;0
WireConnection;166;1;228;0
WireConnection;166;2;118;0
WireConnection;228;0;165;1
WireConnection;228;1;159;0
WireConnection;120;1;220;0
WireConnection;255;0;253;0
WireConnection;255;1;254;0
WireConnection;255;2;256;0
WireConnection;223;0;146;0
WireConnection;223;1;117;0
WireConnection;223;2;222;0
WireConnection;160;0;165;4
WireConnection;160;1;155;0
WireConnection;160;2;156;0
WireConnection;113;0;126;0
WireConnection;113;1;106;0
WireConnection;204;0;113;0
WireConnection;204;1;237;0
WireConnection;204;2;114;0
WireConnection;204;3;227;0
WireConnection;204;4;223;0
WireConnection;204;5;174;0
WireConnection;204;10;232;0
ASEEND*/
//CHKSM=95B003175D537CB842C44C6F1A3F4BC486F6747B