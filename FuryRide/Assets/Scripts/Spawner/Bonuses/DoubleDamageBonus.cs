using System.Collections;
using UnityEngine;

public class DoubleDamageBonus : InteractibleObjects
{
    [SerializeField] private float _bonusDuration;
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            other.transform.TryGetComponent(out PlayerService controller);
            if (controller != null)
                StartCoroutine(TimingBonusDuration(_bonusDuration));

            _meshedObject.gameObject.TryGetComponent(out MeshRenderer mesh);
            _meshRenderer.enabled = false;
        }
    }
    public override void Update()
    {
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
    private IEnumerator TimingBonusDuration(float duration)
    {
        //if (!gun.IsDamageBusted)
        //{
        //    gun.SetGunDamage(gun.GunDamage * 2);
        //    var currentspeed = Speed;
        //    Speed = 0;
        yield return new WaitForSeconds(duration);
        //    gun.SetGunDamage(gun.GunDamage / 2);
        //    Speed = currentspeed;
        //    gun.IsDamageBusted = false;
        //    _meshRenderer.enabled = true;
        //    ParentObject.gameObject.SetActive(false);
        //}
    }
}
