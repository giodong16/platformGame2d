using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    public Animator animBow;
    public GameObject bow;
    string attackTriggerName;

    [Header("Melee Attack")]
    public int damage = 1;
    public Transform meleeAttackPoint;
    public float attackRange; // phạm vi
    public LayerMask enemyLayers;

    [Header("Attack Melee Range:")]
    public float punchRange = 0.3f;
    public float swordRange = 0.4f;

    [Header("Throw and Shoot:")]
    public Transform spawnPoint;
    public GameObject stonePrefab;
    public GameObject arrowPrefab;
    public float throwForce = 6f;


    [Header("Damage:")]
    public int onePunchDamage = 1;
    public int doublePunchDamage = 2;
    public int swordDamage = 2;
    public int rockDamage = 1;

    bool isCompleted =true;

    public float attackDuration = 1.0f; // Thời gian hành động tấn công kéo dài (giây)
    private bool isAttacking = false;
    private float attackStartTime;

    [Header("VFX Blood")]
    public GameObject vfx;

    public bool IsCompleted { get => isCompleted; set => isCompleted = value; }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isAttacking)
        {
            float elapsedTime = Time.time - attackStartTime;

            // Nếu thời gian tấn công vượt quá giới hạn
            if (elapsedTime > attackDuration)
            {
                EndAttack();
            }
        }

        anim.SetBool("IsCompleted",isCompleted);
    }
    public void StartAttack()
    {
        isAttacking = true;
        attackStartTime = Time.time; // Lưu lại thời gian bắt đầu tấn công
    }

    private void EndAttack()
    {
        isAttacking = false;
        SetCompleted();
    }
    public void Punch(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Started && isCompleted)
        {
            StartAttack();

            isCompleted = false;
            float random = Random.Range(0, 1f);
            if (random > 0.5f) { 
                attackTriggerName = AnimationAttackTrigger.DoublePunch.ToString();
                damage = doublePunchDamage;
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(NameSound.DoublePunch.ToString());
                }
            }
            else
            {
                attackTriggerName = AnimationAttackTrigger.Punch.ToString();
                damage = onePunchDamage;
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(NameSound.Punch.ToString());
                }
            }
            
            attackRange = punchRange;
            MeleeAttack();
        }
    }

    public void Sword(InputAction.CallbackContext context) { 
        if(context.phase == InputActionPhase.Started && isCompleted && Pref.SwordSkill == 1)
        {
            StartAttack();
            isCompleted = false;
            float random = Random.Range(0, 1f);
            attackTriggerName = AnimationAttackTrigger.Sword1.ToString();
            if (random > 0.5f)
            {
                attackTriggerName = AnimationAttackTrigger.Sword1.ToString();
            }
            else
            {
                attackTriggerName = AnimationAttackTrigger.Sword2.ToString();
            }
            damage = swordDamage;
            attackRange = swordRange;
            // âm thanh chém đâm
            AudioManager.Instance?.PlaySFX(NameSound.SlashSword.ToString());
            MeleeAttack();
        }
    }

    public void MeleeAttack()
    {
        // Phát hiện kẻ thù và vật phẩm trong phạm vi
        if (anim != null)
        {
            anim.SetTrigger(attackTriggerName);
        }
        attackDuration = anim.GetCurrentAnimatorStateInfo(0).length + 0.2f;
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(meleeAttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D obj in hitObjects)
        {
            //  kẻ thù
            EnemyHeath enemy = obj.GetComponent<EnemyHeath>();
            if (enemy != null)
            {

                enemy.TakeDamage(damage);
                Instantiate(vfx, obj.transform.position, Quaternion.identity);
            }

            //  vật phẩm có thể phá hủy
            DestroyableObject destroyableObject = obj.GetComponent<DestroyableObject>();
            if (destroyableObject != null)
            {

                destroyableObject.Broken();
                //Instantiate(vfx, obj.transform.position, Quaternion.identity);
            }
            BossHealth boss = obj.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Instantiate(vfx, obj.transform.position, Quaternion.identity);
            }
        }
    }

    public void ThrowStone(InputAction.CallbackContext context) 
    {
        if(context.phase == InputActionPhase.Started && isCompleted && Pref.Stones>0)
        {
            AudioManager.Instance.PlaySFX(NameSound.Throw.ToString());       
            Pref.Stones--;

            StartAttack();
            isCompleted = false;
            anim.SetTrigger(AnimationAttackTrigger.ThrowStone.ToString());
            attackDuration = anim.GetCurrentAnimatorStateInfo(0).length+0.2f;
        }
        GUIManager.Instance.UpdateStoneBar();
    }
    public void ShootStone()
    {
        if (stonePrefab != null )
        {
            GameObject stoneClone = Instantiate(stonePrefab,spawnPoint.position,Quaternion.identity);
            Rigidbody2D rb = stoneClone.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(transform.localScale.x * throwForce, rb.velocity.y);
        }
    }
    public void ShootArrow(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isCompleted && Pref.BowSkill == 1 && Pref.Arrows > 0)
        {
            Pref.Arrows--;
            GUIManager.Instance.UpdateArrowBar();
        
            StartAttack();
            isCompleted = false;
            anim.SetTrigger(AnimationAttackTrigger.ShootArrow.ToString());
            AudioManager.Instance?.PlaySFX(NameSound.Bow.ToString());
            attackDuration = anim.GetCurrentAnimatorStateInfo(0).length+0.2f;
            ShootArrow();
        }
    }


    public void ShootArrow()
    {
        animBow.Play("Direction_1");
       
    }
  
    public void SetCompleted()
    {
        isCompleted = true;
        
    }
}

public enum AnimationAttackTrigger
{
    Punch,
    DoublePunch,
    Sword1,
    Sword2,
    ThrowStone,
    ShootArrow

}
