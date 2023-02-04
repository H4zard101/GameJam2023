Shader "Particles/Lit Emissive"
{
	Properties
	{
		_MainTex("Albedo", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)

        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _MetallicGlossMap("Metallic", 2D) = "white" {}
        [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5

        _BumpScale("Scale", Float) = 1.0
        _BumpMap("Normal Map", 2D) = "bump" {}

        _Emission("Emission", Float) = 0.0

        _DistortionStrength("Strength", Float) = 1.0
        _DistortionBlend("Blend", Range(0.0, 1.0)) = 0.5

        _SoftParticlesNearFadeDistance("Soft Particles Near Fade", Float) = 0.0
        _SoftParticlesFarFadeDistance("Soft Particles Far Fade", Float) = 1.0
        _CameraNearFadeDistance("Camera Near Fade", Float) = 1.0
        _CameraFarFadeDistance("Camera Far Fade", Float) = 2.0

        // Hidden properties
        [HideInInspector] _Mode ("__mode", Float) = 0.0
        [HideInInspector] _FlipbookMode ("__flipbookmode", Float) = 0.0
        [HideInInspector] _LightingEnabled ("__lightingenabled", Float) = 1.0
        [HideInInspector] _DistortionEnabled ("__distortionenabled", Float) = 0.0
        [HideInInspector] _EmissionEnabled ("__emissionenabled", Float) = 0.0
        [HideInInspector] _BlendOp ("__blendop", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0
        [HideInInspector] _Cull ("__cull", Float) = 2.0
        [HideInInspector] _SoftParticlesEnabled ("__softparticlesenabled", Float) = 0.0
        [HideInInspector] _CameraFadingEnabled ("__camerafadingenabled", Float) = 0.0
        [HideInInspector] _SoftParticleFadeParams ("__softparticlefadeparams", Vector) = (0,0,0,0)
        [HideInInspector] _CameraFadeParams ("__camerafadeparams", Vector) = (0,0,0,0)
        [HideInInspector] _DistortionStrengthScaled ("__distortionstrengthscaled", Float) = 0.0
	}
	SubShader
    {
        Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "PreviewType"="Plane" "PerformanceChecks"="False" }

        BlendOp [_BlendOp]
        Blend [_SrcBlend] [_DstBlend]
        ZWrite [_ZWrite]
        Cull [_Cull]

        GrabPass
        {
            Tags { "LightMode" = "Always" }
            "_GrabTexture"
        }

        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            BlendOp Add
            Blend One Zero
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma target 3.5

            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _REQUIRE_UV2
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:vertInstancingSetup

            #pragma vertex vertParticleShadowCaster
            #pragma fragment fragParticleShadowCaster

            #include "UnityStandardParticleShadow.cginc"
            ENDCG
        }

        CGPROGRAM
        #pragma surface customSurf Standard nolightmap nometa noforwardadd keepalpha vertex:vert
        #pragma multi_compile __ SOFTPARTICLES_ON
        #pragma multi_compile_instancing
        #pragma instancing_options procedural:vertInstancingSetup
        #pragma target 3.5

        #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
        #pragma shader_feature _METALLICGLOSSMAP
        #pragma shader_feature _NORMALMAP
        #pragma shader_feature _EMISSION
        #pragma shader_feature _FADING_ON
        #pragma shader_feature _REQUIRE_UV2
        #pragma shader_feature EFFECT_BUMP

        float _Emission;

        #include "UnityStandardParticles.cginc"

        void customSurf (Input IN, inout SurfaceOutputStandard o)
        {
            half4 albedo = readTexture (_MainTex, IN);
            albedo *= _Color;

            fragColorMode(IN);
            fragSoftParticles(IN);
            fragCameraFading(IN);

            #if defined(_METALLICGLOSSMAP)
            fixed2 metallicGloss = readTexture (_MetallicGlossMap, IN).ra * fixed2(1.0, _Glossiness);
            #else
            fixed2 metallicGloss = fixed2(_Metallic, _Glossiness);
            #endif

            #if defined(_NORMALMAP)
            float3 normal = normalize (UnpackScaleNormal (readTexture (_BumpMap, IN), _BumpScale));
            #else
            float3 normal = float3(0,0,1);
            #endif

            #if defined(_EMISSION)
            float emission = _Emission;
            #else
            float emission = 0;
            #endif

            fragDistortion(IN);

            o.Albedo = albedo.rgb;
            #if defined(_NORMALMAP)
            o.Normal = normal;
            #endif
            o.Emission = emission * albedo.rgb;
            o.Metallic = metallicGloss.r;
            o.Smoothness = metallicGloss.g;

            #if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON) || defined(_ALPHAOVERLAY_ON)
            o.Alpha = albedo.a;
            #else
            o.Alpha = 1;
            #endif

            #if defined(_ALPHAMODULATE_ON)
            o.Albedo = lerp(half3(1.0, 1.0, 1.0), albedo.rgb, albedo.a);
            #endif

            #if defined(_ALPHATEST_ON)
            clip (albedo.a - _Cutoff + 0.0001);
            #endif
        }
        ENDCG
    }

    Fallback "VertexLit"
    //CustomEditor "StandardParticlesShaderGUI"
}
