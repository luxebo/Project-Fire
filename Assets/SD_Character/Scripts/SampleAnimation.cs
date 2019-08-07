using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    // Hotkeys
    HotkeysSettings hk;

    private KeyCode up = KeyCode.W;
    private KeyCode down = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode dash = KeyCode.LeftShift;

    private KeyCode keybind1 = KeyCode.Z;
    private KeyCode keybind2 = KeyCode.X;
    private KeyCode keybind3 = KeyCode.C;
    private KeyCode keybind4 = KeyCode.V;

    // Attacks
    Attack atk1;
    Attack atk2;
    Attack atk3;
    Attack atk4;

    // Player movement
    PlayerMovement1 movement;

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    // 初期化メソッド
    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();

        // load hotkeys
        hk = Hotkeys.loadHotkeys();
        keybind1 = hk.loadHotkeySpecific(5);
        keybind2 = hk.loadHotkeySpecific(6);
        keybind3 = hk.loadHotkeySpecific(7);
        keybind4 = hk.loadHotkeySpecific(8);
        up = hk.loadHotkeySpecific(0);
        down = hk.loadHotkeySpecific(1);
        left = hk.loadHotkeySpecific(2);
        right = hk.loadHotkeySpecific(3);
        dash = hk.loadHotkeySpecific(10);

        // load attacks
        atk1 = GetComponent<ExampleAttack>();
        atk2 = GetComponent<AttackWithRecoilDamage>();
        atk3 = GetComponent<AttackThatCanHitSelf>();
        atk4 = GetComponent<Shoot>();

        //load movement
        movement = GetComponent<PlayerMovement1>();
    }

    // 1フレームに1回コールされる
    void Update()
    {
        // 矢印上ボタンを押下している
        if (movement.isMoving)
        {
            // IdleからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        else
        { 
            // RunからIdleに遷移する
            this.animator.SetBool(key_isRun, false);
        }

        // パンチ aを押す
        if ((Input.GetKeyDown(keybind4) && atk4.isAvailable) || (Input.GetKeyDown(keybind3) && atk3.isAvailable))
        {
            //Attack01に遷移する
            this.animator.SetBool(key_isAttack01, true);
        }
        else
        {
            // Attack01からIdleに遷移する
            this.animator.SetBool(key_isAttack01, false);
        }
		
		// キック sを押す
        if ((Input.GetKeyDown(keybind1) && atk1.isAvailable) || (Input.GetKeyDown(keybind2) && atk2.isAvailable))
        {
            //Attack02に遷移する
            this.animator.SetBool(key_isAttack02, true);
        }
        else
        {
            // Attack02からIdleに遷移する
            this.animator.SetBool(key_isAttack02, false);
        }
       
        // ジャンプ spaceを押す
        if (Input.GetKeyUp("space"))
        {
            //Jumpに遷移する
            this.animator.SetBool(key_isJump, true);
        }
        else
        {
            // JumpからIdleに遷移する
            this.animator.SetBool(key_isJump, false);
        }

        // ダメージ ｄを押す
        if (Input.GetKeyUp("d"))
        {
            //Damageに遷移する
            this.animator.SetBool(key_isDamage, true);
        }
        else
        {
            // DamageからIdleに遷移する
            this.animator.SetBool(key_isDamage, false);
        }

        // 死亡 fを押す
        if (Input.GetKeyUp("f"))
        {
            //Deadに遷移する
            this.animator.SetBool(key_isDead, true);
        }
    }
}