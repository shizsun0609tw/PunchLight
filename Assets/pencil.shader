Shader "Unlit/pencil"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _tex1  ("tex1", 2D) = "white" {}
        
        _tex2  ("tex2", 2D) = "white" {}
        
        _tex3  ("tex3", 2D) = "white" {}
        
        _tex4  ("tex4", 2D) = "white" {}
        
        _LightDir ("LightDir", vector) = (0,0,1,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightDir;
            sampler2D _tex1;
            
            sampler2D _tex2;
        
            sampler2D _tex3;
            
            sampler2D _tex4;;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal.xyz = UnityObjectToWorldNormal(v.normal);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                _LightDir.xyz = normalize(_LightDir.xyz);

                float dott = dot(i.normal.xyz, _LightDir.xyz);

                dott.x = floor(dott * 4);
                float3 color = float3(0,0,0);

                if (dott.x == 3)
                    color = tex2D(_tex1, i.uv);
                if (dott.x == 2)
                    color = tex2D(_tex2, i.uv);
                if (dott.x == 1)
                    color = tex2D(_tex3, i.uv);
                if (dott.x == 0)
                    color = tex2D(_tex4, i.uv);

                    dott.x /= 4;

                //return float4(i.normal.xyz, 1);
                // apply fog
                col.xyz 
                += color.xyz;
                return col;
            }
            ENDCG
        }
    }
}
