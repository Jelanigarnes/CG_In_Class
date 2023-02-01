Shader "Custom/BasicLambert"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Geometry" }
      

        CGPROGRAM
        
        #pragma surface surf BasicLambert

        half4 LightingBasicLambert(SurfaceOutput s, half3 lightDir,half atten){
                half NdotL = dot(s.Normal, lightDir);
                half4 c;
                c.rbg = s.Albedo * _LightColor0.rgb * (NdotL * atten);
                c.a = s.Alpha;
                return c;
        }

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

       
        fixed4 _Color;


        void surf (Input IN, inout SurfaceOutput o)
        {
          
            o.Albedo = _Color.rgb;
         
        }
        ENDCG
    }
    FallBack "Diffuse"
}
