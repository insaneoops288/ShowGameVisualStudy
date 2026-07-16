using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class TableLayout : EditorWindow
{
    public static Rect rectShowMenu = new Rect(0, 0, 980, 30);
    public static Rect rectShowWorksheet = new Rect(0, 35, 980, 80);
    public static Rect rectShowTable = new Rect(0, 120, 980, 600);

    public static float m_widthX = 30f;
}
