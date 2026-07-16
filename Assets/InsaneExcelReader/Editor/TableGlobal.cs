using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using OfficeOpenXml;
using System.Text;
using System;

public class TableGlobal
{
    public static string m_defaultExcelPath = @"Assets/InsaneExcelReader/Editor/ExcelFiles/";
    public static string m_excelPath = string.Empty;
    public static string m_scriptPath = @"Assets/InsaneExcelReader/Scripts/";
    public static string m_TablesPath = @"Assets/InsaneExcelReader/Resources/Tables/";

    public static List<ExcelWorksheet> m_listWorksheets = new List<ExcelWorksheet>();
    public static Dictionary<string, List<TableInfo>> m_listAllTables = new Dictionary<string, List<TableInfo>>();
    public static Dictionary<ExcelWorksheet, Dictionary<string, List<TableInfo>>> m_tables =
        new Dictionary<ExcelWorksheet, Dictionary<string, List<TableInfo>>>();

    public static TableFolderInfo m_folderInfo = new TableFolderInfo();
    public static bool m_CanShowFolderInfo = false;

    public static List<int> m_shouldBeSkipedList = new List<int>();

    public static void GetResourcePath()
    {
        m_excelPath = string.IsNullOrEmpty(EditorPrefs.GetString("ExcelPath")) ?
            m_defaultExcelPath : EditorPrefs.GetString("ExcelPath");
        EditorPrefs.SetString("ExcelPath", m_excelPath);
    }

    public static void CheckTable(Dictionary<string, List<TableInfo>> tables)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string tempString = string.Empty;

        List<string> collectDescription = new List<string>();
        List<string> collectKey = new List<string>();

        switch (tables.ElementAt(0).Value[0].m_type)
        {
            case "int":
            case "string":
                break;
            default:
                stringBuilder.AppendLine(SetString("Type shuld be int or string for Dictionary key : " + tables.ElementAt(0).Value[0].m_type));
                break;
        }

        for (int i = 0; i < tables.ElementAt(0).Value.Count; i++)
        {
            switch (tables.ElementAt(0).Value[i].m_type.ToLower())
            {
                case "int":
                case "string":
                case "float":
                case "bool":
                case "byte":
                case "long":
                    break;
                default:
                    stringBuilder.AppendLine(SetString("Index = " + i + ", Type = " + tables.ElementAt(0).Value[i].m_type +
                        " : Type is not supported, Please spell check or check type !!"));
                    break;
            }
        }

        for (int i = 0; i < tables.ElementAt(0).Value.Count; i++)
        {
            if (collectDescription.Contains(tables.ElementAt(0).Value[i].m_description))
                stringBuilder.AppendLine(SetString("Same Variable Name is already existed : " + tables.ElementAt(0).Value[i].m_description));
            else
                collectDescription.Add(tables.ElementAt(0).Value[i].m_description);
        }

        for (int i = 0; i < tables.Count; i++)
        {
            for (int j = 0; j < tables.ElementAt(i).Value.Count; j++)
            {
                switch (tables.ElementAt(i).Value[j].m_type.ToLower())
                {
                    case "int":
                        {
                            int value;

                            if (!int.TryParse(tables.ElementAt(i).Value[j].m_value, out value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    case "string":
                        {
                            if (string.IsNullOrEmpty(tables.ElementAt(i).Value[j].m_value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    case "float":
                        {
                            float value;

                            if (!float.TryParse(tables.ElementAt(i).Value[j].m_value, out value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    case "bool":
                        {
                            bool value;

                            if (!bool.TryParse(tables.ElementAt(i).Value[j].m_value, out value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    case "byte":
                        {
                            byte value;

                            if (!byte.TryParse(tables.ElementAt(i).Value[j].m_value, out value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    case "long":
                        {
                            long value;

                            if (!long.TryParse(tables.ElementAt(i).Value[j].m_value, out value))
                                stringBuilder.AppendLine(SetString(
                                    "Type = " + tables.ElementAt(i).Value[j].m_type +
                                    ", Value = " + tables.ElementAt(i).Value[j].m_value +
                                    " : Mismatch"));
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (!string.IsNullOrEmpty(stringBuilder.ToString()))
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"Please check error list in Assets/InsaneExcelReader/Scripts/CheckList.txt"));
            writeStringToFile(stringBuilder.ToString(), "CheckList", ".txt");
            EditorUtility.DisplayDialog("Warning", stringBuilder.ToString(), "Close");
        }
    }

    public static void CheckTables()
    {
        StringBuilder stringBuilder = new StringBuilder();
        string tempString = string.Empty;

        m_tables.Clear();

        foreach (var sheet in m_listWorksheets)
        {
            m_listAllTables = new Dictionary<string, List<TableInfo>>();

            GetTables(sheet);

            if (m_listAllTables.Count == 0)
                return;

            m_tables.Add(sheet, m_listAllTables);
        }

        foreach (var table in m_tables)
        {
            List<string> collectDescription = new List<string>();
            List<string> collectKey = new List<string>();

            switch (table.Value.ElementAt(0).Value[0].m_type)
            {
                case "int":
                case "string":
                    break;
                default:
                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name +
                        ", Type shuld be int or string for Dictionary key : " + table.Value.ElementAt(0).Value[0].m_type));
                    break;
            }

            for (int i = 0; i < table.Value.ElementAt(0).Value.Count; i++)
            {
                switch (table.Value.ElementAt(0).Value[i].m_type.ToLower())
                {
                    case "int":
                    case "string":
                    case "float":
                    case "bool":
                    case "byte":
                    case "long":
                        break;
                    default:
                        stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name +
                            ", Index = " + i + ", Type = " + table.Value.ElementAt(0).Value[i].m_type +
                            " : Type is not supported, Please spell check or check type !!"));
                        break;
                }
            }

            for (int i = 0; i < table.Value.ElementAt(0).Value.Count; i++)
            {
                if (collectDescription.Contains(table.Value.ElementAt(0).Value[i].m_description))
                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name +
                        ", Same Variable Name is already existed : " + table.Value.ElementAt(0).Value[i].m_description));
                else
                    collectDescription.Add(table.Value.ElementAt(0).Value[i].m_description);
            }

            for (int i = 0; i < table.Value.Count; i++)
            {
                for (int j = 0; j < table.Value.ElementAt(i).Value.Count; j++)
                {
                    switch (table.Value.ElementAt(i).Value[j].m_type.ToLower())
                    {
                        case "int":
                            {
                                int value;

                                if (!int.TryParse(table.Value.ElementAt(i).Value[j].m_value, out value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        case "string":
                            {
                                if (string.IsNullOrEmpty(table.Value.ElementAt(i).Value[j].m_value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        case "float":
                            {
                                float value;

                                if (!float.TryParse(table.Value.ElementAt(i).Value[j].m_value, out value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        case "bool":
                            {
                                bool value;

                                if (!bool.TryParse(table.Value.ElementAt(i).Value[j].m_value, out value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        case "byte":
                            {
                                byte value;

                                if (!byte.TryParse(table.Value.ElementAt(i).Value[j].m_value, out value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        case "long":
                            {
                                long value;

                                if (!long.TryParse(table.Value.ElementAt(i).Value[j].m_value, out value))
                                    stringBuilder.AppendLine(SetString("ExcelWorksheet = " + table.Key.Name + ", Index = " + i +
                                        ", Position = " + table.Value.ElementAt(i).Value[j].m_index +
                                        ", Type = " + table.Value.ElementAt(i).Value[j].m_type +
                                        ", Value = " + table.Value.ElementAt(i).Value[j].m_value +
                                        " : Mismatch between type and value."));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        writeStringToFile(stringBuilder.ToString(), "CheckList", ".txt");

        if (string.IsNullOrEmpty(stringBuilder.ToString()))
            WriteReferenceScript();
        else
        {
            m_listWorksheets = new List<ExcelWorksheet>();
            m_listAllTables = new Dictionary<string, List<TableInfo>>();
            EditorUtility.DisplayDialog("Warning", @"Please check error list in Assets/InsaneExcelReader/Scripts/CheckList.txt", "Close");
        }
    }

    public static void SaveCharacter(GameObject target)
    {
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void CreateBinary()
    {
        foreach (var table in m_tables)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            binaryWriter.Write(table.Value.Count);

            for (int i = 0; i < table.Value.Count; i++)
            {
                for (int j = 0; j < table.Value.ElementAt(i).Value.Count; j++)
                {
                    switch (table.Value.ElementAt(i).Value[j].m_type)
                    {
                        case "int":
                            binaryWriter.Write(Int32.Parse(table.Value.ElementAt(i).Value[j].m_value));
                            break;
                        case "string":
                            binaryWriter.Write(table.Value.ElementAt(i).Value[j].m_value);
                            break;
                        case "float":
                            binaryWriter.Write(float.Parse(table.Value.ElementAt(i).Value[j].m_value));
                            break;
                        case "bool":
                            binaryWriter.Write(bool.Parse(table.Value.ElementAt(i).Value[j].m_value));
                            break;
                        case "byte":
                            binaryWriter.Write(byte.Parse(table.Value.ElementAt(i).Value[j].m_value));
                            break;
                        case "long":
                            binaryWriter.Write(long.Parse(table.Value.ElementAt(i).Value[j].m_value));
                            break;
                        default:
                            binaryWriter.Write(table.Value.ElementAt(i).Value[j].m_value);
                            break;
                    }
                }
            }

            byte[] collectData = memoryStream.ToArray();

            FileStream fileStream = new FileStream(m_TablesPath + table.Key.Name + ".bytes", FileMode.Create);
            fileStream.Write(collectData, 0, collectData.Length);
            fileStream.Close();
            binaryWriter.Close();
        }
    }

    public static void CreateTables()
    {
        string tempString = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(SetString(@"using UnityEngine;"));
        stringBuilder.AppendLine(SetString(@"using System.Collections.Generic;"));
        stringBuilder.AppendLine(SetString(@"using System.IO;"));
        stringBuilder.AppendLine(SetString(@"using System.Linq;"));
        stringBuilder.AppendLine();

        foreach (var table in m_tables)
        {
            stringBuilder.AppendLine(SetString("public class " + table.Key.Name + "Info"));
            stringBuilder.AppendLine(SetString("{"));

            for (int i = 1; i < table.Value.ElementAt(0).Value.Count; i++)
                stringBuilder.AppendLine(SetString("public " + table.Value.ElementAt(0).Value[i].m_type +
                    " m_" + table.Value.ElementAt(0).Value[i].m_description + " { get; private set; }", 1));

            stringBuilder.AppendLine();

            for (int i = 1; i < table.Value.ElementAt(0).Value.Count; i++)
                stringBuilder.AppendLine(SetString("public void Set" + table.Value.ElementAt(0).Value[i].m_description +
                    "(" + table.Value.ElementAt(0).Value[i].m_type + " " +
                    table.Value.ElementAt(0).Value[i].m_description + ") { m_" +
                    table.Value.ElementAt(0).Value[i].m_description + " = " +
                    table.Value.ElementAt(0).Value[i].m_description + "; }", 1));

            stringBuilder.AppendLine(SetString("}"));
            stringBuilder.AppendLine();
        }

        foreach (var table in m_tables)
        {
            stringBuilder.AppendLine(SetString("public class " + table.Key.Name + "Table"));
            stringBuilder.AppendLine(SetString("{"));
            stringBuilder.AppendLine(SetString(@"private static Dictionary<" + table.Value.ElementAt(0).Value[0].m_type + ", " + table.Key.Name +
                @"Info> " + "Table" + @" = new Dictionary<" + table.Value.ElementAt(0).Value[0].m_type + ", " + table.Key.Name + @"Info>();", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"public static Dictionary<" + table.Value.ElementAt(0).Value[0].m_type + ", " + table.Key.Name +
                @"Info> " + "GetAll()", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(@"return Table;", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"public static " + table.Key.Name + @"Info " + @"GetByKey(" + table.Value.ElementAt(0).Value[0].m_type +
                " key)", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(table.Key.Name + @"Info value;", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"if (Table.TryGetValue(key, out value))", 2));
            stringBuilder.AppendLine(SetString(@"return value;", 3));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"return null;", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"public static " + table.Key.Name + @"Info " + @"GetByIndex(" + "int index)", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(@"return Table.Values.ElementAt(index);", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"public static " + @"List<" + table.Key.Name + @"Info> " + @"GetAllList()", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(@"return Table.Values.ToList();", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("public " + table.Key.Name + "Table()", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(@"InitTable();", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"private void InitTable()", 1));
            stringBuilder.AppendLine(SetString("{", 1));
            stringBuilder.AppendLine(SetString(@"TextAsset textAsset = Resources.Load(""Tables/" + table.Key.Name + "\") as TextAsset;", 2));
            stringBuilder.AppendLine(SetString("MemoryStream memoryStream = new MemoryStream(textAsset.bytes);", 2));
            stringBuilder.AppendLine(SetString("BinaryReader binaryReader = new BinaryReader(memoryStream);", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("int tableCount = binaryReader.ReadInt32();", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("for( int i = 0; i < tableCount; ++i)", 2));
            stringBuilder.AppendLine(SetString("{", 2));

            switch (table.Value.ElementAt(0).Value[0].m_type)
            {
                case "int":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadInt32();", 3));
                    break;
                case "string":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadString();", 3));
                    break;
                case "float":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadSingle();", 3));
                    break;
                case "bool":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadBoolean();", 3));
                    break;
                case "byte":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadByte();", 3));
                    break;
                case "long":
                    stringBuilder.AppendLine(SetString(table.Value.ElementAt(0).Value[0].m_type + " key = binaryReader.ReadInt64();", 3));
                    break;
                default:
                    break;
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(table.Key.Name + "Info info = new " + table.Key.Name + "Info();", 3));

            for (int i = 1; i < table.Value.ElementAt(0).Value.Count; i++)
            {
                switch (table.Value.ElementAt(0).Value[i].m_type)
                {
                    case "int":
                        {
                            int value;

                            if (int.TryParse(table.Value.ElementAt(0).Value[i].m_value, out value))
                                stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                                    "(binaryReader.ReadInt32());", 3));
                        }
                        break;
                    case "string":
                        stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                            "(binaryReader.ReadString());", 3));
                        break;
                    case "float":
                        {
                            float value;

                            if (float.TryParse(table.Value.ElementAt(0).Value[i].m_value, out value))
                                stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                                    "(binaryReader.ReadSingle());", 3));
                        }
                        break;
                    case "bool":
                        {
                            bool value;

                            if (bool.TryParse(table.Value.ElementAt(0).Value[i].m_value, out value))
                                stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                                    "(binaryReader.ReadBoolean());", 3));
                        }
                        break;
                    case "byte":
                        {
                            byte value;

                            if (byte.TryParse(table.Value.ElementAt(0).Value[i].m_value, out value))
                                stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                                    "(binaryReader.ReadByte());", 3));
                        }
                        break;
                    case "long":
                        {
                            long value;

                            if (long.TryParse(table.Value.ElementAt(0).Value[i].m_value, out value))
                                stringBuilder.AppendLine(SetString(@"info.Set" + table.Value.ElementAt(0).Value[i].m_description +
                                    "(binaryReader.ReadInt64());", 3));
                        }
                        break;
                    default:
                        break;
                }
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString(@"Table.Add(key, info);", 3));
            stringBuilder.AppendLine(SetString("}", 2));
            stringBuilder.AppendLine(SetString("}", 1));
            stringBuilder.AppendLine(SetString("}"));
            stringBuilder.AppendLine();
        }

        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("public class " + "Tables" + " : MonoBehaviour"));
        stringBuilder.AppendLine(SetString("{"));

        foreach (var table in m_tables)
            stringBuilder.AppendLine(SetString("public " + table.Key.Name + "Table " + table.Key.Name + " = null;", 1));

        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("private static Tables instance = null;", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("public static Tables Instance", 1));
        stringBuilder.AppendLine(SetString("{", 1));
        stringBuilder.AppendLine(SetString("get { return instance; }", 2));
        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString(@"void Awake() ", 1));
        stringBuilder.AppendLine(SetString("{", 1));
        stringBuilder.AppendLine(SetString("if (instance == null)", 2));
        stringBuilder.AppendLine(SetString("{", 2));
        stringBuilder.AppendLine(SetString("instance = this;", 3));
        stringBuilder.AppendLine();

        foreach (var table in m_tables)
            stringBuilder.AppendLine(SetString(table.Key.Name + " = new " + table.Key.Name + "Table();", 3));

        stringBuilder.AppendLine(SetString("}", 2));
        stringBuilder.AppendLine(SetString("else if (instance != this)", 2));
        stringBuilder.AppendLine(SetString("{", 2));
        stringBuilder.AppendLine(SetString("Destroy(gameObject);", 3));
        stringBuilder.AppendLine(SetString("}", 2));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("DontDestroyOnLoad(gameObject);", 2));
        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString(@"void Start()", 1));
        stringBuilder.AppendLine(SetString("{", 1));
        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine(SetString("}"));

        writeStringToFile(stringBuilder.ToString(), "Tables", ".cs");
    }

    public static void CreateDisplayTables()
    {
        string tempString = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(SetString(@"using UnityEngine;"));
        stringBuilder.AppendLine(SetString(@"using System.Collections.Generic;"));
        stringBuilder.AppendLine(SetString(@"using System.Collections;"));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("public class DisplayTables : MonoBehaviour"));
        stringBuilder.AppendLine(SetString("{"));
        stringBuilder.AppendLine(SetString("Vector2 m_scrollPosSheet = new Vector2();", 1));
        stringBuilder.AppendLine(SetString("Vector2 m_scrollPosTable = new Vector2();", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("Rect m_rectSheet = new Rect(0, 35, 980, 80);", 1));
        stringBuilder.AppendLine(SetString("Rect m_rectTable = new Rect(0, 120, 980, 500);", 1));
        stringBuilder.AppendLine();

        foreach (var table in m_tables)
            stringBuilder.AppendLine(SetString("bool canShow" + table.Key.Name + " = false;", 1));

        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("void Init()", 1));
        stringBuilder.AppendLine(SetString("{", 1));

        foreach (var table in m_tables)
            stringBuilder.AppendLine(SetString("canShow" + table.Key.Name + " = false;", 2));

        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("void Start ()", 1));
        stringBuilder.AppendLine(SetString("{", 1));
        stringBuilder.AppendLine(SetString("TestExample();", 2));
        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("void TestExample()", 1));
        stringBuilder.AppendLine(SetString("{", 1));

        foreach (var table in m_tables)
        {
            stringBuilder.AppendLine(SetString("var " + table.Key.Name + "All = " + table.Key.Name + "Table.GetAll();", 2));
            stringBuilder.AppendLine(SetString("var " + table.Key.Name + "Index = " + table.Key.Name + "Table.GetByIndex(0);", 2));

            if (table.Value.ElementAt(0).Value.ElementAt(0).m_type == "int")
                stringBuilder.AppendLine(SetString("var " + table.Key.Name + "Key = " + table.Key.Name + "Table.GetByKey(" +
                    table.Value.ElementAt(0).Value.ElementAt(0).m_value + ");", 2));
            else if (table.Value.ElementAt(0).Value.ElementAt(0).m_type == "string")
                stringBuilder.AppendLine(SetString("var " + table.Key.Name + "Key = " + table.Key.Name + "Table.GetByKey(\"" +
                    table.Value.ElementAt(0).Value.ElementAt(0).m_value + "\");", 2));

            stringBuilder.AppendLine(SetString("var " + table.Key.Name + "List = " + table.Key.Name + "Table.GetAllList();", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("Debug.Log(\" < ---" + table.Key.Name + "Table Dictionary --->\");", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("foreach (var item in " + table.Key.Name + "All)", 2));
            stringBuilder.AppendLine(SetString(GetValueStringType(table.Value.ElementAt(0).Value, table.Key.Name, 0), 3));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("Debug.Log(\" < ---" + table.Key.Name + "Table Dictionary Index --->\");", 2));
            stringBuilder.AppendLine(SetString(GetValueStringType(table.Value.ElementAt(0).Value, table.Key.Name, 1), 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("Debug.Log(\" < ---" + table.Key.Name + "Table Dictionary Key --->\");", 2));
            stringBuilder.AppendLine(SetString(GetValueStringType(table.Value.ElementAt(0).Value, table.Key.Name, 2), 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("Debug.Log(\" < ---" + table.Key.Name + "Table List --->\");", 2));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(SetString("foreach (var item in " + table.Key.Name + "List)", 2));
            stringBuilder.AppendLine(SetString(GetValueStringType(table.Value.ElementAt(0).Value, table.Key.Name, 3), 3));
            stringBuilder.AppendLine();
        }

        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(SetString("void OnGUI()", 1));
        stringBuilder.AppendLine(SetString("{", 1));
        stringBuilder.AppendLine(SetString("GUILayout.BeginArea(m_rectSheet, GUI.skin.box);", 2));
        stringBuilder.AppendLine(SetString("{", 2));
        stringBuilder.AppendLine(SetString("m_scrollPosSheet = GUILayout.BeginScrollView(m_scrollPosSheet, true, true);", 3));
        stringBuilder.AppendLine(SetString("{", 3));
        stringBuilder.AppendLine(SetString("GUILayout.BeginHorizontal(GUI.skin.button);", 4));
        stringBuilder.AppendLine(SetString("{", 4));

        foreach (var table in m_tables)
        {
            stringBuilder.AppendLine(SetString("if (GUILayout.Button(\"" + table.Key.Name + "\", GUILayout.Width(200), GUILayout.Height(30)))", 5));
            stringBuilder.AppendLine(SetString("{", 5));
            stringBuilder.AppendLine(SetString("Init();", 6));
            stringBuilder.AppendLine(SetString("canShow" + table.Key.Name + " = true;", 6));
            stringBuilder.AppendLine(SetString("}", 5));
            stringBuilder.AppendLine();
        }

        stringBuilder.AppendLine(SetString("}", 4));
        stringBuilder.AppendLine(SetString("GUILayout.EndHorizontal();", 4));
        stringBuilder.AppendLine(SetString("}", 3));
        stringBuilder.AppendLine(SetString("GUILayout.EndScrollView();", 3));
        stringBuilder.AppendLine(SetString("}", 2));
        stringBuilder.AppendLine(SetString("GUILayout.EndArea();", 2));
        stringBuilder.AppendLine();

        foreach (var table in m_tables)
        {
            stringBuilder.AppendLine(SetString("if (canShow" + table.Key.Name + ")", 2));
            stringBuilder.AppendLine(SetString("{", 2));
            stringBuilder.AppendLine(SetString("GUILayout.BeginArea(m_rectTable, GUI.skin.box);", 3));
            stringBuilder.AppendLine(SetString("{", 3));
            stringBuilder.AppendLine(SetString("if (" + table.Key.Name + "Table.GetAll().Count > 0)", 4));
            stringBuilder.AppendLine(SetString("{", 4));
            stringBuilder.AppendLine(SetString("m_scrollPosTable = GUILayout.BeginScrollView(m_scrollPosTable, true, true);", 5));
            stringBuilder.AppendLine(SetString("{", 5));
            stringBuilder.AppendLine(SetString("GUILayout.BeginVertical(GUI.skin.box);", 6));
            stringBuilder.AppendLine(SetString("{", 6));
            stringBuilder.AppendLine(SetString("foreach (var info in " + table.Key.Name + "Table.GetAll())", 7));
            stringBuilder.AppendLine(SetString("{", 7));
            stringBuilder.AppendLine(SetString("GUILayout.BeginHorizontal(GUI.skin.box);", 8));
            stringBuilder.AppendLine(SetString("{", 8));
            stringBuilder.AppendLine(SetString("GUILayout.TextField(\"" + "Key: " + "\"" +
                " + info.Key.ToString(), GUI.skin.box, GUILayout.Width(200), GUILayout.Height(30));", 9));
            stringBuilder.AppendLine(SetString("GUILayout.TextField(\"" + "\"" + ", GUI.skin.box, GUILayout.Width(30), GUILayout.Height(30));", 9));

            for (int i = 1; i < table.Value.ElementAt(0).Value.Count; i++)
                stringBuilder.AppendLine(SetString("GUILayout.TextField(\"" + table.Value.ElementAt(0).Value[i].m_description.ToString() + ": " +
                    "\"" + " + info.Value." + "m_" + table.Value.ElementAt(0).Value[i].m_description +
                    ".ToString(), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(30));", 9));

            stringBuilder.AppendLine(SetString("}", 8));
            stringBuilder.AppendLine(SetString("GUILayout.EndHorizontal();", 8));
            stringBuilder.AppendLine(SetString("}", 7));
            stringBuilder.AppendLine(SetString("}", 6));
            stringBuilder.AppendLine(SetString("GUILayout.EndVertical();", 6));
            stringBuilder.AppendLine(SetString("}", 5));
            stringBuilder.AppendLine(SetString("GUILayout.EndScrollView();", 5));
            stringBuilder.AppendLine(SetString("}", 4));
            stringBuilder.AppendLine(SetString("}", 3));
            stringBuilder.AppendLine(SetString("GUILayout.EndArea();", 3));
            stringBuilder.AppendLine(SetString("}", 2));
            stringBuilder.AppendLine();

            InsaneExcelReader.m_selectedExcelWorksheet = table.Key;
            InsaneExcelReader.m_worksheetInformation = table.Key.Name + " worksheet is selected !!";
        }

        stringBuilder.AppendLine(SetString("}", 1));
        stringBuilder.AppendLine(SetString("}"));

        writeStringToFile(stringBuilder.ToString(), "DisplayTables", ".cs");
    }

    public static void WriteReferenceScript()
    {
        CreateBinary();
        CreateTables();
        CreateDisplayTables();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public static void writeStringToFile(string str, string filename, string type)
    {
        string path = m_scriptPath + filename + type;

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);
        sw.Close();
        file.Close();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public static List<ExcelWorksheet> GetWorkSheets()
    {
        List<ExcelWorksheet> workSheets = new List<ExcelWorksheet>();

        List<string> collectExcelFiles = new List<string>(System.IO.Directory.GetFiles(m_excelPath, "*.xlsx"));

        List<ExcelPackage> excelPackages = new List<ExcelPackage>();

        foreach (string item in collectExcelFiles)
        {
            FileInfo newFile = new FileInfo(item);

            ExcelPackage excelPackage = new ExcelPackage(newFile);
            excelPackages.Add(excelPackage);
        }

        foreach (var excelPackage in excelPackages)
        {
            foreach (var worksheet in excelPackage.Workbook.Worksheets)
            {
                if (workSheets.Contains(worksheet))
                {
                    EditorUtility.DisplayDialog("Warning", worksheet.Name + " is already existed, Please fix that.", "Close");
                    return new List<ExcelWorksheet>();
                }
                else
                {
                    workSheets.Add(worksheet);
                }
            }
        }

        workSheets = workSheets.OrderBy(o => o.Name.ToLower()).ToList();

        return workSheets;
    }

    public static string SetString(string str, int tabCount = 0)
    {
        int tab = 4;
        string tempString = str;

        if (tabCount > 0)
            tempString = tempString.PadLeft(tempString.Length + tab * tabCount);

        return tempString;
    }

    public static string GetValueStringType(List<TableInfo> info, string tableName, int type)
    {
        StringBuilder stringBuilder = new StringBuilder();

        switch (type)
        {
            case 0:
                {
                    stringBuilder.Append("Debug.Log(string.Format(\"Key = {0}, ");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append("m_" + info[i].m_description + " = {" + i + "},");

                    stringBuilder.Append("\"");
                    stringBuilder.Append(", item.Key");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append(" ," + "item.Value.m_" + info[i].m_description);

                    stringBuilder.Append("));");

                    return stringBuilder.ToString();
                }
            case 1:
                {
                    stringBuilder.Append("Debug.Log(string.Format(\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append("m_" + info[i].m_description + " = {" + (i - 1) + "},");

                    stringBuilder.Append("\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append(" ," + tableName + "Index.m_" + info[i].m_description);

                    stringBuilder.Append("));");

                    return stringBuilder.ToString();
                }
            case 2:
                {
                    stringBuilder.Append("Debug.Log(string.Format(\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append("m_" + info[i].m_description + " = {" + (i - 1) + "},");

                    stringBuilder.Append("\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append(" ," + tableName + "Key.m_" + info[i].m_description);

                    stringBuilder.Append("));");

                    return stringBuilder.ToString();
                }
            case 3:
                {
                    stringBuilder.Append("Debug.Log(string.Format(\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append("m_" + info[i].m_description + " = {" + (i - 1) + "},");

                    stringBuilder.Append("\"");

                    for (int i = 1; i < info.Count; i++)
                        stringBuilder.Append(" ," + "item.m_" + info[i].m_description);

                    stringBuilder.Append("));");

                    return stringBuilder.ToString();
                }
            default:
                return string.Empty;
        }
    }

    public static void SetBackgroundColorToSelected()
    {
        GUI.backgroundColor = Color.green;
    }

    public static void SetBackgroundColorToOriginal()
    {
        GUI.backgroundColor = Color.white;
    }

    public static GUIStyle StylelabelType()
    {
        GUIStyle stylelabel = new GUIStyle(GUI.skin.label);
        stylelabel.fontSize = 15;
        stylelabel.alignment = TextAnchor.MiddleCenter;

        return stylelabel;
    }

    public static List<string> CheckKey(ExcelWorksheet worksheet)
    {
        List<string> keys = new List<string>();
        List<string> checkKeys = new List<string>();

        for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
        {
            string checkKey = worksheet.Cells[i, 1].GetValue<string>();

            if (string.IsNullOrEmpty(checkKey))
                continue;

            if (keys.Contains(checkKey))
                checkKeys.Add(checkKey);
            else
                keys.Add(checkKey);
        }

        return checkKeys;
    }

    public static List<string> CheckType(ExcelWorksheet worksheet)
    {
        m_shouldBeSkipedList.Clear();
        List<string> types = new List<string>();

        for (int i = worksheet.Dimension.Start.Column; i <= worksheet.Dimension.End.Column; i++)
        {
            if (string.IsNullOrEmpty(worksheet.Cells[1, i].GetValue<string>()))
            {
                m_shouldBeSkipedList.Add(i);
                continue;
            }

            switch (worksheet.Cells[1, i].GetValue<string>().ToLower())
            {
                case "int":
                case "string":
                case "float":
                case "bool":
                case "byte":
                case "long":
                    types.Add(worksheet.Cells[1, i].GetValue<string>().ToLower());
                    break;
                default:
                    //Debug.Log("Type is not supported !!");
                    m_shouldBeSkipedList.Add(i);
                    break;
            }
        }

        return types;
    }

    public static List<string> CheckVariableName(ExcelWorksheet worksheet)
    {
        List<string> variableNames = new List<string>();

        for (int i = worksheet.Dimension.Start.Column; i <= worksheet.Dimension.End.Column; i++)
        {
            if (m_shouldBeSkipedList.Contains(i))
                continue;

            if (string.IsNullOrEmpty(worksheet.Cells[2, i].GetValue<string>()))
                continue;

            if (!variableNames.Contains(worksheet.Cells[2, i].GetValue<string>()))
                variableNames.Add(worksheet.Cells[2, i].GetValue<string>());
            else
                Debug.Log("Same Variable name is existed !!");
        }

        return variableNames;
    }

    public static void GetTables(ExcelWorksheet worksheet)
    {
        List<string> types = CheckType(worksheet);
        List<string> variableNames = CheckVariableName(worksheet);
        List<string> checkKeys = CheckKey(worksheet);

        if (checkKeys.Count > 0)
        {
            StringBuilder stringKeys = new StringBuilder();
            stringKeys.Append("Table = " + worksheet.ToString() + ", Keys = ");

            foreach (var item in checkKeys)
            {
                stringKeys.Append(item + ", ");
            }

            stringKeys.Append(" : Same key is existed");

            EditorUtility.DisplayDialog("Warning", stringKeys.ToString(), "Close");

            return;
        }

        if (types.Count != variableNames.Count)
        {
            string errorMessage = string.Format("types.Count = {0}, variableNames.Count = {1} is different. Please fix that", types.Count, variableNames.Count);
            Debug.Log(errorMessage);

            StringBuilder stringTypes = new StringBuilder();
            StringBuilder stringVariableNames = new StringBuilder();

            stringTypes.Append("Types = ");
            stringVariableNames.Append("VariableNames = ");

            foreach (var item in types)
            {
                stringTypes.Append(item + ", ");
            }

            foreach (var item in variableNames)
            {
                stringVariableNames.Append(item + ", ");
            }

            Debug.Log(stringTypes.ToString());
            Debug.Log(stringVariableNames.ToString());

            return;
        }

        try
        {
            List<string> all = GetAll(worksheet);
            List<string> collect = all.GetRange(types.Count * 2, all.Count - (types.Count * 2));
            List<List<string>> collectRanges = CollectRanges(collect, types);
            List<TableInfo> tableInfos = CreateTableInfos(collectRanges, types, variableNames);
            List<string> listKeys = GetKeys(collectRanges);
            m_listAllTables = CreateBaseDictionary(listKeys);
            m_listAllTables = CreateDictionary(tableInfos, m_listAllTables);

            CheckTable(m_listAllTables);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    static List<string> GetAll(ExcelWorksheet worksheet)
    {
        List<string> all = new List<string>();

        for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
        {
            string checkKey = worksheet.Cells[i, 1].GetValue<string>();

            if (string.IsNullOrEmpty(checkKey))
                continue;

            for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
            {
                if (m_shouldBeSkipedList.Contains(j))
                    continue;

                string checkString = string.Empty;
                checkString = worksheet.Cells[1, j].GetValue<string>().ToLower();

                if (string.IsNullOrEmpty(checkString))
                    continue;

                if (checkString == "int" || checkString == "float" || checkString == "string" || checkString == "byte" || checkString == "bool" || checkString == "long")
                {
                    if (!string.IsNullOrEmpty(worksheet.Cells[i, j].GetValue<string>()))
                        all.Add(worksheet.Cells[i, j].GetValue<string>());
                    else
                    {
                        switch (checkString)
                        {
                            case "int":
                                all.Add("0");
                                break;
                            case "float":
                                all.Add("0");
                                break;
                            case "string":
                                all.Add("None");
                                break;
                            case "byte":
                                all.Add("0");
                                break;
                            case "bool":
                                all.Add("false");
                                break;
                            case "long":
                                all.Add("0");
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                    continue;
            }
        }

        return all;
    }

    static List<List<string>> CollectRanges(List<string> collect, List<string> types)
    {
        List<List<string>> collectRanges = new List<List<string>>();

        for (int i = 0; i < collect.Count; i += types.Count)
        {
            List<string> collectRange = collect.GetRange(i, types.Count);
            collectRanges.Add(collectRange);
        }

        return collectRanges;
    }

    static List<TableInfo> CreateTableInfos(List<List<string>> list, List<string> types, List<string> variableNames)
    {
        List<TableInfo> collect = new List<TableInfo>();

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < types.Count; j++)
            {
                TableInfo tableInfo = new TableInfo();

                tableInfo.m_type = types[j];
                tableInfo.m_description = variableNames[j];
                tableInfo.m_key = list[i][0];
                tableInfo.m_value = list[i][j];

                collect.Add(tableInfo);
            }
        }

        return collect;
    }

    static Dictionary<string, List<TableInfo>> CreateDictionary(List<TableInfo> tableInfos, Dictionary<string, List<TableInfo>> all)
    {
        for (int i = 0; i < tableInfos.Count; i++)
            all[tableInfos[i].m_key].Add(tableInfos[i]);

        return all;
    }

    static Dictionary<string, List<TableInfo>> CreateBaseDictionary(List<string> listKeys)
    {
        Dictionary<string, List<TableInfo>> collect = new Dictionary<string, List<TableInfo>>();

        foreach (var key in listKeys)
        {
            List<TableInfo> info = new List<TableInfo>();
            collect.Add(key, info);
        }

        return collect;
    }

    static List<string> GetKeys(List<List<string>> list)
    {
        List<string> listKeys = new List<string>();

        foreach (var item in list)
            listKeys.Add(item[0]);

        return listKeys;
    }
}
