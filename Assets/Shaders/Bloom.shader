Shader "Custom/Bloom" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    CGINCLUDE
    #include "UnityCG.cginc"
    sampler2D _MainTex;
    struct VertexData {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };
    struct Interpolators {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
    };
    Interpolators VertexProgram (VertexData v) {
        Interpolators i;
        i.pos = UnityObjectToClipPos(v.vertex);
        i.uv = v.uv;
        return i;
    }
ENDCG
    SubShader {
        Cull Off
        ZTest Always
        ZWrite Off
        Pass {
            CGPROGRAM
                #pragma vertex VertexProgram
                #pragma fragment FragmentProgram

                half4 FragmentProgram (Interpolators i) : SV_Target {
                return tex2D(_MainTex, i.uv) * half4(1, 0, 0, 0);
            }
            ENDCG
        }
    }
}