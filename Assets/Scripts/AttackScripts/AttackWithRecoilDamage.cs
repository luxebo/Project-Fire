// An example attack that demonstrates recoil (self-damage) on activation.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithRecoilDamage : Attack {
    public float selfDamage;
    private GameObject[] hitboxObjs = new GameObject[4];
    private Hitbox[] hitboxes = new Hitbox[4];

    // Must initialize hitbox factory manually if not supplied.
    protected override void Start()
    {
        base.Start();
        for(int i = 0; i<hitboxObjs.Length; i++)
        {
            hitboxObjs[i] = Hitbox.createHitbox(originObject, 10f, .33f, false, true);
            hitboxes[i] = hitboxObjs[i].GetComponent<Hitbox>();
        }
        // Front
        hitboxObjs[0].transform.SetParent(originObject.transform, false);
        hitboxObjs[0].transform.localScale = new Vector3(2f, .5f, .5f);
        hitboxObjs[0].transform.localPosition = new Vector3(1.55f, 0);
        hitboxObjs[0].SetActive(false);
        // Back
        hitboxObjs[1].transform.SetParent(originObject.transform, false);
        hitboxObjs[1].transform.localScale = new Vector3(2f, .5f, .5f);
        hitboxObjs[1].transform.localPosition = new Vector3(-1.55f, 0);
        hitboxObjs[1].SetActive(false);
        //Left
        hitboxObjs[2].transform.SetParent(originObject.transform, false);
        hitboxObjs[2].transform.localScale = new Vector3(.5f, .5f, 2f);
        hitboxObjs[2].transform.localPosition = new Vector3(0, 0, 1.55f);
        hitboxObjs[2].SetActive(false);
        //Right
        hitboxObjs[3].transform.SetParent(originObject.transform, false);
        hitboxObjs[3].transform.localScale = new Vector3(.5f, .5f, 2f);
        hitboxObjs[3].transform.localPosition = new Vector3(0, 0, -1.55f);
        hitboxObjs[3].SetActive(false);
    }


    protected override void attackAction()
    {
        for(int i = 0; i< hitboxObjs.Length; i++)
        {
            hitboxObjs[i].SetActive(true);
        }
        CombatActor selfDmg = GetComponentInParent<CombatActor>();
        if (selfDmg != null)
        {
            selfDmg.Health -= Mathf.FloorToInt(selfDamage);
            // for debugging
            print(selfDmg.Health);
        }
        
    }
}
