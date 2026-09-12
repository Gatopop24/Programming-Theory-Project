using UnityEngine;

public class SmallBullet : BaseBullet
{
    protected override void OnEnable()
    {
        speed = 20f;
        damage = 5;
        base.OnEnable();
    }
}
