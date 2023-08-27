using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public enum UI { Menu, Setting, Create, Save, Load, Volume}

    [SerializeField] private GameObject m_MenuUI;
    [SerializeField] private GameObject m_CreateUI;


    [SerializeField] private TextMeshProUGUI m_nameChar;
    [SerializeField] private TextMeshProUGUI m_WarningName;
    [SerializeField] private InputField m_fieldName;
    [SerializeField] private TextMeshProUGUI m_WarningNull;

    private SaveLoadManager m_SLManager;
    private VolumeManager m_VolumeManager;
    private string m_curName;

    private void Start()
    {
        m_SLManager = GetComponent<SaveLoadManager>();
        m_VolumeManager = FindObjectOfType<VolumeManager>();
        SetUI(UI.Menu);
    }

    private void SetUI(UI ui)
    {
        m_MenuUI.SetActive(ui == UI.Menu);
        m_CreateUI.SetActive(ui == UI.Create);
    }

    public void SetUIMenu()
    {
        m_SLManager.SetUI_Save(false);
        m_SLManager.SetUI_Load(false);
        m_VolumeManager.SetVolumeUI(false);
        SetUI(UI.Menu);
    }

    public void SetNameCharMenu()
    {
        m_nameChar.text = m_curName;
    }

    public string getCurNameHero()
    {
        return m_curName;
    }

    public void setCurNameHero(string name)
    {
        m_curName = name;
    }

    public void btnCreateChar_Pressed()
    {
        if (!string.IsNullOrEmpty(m_fieldName.text))
        {
            m_curName = m_fieldName.text;
            SetUI(UI.Save);
            m_SLManager.SetUI_Save(true);
        }
        else
            StartCoroutine(NameCharNotFound(UI.Create));
    }


    public void btnPlay_Pressed()
    {
        if (!string.IsNullOrEmpty(m_nameChar.text))
            SceneManager.LoadScene("Home");
        else
            StartCoroutine(NameCharNotFound(UI.Menu));
    }


    private IEnumerator NameCharNotFound(UI ui)
    {
        if (ui == UI.Menu)
            m_WarningName.gameObject.SetActive(true);
        else if (ui == UI.Create)
            m_WarningNull.gameObject.SetActive(true);

        for (int i = 0; i < 10; i++)
            yield return new WaitForSeconds(0.1f);

        m_WarningName.gameObject.SetActive(false);
        m_WarningNull.gameObject.SetActive(false);
    }



    public void btnCancel_Pressed()
    {
        SetUIMenu();
    }

    public void btnLoad_Pressed()
    {
        SetUI(UI.Load);
        m_SLManager.SetUI_Load(true);
    }

    public void btnBack_Pressed()
    {
        SetUIMenu();
    }

    public void btnExit_Pressed()
    {
        Application.Quit();
    }
    public void btnCreateUI_Pressed()
    {
        SetUI(UI.Create);
    }

    public void btnVolume_Pressed()
    {
        m_VolumeManager.SetVolumeUI(true);
        SetUI(UI.Volume);
    }
}
