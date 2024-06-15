using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingComponent : MonoBehaviour
{

    [SerializeField] 
    private GameObject bulletPrefab;

    private Transform myTransform;

    private Vector2 shootDir = Vector2.zero;

    private float elapsedTime = 0;

    [SerializeField]
    private float fireRate;

    private bool shooting = false;


    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    [Tooltip("El objeto que lleva el collider")]
    private GameObject OwnerObject;


    [SerializeField]
    private Transform SpawnPoint;


    [SerializeField]
    private SpriteRenderer VisualElement;



    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;          
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;


        if (shooting  &&  elapsedTime >= fireRate)
        {

            elapsedTime = 0;

            GameObject newBullet = Instantiate(bulletPrefab, SpawnPoint.position, Quaternion.identity);
            newBullet.GetComponent<BulletComponent>().setVelocity(
                shootDir != new Vector2(0,0) ?shootDir : 
                VisualElement.flipX ? new Vector2(-1,0) :
                new Vector2(1,0));


            newBullet.GetComponent<BulletComponent>().setOwner(OwnerObject);
            newBullet.GetComponent<BulletComponent>().setDamage(bulletDamage);

        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {        
        shooting = context.started ? true : context.canceled ? false : true;    
    }

    public void Look(InputAction.CallbackContext context)
    {
        shootDir = context.ReadValue<Vector2>();
    }


}
