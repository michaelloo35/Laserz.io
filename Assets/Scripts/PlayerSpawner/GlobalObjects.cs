using UnityEngine;

public class GlobalObjects
{

    public GameObject smokeImpactEffect;

    public GlobalObjects()
    {
        smokeImpactEffect = new GameObject("Smoke Impact") ;
        smokeImpactEffect.transform.localPosition = new Vector3(-100,0,0);
        smokeImpactEffect.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        
        SpriteRenderer spriteRenderer = smokeImpactEffect.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Graphic/smoke_impact_47");
        spriteRenderer.sortingOrder = 1;
        
        Animator animator = smokeImpactEffect.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/SmokeImpact");

    }
}