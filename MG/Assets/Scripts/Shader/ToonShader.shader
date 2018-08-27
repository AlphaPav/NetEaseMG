﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mini/Toon" {
	Properties {
		_Albedo("Albedo", 2D) = "white"{}
		_OutlineColor("Outline Color", color)=(0,0,0,1)
		_Color("Main Color",color)=(1,1,1,1)//物体的颜色
		_Outline("Thick of Outline",range(0,0.1))=0.02//挤出描边的粗细
		_Factor("Factor",range(0,1))=0.5//挤出多远
		_ToonEffect("Toon Effect",range(0,1))=0.5//卡通化程度（二次元与三次元的交界线）
		_Steps("Steps of toon",range(0,9))=3//色阶层数
		_RimColor("Rim Color", color) = (1,0,0,1)
		_RimPower("Rim Power", Float) = 1.0
		_ToonRimStep("Rim Step", range(0,9)) = 3
		_RimEffect("Toon Effect", range(0,1)) = 0.5
		[Toggle(_Noise_On)]_NoiseOn("NoiseOn", Float) = 0
		_Noise("Noise", 2D) = "white"{}
		[HDR]_EdgeColor1("Edge Color 1", color) = (1,1,1,1)
		[HDR]_EdgeColor2("Edge Color 2", color) = (1,1,1,1)
		_EdgeWidth("Edge Width", Range(0.0, 1.0)) = 0.1
		[HideInInspector]_Timenow("Timenow", Float) = 0.0
	}
	SubShader {
		pass{// outline
			Tags{"LightMode"="Always"}
			Cull Front
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile _DISSOLVE_OFF _DISSOLVE_ON
			
			uniform float _Outline;
			uniform float _Factor;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float4 _EdgeColor1;
			uniform float4 _EdgeColor2;
			uniform float _EdgeWidth;
			uniform float _Timenow;
			uniform float4 _OutlineColor;
		
			struct v2f {
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			v2f vert (appdata_full v) {
				v2f o;
				float3 dir=normalize(v.vertex.xyz);
				float3 dir2=v.normal;
				float D=dot(dir,dir2);
				dir=dir*sign(D);
				dir=dir*_Factor+dir2*(1-_Factor);
				v.vertex.xyz+=dir*_Outline;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}
			float4 frag(v2f i):COLOR
			{
				float4 c=_OutlineColor;
				#ifdef _DISSOLVE_ON
					float noise = tex2D(_Noise,i.uv*_Noise_ST.xy + _Noise_ST.zw).r;
					float thres = saturate(_Time.y-_Timenow);
					if(noise < thres) discard;
					//discard;
				#endif
				return c;
			}
			ENDCG
		}//end of pass


		pass{//forward add : directional lighting
			Tags{"LightMode"="ForwardBase"}
			Cull Back
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma shader_feature _Noise_On
			#pragma multi_compile _DISSOLVE_OFF _DISSOLVE_ON

			uniform float4 _LightColor0;
			uniform float4 _Color;
			uniform float _Steps;
			uniform float _ToonEffect;
			uniform sampler2D _Albedo;
			uniform float4 _Albedo_ST;
			uniform float _RimPower;
			uniform float _ToonRimStep;
			uniform float _RimEffect;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float4 _EdgeColor1;
			uniform float4 _EdgeColor2;
			uniform float _EdgeWidth;
			uniform float _Timenow;
		
			struct v2f {
				float4 pos:SV_POSITION;
				float3 lightDir:TEXCOORD0;
				float3 viewDir:TEXCOORD1;
				float3 normal:TEXCOORD2;
				float2 uv:TEXCOORD3;
			};

			v2f vert (appdata_full v) {
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);//切换到世界坐标
				o.normal=v.normal;
				o.lightDir=ObjSpaceLightDir(v.vertex);
				o.viewDir=ObjSpaceViewDir(v.vertex);
				o.uv = v.texcoord.xy;

				return o;
			}

			float4 frag(v2f i):COLOR
			{
				float4 c=1;
				float3 N=normalize(i.normal);
				float3 viewDir=normalize(i.viewDir);
				float3 lightDir=normalize(i.lightDir);
				float diff=max(0,dot(N,i.lightDir));//求出正常的漫反射颜色
				diff=(diff+1)/2;//做亮化处理
				diff=smoothstep(0,1,diff);//使颜色平滑的在[0,1]范围之内
				float toon=floor(diff*_Steps)/_Steps;//把颜色做离散化处理，把diffuse颜色限制在_Steps种（_Steps阶颜色），简化颜色，这样的处理使色阶间能平滑的显示
				diff=lerp(diff,toon,_ToonEffect);//根据外部我们可控的卡通化程度值_ToonEffect，调节卡通与现实的比重

				float rim = 1.0 - saturate(dot(N, normalize(viewDir)));
				rim += 1;
				rim = pow(rim, _RimPower);
				float toonRim = floor(rim * _ToonRimStep);
				rim = lerp(rim, toonRim, _RimEffect);

				float4 albedoColor = tex2D(_Albedo, i.uv*_Albedo_ST.xy + _Albedo_ST.zw);
				float noise = tex2D(_Noise,i.uv*_Noise_ST.xy + _Noise_ST.zw).r;
				noise = 0.6*noise + 0.5;
				#ifdef _Noise_On
					c=_Color*albedoColor*_LightColor0*(diff)*rim*1.5*noise;//把最终颜色混合
				#else
					c=_Color*albedoColor*_LightColor0*(diff)*rim*1.5;//把最终颜色混合
				#endif

				#ifdef _DISSOLVE_ON
					float thres = saturate(_Time.y-_Timenow);
					float disNoise = tex2D(_Noise,i.uv*_Noise_ST.xy + _Noise_ST.zw).r;
					if(disNoise < thres) discard;
					float degree = saturate((disNoise - thres)/_EdgeWidth);
					float4 edgeColor = lerp(_EdgeColor1*2, _EdgeColor2*2, degree);
					c = lerp(edgeColor, c, degree);
				#endif
				
				return c;
			}
			ENDCG
		}
		
		pass{//forward add 
			Tags{"LightMode"="ForwardAdd"}
			Blend One One
			Cull Back
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile _DISSOLVE_OFF _DISSOLVE_ON

			float4 _LightColor0;
			float4 _Color;
			float _Steps;
			float _ToonEffect;
			float _RimPower;
			float _ToonRimStep;
			float _RimEffect;
			uniform sampler2D _Albedo;
			uniform float4 _Albedo_ST;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float4 _EdgeColor1;
			uniform float4 _EdgeColor2;
			uniform float _EdgeWidth;
			uniform float _Timenow;

			struct v2f {
				float4 pos:SV_POSITION;
				float3 lightDir:TEXCOORD0;
				float3 viewDir:TEXCOORD1;
				float3 normal:TEXCOORD2;
				float2 uv:TEXCOORD3;
			};

			v2f vert (appdata_full v) {
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.normal=v.normal;
				o.viewDir=ObjSpaceViewDir(v.vertex);
				o.lightDir=_WorldSpaceLightPos0-v.vertex;
				o.uv = v.texcoord.xy;

				return o;
			}

			float4 frag(v2f i):COLOR
			{
				float4 c=1;
				float3 N=normalize(i.normal);
				float3 viewDir=normalize(i.viewDir);
				float dist=length(i.lightDir);//求出距离光源的距离
				float3 lightDir=normalize(i.lightDir);
				float diff=max(0,dot(N,i.lightDir));
				diff=(diff+1)/2;
				diff=smoothstep(0,1,diff);
				float atten=1/(dist);//根据距光源的距离求出衰减
				float toon=floor(diff*atten*_Steps)/_Steps;
				diff=lerp(diff,toon,_ToonEffect);

				half3 h = normalize (lightDir + viewDir);//求出半角向量
				float nh = max (0, dot (N, h));
				float spec = pow (nh, 32.0);//求出高光强度
				float toonSpec=floor(spec*atten*2)/ 2;//把高光也离散化
				spec=lerp(spec,toonSpec,_ToonEffect);//调节卡通与现实高光的比重

				float rim = 1.0 - saturate(dot(N, normalize(viewDir)));
				rim += 1;
				rim = pow(rim, _RimPower);
				float toonRim = floor(rim * _ToonRimStep);
				rim = lerp(rim, toonRim, _RimEffect);

				#ifdef _DISSOLVE_ON
					float noise = tex2D(_Noise,i.uv*_Noise_ST.xy + _Noise_ST.zw).r;
					float thres = saturate(_Time.y-_Timenow);
					if(noise < thres) discard;
					float degree = saturate((noise - thres)/_EdgeWidth);
					float4 edgeColor = lerp(_EdgeColor1, _EdgeColor2, degree);
					c = lerp(edgeColor, c, degree);
				#endif

				c=_Color*_LightColor0*(diff+spec);//求出最终颜色
				return c;
			}
			ENDCG
			}
	} 
}