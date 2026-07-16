using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DisplayTables : MonoBehaviour
{
    Vector2 m_scrollPosSheet = new Vector2();
    Vector2 m_scrollPosTable = new Vector2();

    Rect m_rectSheet = new Rect(0, 35, 980, 80);
    Rect m_rectTable = new Rect(0, 120, 980, 500);

    bool canShowBibleWise = false;
    bool canShowHackNSlash = false;
    bool canShowNewTestament = false;
    bool canShowOldTestament = false;
    bool canShowPlayer = false;
    bool canShowTestimony = false;
    bool canShowWiseSaying = false;

    void Init()
    {
        canShowBibleWise = false;
        canShowHackNSlash = false;
        canShowNewTestament = false;
        canShowOldTestament = false;
        canShowPlayer = false;
        canShowTestimony = false;
        canShowWiseSaying = false;
    }

    void Start ()
    {
        TestExample();
    }

    void TestExample()
    {
        var BibleWiseAll = BibleWiseTable.GetAll();
        var BibleWiseIndex = BibleWiseTable.GetByIndex(0);
        var BibleWiseKey = BibleWiseTable.GetByKey(1);
        var BibleWiseList = BibleWiseTable.GetAllList();

        Debug.Log(" < ---BibleWiseTable Dictionary --->");

        foreach (var item in BibleWiseAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_BibleMessage = {2},", item.Key ,item.Value.m_ID ,item.Value.m_BibleMessage));

        Debug.Log(" < ---BibleWiseTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_BibleMessage = {1}," ,BibleWiseIndex.m_ID ,BibleWiseIndex.m_BibleMessage));

        Debug.Log(" < ---BibleWiseTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_BibleMessage = {1}," ,BibleWiseKey.m_ID ,BibleWiseKey.m_BibleMessage));

        Debug.Log(" < ---BibleWiseTable List --->");

        foreach (var item in BibleWiseList)
            Debug.Log(string.Format("m_ID = {0},m_BibleMessage = {1}," ,item.m_ID ,item.m_BibleMessage));

        var HackNSlashAll = HackNSlashTable.GetAll();
        var HackNSlashIndex = HackNSlashTable.GetByIndex(0);
        var HackNSlashKey = HackNSlashTable.GetByKey("Game0001");
        var HackNSlashList = HackNSlashTable.GetAllList();

        Debug.Log(" < ---HackNSlashTable Dictionary --->");

        foreach (var item in HackNSlashAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_Confirm = {2},m_Address = {3},m_Content = {4},", item.Key ,item.Value.m_ID ,item.Value.m_Confirm ,item.Value.m_Address ,item.Value.m_Content));

        Debug.Log(" < ---HackNSlashTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_Confirm = {1},m_Address = {2},m_Content = {3}," ,HackNSlashIndex.m_ID ,HackNSlashIndex.m_Confirm ,HackNSlashIndex.m_Address ,HackNSlashIndex.m_Content));

        Debug.Log(" < ---HackNSlashTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_Confirm = {1},m_Address = {2},m_Content = {3}," ,HackNSlashKey.m_ID ,HackNSlashKey.m_Confirm ,HackNSlashKey.m_Address ,HackNSlashKey.m_Content));

        Debug.Log(" < ---HackNSlashTable List --->");

        foreach (var item in HackNSlashList)
            Debug.Log(string.Format("m_ID = {0},m_Confirm = {1},m_Address = {2},m_Content = {3}," ,item.m_ID ,item.m_Confirm ,item.m_Address ,item.m_Content));

        var NewTestamentAll = NewTestamentTable.GetAll();
        var NewTestamentIndex = NewTestamentTable.GetByIndex(0);
        var NewTestamentKey = NewTestamentTable.GetByKey(1);
        var NewTestamentList = NewTestamentTable.GetAllList();

        Debug.Log(" < ---NewTestamentTable Dictionary --->");

        foreach (var item in NewTestamentAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_English = {2},m_Korean = {3},m_Count = {4},m_EnglishTarget = {5},m_NIV = {6},m_KoreanTarget = {7},m_Source = {8},m_EasyBible = {9},m_RevisedHangul = {10},m_RevisedRevision = {11},m_JointTranslation = {12},m_NewStandardTranslation = {13},m_KoreanBible = {14},m_SourceEnglish = {15},m_KJV = {16},m_NewKJV = {17},m_ESV = {18},m_NewRSV = {19},m_NASB = {20},", item.Key ,item.Value.m_ID ,item.Value.m_English ,item.Value.m_Korean ,item.Value.m_Count ,item.Value.m_EnglishTarget ,item.Value.m_NIV ,item.Value.m_KoreanTarget ,item.Value.m_Source ,item.Value.m_EasyBible ,item.Value.m_RevisedHangul ,item.Value.m_RevisedRevision ,item.Value.m_JointTranslation ,item.Value.m_NewStandardTranslation ,item.Value.m_KoreanBible ,item.Value.m_SourceEnglish ,item.Value.m_KJV ,item.Value.m_NewKJV ,item.Value.m_ESV ,item.Value.m_NewRSV ,item.Value.m_NASB));

        Debug.Log(" < ---NewTestamentTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,NewTestamentIndex.m_ID ,NewTestamentIndex.m_English ,NewTestamentIndex.m_Korean ,NewTestamentIndex.m_Count ,NewTestamentIndex.m_EnglishTarget ,NewTestamentIndex.m_NIV ,NewTestamentIndex.m_KoreanTarget ,NewTestamentIndex.m_Source ,NewTestamentIndex.m_EasyBible ,NewTestamentIndex.m_RevisedHangul ,NewTestamentIndex.m_RevisedRevision ,NewTestamentIndex.m_JointTranslation ,NewTestamentIndex.m_NewStandardTranslation ,NewTestamentIndex.m_KoreanBible ,NewTestamentIndex.m_SourceEnglish ,NewTestamentIndex.m_KJV ,NewTestamentIndex.m_NewKJV ,NewTestamentIndex.m_ESV ,NewTestamentIndex.m_NewRSV ,NewTestamentIndex.m_NASB));

        Debug.Log(" < ---NewTestamentTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,NewTestamentKey.m_ID ,NewTestamentKey.m_English ,NewTestamentKey.m_Korean ,NewTestamentKey.m_Count ,NewTestamentKey.m_EnglishTarget ,NewTestamentKey.m_NIV ,NewTestamentKey.m_KoreanTarget ,NewTestamentKey.m_Source ,NewTestamentKey.m_EasyBible ,NewTestamentKey.m_RevisedHangul ,NewTestamentKey.m_RevisedRevision ,NewTestamentKey.m_JointTranslation ,NewTestamentKey.m_NewStandardTranslation ,NewTestamentKey.m_KoreanBible ,NewTestamentKey.m_SourceEnglish ,NewTestamentKey.m_KJV ,NewTestamentKey.m_NewKJV ,NewTestamentKey.m_ESV ,NewTestamentKey.m_NewRSV ,NewTestamentKey.m_NASB));

        Debug.Log(" < ---NewTestamentTable List --->");

        foreach (var item in NewTestamentList)
            Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,item.m_ID ,item.m_English ,item.m_Korean ,item.m_Count ,item.m_EnglishTarget ,item.m_NIV ,item.m_KoreanTarget ,item.m_Source ,item.m_EasyBible ,item.m_RevisedHangul ,item.m_RevisedRevision ,item.m_JointTranslation ,item.m_NewStandardTranslation ,item.m_KoreanBible ,item.m_SourceEnglish ,item.m_KJV ,item.m_NewKJV ,item.m_ESV ,item.m_NewRSV ,item.m_NASB));

        var OldTestamentAll = OldTestamentTable.GetAll();
        var OldTestamentIndex = OldTestamentTable.GetByIndex(0);
        var OldTestamentKey = OldTestamentTable.GetByKey(1);
        var OldTestamentList = OldTestamentTable.GetAllList();

        Debug.Log(" < ---OldTestamentTable Dictionary --->");

        foreach (var item in OldTestamentAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_English = {2},m_Korean = {3},m_Count = {4},m_EnglishTarget = {5},m_NIV = {6},m_KoreanTarget = {7},m_Source = {8},m_EasyBible = {9},m_RevisedHangul = {10},m_RevisedRevision = {11},m_JointTranslation = {12},m_NewStandardTranslation = {13},m_KoreanBible = {14},m_SourceEnglish = {15},m_KJV = {16},m_NewKJV = {17},m_ESV = {18},m_NewRSV = {19},m_NASB = {20},", item.Key ,item.Value.m_ID ,item.Value.m_English ,item.Value.m_Korean ,item.Value.m_Count ,item.Value.m_EnglishTarget ,item.Value.m_NIV ,item.Value.m_KoreanTarget ,item.Value.m_Source ,item.Value.m_EasyBible ,item.Value.m_RevisedHangul ,item.Value.m_RevisedRevision ,item.Value.m_JointTranslation ,item.Value.m_NewStandardTranslation ,item.Value.m_KoreanBible ,item.Value.m_SourceEnglish ,item.Value.m_KJV ,item.Value.m_NewKJV ,item.Value.m_ESV ,item.Value.m_NewRSV ,item.Value.m_NASB));

        Debug.Log(" < ---OldTestamentTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,OldTestamentIndex.m_ID ,OldTestamentIndex.m_English ,OldTestamentIndex.m_Korean ,OldTestamentIndex.m_Count ,OldTestamentIndex.m_EnglishTarget ,OldTestamentIndex.m_NIV ,OldTestamentIndex.m_KoreanTarget ,OldTestamentIndex.m_Source ,OldTestamentIndex.m_EasyBible ,OldTestamentIndex.m_RevisedHangul ,OldTestamentIndex.m_RevisedRevision ,OldTestamentIndex.m_JointTranslation ,OldTestamentIndex.m_NewStandardTranslation ,OldTestamentIndex.m_KoreanBible ,OldTestamentIndex.m_SourceEnglish ,OldTestamentIndex.m_KJV ,OldTestamentIndex.m_NewKJV ,OldTestamentIndex.m_ESV ,OldTestamentIndex.m_NewRSV ,OldTestamentIndex.m_NASB));

        Debug.Log(" < ---OldTestamentTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,OldTestamentKey.m_ID ,OldTestamentKey.m_English ,OldTestamentKey.m_Korean ,OldTestamentKey.m_Count ,OldTestamentKey.m_EnglishTarget ,OldTestamentKey.m_NIV ,OldTestamentKey.m_KoreanTarget ,OldTestamentKey.m_Source ,OldTestamentKey.m_EasyBible ,OldTestamentKey.m_RevisedHangul ,OldTestamentKey.m_RevisedRevision ,OldTestamentKey.m_JointTranslation ,OldTestamentKey.m_NewStandardTranslation ,OldTestamentKey.m_KoreanBible ,OldTestamentKey.m_SourceEnglish ,OldTestamentKey.m_KJV ,OldTestamentKey.m_NewKJV ,OldTestamentKey.m_ESV ,OldTestamentKey.m_NewRSV ,OldTestamentKey.m_NASB));

        Debug.Log(" < ---OldTestamentTable List --->");

        foreach (var item in OldTestamentList)
            Debug.Log(string.Format("m_ID = {0},m_English = {1},m_Korean = {2},m_Count = {3},m_EnglishTarget = {4},m_NIV = {5},m_KoreanTarget = {6},m_Source = {7},m_EasyBible = {8},m_RevisedHangul = {9},m_RevisedRevision = {10},m_JointTranslation = {11},m_NewStandardTranslation = {12},m_KoreanBible = {13},m_SourceEnglish = {14},m_KJV = {15},m_NewKJV = {16},m_ESV = {17},m_NewRSV = {18},m_NASB = {19}," ,item.m_ID ,item.m_English ,item.m_Korean ,item.m_Count ,item.m_EnglishTarget ,item.m_NIV ,item.m_KoreanTarget ,item.m_Source ,item.m_EasyBible ,item.m_RevisedHangul ,item.m_RevisedRevision ,item.m_JointTranslation ,item.m_NewStandardTranslation ,item.m_KoreanBible ,item.m_SourceEnglish ,item.m_KJV ,item.m_NewKJV ,item.m_ESV ,item.m_NewRSV ,item.m_NASB));

        var PlayerAll = PlayerTable.GetAll();
        var PlayerIndex = PlayerTable.GetByIndex(0);
        var PlayerKey = PlayerTable.GetByKey("Bible0001");
        var PlayerList = PlayerTable.GetAllList();

        Debug.Log(" < ---PlayerTable Dictionary --->");

        foreach (var item in PlayerAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_Content = {2},m_Address = {3},m_ETC = {4},", item.Key ,item.Value.m_ID ,item.Value.m_Content ,item.Value.m_Address ,item.Value.m_ETC));

        Debug.Log(" < ---PlayerTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_ETC = {3}," ,PlayerIndex.m_ID ,PlayerIndex.m_Content ,PlayerIndex.m_Address ,PlayerIndex.m_ETC));

        Debug.Log(" < ---PlayerTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_ETC = {3}," ,PlayerKey.m_ID ,PlayerKey.m_Content ,PlayerKey.m_Address ,PlayerKey.m_ETC));

        Debug.Log(" < ---PlayerTable List --->");

        foreach (var item in PlayerList)
            Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_ETC = {3}," ,item.m_ID ,item.m_Content ,item.m_Address ,item.m_ETC));

        var TestimonyAll = TestimonyTable.GetAll();
        var TestimonyIndex = TestimonyTable.GetByIndex(0);
        var TestimonyKey = TestimonyTable.GetByKey("Bible0001");
        var TestimonyList = TestimonyTable.GetAllList();

        Debug.Log(" < ---TestimonyTable Dictionary --->");

        foreach (var item in TestimonyAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_Content = {2},m_Address = {3},m_Confirm = {4},m_ETC = {5},", item.Key ,item.Value.m_ID ,item.Value.m_Content ,item.Value.m_Address ,item.Value.m_Confirm ,item.Value.m_ETC));

        Debug.Log(" < ---TestimonyTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_Confirm = {3},m_ETC = {4}," ,TestimonyIndex.m_ID ,TestimonyIndex.m_Content ,TestimonyIndex.m_Address ,TestimonyIndex.m_Confirm ,TestimonyIndex.m_ETC));

        Debug.Log(" < ---TestimonyTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_Confirm = {3},m_ETC = {4}," ,TestimonyKey.m_ID ,TestimonyKey.m_Content ,TestimonyKey.m_Address ,TestimonyKey.m_Confirm ,TestimonyKey.m_ETC));

        Debug.Log(" < ---TestimonyTable List --->");

        foreach (var item in TestimonyList)
            Debug.Log(string.Format("m_ID = {0},m_Content = {1},m_Address = {2},m_Confirm = {3},m_ETC = {4}," ,item.m_ID ,item.m_Content ,item.m_Address ,item.m_Confirm ,item.m_ETC));

        var WiseSayingAll = WiseSayingTable.GetAll();
        var WiseSayingIndex = WiseSayingTable.GetByIndex(0);
        var WiseSayingKey = WiseSayingTable.GetByKey(1);
        var WiseSayingList = WiseSayingTable.GetAllList();

        Debug.Log(" < ---WiseSayingTable Dictionary --->");

        foreach (var item in WiseSayingAll)
            Debug.Log(string.Format("Key = {0}, m_ID = {1},m_Korean = {2},m_English = {3},", item.Key ,item.Value.m_ID ,item.Value.m_Korean ,item.Value.m_English));

        Debug.Log(" < ---WiseSayingTable Dictionary Index --->");
        Debug.Log(string.Format("m_ID = {0},m_Korean = {1},m_English = {2}," ,WiseSayingIndex.m_ID ,WiseSayingIndex.m_Korean ,WiseSayingIndex.m_English));

        Debug.Log(" < ---WiseSayingTable Dictionary Key --->");
        Debug.Log(string.Format("m_ID = {0},m_Korean = {1},m_English = {2}," ,WiseSayingKey.m_ID ,WiseSayingKey.m_Korean ,WiseSayingKey.m_English));

        Debug.Log(" < ---WiseSayingTable List --->");

        foreach (var item in WiseSayingList)
            Debug.Log(string.Format("m_ID = {0},m_Korean = {1},m_English = {2}," ,item.m_ID ,item.m_Korean ,item.m_English));

    }

    void OnGUI()
    {
        GUILayout.BeginArea(m_rectSheet, GUI.skin.box);
        {
            m_scrollPosSheet = GUILayout.BeginScrollView(m_scrollPosSheet, true, true);
            {
                GUILayout.BeginHorizontal(GUI.skin.button);
                {
                    if (GUILayout.Button("BibleWise", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowBibleWise = true;
                    }

                    if (GUILayout.Button("HackNSlash", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowHackNSlash = true;
                    }

                    if (GUILayout.Button("NewTestament", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowNewTestament = true;
                    }

                    if (GUILayout.Button("OldTestament", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowOldTestament = true;
                    }

                    if (GUILayout.Button("Player", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowPlayer = true;
                    }

                    if (GUILayout.Button("Testimony", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowTestimony = true;
                    }

                    if (GUILayout.Button("WiseSaying", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        Init();
                        canShowWiseSaying = true;
                    }

                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();

        if (canShowBibleWise)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (BibleWiseTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in BibleWiseTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("BibleMessage: " + info.Value.m_BibleMessage.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowHackNSlash)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (HackNSlashTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in HackNSlashTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Confirm: " + info.Value.m_Confirm.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Address: " + info.Value.m_Address.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Content: " + info.Value.m_Content.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowNewTestament)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (NewTestamentTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in NewTestamentTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("English: " + info.Value.m_English.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Korean: " + info.Value.m_Korean.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Count: " + info.Value.m_Count.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("EnglishTarget: " + info.Value.m_EnglishTarget.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NIV: " + info.Value.m_NIV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KoreanTarget: " + info.Value.m_KoreanTarget.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Source: " + info.Value.m_Source.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("EasyBible: " + info.Value.m_EasyBible.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("RevisedHangul: " + info.Value.m_RevisedHangul.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("RevisedRevision: " + info.Value.m_RevisedRevision.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("JointTranslation: " + info.Value.m_JointTranslation.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewStandardTranslation: " + info.Value.m_NewStandardTranslation.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KoreanBible: " + info.Value.m_KoreanBible.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("SourceEnglish: " + info.Value.m_SourceEnglish.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KJV: " + info.Value.m_KJV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewKJV: " + info.Value.m_NewKJV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("ESV: " + info.Value.m_ESV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewRSV: " + info.Value.m_NewRSV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NASB: " + info.Value.m_NASB.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowOldTestament)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (OldTestamentTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in OldTestamentTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("English: " + info.Value.m_English.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Korean: " + info.Value.m_Korean.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Count: " + info.Value.m_Count.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("EnglishTarget: " + info.Value.m_EnglishTarget.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NIV: " + info.Value.m_NIV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KoreanTarget: " + info.Value.m_KoreanTarget.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Source: " + info.Value.m_Source.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("EasyBible: " + info.Value.m_EasyBible.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("RevisedHangul: " + info.Value.m_RevisedHangul.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("RevisedRevision: " + info.Value.m_RevisedRevision.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("JointTranslation: " + info.Value.m_JointTranslation.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewStandardTranslation: " + info.Value.m_NewStandardTranslation.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KoreanBible: " + info.Value.m_KoreanBible.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("SourceEnglish: " + info.Value.m_SourceEnglish.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("KJV: " + info.Value.m_KJV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewKJV: " + info.Value.m_NewKJV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("ESV: " + info.Value.m_ESV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NewRSV: " + info.Value.m_NewRSV.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("NASB: " + info.Value.m_NASB.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowPlayer)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (PlayerTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in PlayerTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Content: " + info.Value.m_Content.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Address: " + info.Value.m_Address.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("ETC: " + info.Value.m_ETC.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowTestimony)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (TestimonyTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in TestimonyTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Content: " + info.Value.m_Content.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Address: " + info.Value.m_Address.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Confirm: " + info.Value.m_Confirm.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("ETC: " + info.Value.m_ETC.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

        if (canShowWiseSaying)
        {
            GUILayout.BeginArea(m_rectTable, GUI.skin.box);
            {
                if (WiseSayingTable.GetAll().Count > 0)
                {
                    m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);
                    {
                        GUILayout.BeginVertical(GUI.skin.box);
                        {
                            foreach (var info in WiseSayingTable.GetAll())
                            {
                                GUILayout.BeginHorizontal(GUI.skin.box);
                                {
                                    GUILayout.TextField("Key: " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));
                                    GUILayout.TextField("", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));
                                    GUILayout.TextField("ID: " + info.Value.m_ID.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("Korean: " + info.Value.m_Korean.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                    GUILayout.TextField("English: " + info.Value.m_English.ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndArea();
        }

    }
}

