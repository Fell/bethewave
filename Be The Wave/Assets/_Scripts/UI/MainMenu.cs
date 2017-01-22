using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image loadBar;

    public AudioClip m_selecSound;
    public AudioClip m_openSound;

    public MenuPoint[] m_menuPoints;

    public MenuPoint[] m_LevelPoints;

    public float m_distance = 5;

    public float m_rotSpeed = 3;

    public float m_heightOffset = 1f;

    public float m_foodScale = 1.0f;

    public float selecTime = 0.5f;

    private MenuPoint[] m_children = new MenuPoint[0];

    AudioSource aSource;

    float angle;

    bool changeSelection = false;

    bool hold = false;

    int selectedVal = 0;
    int oldSelected = 0;

    float timer = 0;
    float useTimer = 0;


    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = m_openSound;
        aSource.Play();
        CreateMenuPoints();
    }

    void Update()
    {
        //Activates the selected Action when space is hold for a moment
        if (!hold && !changeSelection && Input.GetKey(KeyCode.Space))
        {
            useTimer += Time.deltaTime;
            loadBar.fillAmount = useTimer / selecTime;
            if (useTimer >= selecTime)
            {
                hold = true;
                aSource.clip = m_children[selectedVal].m_clip;
                aSource.Play();
                useTimer = 0;
                m_children[selectedVal].MenuAction();
            }
        }
        else
        {
            useTimer = 0;
            loadBar.fillAmount = 0;
        }

        //Changes the selected MenuPoint
        if (!hold && !changeSelection && Input.GetKeyUp( KeyCode.Space ))
        {
            aSource.clip = m_selecSound;
            aSource.Play();
            changeSelection = true;
            oldSelected = selectedVal;
            selectedVal++;
            if (selectedVal >= m_children.Length)
            {
                selectedVal = 0;
            }
        }

        //Rotates the menu plate
        else if (changeSelection)
        {
            transform.eulerAngles = new Vector3(0,Mathf.LerpAngle(oldSelected * -angle, selectedVal * -angle, timer),0);

            timer += Time.deltaTime * m_rotSpeed;
            if (timer >= 1.0f)
            {
                timer = 0;
                changeSelection = false;
            }
        }

        if (Input.GetKeyUp( KeyCode.Space ))
        {
            hold = false;
        }
    }

    public void CreateMenuPoints()
    {

        foreach (MenuPoint point in m_children)
        {
            Destroy(point.gameObject);
        }

        if (m_menuPoints.Length <= 0)
            return;

        angle = 360 / m_menuPoints.Length;

        //Creates All MenuPoints as Children
        m_children = new MenuPoint[m_menuPoints.Length];

        for (int i = 0; i < m_menuPoints.Length; i++)
        {
            Vector3 dir = new Vector3(Mathf.Cos(2 * Mathf.PI * i / m_menuPoints.Length), 0, -Mathf.Sin(2 * Mathf.PI * i / m_menuPoints.Length));
            dir = dir * m_distance;
            m_children[i] = Instantiate(m_menuPoints[i].gameObject, this.transform.position + dir + Vector3.up * m_heightOffset, Quaternion.identity, this.transform).GetComponent<MenuPoint>();
            m_children[i].transform.localScale = new Vector3(m_foodScale, m_foodScale, m_foodScale);
        }
    }

    public void createLevelPoints()
    {
        
        foreach (MenuPoint point in m_children)
        {
            Destroy(point.gameObject);
        }

        if (m_LevelPoints.Length <= 0)
            return;

        angle = 360 / (GameManager.Instance.m_levels.Count + 1);

        m_children = new MenuPoint[GameManager.Instance.m_levels.Count + 1];

        for (int i = 0; i < GameManager.Instance.m_levels.Count + 1; i++)
        {
            Vector3 dir = new Vector3(Mathf.Cos(2 * Mathf.PI * i / (GameManager.Instance.m_levels.Count + 1)), 0, -Mathf.Sin(2 * Mathf.PI * i / (GameManager.Instance.m_levels.Count + 1)));
            dir = dir * m_distance;
            if (i == 0)
            {
                m_children[i] = Instantiate(m_LevelPoints[0].gameObject, this.transform.position + dir + Vector3.up * m_heightOffset, Quaternion.identity, this.transform).GetComponent<MenuPoint>();
                m_children[i].transform.localScale = new Vector3(m_foodScale, m_foodScale, m_foodScale);
            }
            else
            {
                m_children[i] = Instantiate(m_LevelPoints[1].gameObject, this.transform.position + dir + Vector3.up * m_heightOffset, Quaternion.identity, this.transform).GetComponent<MenuPoint>();
                m_children[i].transform.localScale = new Vector3(m_foodScale, m_foodScale, m_foodScale);
                m_children[i].GetComponent<MPStartGame>().setLevelID(i-1);
            }
        }
    }

}
