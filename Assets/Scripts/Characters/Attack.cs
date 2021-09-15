using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    [SerializeField] GameObject weapon3;
    [SerializeField] Weapon1 w1;
    TurnControl turnControl;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TurnControl>();
    }
    private void Update()
    {
        if (turnControl.Estado >= 6)
        {
            weapon1.SetActive(false);
        }
    }
    public void Attack1()
    {
        w1.IsShoot = false;
        weapon1.SetActive(true);
    }
    public void Attack2()
    {
        weapon2.SetActive(true);
        if (turnControl.Estado >= 6)
        {
            weapon2.SetActive(false);
        }
    }
    public void Attack3()
    {
        weapon3.SetActive(true);
        if (turnControl.Estado >= 6)
        {
            weapon3.SetActive(false);
        }
    }
    //Instantiate(weapon1, SpawnPoint.position, SpawnPoint.rotation);
}
