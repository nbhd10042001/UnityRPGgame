using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCtrl : MonoBehaviour
{
    [SerializeField] private Material m_BeachBg;
    [SerializeField] private Material m_SkyBg;
    [SerializeField] private Material m_CloudBg;
    [SerializeField] private float m_speedScrollBeachBg;
    [SerializeField] private float m_speedScrollSkyBg;
    [SerializeField] private float m_speedScrollCloudBg;

    private int m_MainTexId;

    private void Start()
    {
        m_MainTexId = Shader.PropertyToID("_MainTex");

        if (Application.isPlaying)
        {
            Vector2 offset = m_BeachBg.GetTextureOffset(m_MainTexId);
            offset = new Vector2(0, 0);
            m_BeachBg.SetTextureOffset(m_MainTexId, offset);

            offset = m_SkyBg.GetTextureOffset(m_MainTexId);
            offset = new Vector2(0, 0);
            m_SkyBg.SetTextureOffset(m_MainTexId, offset);

            offset = m_CloudBg.GetTextureOffset(m_MainTexId);
            offset = new Vector2(0, 0);
            m_CloudBg.SetTextureOffset(m_MainTexId, offset);
        }
    }

    private void Update()
    {
        Vector2 offset = m_BeachBg.GetTextureOffset(m_MainTexId);
        offset += new Vector2(m_speedScrollBeachBg * Time.deltaTime, 0);
        m_BeachBg.SetTextureOffset(m_MainTexId, offset);

        offset = m_SkyBg.GetTextureOffset(m_MainTexId);
        offset += new Vector2(m_speedScrollSkyBg * Time.deltaTime, 0);
        m_SkyBg.SetTextureOffset(m_MainTexId, offset);

        offset = m_CloudBg.GetTextureOffset(m_MainTexId);
        offset += new Vector2(m_speedScrollCloudBg * Time.deltaTime, 0);
        m_CloudBg.SetTextureOffset(m_MainTexId, offset);
        
    }

    
}
