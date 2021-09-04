using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] Image barraHP;
    [SerializeField] float maxHP;
    [SerializeField] bool isCapitan;
    [SerializeField] GameObject defeatUI;
    private float curentHP;

    public float CurentHP { get => curentHP; set => curentHP = value; }

    private void Start()
    {
        curentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        barraHP.fillAmount = curentHP / maxHP;
        if (curentHP <= 0)
        {
            Destroy(gameObject);
            if (isCapitan == true)
            {
                defeatUI.SetActive(true);
            }
        }
    }
    public void TackeDamage(float damage)
    {
        curentHP -= damage;
        Debug.Log(damage / maxHP);
    }
}
