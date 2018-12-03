using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject RotationPoint;
    public GameObject Head;

    public GameObject SmokeParticle;

    private GameObject _dirtParticle;
    private GameObject _dashParticle;

    private Rigidbody _rigidBody;
    private ScreenShake _screenShake;
    private PlayerStatus _playerStatus;
    private PlayerParameters _parameters;

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

    [Header("The amount of seconds that the immortality lasts")]
    public float IMMORTALITY_TIME = 2;

    [Header("How much seconds are between turning transparent and normal when immortal")]
    public float transparency_interval = 0.1f;

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
        _dirtParticle = transform.Find("DirtTrail").gameObject;
        _dashParticle = transform.Find("DashTrail").gameObject;

        _dashParticle.SetActive(false);
        _dirtParticle.SetActive(true);

        _lateVelocity = Vector2.zero;
        _normal = Vector2.zero;
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

        if (_rigidBody.velocity.magnitude > _parameters.SPEED + 0.2f)
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
            _dashParticle.SetActive(true);
            _dirtParticle.SetActive(false);
            _timer = _dodgeCooldown;
            _rigidBody.velocity = _walkVelocity * 2.5f;
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            Head.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            gameObject.layer = 10;
            _dodging = true;
            Invoke("StopDodge", _dodgeDuration);
            FMODUnity.RuntimeManager.PlayOneShot("event:/dodge");
        }
    }

    private void StopDodge()
    {
        _dashParticle.SetActive(false);
        _dirtParticle.SetActive(true);

        _dodging = false;
        _rigidBody.velocity = _walkVelocity;
        normalColours();
        gameObject.layer = 9;
    }

    private void OnCollisionEnter(Collision collision)
    {
        reflect(collision.contacts[0].normal);

        if (_rigidBody.velocity.magnitude > _parameters.SPEED + 0.2f || _dodging)
            Instantiate(SmokeParticle, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

        if (collision.gameObject.tag.ToUpper() == "PLAYER")
        {
            Vector3 point = collision.contacts[0].point;
            Vector3 position = new Vector3(point.x, 0.1f, point.z);
            Instantiate(SmokeParticle, position, Quaternion.LookRotation(Vector3.up));
        }
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

        GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
        Head.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
        toggleTransparant();
        Invoke("endImmortality", IMMORTALITY_TIME);
    }

    private void endImmortality()
    {
        normalColours();

        _immortal = false;
    }

    private void toggleNormal()
    {
        if (_immortal)
        {
            normalColours();
            Invoke("toggleTransparant", 0.1f);
        }
        else
        {
            return;
        }
    }

    private void toggleTransparant()
    {
        if (_immortal)
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            Head.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
            Invoke("toggleNormal", 0.1f);
        }
        else
        {
            return;
        }
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
