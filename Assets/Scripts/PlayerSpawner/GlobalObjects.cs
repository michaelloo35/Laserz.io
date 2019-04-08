using UnityEngine;

public class GlobalObjects
{
    public static GameObject smokeImpactEffect;
    public static GameObject deathAnimation;

    public static void init()
    {
        initSmokeEffect();
        initDeathAnimation();
    }

    private static void initSmokeEffect()
    {
        smokeImpactEffect = new GameObject("Smoke Impact");
        smokeImpactEffect.transform.localPosition = new Vector3(-100, 0, 0);
        smokeImpactEffect.transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

        SpriteRenderer spriteRenderer = smokeImpactEffect.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Graphic/smoke_impact_47");
        spriteRenderer.sortingOrder = -1;

        Animator animator = smokeImpactEffect.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/SmokeImpact");
    }

    private static void initDeathAnimation()
    {
        deathAnimation = new GameObject("Death Animation");
        deathAnimation.transform.localPosition = new Vector3(-100, 0, 0);

        SpriteRenderer spriteRenderer = deathAnimation.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Graphic/death_anim_0");
        spriteRenderer.sortingOrder = -2;

        Animator animator = deathAnimation.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Death");
    }
}