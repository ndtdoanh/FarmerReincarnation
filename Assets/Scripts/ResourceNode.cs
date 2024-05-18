using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;
    [SerializeField] Item item;
    [SerializeField] int dropCount = 5;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] ResourceNodeType nodeType;
    private int hitCount = 0;
    private int maxHitCount = 3;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Hit()
    {
        hitCount++;

        if (animator != null)
        {
            animator.SetTrigger("Tree_hit");
        }

        if (hitCount >= maxHitCount)
        {
            while (dropCount > 0)
            {
                dropCount -= 1;
                Vector3 position = transform.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;
                ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
            }

            if (animator != null)
            {
                animator.SetTrigger("Tree_destroy");
                animator.SetTrigger("Rock_destroy");
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}