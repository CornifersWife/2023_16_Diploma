Shader "Custom/OutlinedDiffuse" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (0.0, 0.1)) = 0.05
    }
    SubShader {
        Tags { "Queue"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            float _Outline;
            float4 _OutlineColor;

            v2f vert (appdata v) {
                v2f o;
                // Calculate normal in model view space
                float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                // Apply outline only on X and Z by setting Y component of the normal to 0
                norm.y = 0; 
                // Calculate vertex position with outline applied on X and Z
                float4 offsetPos = float4(norm * _Outline, 0) + v.vertex;
                o.pos = UnityObjectToClipPos(offsetPos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
