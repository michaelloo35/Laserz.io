using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{
    [FormerlySerializedAs("PlayerStatus")] public PlayerStatus playerStatus;
    public Transform firePoint;
    public int playerId;
    public GameObject impactEffectTemplate;
    public float fireRate = 5f;
    private float _charge;
    private float _timeToNextFire;


    void Update()
    {
        if (Input.GetAxisRaw($"Fire_{playerId}") > 0 && Time.time > _timeToNextFire)
        {
            playerStatus.blocked = true;
            _charge += Time.deltaTime;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
            if (hitInfo)
            {
                DrawLine(firePoint.position, hitInfo.point, _charge / 5.0f);
            }
            else
            {
                DrawLine(firePoint.position, firePoint.up.normalized * 999, _charge / 5.0f);
            }

            if (_charge > 1.0f)
            {
                Shoot();
                _charge = 0.0f;
                _timeToNextFire = Time.time + 1f / fireRate;
                playerStatus.blocked = false;
            }
        }
        else
        {
            _charge = 0;
            playerStatus.blocked = false;
        }
    }

    private void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if (hitInfo)
        {
            ImpactEffect(hitInfo);
        }
    }

    private void ImpactEffect(RaycastHit2D hitInfo)
    {
        GameObject impactEffect = Instantiate(impactEffectTemplate, hitInfo.point,
            Quaternion.FromToRotation(transform.up, hitInfo.normal) * transform.rotation);
        Destroy(impactEffect, 0.8f);
    }

    void DrawLine(Vector3 start, Vector3 end, float width, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startWidth = width / 2;
        lr.endWidth = width / 2;
        lr.startColor = Color.magenta;
        lr.endColor = Color.blue;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}