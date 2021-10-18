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
    [FMODUnity.EventRef]
    public string VictorySound;
    //[SerializeField] GameObject slime;
    //Material slimeMaterial;
    private float curentHP;

    public float CurentHP { get => curentHP; set => curentHP = value; }

    private void Start()
    {
        curentHP = maxHP;
        //slimeMaterial = slime.GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        barraHP.fillAmount = curentHP / maxHP;
        if (curentHP <= 0)
        {
            if (isCapitan == true)
            {
                VictoryUI.SetActive(true);
                FMODUnity.RuntimeManager.PlayOneShotAttached(VictorySound, gameObject);
                Time.timeScale = 0;
            }
            Destroy(gameObject);
        }
    }
    public void TackeDamage(float damage)
    {
        curentHP -= damage;
    }
}
