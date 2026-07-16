using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BibleWiseInfo
{
    public int m_ID { get; private set; }
    public string m_BibleMessage { get; private set; }

    public void SetID(int ID) { m_ID = ID; }
    public void SetBibleMessage(string BibleMessage) { m_BibleMessage = BibleMessage; }
}

public class HackNSlashInfo
{
    public string m_ID { get; private set; }
    public string m_Confirm { get; private set; }
    public string m_Address { get; private set; }
    public string m_Content { get; private set; }

    public void SetID(string ID) { m_ID = ID; }
    public void SetConfirm(string Confirm) { m_Confirm = Confirm; }
    public void SetAddress(string Address) { m_Address = Address; }
    public void SetContent(string Content) { m_Content = Content; }
}

public class NewTestamentInfo
{
    public int m_ID { get; private set; }
    public string m_English { get; private set; }
    public string m_Korean { get; private set; }
    public int m_Count { get; private set; }
    public string m_EnglishTarget { get; private set; }
    public string m_NIV { get; private set; }
    public string m_KoreanTarget { get; private set; }
    public string m_Source { get; private set; }
    public string m_EasyBible { get; private set; }
    public string m_RevisedHangul { get; private set; }
    public string m_RevisedRevision { get; private set; }
    public string m_JointTranslation { get; private set; }
    public string m_NewStandardTranslation { get; private set; }
    public string m_KoreanBible { get; private set; }
    public string m_SourceEnglish { get; private set; }
    public string m_KJV { get; private set; }
    public string m_NewKJV { get; private set; }
    public string m_ESV { get; private set; }
    public string m_NewRSV { get; private set; }
    public string m_NASB { get; private set; }

    public void SetID(int ID) { m_ID = ID; }
    public void SetEnglish(string English) { m_English = English; }
    public void SetKorean(string Korean) { m_Korean = Korean; }
    public void SetCount(int Count) { m_Count = Count; }
    public void SetEnglishTarget(string EnglishTarget) { m_EnglishTarget = EnglishTarget; }
    public void SetNIV(string NIV) { m_NIV = NIV; }
    public void SetKoreanTarget(string KoreanTarget) { m_KoreanTarget = KoreanTarget; }
    public void SetSource(string Source) { m_Source = Source; }
    public void SetEasyBible(string EasyBible) { m_EasyBible = EasyBible; }
    public void SetRevisedHangul(string RevisedHangul) { m_RevisedHangul = RevisedHangul; }
    public void SetRevisedRevision(string RevisedRevision) { m_RevisedRevision = RevisedRevision; }
    public void SetJointTranslation(string JointTranslation) { m_JointTranslation = JointTranslation; }
    public void SetNewStandardTranslation(string NewStandardTranslation) { m_NewStandardTranslation = NewStandardTranslation; }
    public void SetKoreanBible(string KoreanBible) { m_KoreanBible = KoreanBible; }
    public void SetSourceEnglish(string SourceEnglish) { m_SourceEnglish = SourceEnglish; }
    public void SetKJV(string KJV) { m_KJV = KJV; }
    public void SetNewKJV(string NewKJV) { m_NewKJV = NewKJV; }
    public void SetESV(string ESV) { m_ESV = ESV; }
    public void SetNewRSV(string NewRSV) { m_NewRSV = NewRSV; }
    public void SetNASB(string NASB) { m_NASB = NASB; }
}

public class OldTestamentInfo
{
    public int m_ID { get; private set; }
    public string m_English { get; private set; }
    public string m_Korean { get; private set; }
    public int m_Count { get; private set; }
    public string m_EnglishTarget { get; private set; }
    public string m_NIV { get; private set; }
    public string m_KoreanTarget { get; private set; }
    public string m_Source { get; private set; }
    public string m_EasyBible { get; private set; }
    public string m_RevisedHangul { get; private set; }
    public string m_RevisedRevision { get; private set; }
    public string m_JointTranslation { get; private set; }
    public string m_NewStandardTranslation { get; private set; }
    public string m_KoreanBible { get; private set; }
    public string m_SourceEnglish { get; private set; }
    public string m_KJV { get; private set; }
    public string m_NewKJV { get; private set; }
    public string m_ESV { get; private set; }
    public string m_NewRSV { get; private set; }
    public string m_NASB { get; private set; }

    public void SetID(int ID) { m_ID = ID; }
    public void SetEnglish(string English) { m_English = English; }
    public void SetKorean(string Korean) { m_Korean = Korean; }
    public void SetCount(int Count) { m_Count = Count; }
    public void SetEnglishTarget(string EnglishTarget) { m_EnglishTarget = EnglishTarget; }
    public void SetNIV(string NIV) { m_NIV = NIV; }
    public void SetKoreanTarget(string KoreanTarget) { m_KoreanTarget = KoreanTarget; }
    public void SetSource(string Source) { m_Source = Source; }
    public void SetEasyBible(string EasyBible) { m_EasyBible = EasyBible; }
    public void SetRevisedHangul(string RevisedHangul) { m_RevisedHangul = RevisedHangul; }
    public void SetRevisedRevision(string RevisedRevision) { m_RevisedRevision = RevisedRevision; }
    public void SetJointTranslation(string JointTranslation) { m_JointTranslation = JointTranslation; }
    public void SetNewStandardTranslation(string NewStandardTranslation) { m_NewStandardTranslation = NewStandardTranslation; }
    public void SetKoreanBible(string KoreanBible) { m_KoreanBible = KoreanBible; }
    public void SetSourceEnglish(string SourceEnglish) { m_SourceEnglish = SourceEnglish; }
    public void SetKJV(string KJV) { m_KJV = KJV; }
    public void SetNewKJV(string NewKJV) { m_NewKJV = NewKJV; }
    public void SetESV(string ESV) { m_ESV = ESV; }
    public void SetNewRSV(string NewRSV) { m_NewRSV = NewRSV; }
    public void SetNASB(string NASB) { m_NASB = NASB; }
}

public class PlayerInfo
{
    public string m_ID { get; private set; }
    public string m_Content { get; private set; }
    public string m_Address { get; private set; }
    public string m_ETC { get; private set; }

    public void SetID(string ID) { m_ID = ID; }
    public void SetContent(string Content) { m_Content = Content; }
    public void SetAddress(string Address) { m_Address = Address; }
    public void SetETC(string ETC) { m_ETC = ETC; }
}

public class TestimonyInfo
{
    public string m_ID { get; private set; }
    public string m_Content { get; private set; }
    public string m_Address { get; private set; }
    public string m_Confirm { get; private set; }
    public string m_ETC { get; private set; }

    public void SetID(string ID) { m_ID = ID; }
    public void SetContent(string Content) { m_Content = Content; }
    public void SetAddress(string Address) { m_Address = Address; }
    public void SetConfirm(string Confirm) { m_Confirm = Confirm; }
    public void SetETC(string ETC) { m_ETC = ETC; }
}

public class WiseSayingInfo
{
    public int m_ID { get; private set; }
    public string m_Korean { get; private set; }
    public string m_English { get; private set; }

    public void SetID(int ID) { m_ID = ID; }
    public void SetKorean(string Korean) { m_Korean = Korean; }
    public void SetEnglish(string English) { m_English = English; }
}

public class BibleWiseTable
{
    private static Dictionary<int, BibleWiseInfo> Table = new Dictionary<int, BibleWiseInfo>();

    public static Dictionary<int, BibleWiseInfo> GetAll()
    {
        return Table;
    }

    public static BibleWiseInfo GetByKey(int key)
    {
        BibleWiseInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static BibleWiseInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<BibleWiseInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public BibleWiseTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/BibleWise") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            int key = binaryReader.ReadInt32();

            BibleWiseInfo info = new BibleWiseInfo();
            info.SetID(binaryReader.ReadInt32());
            info.SetBibleMessage(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class HackNSlashTable
{
    private static Dictionary<string, HackNSlashInfo> Table = new Dictionary<string, HackNSlashInfo>();

    public static Dictionary<string, HackNSlashInfo> GetAll()
    {
        return Table;
    }

    public static HackNSlashInfo GetByKey(string key)
    {
        HackNSlashInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static HackNSlashInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<HackNSlashInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public HackNSlashTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/HackNSlash") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            string key = binaryReader.ReadString();

            HackNSlashInfo info = new HackNSlashInfo();
            info.SetID(binaryReader.ReadString());
            info.SetConfirm(binaryReader.ReadString());
            info.SetAddress(binaryReader.ReadString());
            info.SetContent(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class NewTestamentTable
{
    private static Dictionary<int, NewTestamentInfo> Table = new Dictionary<int, NewTestamentInfo>();

    public static Dictionary<int, NewTestamentInfo> GetAll()
    {
        return Table;
    }

    public static NewTestamentInfo GetByKey(int key)
    {
        NewTestamentInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static NewTestamentInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<NewTestamentInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public NewTestamentTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/NewTestament") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            int key = binaryReader.ReadInt32();

            NewTestamentInfo info = new NewTestamentInfo();
            info.SetID(binaryReader.ReadInt32());
            info.SetEnglish(binaryReader.ReadString());
            info.SetKorean(binaryReader.ReadString());
            info.SetCount(binaryReader.ReadInt32());
            info.SetEnglishTarget(binaryReader.ReadString());
            info.SetNIV(binaryReader.ReadString());
            info.SetKoreanTarget(binaryReader.ReadString());
            info.SetSource(binaryReader.ReadString());
            info.SetEasyBible(binaryReader.ReadString());
            info.SetRevisedHangul(binaryReader.ReadString());
            info.SetRevisedRevision(binaryReader.ReadString());
            info.SetJointTranslation(binaryReader.ReadString());
            info.SetNewStandardTranslation(binaryReader.ReadString());
            info.SetKoreanBible(binaryReader.ReadString());
            info.SetSourceEnglish(binaryReader.ReadString());
            info.SetKJV(binaryReader.ReadString());
            info.SetNewKJV(binaryReader.ReadString());
            info.SetESV(binaryReader.ReadString());
            info.SetNewRSV(binaryReader.ReadString());
            info.SetNASB(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class OldTestamentTable
{
    private static Dictionary<int, OldTestamentInfo> Table = new Dictionary<int, OldTestamentInfo>();

    public static Dictionary<int, OldTestamentInfo> GetAll()
    {
        return Table;
    }

    public static OldTestamentInfo GetByKey(int key)
    {
        OldTestamentInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static OldTestamentInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<OldTestamentInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public OldTestamentTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/OldTestament") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            int key = binaryReader.ReadInt32();

            OldTestamentInfo info = new OldTestamentInfo();
            info.SetID(binaryReader.ReadInt32());
            info.SetEnglish(binaryReader.ReadString());
            info.SetKorean(binaryReader.ReadString());
            info.SetCount(binaryReader.ReadInt32());
            info.SetEnglishTarget(binaryReader.ReadString());
            info.SetNIV(binaryReader.ReadString());
            info.SetKoreanTarget(binaryReader.ReadString());
            info.SetSource(binaryReader.ReadString());
            info.SetEasyBible(binaryReader.ReadString());
            info.SetRevisedHangul(binaryReader.ReadString());
            info.SetRevisedRevision(binaryReader.ReadString());
            info.SetJointTranslation(binaryReader.ReadString());
            info.SetNewStandardTranslation(binaryReader.ReadString());
            info.SetKoreanBible(binaryReader.ReadString());
            info.SetSourceEnglish(binaryReader.ReadString());
            info.SetKJV(binaryReader.ReadString());
            info.SetNewKJV(binaryReader.ReadString());
            info.SetESV(binaryReader.ReadString());
            info.SetNewRSV(binaryReader.ReadString());
            info.SetNASB(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class PlayerTable
{
    private static Dictionary<string, PlayerInfo> Table = new Dictionary<string, PlayerInfo>();

    public static Dictionary<string, PlayerInfo> GetAll()
    {
        return Table;
    }

    public static PlayerInfo GetByKey(string key)
    {
        PlayerInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static PlayerInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<PlayerInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public PlayerTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/Player") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            string key = binaryReader.ReadString();

            PlayerInfo info = new PlayerInfo();
            info.SetID(binaryReader.ReadString());
            info.SetContent(binaryReader.ReadString());
            info.SetAddress(binaryReader.ReadString());
            info.SetETC(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class TestimonyTable
{
    private static Dictionary<string, TestimonyInfo> Table = new Dictionary<string, TestimonyInfo>();

    public static Dictionary<string, TestimonyInfo> GetAll()
    {
        return Table;
    }

    public static TestimonyInfo GetByKey(string key)
    {
        TestimonyInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static TestimonyInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<TestimonyInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public TestimonyTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/Testimony") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            string key = binaryReader.ReadString();

            TestimonyInfo info = new TestimonyInfo();
            info.SetID(binaryReader.ReadString());
            info.SetContent(binaryReader.ReadString());
            info.SetAddress(binaryReader.ReadString());
            info.SetConfirm(binaryReader.ReadString());
            info.SetETC(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}

public class WiseSayingTable
{
    private static Dictionary<int, WiseSayingInfo> Table = new Dictionary<int, WiseSayingInfo>();

    public static Dictionary<int, WiseSayingInfo> GetAll()
    {
        return Table;
    }

    public static WiseSayingInfo GetByKey(int key)
    {
        WiseSayingInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

    public static WiseSayingInfo GetByIndex(int index)
    {
        return Table.Values.ElementAt(index);
    }

    public static List<WiseSayingInfo> GetAllList()
    {
        return Table.Values.ToList();
    }

    public WiseSayingTable()
    {
        InitTable();
    }

    private void InitTable()
    {
        TextAsset textAsset = Resources.Load("Tables/WiseSaying") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tableCount = binaryReader.ReadInt32();

        for( int i = 0; i < tableCount; ++i)
        {
            int key = binaryReader.ReadInt32();

            WiseSayingInfo info = new WiseSayingInfo();
            info.SetID(binaryReader.ReadInt32());
            info.SetKorean(binaryReader.ReadString());
            info.SetEnglish(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }
}


public class Tables : MonoBehaviour
{
    public BibleWiseTable BibleWise = null;
    public HackNSlashTable HackNSlash = null;
    public NewTestamentTable NewTestament = null;
    public OldTestamentTable OldTestament = null;
    public PlayerTable Player = null;
    public TestimonyTable Testimony = null;
    public WiseSayingTable WiseSaying = null;

    private static Tables instance = null;

    public static Tables Instance
    {
        get { return instance; }
    }

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;

            BibleWise = new BibleWiseTable();
            HackNSlash = new HackNSlashTable();
            NewTestament = new NewTestamentTable();
            OldTestament = new OldTestamentTable();
            Player = new PlayerTable();
            Testimony = new TestimonyTable();
            WiseSaying = new WiseSayingTable();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }
}

