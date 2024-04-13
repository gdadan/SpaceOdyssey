using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BattlecruiserLaser : MonoBehaviour
{
    public GameObject battlecruiserLaserCharge;
    public GameObject battlecruiserLaser;

    bool lookPlayerOn = false;

    private void OnEnable()
    {
        StartCoroutine(Laser());
        StartCoroutine(LookPlayerOn());
    }

    IEnumerator Laser()
    {
        battlecruiserLaserCharge.SetActive(true);
        SoundManager.instance.PlaySFX(10);
        lookPlayerOn = true;
        yield return new WaitForSeconds(1.5f);

        battlecruiserLaser.SetActive(true);
        SoundManager.instance.PlaySFX(11);
        lookPlayerOn = false;
        yield return new WaitForSeconds(3f);

        
        battlecruiserLaser.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        battlecruiserLaserCharge.SetActive(false);

        transform.parent.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    IEnumerator LookPlayerOn()
    {
        while (true)
        {
            if (lookPlayerOn)
            {
                float difX = GameManager.instance.player.transform.position.x - transform.parent.position.x;         // Enemy와 Player의 x좌표 차이
                float difY = GameManager.instance.player.transform.position.y - transform.parent.position.y;         // Enemy와 Player의 y좌표 차이

                float radAngel = Mathf.Atan2(difY, difX);       // Enemy와 Player의 각도(radian)

                // Euler 각으로 Enemy z축 회전 조절(플레이어를 향해)
                // Enemy Prefab이 기본적으로 아래를 보고있기 때문에 90도를 보정해줌
                transform.parent.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
