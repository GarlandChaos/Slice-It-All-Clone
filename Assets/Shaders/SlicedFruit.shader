Shader "Unlit/SlicedFruit"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Edge("Edge", Range(-0.5, 0.5)) = 0.0
        [Toggle] _LeftSlice("LeftSlice", Int) = 1
    }
    SubShader
    {
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 hitPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Edge;
            int _LeftSlice;

            // declare the function for the plane
            float planeSDF(float3 ray_position)
            {
                // subtract the edge to the “Y” ray position to increase
                // or decrease the plane position
                float plane = ray_position.x - _Edge;
                return plane;
            }

            #define MAX_MARCHIG_STEPS 50 // maximum of steps to determine the surface intersection
            #define MAX_DISTANCE 10.0 // maximum distance to find the surface intersection
            #define SURFACE_DISTANCE 0.001 // surface distance

            float sphereCasting(float3 ray_origin, float3 ray_direction)
            {
                float distance_origin = 0;
                for (int i = 0; i < MAX_MARCHIG_STEPS; i++)
                {
                    float3 ray_position = ray_origin + ray_direction *
                        distance_origin;
                    float distance_scene = planeSDF(ray_position);
                    distance_origin += distance_scene;
                    if (distance_scene < SURFACE_DISTANCE || distance_origin >
                        MAX_MARCHIG_STEPS);
                    break;
                }
                return distance_origin;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);     
                o.hitPos = v.vertex; // assign the vertex position in object-space
                return o;
            }

            fixed4 frag (v2f i, bool face : SV_isFrontFace) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, i.uv); // sample the texture
                float3 ray_origin = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1)); // transform the camera to local-space
                float3 ray_direction = normalize(i.hitPos - ray_origin); // calculate the ray direction
                float t = sphereCasting(ray_origin, ray_direction); // use the values in the ray casting function
                float3 p = ray_origin + ray_direction * t; // calculate the point position in space
                
                if (_LeftSlice == 1) // discard the pixels that lie on the _Edge
                {
                    if (i.hitPos.x > _Edge) 
                    {
                        discard;
                    }
                }
                else 
                {
                    if (i.hitPos.x < _Edge)
                    {
                        discard;
                    }
                }

                return face ? col : float4(p, 1);
            }
            ENDCG
        }
    }
}
