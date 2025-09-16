Shader "Custom/CurvedWorld_Universal_Final"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        _Curvature ("Curvature", Float) = 0.0005
    }
    SubShader
    {
        // Questo tag universale funziona per la maggior parte degli oggetti
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        half _Cutoff;
        float _Curvature;
        float4 _PlayerPosition;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            float4 world_pos = mul(unity_ObjectToWorld, v.vertex);

            float dist_z = world_pos.z - _PlayerPosition.z;

            float y_offset = -_Curvature * dist_z * dist_z;

            if (dist_z > 0) 
            {
                world_pos.y += y_offset;
            }

            v.vertex = mul(unity_WorldToObject, world_pos);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            clip(c.a - _Cutoff);
        }
        ENDCG

        // ============== PASSAGGIO PER LE OMBRE ==============
        Pass
        {
            Tags { "LightMode"="ShadowCaster" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f
            {
                V2F_SHADOW_CASTER;
            };

            float _Curvature;
            float4 _PlayerPosition;

            v2f vert(appdata_base v)
            {
                v2f o;

                float4 world_pos = mul(unity_ObjectToWorld, v.vertex);
                float dist_z = world_pos.z - _PlayerPosition.z;
                float y_offset = -_Curvature * dist_z * dist_z;

                if (dist_z > 0)
                {
                    world_pos.y += y_offset;
                }

                v.vertex = mul(unity_WorldToObject, world_pos);

                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}