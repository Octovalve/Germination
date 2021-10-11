using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    [SerializeField] GameObject weapon3;
    Weapon1 w1;
    Weapon2 w2;
    Weapon3 w3;
    TurnControl turnControl;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        w1 = weapon1.GetComponent<Weapon1>();
        w2 = weapon2.GetComponent<Weapon2>();
        w3 = weapon3.GetComponent<Weapon3>();
    }
    private void Update()
    {
        if (turnControl.Estado >= 6 || turnControl.Estado == 0)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
        }
    }
    public void Attack1()
    {
        w1.IsShoot = false;
        weapon1.SetActive(true);
        if (turnControl.Estado >= 6)
        {
            weapon1.SetActive(false);
        }
    }
    public void Attack2()
    {
        w2.IsShoot = false;
        weapon2.SetActive(true);
        if (turnControl.Estado >= 6)
        {
            weapon2.SetActive(false);
        }
    }
    public void Attack3()
    {
        w3.IsShoot = false;
        weapon3.SetActive(true);
        if (turnControl.Estado >= 6)
        {
            weapon3.SetActive(false);
        }
    }
    //Instantiate(weapon1, SpawnPoint.position, SpawnPoint.rotation);
}
