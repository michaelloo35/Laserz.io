using System;
using TMPro;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    private float hoveringSpeed = 0.01f;
    private float disappearingSpeed;
    public TextMeshProUGUI damageText;
    public void Initialize(float damage)
    {
        string damageStr = Convert.ToInt32(damage).ToString();
        damageText.SetText(damageStr);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = new Vector3(0.0f, hoveringSpeed, 0.0f);
        transform.position += moveVector * Time.deltaTime;
        byte faceColorA = Convert.ToByte(damageText.faceColor.a * 0.98);
        damageText.faceColor = new Color32(
            damageText.faceColor.r, 
            damageText.faceColor.g,
            damageText.faceColor.b, 
            faceColorA);
    }
}