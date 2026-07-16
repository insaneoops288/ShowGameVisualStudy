using UnityEngine;

public class BibleItemTestimony : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public HackNSlashInfo m_TestimonyInfo;

    public int m_intIndex;
    public int m_intMax;

    public UILabel m_UILabelBibleName;

    

    public void SetInfo()
    {
        m_UILabelBibleName.text = m_intIndex + "/" + m_intMax + " (" + m_TestimonyInfo.m_ID + ")" + ". " + m_TestimonyInfo.m_Content;
        // m_UILabelBibleName.text = m_intIndex + "/" + m_intMax + ". " + m_TestimonyInfo.m_Content;
    }

    public void OnClicked()
    {
        BibleTestimony.Get.InitButton();
        GetComponentInChildren<UISprite>().color = new Color(0, 1, 0, 1);

        Application.OpenURL(m_TestimonyInfo.m_Address);
    }
}
