Shader "Custom/XORShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // The sprite texture
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        //ZWrite Off
        //Blend OneMinusDstColor OneMinusSrcColor // XOR Blending
        Blend One One
        BlendOp Sub // Subtraction blending operation

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}