using UnityEngine;
using UnityEditor;

public class RealisticWaterCausticsShaderGUI : ShaderGUI
{
	enum EBlendMode
	{
		Blend,
		Additive,
		Unknown,
	}
	EBlendMode m_Func = EBlendMode.Blend;
	enum ECausticsFunc
	{
		ProceduralFunc1,
		ProceduralFunc2,
	}
	ECausticsFunc m_Caustics = ECausticsFunc.ProceduralFunc1;

	public override void OnGUI (MaterialEditor me, MaterialProperty[] props)
	{
		Material mat = me.target as Material;
		
		// collect current material state
		UnityEngine.Rendering.BlendMode src = (UnityEngine.Rendering.BlendMode)mat.GetInt ("_BlendSrc");
		UnityEngine.Rendering.BlendMode dst = (UnityEngine.Rendering.BlendMode)mat.GetInt ("_BlendDst");
		if (src == UnityEngine.Rendering.BlendMode.SrcAlpha && dst == UnityEngine.Rendering.BlendMode.One)
			m_Func = EBlendMode.Additive;
		else if (src == UnityEngine.Rendering.BlendMode.SrcAlpha && dst == UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha)
			m_Func = EBlendMode.Blend;
		else
			m_Func = EBlendMode.Unknown;
		
		string[] keys = mat.shaderKeywords;
		if (keys.Length == 1)
		{
			if (keys[0] == "RWC_TYPE1")
				m_Caustics = ECausticsFunc.ProceduralFunc1;
			else if (keys[0] == "RWC_TYPE2")
				m_Caustics = ECausticsFunc.ProceduralFunc2;
		}
		
		// gui
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Transparent Mode:");
		m_Func = (EBlendMode)EditorGUILayout.EnumPopup (m_Func);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Caustics Func:");
		m_Caustics = (ECausticsFunc)EditorGUILayout.EnumPopup (m_Caustics);
		EditorGUILayout.EndHorizontal ();
		
		// apply changed material state
		if (EditorGUI.EndChangeCheck())
		{
			if (m_Func == EBlendMode.Blend)
			{
				mat.SetInt ("_BlendSrc", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				mat.SetInt ("_BlendDst", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			}
			else if (m_Func == EBlendMode.Additive)
			{
				mat.SetInt ("_BlendSrc", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				mat.SetInt ("_BlendDst", (int)UnityEngine.Rendering.BlendMode.One);
			}
			
			if (m_Caustics == ECausticsFunc.ProceduralFunc1)
			{
				mat.EnableKeyword ("RWC_TYPE1");
				mat.DisableKeyword ("RWC_TYPE2");
			}			
			else if (m_Caustics == ECausticsFunc.ProceduralFunc2)
			{
				mat.EnableKeyword ("RWC_TYPE2");
				mat.DisableKeyword ("RWC_TYPE1");
			}
        }
		if (m_Func == EBlendMode.Unknown)
		{
			GUI.color = Color.yellow;
			EditorGUILayout.LabelField ("Unknown Transparent Mode !!");
			GUI.color = Color.white;
		}
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		base.OnGUI (me, props);
	}
}