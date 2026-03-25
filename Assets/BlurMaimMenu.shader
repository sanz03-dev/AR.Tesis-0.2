Shader "Custom/GlassPanel"
{
    Properties
    {
        _BlurSize ("Blur Size", Range(0, 10)) = 3
        _TintColor ("Tint Color", Color) = (0.76, 0.93, 1, 0.1)
        _BorderColor ("Border Color", Color) = (0.76, 0.93, 1, 0.92)
        _BorderSize ("Border Size", Range(0, 0.02)) = 0.005
        _CornerRadius ("Corner Radius", Range(0, 0.5)) = 0.15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        GrabPass { "_GrabTexture" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;

            float _BlurSize;
            float4 _TintColor;
            float4 _BorderColor;
            float _BorderSize;
            float _CornerRadius;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 grabPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }

            float roundedRect(float2 uv, float radius)
            {
                float2 dist = abs(uv - 0.5);
                float2 size = 0.5 - radius;
                float2 d = dist - size;
                return length(max(d, 0.0)) - radius;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.grabPos.xy / i.grabPos.w;

                // Simple blur (4 samples)
                float2 blur = _GrabTexture_TexelSize.xy * _BlurSize;

                fixed4 col = tex2D(_GrabTexture, uv) * 0.4;
                col += tex2D(_GrabTexture, uv + blur) * 0.15;
                col += tex2D(_GrabTexture, uv - blur) * 0.15;
                col += tex2D(_GrabTexture, uv + float2(blur.x, -blur.y)) * 0.15;
                col += tex2D(_GrabTexture, uv + float2(-blur.x, blur.y)) * 0.15;

                // Rounded corners mask
                float mask = roundedRect(i.uv, _CornerRadius);
                if (mask > 0)
                    discard;

                // Border detection
                float borderMask = roundedRect(i.uv, _CornerRadius - _BorderSize);
                float border = step(0, borderMask);

                // Apply tint
                col = lerp(col, _TintColor, _TintColor.a);

                // Apply border
                col = lerp(col, _BorderColor, border * _BorderColor.a);

                return col;
            }
            ENDCG
        }
    }
}