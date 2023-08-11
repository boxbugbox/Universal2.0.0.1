using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public GameObject m_model;
    public Texture _onehand, _bothhands;
    public Material[] m_material;

    private void Awake()
    {
        m_model = this.gameObject;
        m_material = m_model.GetComponent<Renderer>().materials;
        m_material[2].SetFloat("_CutMode", 0f);
    }
    public void OnStart()
    {
        m_material[2].SetFloat("_CutMode", 1f);
    }
    public void OnClose()
    {
        m_material[2].SetFloat("_CutMode", 0f);
    }
    public void OneHand()
    {
        m_material[2].SetTexture("_AlphaMask", _onehand);
    }
    public void BothHands()
    {
        m_material[2].SetTexture("_AlphaMask", _bothhands);
    }
}
