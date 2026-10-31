using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BibleTestimony : MonoBehaviour
{
    private static BibleTestimony m_Instance = null;

    public static BibleTestimony Get { get { return m_Instance; } set { m_Instance = value; } }


    public UILabel LabelVersion;

    public List<Transform> InitItem = new List<Transform>();

    public List<HackNSlashInfo> TestimonyInfos = new List<HackNSlashInfo>();

    public List<HackNSlashInfo> CollectTestimonyInfos = new List<HackNSlashInfo>();

    public BibleItemTestimony m_BibleItemTestimony;

    public List<BibleItemTestimony> m_BibleItemTestimonies = new List<BibleItemTestimony>();

    public List<PlayerInfo> TestimonyPrayerInfos = new List<PlayerInfo>();

    public List<PlayerInfo> CollectTestimonyPrayerInfos = new List<PlayerInfo>();

    public UIScrollView m_ScrollViewTestimony;
    public UIGrid m_UIGridTestimony;

    public UILabel m_LabelJustTest;

    public void ButtonExitPanelTestimony()
    {
        Application.Quit();
    }

    public void Awake()
    {
        m_Instance = this; 
    }

    void Start()
    {
        TestimonyInfos.Clear();

        TestimonyInfos = HackNSlashTable.GetAllList();

        string VersionInfo = "Game Visual 6000.6.7f1";
        m_LabelJustTest.text = VersionInfo;

        m_BibleItemTestimonies = m_UIGridTestimony.GetComponentsInChildren<BibleItemTestimony>().ToList();

        for (int i = 0; i < m_BibleItemTestimonies.Count; i++)
        {
            m_BibleItemTestimonies[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < m_BibleItemTestimonies.Count; i++)
        {
            m_BibleItemTestimonies[i].m_UILabelBibleName.text = "";
        }

        for (int i = 0; i < m_BibleItemTestimonies.Count; i++)
        {
            Buttons.Add(m_BibleItemTestimonies[i].GetComponentInChildren<UISprite>());
        }

        InitButton();
    }

    public void Init()
    {

    }

    public void ButtonTestimonyBible()
    {
        Init();
        InitButton();

        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "Excellent")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void SpawnItem()
    {
        for (int i = 0; i < m_BibleItemTestimonies.Count; i++)
        {
            m_BibleItemTestimonies[i].gameObject.SetActive(true);
        }

        for (int i = CollectTestimonyInfos.Count; i < m_BibleItemTestimonies.Count; i++)
        {
            m_BibleItemTestimonies[i].gameObject.SetActive(false);
        }

        CollectTestimonyInfos = (from prod in CollectTestimonyInfos
                                 orderby prod.m_Content
                                 select prod).ToList();

        for (int i = 0; i < CollectTestimonyInfos.Count; i++)
        {
            m_BibleItemTestimonies[i].m_intIndex = i + 1;
            m_BibleItemTestimonies[i].m_intMax = CollectTestimonyInfos.Count;
            m_BibleItemTestimonies[i].m_TestimonyInfo = CollectTestimonyInfos[i];
            m_BibleItemTestimonies[i].SetInfo();
        }

        m_UIGridTestimony.repositionNow = true;
        m_UIGridTestimony.Reposition();
        m_ScrollViewTestimony.ResetPosition();
    }

    public void ButtonTestimonyGrace()
    {
        Init();
        InitButton();

        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "AAA")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void ButtonTestimonyPrayer()
    {
        Init();
        InitButton();

        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "ActionRPG")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void ButtonTestimonySalvation()
    {
        Init();
        InitButton();

        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "SOULS-LIKE")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void ButtonTestimonyHolySpirit()
    {
        Init();
        InitButton();
        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "Boss")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void ButtonTestimonyHeaven()
    {
        Init();
        InitButton();
        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "AI")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public void ButtonTestimonyPenitence()
    {
        Init();
        InitButton();
        CollectTestimonyInfos.Clear();

        for (int i = 0; i < TestimonyInfos.Count; i++)
        {
            if (TestimonyInfos[i].m_Confirm == "Phantom")
            {
                CollectTestimonyInfos.Add(TestimonyInfos[i]);
            }
        }

        SpawnItem();
    }

    public List<UISprite> Buttons = new List<UISprite>();
    public void InitButton()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].color = new Color(1, 1, 1, 1);
        }
    }
}
