Shader "VertexStudio/FurnitureColorMask" {
    Properties {
		[NoScaleOffset]
        _Mask ("Mask (RGB)", 2D) = "black" {}
 		[NoScaleOffset]
        _MainTex ("Diffuse", 2D) = "white" {}
        [NoScaleOffset]
        _Spec ("Specular", 2D) = "black" {}
        [NoScaleOffset]
        _Normal ("Normal", 2D) = "bump" {}

        [NoScaleOffset]
        _OcclusionMap("Ambient Occlusion", 2D) = "white" {}
        _OcclusionStrength("AO intensity", Range(0.0, 1.0)) = 1.0

        [NoScaleOffset]
        _Emission ("Emission", 2D) = "white" {}
 
        _ColorR ("Color (R)", Color) = (1,1,1,1)
        _ColorG ("Color (G)", Color) = (1,1,1,1)
        _ColorB ("Color (B)", Color) = (1,1,1,1)
 
        [HDR]
        _EmissionColor("EmissionColor", Color) = (0,0,0)

    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
       
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardSpecular fullforwardshadows
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        sampler2D _MainTex,_Mask;
        sampler2D _Normal;
        sampler2D _Spec;
        sampler2D _Emission;
        sampler2D   _OcclusionMap;
        half        _OcclusionStrength;
        half4 _EmissionColor;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        fixed4 _ColorR,_ColorG,_ColorB;
 
        void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
 
            fixed4 mask = tex2D (_Mask, IN.uv_MainTex);
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * saturate( ( _ColorR * mask.r + _ColorG * mask.g + _ColorB * mask.b) );
 
            half occ = tex2D(_OcclusionMap, IN.uv_MainTex).g;
            o.Occlusion = LerpOneTo (occ, _OcclusionStrength);
 
            o.Albedo = c.rgb;
 
            o.Normal = UnpackNormal ( tex2D (_Normal, IN.uv_MainTex) );
 
            fixed4 spec = tex2D (_Spec, IN.uv_MainTex);
 
            o.Specular = spec.rgb;
 
            o.Smoothness = spec.a;
 
            o.Emission = _EmissionColor * ( tex2D (_Emission, IN.uv_MainTex) );
 
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}