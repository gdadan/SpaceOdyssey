using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public string synergy1;
    public string synergy2;
    public string synergy3;

    public float atk;
    public float def;

    private void Update()
    {
        OnEngine();
    }
    protected virtual void OnEngine()
    {
        // ¿£Áø Âø¿ë
    }

    protected void EquipEngine()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    protected void OffEngine()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
