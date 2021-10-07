using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] Image barraHP;
    [SerializeField] float maxHP;
    [SerializeField] bool isCapitan;
    [SerializeField] GameObject VictoryUI;
    [SerializeField] GameObject slime;
    Material slimeMaterial;
    private float curentHP;

    public float CurentHP { get => curentHP; set => curentHP = value; }

    private void Start()
    {
        curentHP = maxHP;
        slimeMaterial = slime.GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        barraHP.fillAmount = curentHP / maxHP;
        if (curentHP <= 0)
        {
            float timer = 0 + Time.deltaTime;
            slimeMaterial.SetFloat("_Dissolve", timer);
            if (timer > 1)
            {
                Destroy(gameObject);
                if (isCapitan == true)
                {
                    VictoryUI.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
    }
    public void TackeDamage(float damage)
    {
        curentHP -= damage;
    }
}
