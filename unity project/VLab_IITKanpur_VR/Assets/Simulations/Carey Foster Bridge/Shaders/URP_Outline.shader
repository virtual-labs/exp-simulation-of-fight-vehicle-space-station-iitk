Shader "Custom/URP/Outline"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _BaseColor("Base Color", Color) = (1,1,1,1)

        _OutlineColor("Outline Color", Color) = (1,0.8,0,1)
        _OutlineWidth("Outline Width", Range(0,0.2)) = 0.02
    }

        SubShader
        {
            Tags { "RenderPipeline" = "UniversalRenderPipeline" "Queue" = "Geometry" }

            // ===== OUTLINE PASS =====
            Pass
            {
                Name "Outline"
                Tags { "LightMode" = "UniversalForward" }

                Cull Front              // draw backfaces
                ZWrite On
                ZTest LEqual

                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                float _OutlineWidth;
                float4 _OutlineColor;

                struct Attributes
                {
                    float4 positionOS : POSITION;
                    float3 normalOS   : NORMAL;
                };

                struct Varyings
                {
                    float4 positionHCS : SV_POSITION;
                };

                Varyings vert(Attributes IN)
                {
                    Varyings o;

                    float3 normalWS = TransformObjectToWorldNormal(IN.normalOS);
                    float3 posWS = TransformObjectToWorld(IN.positionOS).xyz;

                    posWS += normalWS * _OutlineWidth;

                    o.positionHCS = TransformWorldToHClip(posWS);
                    return o;
                }

                half4 frag() : SV_Target
                {
                    return _OutlineColor;
                }
                ENDHLSL
            }

            // ===== NORMAL URP LIT PASS =====
            UsePass "Universal Render Pipeline/Lit/ForwardLit"
        }
}
