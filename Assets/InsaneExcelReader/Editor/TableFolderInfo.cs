using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class TableFolderInfo
{
    public void ShowFolderInfo()
    {
        GUILayout.BeginArea(new Rect(10, 45, 800, 35), GUI.skin.button);
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                if (GUILayout.Button("Select Excel Folder", GUI.skin.box, GUILayout.Width(150)))
                {
                    string previousPath = TableGlobal.m_excelPath;
                    TableGlobal.m_excelPath = EditorUtility.OpenFolderPanel("Location of Excel files", "Assets/", "xlsx");

                    if (string.IsNullOrEmpty(TableGlobal.m_excelPath))
                    {
                        TableGlobal.m_excelPath = previousPath;
                        return;
                    }

                    TableGlobal.m_excelPath = TableGlobal.m_excelPath.Substring(TableGlobal.m_excelPath.IndexOf("Asset"));
                    EditorPrefs.SetString("ExcelPath", TableGlobal.m_excelPath);
                    PlayerPrefs.SetString("ExcelPath", TableGlobal.m_excelPath);
                }

                GUILayout.Label(TableGlobal.m_excelPath, GUI.skin.label, GUILayout.Width(500));

                if (GUILayout.Button("Set Default", GUI.skin.box, GUILayout.Width(100)))
                {
                    TableGlobal.m_excelPath = TableGlobal.m_defaultExcelPath;
                    EditorPrefs.SetString("ExcelPath", TableGlobal.m_excelPath);
                    PlayerPrefs.SetString("ExcelPath", TableGlobal.m_excelPath);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }
}
