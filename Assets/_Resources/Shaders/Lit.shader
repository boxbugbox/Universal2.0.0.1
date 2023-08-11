// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LHZN/PBR/Lit"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[Enum(UnityEngine.Rendering.CullMode)]_Culling("Culling", Float) = 2
		[Toggle]_CutOff("CutOff", Float) = 0
		_Color("Color", Color) = (1,1,1,1)
		[NoScaleOffset][SingleLineTexture]_MainTex("Albedo", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_MM("MG(M)(AO)(BM)(SR)", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[Enum(Smothness,0,Roughness,1)]_RSType("Smothness(Roughness)", Float) = 0
		_SRMin("Smothness(Roughness) Min", Range( 0 , 1)) = 0
		_SRMax("Smothness(Roughness) Max", Range( 0 , 1)) = 0.5
		_AO("AO", Range( 0 , 1)) = 0
		[NoScaleOffset][Normal][SingleLineTexture]_BumpMap("Normal", 2D) = "bump" {}
		_NormalInt("Normal Int", Range( 0 , 3)) = 1
		[Toggle]_Emissive("Emissive", Float) = 0
		[HDR]_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		[HDR][NoScaleOffset][SingleLineTexture]_EmissiveMap("Emissive Map", 2D) = "white" {}
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
		uniform sampler2D _MainTex;
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
			float4 Normal244 = appendResult237;
			o.Normal = Normal244.xyz;
			float4 tex2DNode106 = tex2D( _MainTex, uv_TexCoord109 );
			float4 BaseColor238 = ( _Color * tex2DNode106 );
			o.Albedo = BaseColor238.rgb;
			float4 temp_output_114_0 = ( _EmissiveColor * tex2D( _EmissiveMap, uv_TexCoord109 ) * _Emissive );
			o.Emission = temp_output_114_0.rgb;
			float4 tex2DNode132 = tex2D( _MM, uv_TexCoord109 );
			float Metallic241 = ( tex2DNode132.r * _Metallic );
			o.Metallic = Metallic241;
			float clampResult146 = clamp( tex2DNode132.a , _SRMin , _SRMax );
			float lerpResult223 = lerp( clampResult146 , ( 1.0 - clampResult146 ) , _RSType);
			float Smoothness242 = lerpResult223;
			o.Smoothness = Smoothness242;
			float lerpResult174 = lerp( 1.0 , tex2DNode132.g , _AO);
			float AO243 = lerpResult174;
			o.Occlusion = AO243;
			o.Alpha = 1;
			float lerpResult232 = lerp( 1.0 , ( _Color.a * tex2DNode106.a ) , _CutOff);
			float AlphaMask239 = lerpResult232;
			clip( AlphaMask239 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Legacy Shaders/Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
386;125;1759;1047;979.8608;1223.503;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;196;-1728.946,-1356.295;Inherit;False;1953.847;1951.117;Main Tex;33;223;239;232;233;242;243;241;240;238;113;174;114;227;231;222;116;117;209;108;102;111;106;126;146;110;130;132;145;107;109;122;115;244;Main Tex;1,0.4103774,0.9015171,1;0;0
Node;AmplifyShaderEditor.Vector2Node;115;-1656.476,-867.9559;Float;False;Property;_Tiling;Tiling;16;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;122;-1662.495,-695.5003;Inherit;False;Property;_Offset;Offset;17;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;109;-1412.162,-782.3006;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;107;-1076.208,259.386;Float;False;Property;_NormalInt;Normal Int;12;0;Create;True;0;0;0;False;0;False;1;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;145;-769.7454,-25.74644;Float;False;Property;_SRMin;Smothness(Roughness) Min;8;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;132;-1221.47,-255.0018;Inherit;True;Property;_MM;MG(M)(AO)(BM)(SR);5;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;130;-740.1666,177.6434;Inherit;True;Property;_BumpMap;Normal;11;3;[NoScaleOffset];[Normal];[SingleLineTexture];Create;False;0;0;0;False;0;False;-1;None;638c51081ab2e934b858e913ec28dfc6;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;110;-767.1277,56.55165;Float;False;Property;_SRMax;Smothness(Roughness) Max;9;0;Create;False;0;0;0;False;0;False;0.5;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;146;-430.103,-62.16954;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FaceVariableNode;234;-385.1538,500.4264;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;235;-407.7786,349.6473;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SamplerNode;106;-779.3463,-1080.194;Inherit;True;Property;_MainTex;Albedo;4;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;-1;None;e93ca11d2b63cbf4ba35a43dbdcacbc5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;111;-762.6797,-311.8798;Float;False;Property;_Metallic;Metallic;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;236;-243.2298,471.2573;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;117;-262.5184,76.87758;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;116;-763.7426,-181.2843;Float;False;Property;_AO;AO;10;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;222;-355.8545,164.6912;Inherit;False;Property;_RSType;Smothness(Roughness);7;1;[Enum];Create;False;0;2;Smothness;0;Roughness;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;126;-692.5463,-1262.295;Inherit;False;Property;_Color;Color;3;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;233;-377.2725,-873.7153;Inherit;False;Property;_CutOff;CutOff;2;1;[Toggle];Create;True;0;2;Off;0;On;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;231;-377.2285,-982.368;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;174;-453.6786,-214.8536;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;223;-88.66051,29.56083;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;227;-440.2295,-346.7249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;237;-99.74926,291.6428;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-388.3463,-1172.494;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;232;-195.3597,-934.1332;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;238;-142.3248,-1173.359;Inherit;False;BaseColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;242;-72.33126,169.7724;Inherit;False;Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;244;-15.28794,451.768;Inherit;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;102;-699.8516,-821.7891;Inherit;False;Property;_EmissiveColor;Emissive Color;14;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;209;-400.8177,-625.8589;Inherit;False;Property;_Emissive;Emissive;13;1;[Toggle];Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;108;-787.0767,-616.0385;Inherit;True;Property;_EmissiveMap;Emissive Map;15;3;[HDR];[NoScaleOffset];[SingleLineTexture];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;207;364.0548,-1235.374;Inherit;False;225.3333;165;Cull Mode;1;205;Cull Mode;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;241;-113.8569,-327.3486;Inherit;False;Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;243;-116.23,-154.1277;Inherit;False;AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-366.2751,-785.9893;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;246;951.4869,-409.2789;Inherit;False;244;Normal;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;245;949.4869,-491.2789;Inherit;False;238;BaseColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;248;950.4869,-252.2789;Inherit;False;242;Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;205;428.0547,-1187.374;Float;False;Property;_Culling;Culling;1;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;249;961.4869,-173.2789;Inherit;False;243;AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;240;-129.2809,-651.2482;Inherit;False;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;239;-25.30809,-810.1104;Inherit;False;AlphaMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;250;965.4869,-92.27893;Inherit;False;239;AlphaMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;247;953.4869,-331.2789;Inherit;False;241;Metallic;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;204;1222.255,-398.0067;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;LHZN/PBR/Lit;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Opaque;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;Legacy Shaders/Diffuse;0;-1;-1;-1;0;False;0;0;True;205;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;109;0;115;0
WireConnection;109;1;122;0
WireConnection;132;1;109;0
WireConnection;130;1;109;0
WireConnection;130;5;107;0
WireConnection;146;0;132;4
WireConnection;146;1;145;0
WireConnection;146;2;110;0
WireConnection;235;0;130;0
WireConnection;106;1;109;0
WireConnection;236;0;235;2
WireConnection;236;1;234;0
WireConnection;117;0;146;0
WireConnection;231;0;126;4
WireConnection;231;1;106;4
WireConnection;174;1;132;2
WireConnection;174;2;116;0
WireConnection;223;0;146;0
WireConnection;223;1;117;0
WireConnection;223;2;222;0
WireConnection;227;0;132;1
WireConnection;227;1;111;0
WireConnection;237;0;235;0
WireConnection;237;1;235;1
WireConnection;237;2;236;0
WireConnection;113;0;126;0
WireConnection;113;1;106;0
WireConnection;232;1;231;0
WireConnection;232;2;233;0
WireConnection;238;0;113;0
WireConnection;242;0;223;0
WireConnection;244;0;237;0
WireConnection;108;1;109;0
WireConnection;241;0;227;0
WireConnection;243;0;174;0
WireConnection;114;0;102;0
WireConnection;114;1;108;0
WireConnection;114;2;209;0
WireConnection;240;0;114;0
WireConnection;239;0;232;0
WireConnection;204;0;245;0
WireConnection;204;1;246;0
WireConnection;204;2;114;0
WireConnection;204;3;247;0
WireConnection;204;4;248;0
WireConnection;204;5;249;0
WireConnection;204;10;239;0
ASEEND*/
//CHKSM=57C36986EFE80456110AD3B96F1A09E87B6744BC