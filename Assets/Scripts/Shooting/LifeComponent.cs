using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LifeComponent : MonoBehaviour
{
    bool onDeath = false;
    public enum EntityType
    {
        Player,
        Enemy
    }

    [SerializeField]
    private float currentLife;
    
    [SerializeField]
    private float maxlife;

    [SerializeField] private float lifeRegen;

    [SerializeField]
    private EntityType type;

    [SerializeField] UpdateHPbar hpBar;

    private PinataManager manager;

    [SerializeField]
    private PlayerId playerId;

    [SerializeField]
    private PlayerAnimations playerAnimations;


    [SerializeField]
    private GameObject actionMapContainer = null;

    private InputActionMap actionMap;

    [SerializeField]
    private LayerMask elevatorMask;

    [SerializeField]
    private GameObject respawnVFX;


    [SerializeField]
    Transform raycastOrigin;
    public void SetLifeRegen(float _lifeRegen)
    {
        lifeRegen = _lifeRegen;
    }

    public void setMaxLife(float _maxlife)
    {
        float lifeOffset = _maxlife - maxlife;
        currentLife += lifeOffset; // Esto es para que al ganar vida m�xima tambi�n ganes vida actual, es decir si est�s a 47/50 y ganas 3, estar�s a 50/53.

        maxlife = _maxlife;

    }

    private void Awake()
    {
        if(actionMapContainer != null)
        {
            actionMap = actionMapContainer.GetComponent<PlayerInput>().currentActionMap;
        }

    }
    private void Start()
    {
        currentLife = maxlife;
        manager = GetComponent<PinataManager>();

        if (type == EntityType.Player)
            playerAnimations = transform.GetChild(GetComponent<PlayerId>().GetPlayerId() == 0 ? 1 : 0).GetComponent<PlayerAnimations>();


        if (type == EntityType.Player)
        {
            if (playerId.GetPlayerId() == 0)
                hpBar = GameObject.Find("P1HPBar").GetComponent<UpdateHPbar>();

            else if (playerId.GetPlayerId() == 1)
                hpBar = GameObject.Find("P2HPBar").GetComponent<UpdateHPbar>();
        }
            
    }

    private void Update()
    {
        // sistema de regeneraci�n de vida
        if(currentLife < maxlife)
        {
            currentLife += lifeRegen * Time.deltaTime;
            if (currentLife > maxlife) currentLife = maxlife;

            if (hpBar != null)
                hpBar.UpdateBar(currentLife, maxlife);
        }
    }

    public void ReciveDamage(float damage, GameObject bulletOwner)
    {
        currentLife -= damage;

        if(playerAnimations != null)
        {
            playerAnimations.OnHit();
            StartCoroutine(Camera.main.GetComponent<CameraManager>().CameraShake(1, 3, 0.2f));
        }
        

        if (hpBar != null)
            hpBar.UpdateBar(currentLife, maxlife);

        if (currentLife <= 0)
        {
            Die(bulletOwner);
        }

        if (type == EntityType.Player)
        {
            SoundManager.instance.playSound((int)SoundManager.CLIPS.PLAYER_HIT);
        }
        else if (type == EntityType.Enemy)
        {
            SoundManager.instance.playSound((int)SoundManager.CLIPS.ENEMY_HIT);

        }
    }

    private void Die(GameObject bulletOwner)
    {
        if(type == EntityType.Player)
        {
            if(!onDeath)
                StartCoroutine(DeathCoroutine());
            
        }

        if(type == EntityType.Enemy)
        {
            int bulletOwnerId = bulletOwner.GetComponentInChildren<PlayerId>().GetPlayerId();

            // Le damos nuestro drop al jugador que nos ha matado
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(bulletOwnerId);

            _player.ChangePower(+manager.powerDrop);
            _player.ChangeCoins(+manager.coinDrop);

            PlayerDataManager.THIS.SetPlayer(bulletOwnerId, _player);
            Destroy(gameObject);
        }


       
    }

    IEnumerator DeathCoroutine()
    {
        onDeath = true;

        SoundManager.instance.playSound((int)SoundManager.CLIPS.DEATH);


        playerAnimations.OnDeath();


        actionMap.Disable();


        GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, 0.35f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.7f, 0.7f);


        yield return new WaitForSecondsRealtime(2f);

        GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, 0.45f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.9f, 0.9f);

        playerAnimations.OnRespawn();

        GameObject elevator = getElevatorInFloor();

        print(elevator);


        if (elevator != null)
        {
            elevator.GetComponentInChildren<ElevatorComponent>().SubirPiso();
            //transform.position = elevator.transform.position;   
        }

        // Subimos el player un pisito
        int myId = GetComponentInChildren<PlayerId>().GetPlayerId();
        var data = PlayerDataManager.THIS.GetPlayer(myId);
        data.SetPiso(math.clamp(PlayerDataManager.THIS.GetPlayer(playerId.GetPlayerId()).GetPiso() - 1, 0, 7));
        PlayerDataManager.THIS.SetPlayer(myId, data);


        // Spawneamos en el nuevo piso
        GetComponentInChildren<SpawnSystem>().SpawnOnPiso();

        // Player pierde una parte de poder
        PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(myId);
        _player.PowerMultiplier(0.8f);
        PlayerDataManager.THIS.SetPlayer(myId, _player);

        // Reseteamos la vida
        currentLife = maxlife;

        if (hpBar != null)
            hpBar.UpdateBar(currentLife, maxlife);

        onDeath = false;


        actionMap.Enable();

        SoundManager.instance.playSound((int)SoundManager.CLIPS.RESPAWN);

        if (respawnVFX != null)
        {
            GameObject effect = Instantiate(respawnVFX, transform.position, Quaternion.identity) as GameObject;
            Destroy(effect, 2f);
        }

    }


    GameObject getElevatorInFloor()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(raycastOrigin.position, new Vector2(1, 0), 10000f, elevatorMask);
        RaycastHit2D hit2 = Physics2D.Raycast(raycastOrigin.position, new Vector2(-1, 0),10000f, elevatorMask);

        GameObject ob = null;

        if (hit1.rigidbody != null) ob = hit1.rigidbody.gameObject;
        else if (hit2.rigidbody != null) ob = hit2.rigidbody.gameObject;


        return ob;
    }
}
