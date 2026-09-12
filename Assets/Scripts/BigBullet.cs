using UnityEngine;

public class BigBullet : BaseBullet
{
    protected override void OnEnable()
    {
        speed = 5f;
        damage = 20;
        base.OnEnable();
    }

    protected override void Aim()
    {
        if(PlayerController.Instance != null)
        {
            if (transform.position.x >= 0f)
            {
                moveDirection = Vector3.left; 
            }
            else
            {
                moveDirection = Vector3.right; 
            }
        }
        
    }
}
