using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float _attackCooldown = 0.5f;
    private float _timer = 0;
    public float Force = 10;

    private Movement _movement;

    private PlayerParameters _parameters;

    //private Movement _playerMovement;

    void Start()
    {
        //_playerMovement = transform.root.GetChild(0).GetComponent<Movement>();
        _parameters = transform.root.GetComponent<PlayerParameters>();
        _movement = transform.root.GetComponentInChildren<Movement>();
    }

    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }


        if (Pause.Paused /* || _playerMovement.GetDodging()*/)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("RightBumper_P" + _parameters.PLAYER))
        {
            if (_movement.Immortal)
            {
                return;
            }

            SetCooldown();
            transform.GetChild(0).gameObject.SetActive(true);

            //TODO: MAKE IT DO IT FOR EVERYONE
            AnimationScript animation = transform.root.GetComponentInChildren<AnimationScript>();
            animation.PlayAttackAnimation();
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/attack", gameObject);

            Invoke("DisableHitBox", 0.25f);
        }
    }

    void DisableHitBox()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetCooldown()
    {
        _timer = _attackCooldown;
    }
}
