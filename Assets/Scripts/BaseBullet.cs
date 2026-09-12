using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speed = 10f;
    protected Vector3 moveDirection;

    protected virtual void OnEnable()
    {
        Aim();
    }

    protected virtual void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DealDamage();
            gameObject.SetActive(false);
        }
    }

    protected virtual void Aim()
    {
        if(PlayerController.Instance != null)
        {
            Vector3 targetPosition = PlayerController.Instance.transform.position;
            Vector3 bulletPosition = transform.position;
            Vector3 direction = new Vector3(targetPosition.x - bulletPosition.x, targetPosition.y - bulletPosition.y, 0f);
            moveDirection = direction.normalized;
        }
    }

    protected virtual void DealDamage()
    {
        PlayerController.Instance.TakeDamage(damage);
    }
}
