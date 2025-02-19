using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            gunBase.StartShoot();
        }

        public override void Update()
        {

            base.Update();
            if (!isAlive)
            {
                gunBase.StopShoot();
            }
        }
    }
}
