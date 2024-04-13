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
                float difX = GameManager.instance.player.transform.position.x - transform.parent.position.x;         // Enemy�� Player�� x��ǥ ����
                float difY = GameManager.instance.player.transform.position.y - transform.parent.position.y;         // Enemy�� Player�� y��ǥ ����

                float radAngel = Mathf.Atan2(difY, difX);       // Enemy�� Player�� ����(radian)

                // Euler ������ Enemy z�� ȸ�� ����(�÷��̾ ����)
                // Enemy Prefab�� �⺻������ �Ʒ��� �����ֱ� ������ 90���� ��������
                transform.parent.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
