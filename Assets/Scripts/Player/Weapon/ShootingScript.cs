using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private Transform pivot;
    private Vector2 direction;
    public GameObject bullet;
    private float bulletSpeed = 2000f;
    public Animator recoilAnimation;
    private float fireRate = 0.35f;
    public Transform shootPoint;
    private bool readyToShoot = true;
    public static ShootingScript instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)pivot.position;
        FollowMouse();

        if(Input.GetMouseButtonDown(0) && readyToShoot && fireRate >= 0.1f && bulletSpeed <= 10000f)
        {
            StartCoroutine(Shoot());
            recoilAnimation.SetTrigger("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.right * bulletSpeed);
        readyToShoot=false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }

    private void FollowMouse()
    {
        pivot.right = direction;
        float zValue = pivot.rotation.eulerAngles.z;
        if (zValue > 90f && zValue < 270f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    public void shotSpeedMultiplier(float speedMultiplier)
    {
        bulletSpeed += speedMultiplier;
    }
    public void fireRateSubstract(float fireRateMultiplier)
    {
        fireRate -= fireRateMultiplier;
    }
}
