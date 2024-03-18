using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewTypes;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float _forceMultiplyer;
    [SerializeField] private float _spearAngle;
    private InputSystem _inputSystem;
    private TouchInfo _touchInfo;
    private Vector3 _target;
    public TMP_Text timerText;
    public TMP_Text tiempoFinal;
    public TMP_Text tiempoRecord;
    public float timer;
    public GameObject panelGameOver;
    public GameObject panelPalmao;

    public TMP_Text stikersPalmaos;


    void Awake()
    {
        _inputSystem = new InputSystem();
    }
    void Start()
    {
        Seguir();
        panelGameOver.SetActive(false);
        panelPalmao.SetActive(false);
        tiempoRecord.text = PlayerPrefs.GetFloat("recordtiempo", 0).ToString("F2");
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "" + timer.ToString("F2");

        if (timer > 180f)
        {
            Palmao();
        }
        GameOver();

        _touchInfo = _inputSystem.ReadInput();

        if (_touchInfo.Phase == TouchPhase.Began)
        {
            _target = new Vector3(_touchInfo.StartPosWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.StartPosWorld.z);
            //Vector3 target = new Vector3(_touchInfo.StartPosWorld.x, _touchInfo.StartPosWorld.y, _touchInfo.StartPosWorld.z);
            Spear.Instance.LookAt(_target);
        }
        if (_touchInfo.Phase == TouchPhase.Moved || _touchInfo.Phase == TouchPhase.Stationary)
        {
            _target = new Vector3(_touchInfo.DirectionWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.DirectionWorld.z);
            // Vector3 target = new Vector3(_touchInfo.DirectionWorld.x, _touchInfo.DirectionWorld.y, _touchInfo.DirectionWorld.z);
            Spear.Instance.LookAt(_target);
        }
        if (_touchInfo.Phase == TouchPhase.Ended)
        {
            _target = new Vector3(_touchInfo.DirectionWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.DirectionWorld.z);
            //Vector3 target = new Vector3(_touchInfo.DirectionWorld.x, _touchInfo.DirectionWorld.y, _touchInfo.DirectionWorld.z);
            Spear.Instance.Throw(_target);
        }


    }

    public void GameOver()
    {
        if (Spear.Instance.matados == 0)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f;
            tiempoFinal.text = timer.ToString("F2");
            if (timer < PlayerPrefs.GetFloat("recordtiempo"))
            {
                PlayerPrefs.SetFloat("recordtiempo", timer);
                tiempoRecord.text = PlayerPrefs.GetFloat("recordtiempo").ToString("F2");
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Seguir()
    {
        Time.timeScale = 1f;
    }
    public void Palmao()
    {
        panelPalmao.SetActive(true);
        stikersPalmaos.text = (10 - Spear.Instance.matados).ToString();
    }
    public void Jugar(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Seguir();
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void BorraRecord()
    {
        PlayerPrefs.DeleteAll();
    }
}
