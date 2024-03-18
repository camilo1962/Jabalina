using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spear : MonoBehaviour
{
    [SerializeField] private float _startAngle;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _timeToReset;
    private Rigidbody _rigidBody;
    private Transform _trajectoryStart;
    private GameObject _stickJoint;
    private Trajectory _trajectory;
    private bool _isWaitingThrow;
    private Vector3 _startPos;
    private Quaternion _startRot;
    private int score;
    public int matados = 10;
    public TMP_Text scoreText, recordText, scorefinal, recordfinal, matadosText;

    public GameObject sticmanNegro;
    public GameObject sticmanBlanco;
    public GameObject sticmanRojo;
    public GameObject sticmanAmarillo;
    public GameObject sticmanVerde;
    public GameObject sticmanCian;
    public GameObject sticmanMagenta;
    public GameObject sticmanAzul;
    public GameObject sticmanGris;
    public GameObject sticmanNaranja;

    public AudioSource lanzamiento;
    public AudioSource clavar;
    public AudioSource matar;

    public GameObject negro, blanco, rojo, amarillo1, verde, cian, magenta, azul1, gris1, naranja1;
   
    public Color naranja = new Color(255f, 165f, 0f);
    public Color gris= new Color(123f, 123f, 123f);
    public Color amarillo = new Color(255f, 255f, 0f);
    public Color azul = new Color(0f, 0f, 255f);
    public float StartAngle
    {
        get => _startAngle;
    }

    public static Spear Instance;
    private void Awake()
    {
        Instance = this;
        _rigidBody = GetComponent<Rigidbody>();
        _trajectoryStart = GameObject.Find("trajectory_start").transform;
        _stickJoint = GameObject.Find("stick_joint");
        _trajectory = GetComponent<Trajectory>();
        _isWaitingThrow = true;

        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
                StopPhysics();
                clavar.Play();
                Invoke("Reset", _timeToReset);
                break;
            case "Reset":
                Reset();
                break;
            case "Ground":
                Attach(other.gameObject);
                break;          
            case "Object":
                Attach(other.gameObject);
                score++;
                scoreText.text = score.ToString();
                scorefinal.text = scoreText.text;
                matar.Play();
                break;
            default:
                Debug.LogError("No collision for " + other.gameObject.tag);
                break;
        }
        
    }
    public void BorraNegro()
    {
        negro.SetActive(true);
        matados--;
      
        sticmanNegro.SetActive(false);
    }     
    public void BorraBlanco()
    {
        blanco.SetActive(true);
        matados--;
     
        sticmanBlanco.SetActive(false);
    }            
    public void BorraRojo()
    {
        rojo.SetActive(true);
        matados--;
       
        sticmanRojo.SetActive(false);
    } 
    public void BorraAmarillo()
    {
        amarillo1.SetActive(true);
        matados--;
       
        sticmanAmarillo.SetActive(false);
    } 
    public void BorraVerde()
    {
        verde.SetActive(true);
        matados--;
     
        sticmanVerde.SetActive(false);
    }
    public void BorraCian()
    {
        cian.SetActive(true);
        matados--;
       
        sticmanCian.SetActive(false);
    }
    public void BorraMagenta()
    {
        magenta.SetActive(true);
        matados--;
       
        sticmanMagenta.SetActive(false);
    }
    public void BorraAzul()
    {
        azul1.SetActive(true);
        matados--;
        sticmanAzul.SetActive(false);                
    }    
    public void BorraGris()
    {
        gris1.SetActive(true);
        matados--;
     
        sticmanGris.SetActive(false);
    }
    public void BorraNaranja()
    {
        naranja1.SetActive(true);
        matados--;
       
        sticmanNaranja.SetActive(false);
    }


    private void Attach(GameObject objectToAttach)
    {
        FixedJoint joint = _stickJoint.AddComponent<FixedJoint>(); 
        joint.anchor = _stickJoint.transform.position; 
        joint.connectedBody = objectToAttach.GetComponent<Rigidbody>(); 
        joint.enableCollision = false; 
        Stickman stickman = objectToAttach.GetComponentInParent<Stickman>();
        stickman.IgnoreCollisions(gameObject.GetComponentInChildren<Collider>());
        stickman.ClearConstraints();
        objectToAttach.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //score++;
        //scoreText.text = score.ToString();
        //scorefinal.text = scoreText.text;
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.black)
        {
            BorraNegro();
           
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.white)
        {
            BorraBlanco();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.red)
        {
            BorraRojo();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == amarillo)
        {
            BorraAmarillo();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.green)
        {
            BorraVerde();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.cyan)
        {
            BorraCian();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == Color.magenta)
        {
            BorraMagenta();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == azul) 
        {
            BorraAzul();
        }
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == gris)
        {
            BorraGris();
        }
        
        if (objectToAttach.GetComponentInParent<Stickman>().transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color == naranja)
        {
            BorraNaranja();
        }



    }


    public void LookAt(Vector3 target)
    {
        if (_isWaitingThrow)
        {
            transform.LookAt(target, Vector3.up);
            _trajectory.Draw(target * _throwForce, _rigidBody, _trajectoryStart.position);
        }
    }

    public void Throw(Vector3 target)
    {
        if (_isWaitingThrow)
        {
            _rigidBody.useGravity = true;
            _rigidBody.AddForce(target * _throwForce, ForceMode.Force);
            _trajectory.Clear();
            lanzamiento.Play();
            _isWaitingThrow = false;
           
        }
    }

    private void StopPhysics()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.useGravity = false;
    }

    private void Reset()
    {
        StopPhysics();
        FixedJoint[] joints = _stickJoint.GetComponents<FixedJoint>(); 
        if (joints.Length > 0)
        {
            foreach(FixedJoint joint in joints)
            {
                if(joint.connectedBody != null)
                {
                    Stickman stickman = joint.connectedBody.gameObject.GetComponentInParent<Stickman>();
                    if (stickman != null) stickman.ClearConstraints();
                    joint.connectedBody = null; 
                }
            }
        }

        transform.position = _startPos;
        transform.rotation = _startRot;
        _isWaitingThrow = true;
    }
    public void Start()
    {
        recordText.text = PlayerPrefs.GetInt("record").ToString();
        negro.gameObject.SetActive(false);
        blanco.gameObject.SetActive(false);
        rojo.gameObject.SetActive(false);
        amarillo1.gameObject.SetActive(false);
        verde.gameObject.SetActive(false);
        cian.gameObject.SetActive(false);
        magenta.gameObject.SetActive(false);
        azul1.gameObject.SetActive(false);
        gris1.gameObject.SetActive(false);
        naranja1.gameObject.SetActive(false);
    }
    public void Update()
    {
       
        matadosText.text = matados.ToString();
        if (score> PlayerPrefs.GetInt("record"))
        {
            PlayerPrefs.SetInt("record", score);
            recordText.text = PlayerPrefs.GetInt("record",score).ToString();
            
        }
    }
}
