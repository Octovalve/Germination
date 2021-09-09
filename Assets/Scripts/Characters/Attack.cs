using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Rigidbody weapon1;
    [SerializeField] Transform SpawnPoint;
    public void Attack1() { Instantiate(weapon1, SpawnPoint.position, SpawnPoint.rotation); }
    public void Attack2() { }
    public void Attack3() { }
}
