using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int LVL;
    public int HP;

    public void GetLVL()
    {
        LVL++;
    }

    public void GetDamage(int damage)
    {
        HP -= damage;
    }
}
