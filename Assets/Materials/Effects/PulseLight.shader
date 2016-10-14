// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:6,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33271,y:33190,varname:node_3138,prsc:2|emission-3459-OUT,alpha-3135-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32406,y:32804,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1785251,c2:0.777555,c3:0.9338235,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3293,x:31363,y:33361,varname:node_3293,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9794,x:31879,y:32965,varname:node_9794,prsc:2|A-3293-UVOUT,B-9948-OUT;n:type:ShaderForge.SFN_Vector1,id:9948,x:31608,y:32997,varname:node_9948,prsc:2,v1:2;n:type:ShaderForge.SFN_Frac,id:9565,x:32055,y:32965,varname:node_9565,prsc:2|IN-9794-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5764,x:32238,y:32965,varname:node_5764,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-9565-OUT;n:type:ShaderForge.SFN_OneMinus,id:8245,x:32420,y:32965,varname:node_8245,prsc:2|IN-5764-OUT;n:type:ShaderForge.SFN_Multiply,id:5275,x:32690,y:32923,varname:node_5275,prsc:2|A-7241-RGB,B-8245-OUT;n:type:ShaderForge.SFN_Tex2d,id:6651,x:32134,y:33129,ptovrint:False,ptlb:Gradient,ptin:_Gradient,varname:node_6651,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2df556300fc91684a8161e5f7d0f6205,ntxv:0,isnm:False|UVIN-1860-UVOUT;n:type:ShaderForge.SFN_Panner,id:1860,x:31927,y:33129,varname:node_1860,prsc:2,spu:1,spv:-0.3|UVIN-3293-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6116,x:32690,y:33094,varname:node_6116,prsc:2|A-5275-OUT,B-177-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:177,x:32325,y:33150,varname:node_177,prsc:2,a:0.3,b:1|IN-6651-R;n:type:ShaderForge.SFN_DepthBlend,id:3135,x:33011,y:33453,varname:node_3135,prsc:2|DIST-7977-OUT;n:type:ShaderForge.SFN_Slider,id:7977,x:32620,y:33495,ptovrint:False,ptlb:Depth Blend,ptin:_DepthBlend,varname:node_7977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:5576,x:32172,y:33667,varname:node_5576,prsc:2,tex:7030e256d00b9484cbdf53583c2541e9,ntxv:0,isnm:False|UVIN-7175-UVOUT,TEX-5099-TEX;n:type:ShaderForge.SFN_Divide,id:7940,x:31741,y:33691,varname:node_7940,prsc:2|A-3293-UVOUT,B-1899-OUT;n:type:ShaderForge.SFN_Panner,id:7175,x:31951,y:33633,varname:node_7175,prsc:2,spu:0,spv:-0.05|UVIN-7940-OUT;n:type:ShaderForge.SFN_Panner,id:6455,x:31951,y:33775,varname:node_6455,prsc:2,spu:0.1,spv:0|UVIN-7940-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5099,x:31866,y:33930,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_5099,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7030e256d00b9484cbdf53583c2541e9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9159,x:32175,y:33884,varname:node_9159,prsc:2,tex:7030e256d00b9484cbdf53583c2541e9,ntxv:0,isnm:False|UVIN-6455-UVOUT,TEX-5099-TEX;n:type:ShaderForge.SFN_Add,id:7791,x:32411,y:33802,varname:node_7791,prsc:2|A-5576-G,B-9159-B;n:type:ShaderForge.SFN_Multiply,id:3459,x:32901,y:33156,varname:node_3459,prsc:2|A-6116-OUT,B-7791-OUT,C-2445-OUT;n:type:ShaderForge.SFN_Vector1,id:1899,x:31505,y:33749,varname:node_1899,prsc:2,v1:4;n:type:ShaderForge.SFN_ComponentMask,id:8313,x:31813,y:33429,varname:node_8313,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3293-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:7471,x:31813,y:33289,varname:node_7471,prsc:2|IN-8313-OUT;n:type:ShaderForge.SFN_RemapRange,id:4434,x:32028,y:33289,varname:node_4434,prsc:2,frmn:0.5,frmx:1,tomn:-1,tomx:1|IN-7471-OUT;n:type:ShaderForge.SFN_RemapRange,id:1477,x:32028,y:33429,varname:node_1477,prsc:2,frmn:0.5,frmx:1,tomn:-1,tomx:1|IN-8313-OUT;n:type:ShaderForge.SFN_Clamp01,id:5430,x:32198,y:33289,varname:node_5430,prsc:2|IN-4434-OUT;n:type:ShaderForge.SFN_Clamp01,id:3021,x:32198,y:33429,varname:node_3021,prsc:2|IN-1477-OUT;n:type:ShaderForge.SFN_Add,id:979,x:32388,y:33366,varname:node_979,prsc:2|A-5430-OUT,B-3021-OUT;n:type:ShaderForge.SFN_OneMinus,id:2445,x:32589,y:33355,varname:node_2445,prsc:2|IN-979-OUT;proporder:7241-6651-7977-5099;pass:END;sub:END;*/

Shader "Shader Forge/PulseLight" {
    Properties {
        _Color ("Color", Color) = (0.1785251,0.777555,0.9338235,1)
        _Gradient ("Gradient", 2D) = "white" {}
        _DepthBlend ("Depth Blend", Range(0, 1)) = 1
        _Noise ("Noise", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcColor
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Gradient; uniform float4 _Gradient_ST;
            uniform float _DepthBlend;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float2 node_9794 = (i.uv0*2.0);
                float node_5764 = frac(node_9794).g;
                float4 node_2511 = _Time + _TimeEditor;
                float2 node_1860 = (i.uv0+node_2511.g*float2(1,-0.3));
                float4 _Gradient_var = tex2D(_Gradient,TRANSFORM_TEX(node_1860, _Gradient));
                float2 node_7940 = (i.uv0/4.0);
                float2 node_7175 = (node_7940+node_2511.g*float2(0,-0.05));
                float4 node_5576 = tex2D(_Noise,TRANSFORM_TEX(node_7175, _Noise));
                float2 node_6455 = (node_7940+node_2511.g*float2(0.1,0));
                float4 node_9159 = tex2D(_Noise,TRANSFORM_TEX(node_6455, _Noise));
                float node_8313 = i.uv0.r;
                float node_4434 = ((1.0 - node_8313)*4.0+-3.0);
                float node_1477 = (node_8313*4.0+-3.0);
                float3 emissive = (((_Color.rgb*(1.0 - node_5764))*lerp(0.3,1,_Gradient_var.r))*(node_5576.g+node_9159.b)*(1.0 - (saturate(node_4434)+saturate(node_1477))));
                float3 finalColor = emissive;
                return fixed4(finalColor,saturate((sceneZ-partZ)/_DepthBlend));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
