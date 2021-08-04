using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        //attacks the target
        public void Attack(EnemyTarget target)
        {
            print("You are hit" + target.name);
        }
    }
}
