using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject RotationPoint;
    public GameObject Head;

    private Rigidbody _rigidBody;
    private ScreenShake _screenShake;
    private PlayerStatus _playerStatus;
    private PlayerParameters _parameters;
    private TrailRenderer _trailRenderer;

    [SerializeField] private float _maxSpeed = 50f;
    [SerializeField] private float _dodgeCooldown = 1f;
    [SerializeField] private float _dodgeSpeed = 2.5f;
    [SerializeField] private float _dodgeDuration = 0.3f;
    [SerializeField] private float _bendingPower = 1f;
    [SerializeField] private float _bendingPowerDecrease = 0.5f;
    [SerializeField] private int _receivingDamage = 5;

    [Range(1, 2)]
    [SerializeField] private int _playerID = 1;
    public bool CANTMOVE = false;

    public float IMMORTALITY_TIME = 2;

    private bool _dodging;
    private float _timer;

    private Attack _attackScript;

    private Vector3 _walkVelocity;
    private Vector3 _flyVelocity;

    private Vector2 _lateVelocity;
    private Vector2 _normal;

    private bool _immortal = false;

    void Start()
    {
        _attackScript = RotationPoint.GetComponent<Attack>();
        _rigidBody = GetComponent<Rigidbody>();
        _screenShake = Camera.main.GetComponent<ScreenShake>();
        _playerStatus = GetComponent<PlayerStatus>();
        _parameters = transform.root.GetComponent<PlayerParameters>();
        _trailRenderer = GetComponent<TrailRenderer>();

        _lateVelocity = Vector2.zero;
        _normal = Vector2.zero;

        TrailTransparency(0f);
    }

    void Update()
    {
        if (CANTMOVE || _dodging)
        {
            return;
        }
        _walkVelocity = Vector3.zero;

        Vector2 stickInput = new Vector2(Input.GetAxis("LeftHorizontal_P" + _parameters.PLAYER), Input.GetAxis("LeftVertical_P" + _parameters.PLAYER));
        if (stickInput.magnitude < 0.25f)
            stickInput = Vector2.zero;

        _walkVelocity.x = stickInput.x;
        _walkVelocity.z = stickInput.y;

        _walkVelocity = _walkVelocity.normalized;
        _walkVelocity = _walkVelocity * _parameters.SPEED;

        if (_walkVelocity.magnitude > _parameters.SPEED)
        {
            _walkVelocity = _walkVelocity.normalized;
            _walkVelocity = _walkVelocity * _parameters.SPEED;
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }     
    }

    private void FixedUpdate()
    {
        //Player flying
        if (_rigidBody.velocity.magnitude > _parameters.SPEED && _walkVelocity.magnitude <= 0f)
        {
            _rigidBody.velocity *= 0.99f;
        }
        //Player flying and controling
        else if (_rigidBody.velocity.magnitude > _parameters.SPEED && !_dodging)
        {
            _rigidBody.velocity *= 0.99f;
            _rigidBody.velocity = Vector3.RotateTowards(_rigidBody.velocity, _walkVelocity, Time.deltaTime * (_bendingPower - _bendingPowerDecrease / _maxSpeed * _rigidBody.velocity.magnitude), 0);
        }
        //Player stops moving
        else if (_walkVelocity.magnitude <= 0f && _rigidBody.velocity.magnitude > 0.01f)
        {
            _rigidBody.velocity *= 0.9f;
        }
        //Player walks around
        else if (_rigidBody.velocity.magnitude <= _parameters.SPEED)
        {
            _rigidBody.velocity = _walkVelocity;

            if ((Input.GetButton("Fire2") || Input.GetButton("LeftBumper_P" + _parameters.PLAYER)))
            {
                dodge();
            }
        }

        if (_rigidBody.velocity.magnitude > _maxSpeed)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
        }

        _lateVelocity.x = _rigidBody.velocity.x;
        _lateVelocity.y = _rigidBody.velocity.z;

    }

    private void reflect(Vector3 pNormal)
    {
        if (_dodging)
            return;

        if (_rigidBody.velocity.magnitude > _parameters.SPEED)
        {
            StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.1f));
        }
        _normal.Set(pNormal.x, pNormal.z);
        Vector2 _velocity = Vector2.Reflect(_lateVelocity, _normal);
        Vector3 vector = new Vector3(_velocity.x, 0, _velocity.y);
        _rigidBody.velocity = vector;
    }

    private void dodge()
    {
        if (_immortal)
        {
            return;
        }

        if (_timer <= 0 && _rigidBody.velocity.magnitude > 0f)
        {
            _timer = _dodgeCooldown;
            _rigidBody.velocity = _walkVelocity * 2.5f;
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            Head.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            gameObject.layer = 10;
            _dodging = true;
            Invoke("StopDodge", _dodgeDuration);
            TrailTransparency(1f);
            _trailRenderer.time = 5f;
            FMODUnity.RuntimeManager.PlayOneShot("event:/dodge");
        }
    }

    private void StopDodge()
    {
        _dodging = false;
        TrailTransparency(0f);
        _trailRenderer.time = 0;
        _rigidBody.velocity = _walkVelocity;
        normalColours();
        gameObject.layer = 9;
    }

    private void OnCollisionEnter(Collision collision)
    {
        reflect(collision.contacts[0].normal);
    }

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag.ToUpper() == "WEAPON" && pOther.transform.root != transform.root && !_dodging)
        {
            if (_immortal)
            {
                return;
            }

            StartCoroutine(_screenShake.Shake(0.2f, 0.1f + _playerStatus.GetDamage() / 300f)); //shake the screen depending on damage
            _playerStatus.IncreaseDamage(pOther.transform.root.GetComponent<PlayerParameters>().ATTACK);
            GetComponent<MeshRenderer>().material.color = new Color(0.8f, 0, 0, 1);
            Head.GetComponent<MeshRenderer>().material.color = new Color(0.8f, 0, 0, 1);
            Invoke("normalColours", 0.1f);
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + transform.root.name + " gets hit");

            _attackScript.SetCooldown();
            pOther.gameObject.SetActive(false);
            RotationPoint.transform.GetChild(0).gameObject.SetActive(false);

            Vector3 delta = transform.position - pOther.gameObject.transform.position;
            delta = delta.normalized * pOther.GetComponentInParent<Attack>().Force;
            delta.y = 0;
            _rigidBody.velocity = delta * _playerStatus.GetDamage();
            _rigidBody.velocity -= _rigidBody.velocity * (_parameters.DAMAGE_ABSORPTION / 100);
        }
    }

    private void normalColours()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
        Head.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
    }


    public bool GetDodging()
    {
        return _dodging;
    }

    private void TrailTransparency(float pTransparency)
    {
        _trailRenderer.startColor = new Color(1, 1, 1, pTransparency);
        _trailRenderer.endColor = new Color(1, 1, 1, pTransparency);
    }

    public bool Immortal
    {
        get
        {
            return _immortal;
        }
    }

    public void startImmortality()
    {
        _immortal = true;


        _rigidBody.velocity = Vector3.zero;
        _walkVelocity = Vector3.zero;
        _flyVelocity = Vector3.zero;

        GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0.8f, 1);
        Head.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0.8f, 1);

        Invoke("endImmortality", IMMORTALITY_TIME);
    }

    private void endImmortality()
    {
        normalColours();

        _immortal = false;
    }

    public Vector3 WalkVelocity
    {
        get
        {
            return _walkVelocity;
        }
        set
        {
            _walkVelocity = value;
        }
    }

    public Vector3 FlyVelocity
    {
        get
        {
            return _flyVelocity;
        }
        set
        {
            _flyVelocity = value;
        }
    }
}
