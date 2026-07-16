using UnityEngine;
using UnityEditor;
using System.Linq;
using OfficeOpenXml;

public class InsaneExcelReader : EditorWindow
{
    public static Vector2 m_scrollPosWorksheet = new Vector2();
    public static Vector2 m_scrollPosTable = new Vector2();

    public static string m_worksheetInformation = string.Empty;

    public static ExcelWorksheet m_selectedExcelWorksheet = null;

    [MenuItem("Window/InsaneExcelReader/ExcelReader")]
    public static void Init()
    {
        EditorWindow excelWindow = (InsaneExcelReader)EditorWindow.GetWindow(typeof(InsaneExcelReader));
#if (UNITY_5_0)
        excelWindow.title = "ExcelReader";
#elif (UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5)
        excelWindow.titleContent = new GUIContent("ExcelReader");
#endif
        excelWindow.maxSize = new Vector2(1000, 720);
        excelWindow.minSize = excelWindow.maxSize;
        excelWindow.maximized = true;

        TableGlobal.m_listWorksheets.Clear();
        InitTool();
    }

    public static void InitTool()
    {
        TableGlobal.GetResourcePath();
        TableGlobal.m_listAllTables.Clear();
        m_worksheetInformation = string.Empty;
        m_selectedExcelWorksheet = null;
        TableGlobal.m_CanShowFolderInfo = false;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(TableLayout.rectShowMenu, GUI.skin.box);
        {
            GUILayout.BeginHorizontal(GUI.skin.button);
            {
                if (GUILayout.Button("Set Excel Folder", GUILayout.Width(TableLayout.m_widthX * 4f)))
                {
                    InitTool();
                    TableGlobal.m_listWorksheets.Clear();
                    TableGlobal.m_CanShowFolderInfo = true;
                }

                if (GUILayout.Button("Load WorkSheets", GUILayout.Width(TableLayout.m_widthX * 4f)))
                {
                    InitTool();
                    TableGlobal.m_listWorksheets = TableGlobal.GetWorkSheets();
                }

                if (GUILayout.Button("Check Tables & Create Reference Class", GUILayout.Width(TableLayout.m_widthX * 8.5f)))
                {
                    InitTool();
                    TableGlobal.m_listWorksheets = TableGlobal.GetWorkSheets();
                    TableGlobal.CheckTables();
                }

                GUILayout.Label(TableGlobal.m_excelPath);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(TableLayout.rectShowWorksheet, GUI.skin.box);
        {
            if (TableGlobal.m_listWorksheets.Count > 0)
            {
                GUILayout.Label(m_worksheetInformation, TableGlobal.StylelabelType());

                m_scrollPosWorksheet = EditorGUILayout.BeginScrollView(m_scrollPosWorksheet, true, false);
                {
                    GUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        foreach (var spreedsheet in TableGlobal.m_listWorksheets)
                        {
                            if (m_selectedExcelWorksheet == spreedsheet)
                                TableGlobal.SetBackgroundColorToSelected();

                            if (GUILayout.Button(spreedsheet.Name, GUI.skin.button, GUILayout.Width(TableLayout.m_widthX * 4f), GUILayout.Height(TableLayout.m_widthX * 0.7f)))
                            {
                                InitTool();
                                m_selectedExcelWorksheet = spreedsheet;
                                m_worksheetInformation = spreedsheet.Name + " worksheet is selected !!";
                                TableGlobal.GetTables(spreedsheet);
                            }

                            TableGlobal.SetBackgroundColorToOriginal();
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(TableLayout.rectShowTable, GUI.skin.box);
        {
            if (TableGlobal.m_listAllTables.Count > 0)
            {
                m_scrollPosTable = EditorGUILayout.BeginScrollView(m_scrollPosTable, true, true);
                {
                    GUILayout.BeginVertical(GUI.skin.box);
                    {
                        int index = 0;

                        GUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            EditorGUILayout.LabelField(@"[Variable Type]", GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                            for (int i = 0; i < TableGlobal.m_listAllTables.Values.ElementAt(0).Count; i++)
                                EditorGUILayout.LabelField(TableGlobal.m_listAllTables.Values.ElementAt(0)[i].m_type, GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4f), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                            GUILayout.FlexibleSpace();
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            EditorGUILayout.LabelField(@"[Variable Name]", GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                            for (int i = 0; i < TableGlobal.m_listAllTables.Values.ElementAt(0).Count; i++)
                                EditorGUILayout.LabelField(TableGlobal.m_listAllTables.Values.ElementAt(0)[i].m_description, GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4f), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                            GUILayout.FlexibleSpace();
                        }
                        GUILayout.EndHorizontal();

                        foreach (var item in TableGlobal.m_listAllTables)
                        {
                            GUILayout.BeginHorizontal(GUI.skin.box);
                            {
                                EditorGUILayout.LabelField(index.ToString(), GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                                foreach (var table in item.Value)
                                    EditorGUILayout.LabelField(table.m_value.ToString(), GUI.skin.box, GUILayout.Width(TableLayout.m_widthX * 4f), GUILayout.Height(TableLayout.m_widthX * 0.7f));

                                GUILayout.FlexibleSpace();

                                index++;
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                    GUILayout.EndVertical();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        GUILayout.EndArea();

        if (TableGlobal.m_CanShowFolderInfo)
            TableGlobal.m_folderInfo.ShowFolderInfo();
    }
}
