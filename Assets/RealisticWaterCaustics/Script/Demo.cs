using UnityEngine;

public class Demo : MonoBehaviour
{
	public Projector m_Projector;
	[Header("Realistic Water Caustics 1")]
	[Range(0.002f, 0.005f)] public float m_RWC1_Density = 0.003f;
	[Range(0f, 2f)] public float m_RWC1_Intensity = 1.5f;
	[Header("Realistic Water Caustics 2")]
	[Range(0f, 32f)] public float m_RWC2_Intensity = 20f;
	[Range(0f, 1f)] public float m_RWC2_Wave = 0.3f;
	int m_ID_Density = 0;
	int m_ID_Intensity = 0;
	int m_ID_RWC2_Intensity = 0;
	int m_ID_RWC2_Wave = 0;
	
	void Start ()
	{
		m_ID_Density = Shader.PropertyToID ("_Density");
		m_ID_Intensity = Shader.PropertyToID ("_Intensity");
		m_ID_RWC2_Intensity = Shader.PropertyToID ("_RWC2_Intensity");
		m_ID_RWC2_Wave = Shader.PropertyToID ("_RWC2_Wave");
	}
	void Update ()
	{
		m_Projector.material.SetFloat (m_ID_Density, m_RWC1_Density);
		m_Projector.material.SetFloat (m_ID_Intensity, m_RWC1_Intensity);
		m_Projector.material.SetFloat (m_ID_RWC2_Intensity, m_RWC2_Intensity);
		m_Projector.material.SetFloat (m_ID_RWC2_Wave, m_RWC2_Wave);
	}
	void OnGUI ()
	{
		GUI.Box (new Rect (10, 10, 230, 25), "Dynamic Water Caustic Demo");
	}
}
