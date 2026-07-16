using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InsaneUIHelper : EditorWindow
{
    [MenuItem("Tools/InsaneUIHelper")]
    public static void Init()
    {
        EditorWindow window = (InsaneUIHelper)EditorWindow.GetWindow(typeof(InsaneUIHelper));
    }

    public static void TestTest(string bibleName)
    {
        TextAsset[] RevisedRevision = Resources.LoadAll<TextAsset>("Grace/");

        for (int k = 0; k < RevisedRevision.Length; k++)
        {
            // 한글 성경 TextAsset을 저장할 변수입니다. 
            TextAsset textFile;

            List<string> result = new List<string>();

            // Resources폴더에서 성경이름을 가져와서 한글 성경 TextAsset에 저장합니다. 
            textFile = RevisedRevision[k];

            result.Clear();
            // textFile의 내용을 stringReader에 저장합니다. 
            StringReader stringReader = new StringReader(textFile.text);
            // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
            while (stringReader.Peek() >= 0)
            {
                // stringReader에서 라인을 읽어 옵니다. 
                string temp = stringReader.ReadLine();

                result.Add(temp + " ");
            }



            stringReader.Close();


            List<string> resultK = new List<string>();
            List<string> resultA = new List<string>();

            for (int x = 0; x < result.Count; x = x +2)
            {
                resultA.Add(result[x]);
            }

            for (int x = 0; x < resultA.Count; x = x + 4)
            {
                string temptemp = string.Empty;
                try { temptemp += resultA[x]; } catch(Exception e) {}
                try { temptemp += resultA[x + 1]; } catch (Exception e) { }
                try { temptemp += resultA[x + 2]; } catch (Exception e) { }
                try { temptemp += resultA[x + 3]; } catch (Exception e) { }
                // try { temptemp += result[x + 4]; } catch (Exception e) { }
                resultK.Add(temptemp);
            }


            // 저장될 경로입니다. 
            string path = @"c:\Bible\Grace\";

            DirectoryInfo di = new DirectoryInfo(path);

            // 만약 폴더가 존재하지 않으면
            if (di.Exists == false)
            {
                di.Create();
            }

            // 파일을 저장할 준비를 합니다. 
            StreamWriter streamWriter = new StreamWriter(path + RevisedRevision[k].name + ".txt", true);

            for (int i = 0; i < resultK.Count; i++)
            {
                // 결과 리스트를 파일에 저장합니다. 
                streamWriter.WriteLine(resultK[i]);
            }
            streamWriter.Close();
        }
    }


    /// <summary>
    /// 1. 한글이 보이도록 UTF8로 저장하기
    /// </summary>
    public static void SaveUTF8()
    {
        // c:\Bible\AAA 폴더에 있는 파일들을 모두 가져옵니다. 
        string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);
        // 유니티에서 Resources의 특정 폴더에 있는 파일들을 모두 가져옵니다. 
        // TextAsset[] CollectAllFiles = Resources.LoadAll<TextAsset>("ModernManBible/");
        
        // 파일들을 대상으로 반복문 처리합니다. 
        for (int i = 0; i < CollectAllFiles.Length; i++)
        {
            // 경로를 빼고 파일의 이름을 가져옵니다. 
            string fileName = Path.GetFileName(CollectAllFiles[i]);
            // 언리얼에서 임폴트하기 위해서 csv 파일로 저장합니다. 
            //string outputFilePath = @"c:\Bible\BBB\" + fileName + ".csv";
            // c:\Bible\BBB\ 폴더에 저장될 파일 이름입니다. 
            string outputFilePath = @"c:\Bible\BBB\" + fileName;

            // string[] inputArr = File.ReadAllLines(CollectAllFiles[i], Encoding.UTF8);
            // 폴더에 있는 파일의 내용을 UTF8로 인코딩해서 가져옵니다. 
            string[] inputArr = File.ReadAllLines(CollectAllFiles[i], Encoding.GetEncoding("euc-kr"));
            // UTF8로 인코딩한 것을 리스트에 저장합니다. 
            List<string> inputList = new List<string>(inputArr);

            List<string> outputList = new List<string>();

            inputList.ForEach(x => outputList.Add(string.Format("{0}", x)));
            // 파일을 저장합니다. 
            File.WriteAllLines(outputFilePath, outputList, Encoding.UTF8);
        }

        Debug.Log(string.Format("utf8"));
    }

    public static void AllToOneEasyBible()
    {
    }

    /// <summary>
    /// 2. 성경TXT파일을 각 장으로 나누기
    /// </summary>
    public static void ConvertBibleEach(string bibleName)
    {
        // 한글 성경 TextAsset을 저장할 변수입니다. 
        TextAsset textFile;

        List<string> result = new List<string>();

        string[] keywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
        // string[] keywords = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66" };
        int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

        // string[] keywords = {"마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
        // string[] keywords = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66" };
        // int[] chapterCount = { 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

        // 한글 키워드를 대상으로 반복문 처리합니다. 
        for (int m = 0; m < keywords.Length; m++)
        {
            // Resources폴더에서 성경이름을 가져와서 한글 성경 TextAsset에 저장합니다. 
            textFile = Resources.Load(bibleName) as TextAsset;
            // 성경의 각 챕터들을 저장해둔 배열을 대상으로 반복문 처리합니다. 
            for (int i = 0; i < chapterCount[m]; i++)
            {
                result.Clear();
                // textFile의 내용을 stringReader에 저장합니다. 
                StringReader stringReader = new StringReader(textFile.text);
                // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
                while (stringReader.Peek() >= 0)
                {
                    // stringReader에서 라인을 읽어 옵니다. 
                    string temp = stringReader.ReadLine();
                    // 만일 stringReader에서 읽어온 문자열이 "창1:"로 시작한다면? 
                    // 라인번호는 첫번째인데. 리스트의 문자열은 0부터 시작하므로 1이 되게 해 줍니다. 
                    // if (temp.StartsWith(keywords[m] + " " + (i + 1) + ":"))
                    if (temp.StartsWith(keywords[m] + "" + (i + 1) + ":"))
                        {
                        // 결과 리스트에 저장해 줍니다. 
                        result.Add(temp);
                    }
                }
                stringReader.Close();
                // 저장될 경로입니다. 
                string path = @"c:\Bible\CCC\";
                // 파일을 저장할 준비를 합니다. 
                StreamWriter streamWriter = new StreamWriter(path + bibleName + "_" + keywords[m] + (i + 1) + ".txt");

                for (int j = 0; j < result.Count; j++)
                {
                    // 결과 리스트를 파일에 저장합니다. 
                    streamWriter.WriteLine(result[j]);
                }
                streamWriter.Close();
            }
        }
    }

    /// <summary>
    /// 영어 성경TXT파일을 각 장으로 나누기
    /// </summary>
    /// <param name="bibleName"></param>
    public static void ConvertBibleEachEnglish(string bibleName)
    {
        // 영어 성경의 파일 이름은 Gen1:으로 시작할 수도 있고, Gen.1:으로 시작할 수도 있습니다. 
        // 영어 성경 TextAsset을 저장할 변수입니다. 
        TextAsset textFile;

        List<string> result = new List<string>();

        string[] keywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam",  "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
        int[] chapterCount = { 50,    40,   27,    36,    34,     24,     21,     4,      31,     24,     22,     25,     29,     36,     10,     13,    10,       42,    150,  31,     12,       8,      66,   52,    5,      48,     12,    14,    3,      9,      1,      4,       7,     3,     3,     3,      2,     14,     4,     28,     16,     24,     21,     28,     16,    16,     13,     6,     6,     4,      4,     5,        3,        6,      4,      3,       1,        13,    5,       5,      3,      5,       1,       1,       1,      22 };
        // 영어 키워드를 대상으로 반복문 처리합니다. 
        for (int m = 0; m < keywords.Length; m++)
        {
            // Resources폴더에서 성경이름을 가져와서 영어 성경 TextAsset에 저장합니다. 
            textFile = Resources.Load(bibleName) as TextAsset;
            // 성경의 각 챕터들을 저장해둔 배열을 대상으로 반복문 처리합니다. 
            for (int i = 0; i < chapterCount[m]; i++)
            {
                result.Clear();
                // textFile의 내용을 stringReader에 저장합니다. 
                StringReader stringReader = new StringReader(textFile.text);
                // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
                while (stringReader.Peek() >= 0)
                {
                    // Gen1:
                    // Amos9:
                    // stringReader에서 라인을 읽어 옵니다. 
                    string temp = stringReader.ReadLine();
                    // 만일 stringReader에서 읽어온 문자열이 "Gen1:"로 시작한다면? 
                    // 라인번호는 첫번째인데. 리스트의 문자열은 0부터 시작하므로 1이 되게 해 줍니다. 
                    if (temp.StartsWith(keywords[m] + (i + 1) + ":"))
                    {
                        // 결과 리스트에 저장해 줍니다. 
                        result.Add(temp);
                    }
                }

                stringReader.Close();

                if (result.Count > 0)
                {
                    // 저장될 경로입니다. 
                    string path = @"c:\Bible\BBB\";
                    // 파일을 저장할 준비를 합니다. 
                    StreamWriter streamWriter = new StreamWriter(path + bibleName + "_" + keywords[m] + (i + 1) + ".txt");

                    for (int j = 0; j < result.Count; j++)
                    {
                        // 결과 리스트를 파일에 저장합니다. 
                        streamWriter.WriteLine(result[j]);
                    }
                    streamWriter.Close();
                }
            }
        }
        // 영어 키워드를 대상으로 반복문 처리합니다. 
        for (int m = 0; m < keywords.Length; m++)
        {
            // Resources폴더에서 성경이름을 가져와서 한글 성경 TextAsset에 저장합니다. 
            textFile = Resources.Load(bibleName) as TextAsset;
            // 성경의 각 챕터들을 저장해둔 배열을 대상으로 반복문 처리합니다. 
            for (int i = 0; i < chapterCount[m]; i++)
            {
                result.Clear();
                // textFile의 내용을 stringReader에 저장합니다. 
                StringReader stringReader = new StringReader(textFile.text);
                // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
                while (stringReader.Peek() >= 0)
                {
                    // Gen.1:
                    // Amos.9:
                    // stringReader에서 라인을 읽어 옵니다. 
                    string temp = stringReader.ReadLine();
                    // 만일 stringReader에서 읽어온 문자열이 "Gen.:"로 시작한다면? 
                    // 라인번호는 첫번째인데. 리스트의 문자열은 0부터 시작하므로 1이 되게 해 줍니다. 
                    if (temp.StartsWith(keywords[m] + "." + (i + 1) + ":"))
                    {
                        // 결과 리스트에 저장해 줍니다. 
                        result.Add(temp);
                    }
                }

                stringReader.Close();

                if (result.Count > 0)
                {
                    // 저장될 경로입니다. 
                    string path = @"c:\Bible\BBB\";
                    // 파일을 저장할 준비를 합니다. 
                    StreamWriter streamWriter = new StreamWriter(path + bibleName + "_" + keywords[m] + (i + 1) + ".txt");

                    for (int j = 0; j < result.Count; j++)
                    {
                        // 결과 리스트를 파일에 저장합니다. 
                        streamWriter.WriteLine(result[j]);
                    }
                    streamWriter.Close();
                }
            }
        }
    }

    /// <summary>
    /// 2. 성경TXT파일을 각 장으로 나누기
    /// </summary>
    public static void ConvertBibleEachA()
    {
        // 한글 성경 TextAsset을 저장할 변수입니다. 
        TextAsset textFile;

        List<string> result = new List<string>();

        string[] chapters = { "1-01창세기", "1-02출애굽기", "1-03레위기", "1-04민수기", "1-05신명기", "1-06여호수아", "1-07사사기", "1-08룻기", "1-09사무엘상", "1-10사무엘하", "1-11열왕기상", "1-12열왕기하", "1-13역대상", "1-14역대하", "1-15에스라", "1-16느헤미야", "1-17에스더", "1-18욥기", "1-19시편", "1-20잠언", "1-21전도서", "1-22아가", "1-23이사야", "1-24예레미야", "1-25예레미야애가", "1-26에스겔", "1-27다니엘", "1-28호세아", "1-29요엘", "1-30아모스", "1-31오바댜", "1-32요나", "1-33미가", "1-34나훔", "1-35하박국", "1-36스바냐", "1-37학개", "1-38스가랴", "1-39말라기", "2-01마태복음", "2-02마가복음", "2-03누가복음", "2-04요한복음", "2-05사도행전", "2-06로마서", "2-07고린도전서", "2-08고린도후서", "2-09갈라디아서", "2-10에베소서", "2-11빌립보서", "2-12골로새서", "2-13데살로니가전서", "2-14데살로니가후서", "2-15디모데전서", "2-16디모데후서", "2-17디도서", "2-18빌레몬서", "2-19히브리서", "2-20야고보서", "2-21베드로전서", "2-22베드로후서", "2-23요한일서", "2-24요한이서", "2-25요한삼서", "2-26유다서", "2-27요한계시록" };
        string[] keywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
        int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

        Debug.Log(string.Format("chapters : {0}, keywords : {1}, chapterCount : {2}", chapters.Length, keywords.Length, chapterCount.Length));
        // 한글 키워드를 대상으로 반복문 처리합니다. 
        for (int m = 0; m < keywords.Length; m++)
        {
            // Resources폴더에서 chapters리스트의 요소의 성경이름을 가져와서 한글 성경 TextAsset에 저장합니다. 
            textFile = Resources.Load(chapters[m]) as TextAsset;
            // chapters리스트의 요소의 성경의 각 챕터들을 저장해둔 배열을 대상으로 반복문 처리합니다. 
            for (int i = 0; i < chapterCount[m]; i++)
            {
                result.Clear();
                // textFile의 내용을 stringReader에 저장합니다. 
                StringReader stringReader = new StringReader(textFile.text);
                // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
                while (stringReader.Peek() >= 0)
                {
                    // stringReader에서 라인을 읽어 옵니다. 
                    string temp = stringReader.ReadLine();
                    // 만일 stringReader에서 읽어온 문자열이 "창1:"로 시작한다면? 
                    // 라인번호는 첫번째인데. 리스트의 문자열은 0부터 시작하므로 1이 되게 해 줍니다. 
                    if (temp.StartsWith(keywords[m] + (i + 1) + ":"))
                    {
                        result.Add(temp);
                    }
                }
                stringReader.Close();
                // string path = Application.persistentDataPath + "/";
                string path = @"c://BibleText//path1//";
                StreamWriter streamWriter = new StreamWriter(path + textFile.name + (i + 1) + ".txt");

                for (int j = 0; j < result.Count; j++)
                {
                    streamWriter.WriteLine(result[j]);
                }
                streamWriter.Close();
            }
        }
    }

    /// <summary>
    /// 2. 성경TXT파일을 각 장으로 나누기
    /// </summary>
    public static void ConvertBibleEachAll()
    {
        // 한글 성경 TextAsset을 저장할 변수입니다. 
        TextAsset textFile;

        List<string> result = new List<string>();

        string[] keywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
        int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };
        string[] biblePath = { "path2", "path3", "path4", "path5", "path6" };
        string[] bibleName = { "개역개정", "개역한글", "공동번역", "표준새번역", "현대인의성경" };

        // bibleName배열을 대상으로 반복문 처리합니다. 
        for (int k = 0; k < bibleName.Length; k++)
        {
            // 한글 키워드를 대상으로 반복문 처리합니다. 
            for (int m = 0; m < keywords.Length; m++)
            {
                // Resources폴더에서 bibleName배열의 요소의 성경이름을 가져와서 한글 성경 TextAsset에 저장합니다. 
                textFile = Resources.Load(bibleName[k]) as TextAsset;
                // 성경의 각 챕터들을 저장해둔 배열을 대상으로 반복문 처리합니다. 
                for (int i = 0; i < chapterCount[m]; i++)
                {
                    result.Clear();
                    // textFile의 내용을 stringReader에 저장합니다. 
                    StringReader stringReader = new StringReader(textFile.text);
                    // stringReader에 저장되어 있는 문자열이 끝이 아닐 때까지 반복합니다. 
                    while (stringReader.Peek() >= 0)
                    {
                        // stringReader에서 라인을 읽어 옵니다. 
                        string temp = stringReader.ReadLine();
                        // 만일 stringReader에서 읽어온 문자열이 "창1:"로 시작한다면? 
                        // 라인번호는 첫번째인데. 리스트의 문자열은 0부터 시작하므로 1이 되게 해 줍니다. 
                        if (temp.StartsWith(keywords[m] + (i + 1) + ":"))
                        {
                            // 결과 리스트에 저장해 줍니다. 
                            result.Add(temp);
                        }
                    }
                    stringReader.Close();
                    // 저장될 경로입니다. 
                    string path = @"c://BibleText//";
                    // 파일을 저장할 준비를 합니다. 
                    StreamWriter streamWriter = new StreamWriter(path + bibleName[k] + "_" + keywords[m] + (i + 1) + ".txt");

                    for (int j = 0; j < result.Count; j++)
                    {
                        // 결과 리스트를 파일에 저장합니다. 
                        streamWriter.WriteLine(result[j]);
                    }

                    streamWriter.Close();
                }
            }
        }
    }

    /// <summary>
    /// 5. 성경TXT파일 이름 변경
    /// </summary>
    public static void RenameFiles()
    {
        // c:\aaa\bbb 폴더에 있는 모든 파일들을 가져옵니다. 
        string[] CollectAllFiles = Directory.GetFiles(@"c:\aaa\bbb", "*.txt", SearchOption.AllDirectories);

        Debug.Log(string.Format("CollectAllFiles.Length {0}", CollectAllFiles.Length));
        // 가져온 파일들을 대상으로 반복문 처리합니다. 
        for (int i = 0; i < CollectAllFiles.Length; i++)
        {
            // 파일을 경로는 무시하고 파일 이름을 가져옵니다. 
            string oldFile = Path.GetFileName(CollectAllFiles[i]);
            Debug.Log(string.Format("oldFile {0}", oldFile));
            // 파일 이름을 변경해 줍니다. 
            string newFile = @"c:\aaa\ccc\" + "개역한글_" + oldFile;
            Debug.Log(string.Format("newFile {0}", newFile));
            // 변경된 파일 이름을 복사해 줍니다. 
            System.IO.File.Copy(CollectAllFiles[i], newFile);
        }
    }

    /// <summary>
    /// 4. 1 도 없애주기
    /// </summary>
    public static void Remove1()
    {
        
    }

    /// <summary>
    /// 4. 1 도 없애주기
    /// </summary>
    public static void Remove1Other()
    {
        // c:\Bible\CCC 폴더의 파일들을 모두 가져옵니다. 
        string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\DDD", "*.txt", SearchOption.AllDirectories);
        // 폴더에 있는 파일들을 대상으로 반복문 처리합니다. 
        for (int k = 0; k < CollectAllFiles.Length; k++)
        {
            // 파일의 경로는 무시하고 파일이름을 가져옵니다. 
            string fileName = Path.GetFileName(CollectAllFiles[k]);
            // 저장될 파일 이름입니다. 
            string outputFilePath = @"c:\Bible\EEE\" + fileName;
            // 파일을 읽어서 문자열로 저장합니다. 
            string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
            // 문자열을 '\n'으로 분리해서 collectKorean리스트에 저장합니다. 
            List<string> collectKorean = new List<string>(textValue.Split('\n'));
            List<string> collectKoreanModify = new List<string>();

            List<string> result = new List<string>();
            // collectKorean리스트를 대상으로 반복문 처리합니다. 
            for (int i = 0; i < collectKorean.Count; i++)
            {
                // collectKorean리스트의 요소를 임시변수에 저장합니다. 
                string temp = collectKorean[i];
                string tempA = string.Empty;

                {
                    // 최대로 있을 수 있는 장이 시150입니다. 
                    // 만일 임시변수가 "1 "으로 시작한다면? 
                    if (temp.StartsWith("1 "))
                    {
                        // 임수변수의 "1 "를 ""으로 변경해서 tempA임시변수에 저장합니다. 
                        tempA = temp.Replace("1 ", "");
                        // 결과를 result리스트에 저장합니다. 
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("2 "))
                    {
                        tempA = temp.Replace("2 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("3 "))
                    {
                        tempA = temp.Replace("3 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("4 "))
                    {
                        tempA = temp.Replace("4 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("5 "))
                    {
                        tempA = temp.Replace("5 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("6 "))
                    {
                        tempA = temp.Replace("6 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("7 "))
                    {
                        tempA = temp.Replace("7 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("8 "))
                    {
                        tempA = temp.Replace("8 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("9 "))
                    {
                        tempA = temp.Replace("9 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("10 "))
                    {
                        tempA = temp.Replace("10 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("11 "))
                    {
                        tempA = temp.Replace("11 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("12 "))
                    {
                        tempA = temp.Replace("12 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("13 "))
                    {
                        tempA = temp.Replace("13 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("14 "))
                    {
                        tempA = temp.Replace("14 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("15 "))
                    {
                        tempA = temp.Replace("15 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("16 "))
                    {
                        tempA = temp.Replace("16 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("17 "))
                    {
                        tempA = temp.Replace("17 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("18 "))
                    {
                        tempA = temp.Replace("18 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("19 "))
                    {
                        tempA = temp.Replace("19 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("20 "))
                    {
                        tempA = temp.Replace("20 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("21 "))
                    {
                        tempA = temp.Replace("21 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("22 "))
                    {
                        tempA = temp.Replace("22 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("23 "))
                    {
                        tempA = temp.Replace("23 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("24 "))
                    {
                        tempA = temp.Replace("24 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("25 "))
                    {
                        tempA = temp.Replace("25 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("26 "))
                    {
                        tempA = temp.Replace("26 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("27 "))
                    {
                        tempA = temp.Replace("27 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("28 "))
                    {
                        tempA = temp.Replace("28 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("29 "))
                    {
                        tempA = temp.Replace("29 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("30 "))
                    {
                        tempA = temp.Replace("30 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("31 "))
                    {
                        tempA = temp.Replace("31 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("32 "))
                    {
                        tempA = temp.Replace("32 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("33 "))
                    {
                        tempA = temp.Replace("33 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("34 "))
                    {
                        tempA = temp.Replace("34 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("35 "))
                    {
                        tempA = temp.Replace("35 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("36 "))
                    {
                        tempA = temp.Replace("36 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("37 "))
                    {
                        tempA = temp.Replace("37 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("38 "))
                    {
                        tempA = temp.Replace("38 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("39 "))
                    {
                        tempA = temp.Replace("39 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("40 "))
                    {
                        tempA = temp.Replace("40 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("41 "))
                    {
                        tempA = temp.Replace("41 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("42 "))
                    {
                        tempA = temp.Replace("42 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("43 "))
                    {
                        tempA = temp.Replace("43 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("44 "))
                    {
                        tempA = temp.Replace("44 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("45 "))
                    {
                        tempA = temp.Replace("45 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("46 "))
                    {
                        tempA = temp.Replace("46 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("47 "))
                    {
                        tempA = temp.Replace("47 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("48 "))
                    {
                        tempA = temp.Replace("48 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("49 "))
                    {
                        tempA = temp.Replace("49 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("50 "))
                    {
                        tempA = temp.Replace("50 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("51 "))
                    {
                        tempA = temp.Replace("51 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("52 "))
                    {
                        tempA = temp.Replace("52 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("53 "))
                    {
                        tempA = temp.Replace("53 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("54 "))
                    {
                        tempA = temp.Replace("54 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("55 "))
                    {
                        tempA = temp.Replace("55 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("56 "))
                    {
                        tempA = temp.Replace("56 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("57 "))
                    {
                        tempA = temp.Replace("57 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("58 "))
                    {
                        tempA = temp.Replace("58 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("59 "))
                    {
                        tempA = temp.Replace("59 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("60 "))
                    {
                        tempA = temp.Replace("60 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("61 "))
                    {
                        tempA = temp.Replace("61 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("62 "))
                    {
                        tempA = temp.Replace("62 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("63 "))
                    {
                        tempA = temp.Replace("63 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("64 "))
                    {
                        tempA = temp.Replace("64 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("65 "))
                    {
                        tempA = temp.Replace("65 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("66 "))
                    {
                        tempA = temp.Replace("66 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("67 "))
                    {
                        tempA = temp.Replace("67 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("68 "))
                    {
                        tempA = temp.Replace("68 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("69 "))
                    {
                        tempA = temp.Replace("69 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("70 "))
                    {
                        tempA = temp.Replace("70 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("71 "))
                    {
                        tempA = temp.Replace("71 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("72 "))
                    {
                        tempA = temp.Replace("72 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("73 "))
                    {
                        tempA = temp.Replace("73 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("74 "))
                    {
                        tempA = temp.Replace("74 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("75 "))
                    {
                        tempA = temp.Replace("75 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("76 "))
                    {
                        tempA = temp.Replace("76 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("77 "))
                    {
                        tempA = temp.Replace("77 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("78 "))
                    {
                        tempA = temp.Replace("78 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("79 "))
                    {
                        tempA = temp.Replace("79 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("80 "))
                    {
                        tempA = temp.Replace("80 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("81 "))
                    {
                        tempA = temp.Replace("81 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("82 "))
                    {
                        tempA = temp.Replace("82 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("83 "))
                    {
                        tempA = temp.Replace("83 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("84 "))
                    {
                        tempA = temp.Replace("84 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("85 "))
                    {
                        tempA = temp.Replace("85 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("86 "))
                    {
                        tempA = temp.Replace("86 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("87 "))
                    {
                        tempA = temp.Replace("87 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("88 "))
                    {
                        tempA = temp.Replace("88 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("89  "))
                    {
                        tempA = temp.Replace("89 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("90 "))
                    {
                        tempA = temp.Replace("90 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("91 "))
                    {
                        tempA = temp.Replace("91 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("92 "))
                    {
                        tempA = temp.Replace("92 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("93 "))
                    {
                        tempA = temp.Replace("93 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("94 "))
                    {
                        tempA = temp.Replace("94 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("95 "))
                    {
                        tempA = temp.Replace("95 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("96 "))
                    {
                        tempA = temp.Replace("96 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("97 "))
                    {
                        tempA = temp.Replace("97 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("98 "))
                    {
                        tempA = temp.Replace("98 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("99 "))
                    {
                        tempA = temp.Replace("99 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("100 "))
                    {
                        tempA = temp.Replace("100 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("101 "))
                    {
                        tempA = temp.Replace("101 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("102 "))
                    {
                        tempA = temp.Replace("102 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("103 "))
                    {
                        tempA = temp.Replace("103 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("104 "))
                    {
                        tempA = temp.Replace("104 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("105 "))
                    {
                        tempA = temp.Replace("105 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("106 "))
                    {
                        tempA = temp.Replace("106 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("107 "))
                    {
                        tempA = temp.Replace("107 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("108 "))
                    {
                        tempA = temp.Replace("108 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("109 "))
                    {
                        tempA = temp.Replace("109 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("110 "))
                    {
                        tempA = temp.Replace("110 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("111 "))
                    {
                        tempA = temp.Replace("111 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("112 "))
                    {
                        tempA = temp.Replace("112 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("113 "))
                    {
                        tempA = temp.Replace("113", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("114 "))
                    {
                        tempA = temp.Replace("114 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("115 "))
                    {
                        tempA = temp.Replace("115 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("116 "))
                    {
                        tempA = temp.Replace("116 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("117 "))
                    {
                        tempA = temp.Replace("117 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("118 "))
                    {
                        tempA = temp.Replace("118 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("119 "))
                    {
                        tempA = temp.Replace("119 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("120 "))
                    {
                        tempA = temp.Replace("120 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("121 "))
                    {
                        tempA = temp.Replace("121 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("122 "))
                    {
                        tempA = temp.Replace("122 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("123 "))
                    {
                        tempA = temp.Replace("123 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("124 "))
                    {
                        tempA = temp.Replace("124 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("125 "))
                    {
                        tempA = temp.Replace("125 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("126 "))
                    {
                        tempA = temp.Replace("126 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("127 "))
                    {
                        tempA = temp.Replace("127 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("128 "))
                    {
                        tempA = temp.Replace("128 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("129 "))
                    {
                        tempA = temp.Replace("129 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("130 "))
                    {
                        tempA = temp.Replace("130 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("131 "))
                    {
                        tempA = temp.Replace("131 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("132 "))
                    {
                        tempA = temp.Replace("132 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("133 "))
                    {
                        tempA = temp.Replace("133 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("134 "))
                    {
                        tempA = temp.Replace("134 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("135 "))
                    {
                        tempA = temp.Replace("135 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("136 "))
                    {
                        tempA = temp.Replace("136 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("137 "))
                    {
                        tempA = temp.Replace("137 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("138 "))
                    {
                        tempA = temp.Replace("138 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("139 "))
                    {
                        tempA = temp.Replace("139 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("140 "))
                    {
                        tempA = temp.Replace("140 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("141 "))
                    {
                        tempA = temp.Replace("141 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("142 "))
                    {
                        tempA = temp.Replace("142 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("143 "))
                    {
                        tempA = temp.Replace("143 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("144 "))
                    {
                        tempA = temp.Replace("144 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("145 "))
                    {
                        tempA = temp.Replace("145 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("146 "))
                    {
                        tempA = temp.Replace("146 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("147 "))
                    {
                        tempA = temp.Replace("147 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("148 "))
                    {
                        tempA = temp.Replace("148 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("149 "))
                    {
                        tempA = temp.Replace("149 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("150 "))
                    {
                        tempA = temp.Replace("150 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("151 "))
                    {
                        tempA = temp.Replace("151 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("152 "))
                    {
                        tempA = temp.Replace("152 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("153 "))
                    {
                        tempA = temp.Replace("153 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("154 "))
                    {
                        tempA = temp.Replace("154 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("155 "))
                    {
                        tempA = temp.Replace("155 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("156 "))
                    {
                        tempA = temp.Replace("156 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("157 "))
                    {
                        tempA = temp.Replace("157 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("158 "))
                    {
                        tempA = temp.Replace("158 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("159 "))
                    {
                        tempA = temp.Replace("159 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("160 "))
                    {
                        tempA = temp.Replace("160 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("161 "))
                    {
                        tempA = temp.Replace("161 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("162 "))
                    {
                        tempA = temp.Replace("162 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("163 "))
                    {
                        tempA = temp.Replace("163 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("164 "))
                    {
                        tempA = temp.Replace("164 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("165 "))
                    {
                        tempA = temp.Replace("165 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("166 "))
                    {
                        tempA = temp.Replace("166 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("167 "))
                    {
                        tempA = temp.Replace("167 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("168 "))
                    {
                        tempA = temp.Replace("168 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("169 "))
                    {
                        tempA = temp.Replace("169 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("170 "))
                    {
                        tempA = temp.Replace("170 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("171 "))
                    {
                        tempA = temp.Replace("171 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("172 "))
                    {
                        tempA = temp.Replace("172 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("173 "))
                    {
                        tempA = temp.Replace("173 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("174 "))
                    {
                        tempA = temp.Replace("174 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("175 "))
                    {
                        tempA = temp.Replace("175 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("176 "))
                    {
                        tempA = temp.Replace("176 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("177 "))
                    {
                        tempA = temp.Replace("177 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("178 "))
                    {
                        tempA = temp.Replace("178 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("179 "))
                    {
                        tempA = temp.Replace("179 ", "");
                        result.Add(tempA);
                    }
                    else if (temp.StartsWith("180 "))
                    {
                        tempA = temp.Replace("180 ", "");
                        result.Add(tempA);
                    }
                    else
                    {
                        result.Add(temp);
                    }
                }
            }

            StreamWriter streamWriter = new StreamWriter(outputFilePath);

            for (int j = 0; j < result.Count - 1; j++)
            {
                streamWriter.WriteLine(result[j].TrimEnd());
            }
            streamWriter.Close();
        }        
    }    

    /// <summary>
    /// 3. 창세기1:1 에서 창세기1: 없애주기
    /// </summary>
    public static void RemoveFront()
    {
        // c:\Bible\BBB 폴더에 있는 모든 파일들을 가져 옵니다. 
        string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\CCC", "*.txt", SearchOption.AllDirectories);
        // 모든 파일들을 대상으로 반복문 처리합니다. 
        for (int k = 0; k < CollectAllFiles.Length; k++)
        {
            // 경로를 뺀 파일이름을 가져옵니다. 
            string fileName = Path.GetFileName(CollectAllFiles[k]);
            // 저장될 파일 이름을 지정해 줍니다. 
            string outputFilePath = @"c:\Bible\DDD\" + fileName;
            // 파일에 있는 문자열을 가져옵니다. 
            string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
            // 가져온 문자열을 '\n' 문자로 구분해서 collectKorean 리스트에 저장합니다. 
            List<string> collectKorean = new List<string>(textValue.Split('\n'));
            List<string> collectKoreanModify = new List<string>();
            // collectKorean 리스트를 대상으로 반복문 처리합니다. 
            // 리스트 맨 끝에는 공백이 있습니다. 공백은 반복문 처리에서 빼 줍니다. 
            for (int i = 0; i < collectKorean.Count - 1; i++)
            {
                // 문자열을 ":"로 분리합니다. 창1:15
                string[] ModifyA = collectKorean[i].Split(":");
                if (ModifyA.Length > 1)
                {
                    // ":" 기준으로 앞부분은 날려 주고, 뒷부분만 collectKoreanModify 리스트에 추가해 줍니다. 
                    collectKoreanModify.Add(ModifyA[1]);
                }
            }

            StreamWriter writer = new StreamWriter(outputFilePath);

            for (int i = 0; i < collectKoreanModify.Count; i++)
            {
                writer.WriteLine(collectKoreanModify[i].TrimEnd());
            }

            writer.Close();
        }
    }

    int intCount = 0;

    public void ExtractBible()
    {
        string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
        string[] keywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
        int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

        TextAsset[] RevisedRevision = Resources.LoadAll<TextAsset>("RevisedRevision/");
        TextAsset[] RevisedHangul = Resources.LoadAll<TextAsset>("RevisedHangul/");
        TextAsset[] EasyBible = Resources.LoadAll<TextAsset>("EasyBible/");
        TextAsset[] NewStandardTranslation = Resources.LoadAll<TextAsset>("NewStandardTranslation/");

        string fileName = Path.GetFileName(RevisedRevision[intCount].name);
        string outputFilePath = @"c:\Bible\AAA\" + fileName + ".txt";

        StreamWriter streamWriter = new StreamWriter(outputFilePath);

        List<string> RevisedRevisionList = new List<string>(RevisedRevision[intCount].text.Split('\n'));
        List<string> RevisedHangulList = new List<string>(RevisedHangul[intCount].text.Split('\n'));
        List<string> EasyBibleList = new List<string>(EasyBible[intCount].text.Split('\n'));
        List<string> NewStandardTranslationList = new List<string>(NewStandardTranslation[intCount].text.Split('\n'));

        Debug.Log("RevisedRevisionList.Count : " + RevisedRevisionList.Count);
        Debug.Log("RevisedHangulList.Count : " + RevisedHangulList.Count);
        Debug.Log("EasyBibleList.Count : " + EasyBibleList.Count);
        Debug.Log("NewStandardTranslationList.Count : " + NewStandardTranslationList.Count);

        for (int i = 0; i < RevisedRevisionList.Count; i++)
        {
            streamWriter.WriteLine((i + 1) + "절 " + RevisedRevisionList[i].Trim());
            streamWriter.WriteLine((i + 1) + "절 " + RevisedHangulList[i].Trim());
            streamWriter.WriteLine((i + 1) + "절 " + EasyBibleList[i].Trim());
            streamWriter.WriteLine((i + 1) + "절 " + NewStandardTranslationList[i].Trim());
        }

        streamWriter.Close();

        intCount++;
    }

    void OnGUI()
    {
        Repaint();

        GUILayout.BeginVertical();
        {
            // 성경별로 각각의 챕터들의 절들이 몇 절들인가를 확인합니다. 
            if (GUILayout.Button("TestH", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] keywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

                TextAsset[] RevisedRevision = Resources.LoadAll<TextAsset>("RevisedRevision/");
                TextAsset[] RevisedHangul = Resources.LoadAll<TextAsset>("RevisedHangul/");
                TextAsset[] EasyBible = Resources.LoadAll<TextAsset>("EasyBible/");
                TextAsset[] NewStandardTranslation = Resources.LoadAll<TextAsset>("NewStandardTranslation/");

                string fileName = Path.GetFileName(RevisedRevision[intCount].name);
                string outputFilePath = @"c:\Bible\AAA\" + fileName + ".txt";

                StreamWriter streamWriter = new StreamWriter(outputFilePath);

                List<string> RevisedRevisionList = new List<string>(RevisedRevision[intCount].text.Split('\n'));
                List<string> RevisedHangulList = new List<string>(RevisedHangul[intCount].text.Split('\n'));
                List<string> EasyBibleList = new List<string>(EasyBible[intCount].text.Split('\n'));
                List<string> NewStandardTranslationList = new List<string>(NewStandardTranslation[intCount].text.Split('\n'));

                Debug.Log("RevisedRevisionList.Count : " + RevisedRevisionList.Count);
                Debug.Log("RevisedHangulList.Count : " + RevisedHangulList.Count);
                Debug.Log("EasyBibleList.Count : " + EasyBibleList.Count);
                Debug.Log("NewStandardTranslationList.Count : " + NewStandardTranslationList.Count);

                for (int i = 0; i < RevisedRevisionList.Count; i++)
                {
                    streamWriter.WriteLine((i + 1) + "절 " + RevisedRevisionList[i].Trim());
                    streamWriter.WriteLine((i + 1) + "절 " + RevisedHangulList[i].Trim());
                    streamWriter.WriteLine((i + 1) + "절 " + EasyBibleList[i].Trim());
                    streamWriter.WriteLine((i + 1) + "절 " + NewStandardTranslationList[i].Trim());
                }

                streamWriter.Close();

                intCount++;
            }
            // 1189개의 쉬운성경의 파일들을 하나의 텍스트 파일로 생성하기 위한 이전 단계입니다. 
            if (GUILayout.Button("RenameFiles", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] Englishkeywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

                // c:\Bible\AAA 폴더에 있는 모든 텍스트 파일들을 읽어 옵니다. 
                string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);

                string newFile = string.Empty;
                // 텍스트 파일들을 대상으로 반복문 처리합니다. 
                for (int i = 0; i < CollectAllFiles.Length; i++)
                {
                    // 가져온 텍스트 파일의 파일 이름을 가져옵니다. 
                    string oldFile = Path.GetFileName(CollectAllFiles[i]);
                    // 파일 이름은 기본적으로 쉬운성경_창1 입니다. 하나의 파일로 생성해 주기 위해서는 파일 이름 앞에 01과 같은 인덱스를 추가해 주어야 합니다. 
                    // 파일들을 읽어서 파일 이름앞에 인덱스를 추가해 줍니다.
                    for (int j = 0; j < Koreankeywords.Length; j++)
                    {
                        // 만일 파일 이름에 "창"문자열이 있다면? 
                        if (oldFile.Contains(Koreankeywords[j]))
                        {
                            // "쉬운성경_창1" 파일 이름을 "01쉬운성경_창1"로 변경해 줍니다. 
                            // "01쉬운성경_창1"을 "01쉬운성경_창01"로 변경해 주어야 하는데 이후에 ACDSee를 이용합니다. 
                            string result = string.Format("{0:D2}{1}", (j + 1), oldFile);
                            newFile = result;
                        }
                    }
                    // c:\Bible\BBB\ 폴더에 저장될 파일 이름입니다. 
                    newFile = @"c:\Bible\BBB\" + newFile;
                    // c:\Bible\BBB\ 폴더에 파일 이름을 저장합니다. 
                    System.IO.File.Copy(CollectAllFiles[i], newFile);
                }
                // 이제 파일의 내용을 수정해 주어야 합니다. 쉬운 성경의 내용들은 기본적으로 "창1:1"와 같은 내용이 없으므로 구분을 위해서 추가해 주는 것입니다. 
                // c:\Bible\BBB 폴더의 모든 파일들을 읽어 옵니다. 
                CollectAllFiles = Directory.GetFiles(@"c:\Bible\BBB", "*.txt", SearchOption.AllDirectories);
                // 읽어온 파일들을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Length; k++)
                {
                    // 확장자가 빠진 파일 이름을 가져옵니다. 
                    string fileName = Path.GetFileNameWithoutExtension(CollectAllFiles[k]);

                    Debug.Log("fileName" + fileName);
                    // c:\Bible\CCC\ 폴더에 저장될 파일 이름입니다. 
                    string outputFilePath = @"c:\Bible\CCC\" + fileName + ".txt";
                    // 텍스트 파일의 내용을 문자열로 읽어 옵니다. 
                    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
                    // 읽어온 문자열을 '\n' 기준으로 분리해서 리스트 컬렉션에 저장합니다. 
                    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                    // 최종 결과 문자열들을 저장할 배열입니다. 
                    List<string> collectKoreanModify = new List<string>();
                    // 저장된 collectKorean 컬렉션을 대상으로 반복문 처리합니다. 
                    for (int i = 0; i < collectKorean.Count - 1; i++)
                    {
                        // 기본적인 파일 이름은 "01쉬운성경_창1" 입니다. "_"를 기준으로 파일 이름을 분리합니다. 
                        string[] ModifyA = fileName.Split("_");

                        Debug.Log("ModifyA[1]" + ModifyA[1]);
                        
                        if (ModifyA.Length > 1)
                        {
                            // 파일의 내용을 읽어서 절 앞에 창1:1을 표시합니다. 
                            collectKoreanModify.Add(ModifyA[1] + ":" + (i + 1) + " " + collectKorean[i]);
                        }
                    }
                    // 파일로 저장할 준비를 합니다. 
                    StreamWriter writer = new StreamWriter(outputFilePath);

                    for (int i = 0; i < collectKoreanModify.Count; i++)
                    {
                        // 최종 결과 문자열들을 파일에 씁니다. 
                        writer.WriteLine(collectKoreanModify[i].Trim());
                    }

                    writer.Close();
                }
            }
            // 여러개의 텍스트 파일들을 하나의 텍스트 파일로 생성해 줍니다. 개별적으로 저장되어 있는 쉬운 성경과 NIV 성경 파일들을 하나의 파일로 묶기 위한 과정입니다. 
            if (GUILayout.Button("CreateOneTextFileInEasyBable", GUILayout.Height(30)))
            {
                // c:\Bible\AAA 폴더에 있는 모든 파일들을 읽어 옵니다. 
                List<string> CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.TopDirectoryOnly).ToList();
                // 소팅도 해 줍니다. 
                CollectAllFiles = CollectAllFiles.OrderBy(o => o[0]).ToList();

                Debug.Log("CollectAllFiles[0] : " + CollectAllFiles[0]);
                // 최종 저장될 파일 이름 입니다. 
                string outputFilePath = @"c:\Bible\BBB\aaa.txt";

                List<string> collectKorean = new List<string>();

                StreamWriter writer;
                // 모든 파일들이 저장되어 있는 컬렉션을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Count; k++)
                {
                    // 파일의 내용을 문자열로 읽어 옵니다. 
                    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
                    // 읽어 온 문자열을 '\n' 기준으로 분리해서 collectKorean리스트에 저장합니다. 
                    collectKorean = new List<string>(textValue.Split('\n'));
                    // 파일을 저장할 준비를 합니다. 1189개의 파일들의 내용을 하나의 텍스트 파일에 저장해야 하니 파일에 추가가 가능하도록 설정해 줍니다. 
                    writer = new StreamWriter(outputFilePath, true);
                    // 파일들에는 끝에 공백이 있습니다. 공백을 빼주기 위해서 collectKorean.Count - 1 해 줍니다. 
                    for (int i = 0; i < collectKorean.Count - 1; i++)
                    {
                        // 문자열들을 파일에 저장합니다. 
                        writer.WriteLine(collectKorean[i].TrimEnd());
                    }

                    writer.Close();
                }
            }
            // 1189개의 파일들을 하나의 파일로 생성합니다. 
            if (GUILayout.Button("TestF", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] keywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };
                // c:\Bible\AAA 폴더에 있는 파일들을 모두 읽어 옵니다. 
                string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\BBB", "*.txt", SearchOption.TopDirectoryOnly);
                // c:\Bible\ 폴더에 개역개정A.txt 이라는 이름으로 파일을 저장할 것입니다. 
                string outputFilePath = @"c:\Bible\" + "개역개정A.txt";

                List<string> collectKorean = new List<string>();
                // 폴더에 있는 파일들을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Length; k++)
                {
                    // 한글 키워드들을 대상으로 반복문 처리합니다. 
                    for (int i = 0; i < Koreankeywords.Length; i++)
                    {
                        // 만일 파일의 이름에 "_창" 문자열이 포함되어 있다면? 
                        if (CollectAllFiles[k].Contains("_" + Koreankeywords[i]))
                        {
                            // 파일의 내용을 문자열로 읽어 옵니다. 
                            string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
                            // 저장된 문자열을 '\n' 문자로 구분해서 임시 리스트에 저장합니다. 
                            List<string> temp = new List<string>(textValue.Split('\n'));
                            // 맨 나중에 있는 공백은 임시 리스트에서 없애 줍니다. 
                            temp.RemoveAt(temp.Count - 1);
                            // 임시 리스트에 있는 문자열들을 collectKorean 리스트에 저장합니다. 
                            collectKorean.AddRange(temp);
                        }
                    }
                }
                Debug.Log("collectKorean.Count : " + collectKorean.Count);
                // 파일을 저장할 준비를 합니다. 
                StreamWriter writer = new StreamWriter(outputFilePath);
                // collectKorean 리스트를 대상으로 반복문 처리합니다. 
                for (int i = 0; i < collectKorean.Count; i++)
                {
                    writer.WriteLine(collectKorean[i].Trim());
                }

                writer.Close();
            }
            // 성경별로 각각의 챕터들의 절들이 몇 절들인가를 확인합니다. 
            if (GUILayout.Button("TestD", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] keywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };

                Debug.Log("Koreankeywords.Length : " + Koreankeywords.Length);
                Debug.Log("keywords.Length : " + keywords.Length);
                Debug.Log("chapterCount.Length : " + chapterCount.Length);
                // RevisedRevision 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] RevisedRevision = Resources.LoadAll<TextAsset>("RevisedRevision/");
                // RevisedHangul 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] RevisedHangul = Resources.LoadAll<TextAsset>("RevisedHangul/");
                // EasyBible 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] EasyBible = Resources.LoadAll<TextAsset>("EasyBible/");
                TextAsset[] KoreanBible = Resources.LoadAll<TextAsset>("KoreanBible/");
                // NewStandardTranslation 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] NewStandardTranslation = Resources.LoadAll<TextAsset>("NewStandardTranslation/");
                // NIV 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] NIV = Resources.LoadAll<TextAsset>("NIV/");
                // KJV 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] KJV = Resources.LoadAll<TextAsset>("KJV/");
                // NewKJV 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] NewKJV = Resources.LoadAll<TextAsset>("NewKJV/");
                // ASV 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] ASV = Resources.LoadAll<TextAsset>("ASV/");
                // NewRSV 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] NewRSV = Resources.LoadAll<TextAsset>("NewRSV/");
                // NASB 폴더에 있는 모든 파일들을 읽어 옵니다. 
                TextAsset[] NASB = Resources.LoadAll<TextAsset>("NASB/");
                // 읽어 온 파일들을 배열에 저장합니다. 
                List<TextAsset[]> CollectTextAssets = new List<TextAsset[]>() { RevisedRevision , RevisedHangul , EasyBible , KoreanBible, NewStandardTranslation , NIV, KJV, NewKJV, ASV, NewRSV, NASB};
                // 저장될 파일 이름입니다. 
                StreamWriter streamWriter = new StreamWriter(@"c:\Bible\JustTest.txt");

                string Count = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", RevisedRevision.Length, RevisedHangul.Length, EasyBible.Length, KoreanBible.Length, NewStandardTranslation.Length, NIV.Length, KJV.Length, NewKJV.Length, ASV.Length, NewRSV.Length, NASB.Length);
                // 구분을 위해서 폴더 이름을 파일에 저장합니다. 
                streamWriter.WriteLine("01 : RevisedRevision");
                streamWriter.WriteLine("02 : RevisedHangul");
                streamWriter.WriteLine("03 : EasyBible");
                streamWriter.WriteLine("04 : KoreanBible");
                streamWriter.WriteLine("05 : NewStandardTranslation");
                streamWriter.WriteLine("06 : NIV");
                streamWriter.WriteLine("07 : KJV");
                streamWriter.WriteLine("08 : NewKJV");
                streamWriter.WriteLine("09 : ASV");
                streamWriter.WriteLine("10 : NewRSV");
                streamWriter.WriteLine("11 : NASB");

                {
                    // 한글 키워드를 대상으로 반복문 처리합니다. 
                    for (int m = 0; m < Koreankeywords.Length; m++)
                    {
                        /////////////////////////////////////////////////////
                        streamWriter.WriteLine("\n");
                        // 문자열들을 하나의 문자열로 저장하기 위해서 StringBuilder객체를 생성합니다. 
                        StringBuilder stringBuilder = new StringBuilder();
                        List<string> collect = new List<string>();
                        // stringBuilder에 "01 : "을 추가해 줍니다. 
                        stringBuilder.Append(Koreankeywords[m] + "01 : ");
                        // RevisedRevision 폴더에 있는 파일들을 대상으로 반복문 처리합니다. 
                        for (int i = 0; i < RevisedRevision.Length; i++)
                        {
                            // 파일 이름에 "_창" 문자열이 포함되어 있다면? 
                            if (RevisedRevision[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                // 파일에 있는 문자열을 '\n'로 구분해서 collect 리스트에 저장합니다. 
                                collect = new List<string>(RevisedRevision[i].text.Split('\n'));
                                // stringBuilder에 collect리스트의 사이즈를 저장합니다. 
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }
                        // stringBuilder에 있는 내용을 파일에 저장합니다. 
                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "02 : ");

                        for (int i = 0; i < RevisedHangul.Length; i++)
                        {
                            if (RevisedHangul[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(RevisedHangul[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "03 : ");

                        for (int i = 0; i < EasyBible.Length; i++)
                        {
                            if (EasyBible[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(EasyBible[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////
                        
                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "04 : ");

                        for (int i = 0; i < KoreanBible.Length; i++)
                        {
                            if (KoreanBible[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(KoreanBible[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "05 : ");

                        for (int i = 0; i < NewStandardTranslation.Length; i++)
                        {
                            if (NewStandardTranslation[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(NewStandardTranslation[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "06 : ");

                        for (int i = 0; i < NIV.Length; i++)
                        {
                            if (NIV[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(NIV[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "07 : ");

                        for (int i = 0; i < KJV.Length; i++)
                        {
                            if (KJV[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(KJV[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "08 : ");

                        for (int i = 0; i < NewKJV.Length; i++)
                        {
                            if (NewKJV[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(NewKJV[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "09 : ");

                        for (int i = 0; i < ASV.Length; i++)
                        {
                            if (ASV[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(ASV[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "10 : ");

                        for (int i = 0; i < NewRSV.Length; i++)
                        {
                            if (NewRSV[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(NewRSV[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////
                        stringBuilder = new StringBuilder();
                        collect = new List<string>();
                        stringBuilder.Append(Koreankeywords[m] + "11 : ");

                        for (int i = 0; i < NASB.Length; i++)
                        {
                            if (NASB[i].name.Contains("_" + Koreankeywords[m]))
                            {
                                collect = new List<string>(NASB[i].text.Split('\n'));
                                stringBuilder.Append(collect.Count + ", ");
                            }
                        }

                        streamWriter.WriteLine(stringBuilder.ToString());
                        /////////////////////////////////////////////////////
                        streamWriter.WriteLine("01 : RevisedRevision");
                        streamWriter.WriteLine("02 : RevisedHangul");
                        streamWriter.WriteLine("03 : EasyBible");
                        streamWriter.WriteLine("04 : KoreanBible");
                        streamWriter.WriteLine("05 : NewStandardTranslation");
                        streamWriter.WriteLine("06 : NIV");
                        streamWriter.WriteLine("07 : KJV");
                        streamWriter.WriteLine("08 : NewKJV");
                        streamWriter.WriteLine("09 : ASV");
                        streamWriter.WriteLine("10 : NewRSV");
                        streamWriter.WriteLine("11 : NASB");
                    }
                }
                streamWriter.Close();
            }
            // 성경의 이름이 01로 시작한다면? 01을 "Gen"으로 변경합니다. 01
            if (GUILayout.Button("Test", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] Englishkeywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] NumberCount = {01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 36, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66};
                // Resources에 있는 영어 성경을 가져와서 textFile에 저장합니다. 
                TextAsset textFile = Resources.Load("NASB 영어텍스트") as TextAsset;
                // textFile을 '\n'로 분리해서 collectKorean리스트에 저장합니다. 
                List<string> collectKorean = new List<string>(textFile.text.Split('\n'));
                List<string> collectAll = new List<string>();

                //for (int i = 0; i < collectKorean.Count; i++)
                //{
                //    Debug.Log("collectKorean[i] : " + collectKorean[i]);
                //}

                    string modifyString = string.Empty;
                // collectKorean리스트를 대상으로 반복문 처리합니다. 
                for (int i = 0; i < collectKorean.Count; i++)
                {
                    // 만일 collectKorean리스트의 요소가 "01 "로 시작한다면? "01 "을 영어키워드의 요소로 변경해 줍니다. 
                    if (collectKorean[i].StartsWith("01 ")) modifyString = collectKorean[i].Replace("01 ", Englishkeywords[0]);
                    if (collectKorean[i].StartsWith("02 ")) modifyString = collectKorean[i].Replace("02 ", Englishkeywords[1]); 
                    if (collectKorean[i].StartsWith("03 ")) modifyString = collectKorean[i].Replace("03 ", Englishkeywords[2]); 
                    if (collectKorean[i].StartsWith("04 ")) modifyString = collectKorean[i].Replace("04 ", Englishkeywords[3]);
                    if (collectKorean[i].StartsWith("05 ")) modifyString = collectKorean[i].Replace("05 ", Englishkeywords[4]);
                    if (collectKorean[i].StartsWith("06 ")) modifyString = collectKorean[i].Replace("06 ", Englishkeywords[5]);
                    if (collectKorean[i].StartsWith("07 ")) modifyString = collectKorean[i].Replace("07 ", Englishkeywords[6]);
                    if (collectKorean[i].StartsWith("08 ")) modifyString = collectKorean[i].Replace("08 ", Englishkeywords[7]);
                    if (collectKorean[i].StartsWith("09 ")) modifyString = collectKorean[i].Replace("09 ", Englishkeywords[8]);
                    if (collectKorean[i].StartsWith("10 ")) modifyString = collectKorean[i].Replace("10 ", Englishkeywords[9]);

                    if (collectKorean[i].StartsWith("11 ")) modifyString = collectKorean[i].Replace("11 ", Englishkeywords[10]);
                    if (collectKorean[i].StartsWith("12 ")) modifyString = collectKorean[i].Replace("12 ", Englishkeywords[11]);
                    if (collectKorean[i].StartsWith("13 ")) modifyString = collectKorean[i].Replace("13 ", Englishkeywords[12]);
                    if (collectKorean[i].StartsWith("14 ")) modifyString = collectKorean[i].Replace("14 ", Englishkeywords[13]);
                    if (collectKorean[i].StartsWith("15 ")) modifyString = collectKorean[i].Replace("15 ", Englishkeywords[14]);
                    if (collectKorean[i].StartsWith("16 ")) modifyString = collectKorean[i].Replace("16 ", Englishkeywords[15]);
                    if (collectKorean[i].StartsWith("17 ")) modifyString = collectKorean[i].Replace("17 ", Englishkeywords[16]);
                    if (collectKorean[i].StartsWith("18 ")) modifyString = collectKorean[i].Replace("18 ", Englishkeywords[17]);
                    if (collectKorean[i].StartsWith("19 ")) modifyString = collectKorean[i].Replace("19 ", Englishkeywords[18]);
                    if (collectKorean[i].StartsWith("20 ")) modifyString = collectKorean[i].Replace("20 ", Englishkeywords[19]);

                    if (collectKorean[i].StartsWith("21 ")) modifyString = collectKorean[i].Replace("21 ", Englishkeywords[20]);
                    if (collectKorean[i].StartsWith("22 ")) modifyString = collectKorean[i].Replace("22 ", Englishkeywords[21]);
                    if (collectKorean[i].StartsWith("23 ")) modifyString = collectKorean[i].Replace("23 ", Englishkeywords[22]);
                    if (collectKorean[i].StartsWith("24 ")) modifyString = collectKorean[i].Replace("24 ", Englishkeywords[23]);
                    if (collectKorean[i].StartsWith("25 ")) modifyString = collectKorean[i].Replace("25 ", Englishkeywords[24]);
                    if (collectKorean[i].StartsWith("26 ")) modifyString = collectKorean[i].Replace("26 ", Englishkeywords[25]);
                    if (collectKorean[i].StartsWith("27 ")) modifyString = collectKorean[i].Replace("27 ", Englishkeywords[26]);
                    if (collectKorean[i].StartsWith("28 ")) modifyString = collectKorean[i].Replace("28 ", Englishkeywords[27]);
                    if (collectKorean[i].StartsWith("29 ")) modifyString = collectKorean[i].Replace("29 ", Englishkeywords[28]);
                    if (collectKorean[i].StartsWith("30 ")) modifyString = collectKorean[i].Replace("30 ", Englishkeywords[29]);

                    if (collectKorean[i].StartsWith("31 ")) modifyString = collectKorean[i].Replace("31 ", Englishkeywords[30]);
                    if (collectKorean[i].StartsWith("32 ")) modifyString = collectKorean[i].Replace("32 ", Englishkeywords[31]);
                    if (collectKorean[i].StartsWith("33 ")) modifyString = collectKorean[i].Replace("33 ", Englishkeywords[32]);
                    if (collectKorean[i].StartsWith("34 ")) modifyString = collectKorean[i].Replace("34 ", Englishkeywords[33]);
                    if (collectKorean[i].StartsWith("35 ")) modifyString = collectKorean[i].Replace("35 ", Englishkeywords[34]);
                    if (collectKorean[i].StartsWith("36 ")) modifyString = collectKorean[i].Replace("36 ", Englishkeywords[35]);
                    if (collectKorean[i].StartsWith("37 ")) modifyString = collectKorean[i].Replace("37 ", Englishkeywords[36]);
                    if (collectKorean[i].StartsWith("38 ")) modifyString = collectKorean[i].Replace("38 ", Englishkeywords[37]);
                    if (collectKorean[i].StartsWith("39 ")) modifyString = collectKorean[i].Replace("39 ", Englishkeywords[38]);
                    if (collectKorean[i].StartsWith("40 ")) modifyString = collectKorean[i].Replace("40 ", Englishkeywords[39]);

                    if (collectKorean[i].StartsWith("41 ")) modifyString = collectKorean[i].Replace("41 ", Englishkeywords[40]);
                    if (collectKorean[i].StartsWith("42 ")) modifyString = collectKorean[i].Replace("42 ", Englishkeywords[41]);
                    if (collectKorean[i].StartsWith("43 ")) modifyString = collectKorean[i].Replace("43 ", Englishkeywords[42]);
                    if (collectKorean[i].StartsWith("44 ")) modifyString = collectKorean[i].Replace("44 ", Englishkeywords[43]);
                    if (collectKorean[i].StartsWith("45 ")) modifyString = collectKorean[i].Replace("45 ", Englishkeywords[44]);
                    if (collectKorean[i].StartsWith("46 ")) modifyString = collectKorean[i].Replace("46 ", Englishkeywords[45]);
                    if (collectKorean[i].StartsWith("47 ")) modifyString = collectKorean[i].Replace("47 ", Englishkeywords[46]);
                    if (collectKorean[i].StartsWith("48 ")) modifyString = collectKorean[i].Replace("48 ", Englishkeywords[47]);
                    if (collectKorean[i].StartsWith("49 ")) modifyString = collectKorean[i].Replace("49 ", Englishkeywords[48]);
                    if (collectKorean[i].StartsWith("50 ")) modifyString = collectKorean[i].Replace("50 ", Englishkeywords[49]);

                    if (collectKorean[i].StartsWith("51 ")) modifyString = collectKorean[i].Replace("51 ", Englishkeywords[50]);
                    if (collectKorean[i].StartsWith("52 ")) modifyString = collectKorean[i].Replace("52 ", Englishkeywords[51]);
                    if (collectKorean[i].StartsWith("53 ")) modifyString = collectKorean[i].Replace("53 ", Englishkeywords[52]);
                    if (collectKorean[i].StartsWith("54 ")) modifyString = collectKorean[i].Replace("54 ", Englishkeywords[53]);
                    if (collectKorean[i].StartsWith("55 ")) modifyString = collectKorean[i].Replace("55 ", Englishkeywords[54]);
                    if (collectKorean[i].StartsWith("56 ")) modifyString = collectKorean[i].Replace("56 ", Englishkeywords[55]);
                    if (collectKorean[i].StartsWith("57 ")) modifyString = collectKorean[i].Replace("57 ", Englishkeywords[56]);
                    if (collectKorean[i].StartsWith("58 ")) modifyString = collectKorean[i].Replace("58 ", Englishkeywords[57]);
                    if (collectKorean[i].StartsWith("59 ")) modifyString = collectKorean[i].Replace("59 ", Englishkeywords[58]);
                    if (collectKorean[i].StartsWith("60 ")) modifyString = collectKorean[i].Replace("60 ", Englishkeywords[59]);

                    if (collectKorean[i].StartsWith("61 ")) modifyString = collectKorean[i].Replace("61 ", Englishkeywords[60]);
                    if (collectKorean[i].StartsWith("62 ")) modifyString = collectKorean[i].Replace("62 ", Englishkeywords[61]);
                    if (collectKorean[i].StartsWith("63 ")) modifyString = collectKorean[i].Replace("63 ", Englishkeywords[62]);
                    if (collectKorean[i].StartsWith("64 ")) modifyString = collectKorean[i].Replace("64 ", Englishkeywords[63]);
                    if (collectKorean[i].StartsWith("65 ")) modifyString = collectKorean[i].Replace("65 ", Englishkeywords[64]);
                    if (collectKorean[i].StartsWith("66 ")) modifyString = collectKorean[i].Replace("66 ", Englishkeywords[65]);
                    // 수정된 파일 이름을 collectAll리스트에 저장합니다. 
                    collectAll.Add(modifyString);

                    //for (int j = 0; j < NumberCount.Length; j++)
                    //{
                    //    if (collectKorean[i].StartsWith(NumberCount[j] + " "))
                    //    {
                    //        modifyString = collectKorean[i].Replace(NumberCount[j] + " ", Englishkeywords[j]);
                    //        // Debug.Log("modifyString : " + modifyString);
                    //        collectAll.Add(modifyString);
                    //    }
                    //}
                }

                string folderPath = @"c:\Bible\EEE\";
                // 저장될 파일 이름입니다. 
                string outputFilePath = folderPath + "NASB 영어텍스트A" + ".txt";
                // 파일로 저장할 준비를 합니다. 
                StreamWriter writer = new StreamWriter(outputFilePath);
                // collectAll리스트를 대상으로 반복문 처리합니다. 
                for (int i = 0; i < collectAll.Count; i++)
                {
                    // 파일을 저장하고 공백은 없애 줍니다. 
                    writer.WriteLine(collectAll[i].Trim());
                }

                writer.Close();

                Debug.Log("collectKorean.Count : " + collectKorean.Count);
                Debug.Log("collectAll.Count : " + collectAll.Count);
            }
            // 성경에서 특정 문자열로 검색합니다. 
            if (GUILayout.Button("SearchFor", GUILayout.Height(30)))
            {
                // string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\DDD", "*.txt", SearchOption.TopDirectoryOnly);
                // Resources 폴더의 ModernManBible의 모든 파일들을 읽어 옵니다. 
                TextAsset[] CollectAllFiles = Resources.LoadAll<TextAsset>("ModernManBible/");

                Debug.Log("CollectAllFiles[0].name : " + CollectAllFiles[0].name);

                 List<string> collectKorean = new List<string>();

                List<string> CollectSearchForWordInBible = new List<string>();
                // 찾을 문자열입니다. 
                string searchForWord = "외아들";
                // 파일이 저장될 폴더 이름입니다. 만일 폴더 이름이 없을 경우에는 생성해 줍니다. 
                string folderPath = @"c:\BibleSearch\";
                DirectoryInfo di = new DirectoryInfo(folderPath);

                // 만약 폴더가 존재하지 않으면
                if (di.Exists == false)
                {
                    di.Create();
                }
                // 저장될 문자열입니다. 
                string outputFilePath = folderPath + searchForWord + ".txt";

                string replaceWord = string.Empty;
                string modifyWord = string.Empty;
                // 모든 파일들을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Length; k++)
                {
                    // CollectAllFiles배열의 요소를 문자열로 변경해 줍니다. 
                    string textValue = CollectAllFiles[k].text;
                    // 변경된 문자열을 '\n'로 구분해서 collectKorean리스트에 저장해 줍니다. 
                    collectKorean = new List<string>(textValue.Split('\n'));
                    // collectKorean리슽트를 대상으로 반복문 처리해 줍니다. 
                    for (int j = 0; j < collectKorean.Count; j++)
                    {
                        // 만일 collectKorean리스트의 요소에 찾을 문자열이 포함되어 있다면? 
                        if (collectKorean[j].Contains(searchForWord))
                        {
                            // 찾을 문자열이 있는 문장을 CollectSearchForWordInBible리스트에 저장합니다. 
                            CollectSearchForWordInBible.Add(collectKorean[j]);
                            replaceWord = string.Empty;
                            // CollectAllFiles배열의 요소의 이름을 저장합니다. 
                            replaceWord = CollectAllFiles[k].name;
                            // 만일 CollectAllFiles배열의 요소의 이름이 "현대인의성경_창"이라면? "현대인의성경_창"를 "창세기 "로 변경합니다. 
                            if (replaceWord.StartsWith("현대인의성경_창")) modifyWord = replaceWord.Replace("현대인의성경_창", "창세기 ");
                            if (replaceWord.StartsWith("현대인의성경_출")) modifyWord = replaceWord.Replace("현대인의성경_출", "출애굽기 ");
                            if (replaceWord.StartsWith("현대인의성경_레")) modifyWord = replaceWord.Replace("현대인의성경_레", "레위기 ");
                            if (replaceWord.StartsWith("현대인의성경_민")) modifyWord = replaceWord.Replace("현대인의성경_민", "민수기 ");
                            if (replaceWord.StartsWith("현대인의성경_신")) modifyWord = replaceWord.Replace("현대인의성경_신", "신명기 ");
                            if (replaceWord.StartsWith("현대인의성경_수")) modifyWord = replaceWord.Replace("현대인의성경_수", "여호수아 ");
                            if (replaceWord.StartsWith("현대인의성경_삿")) modifyWord = replaceWord.Replace("현대인의성경_삿", "사사기 ");
                            if (replaceWord.StartsWith("현대인의성경_룻")) modifyWord = replaceWord.Replace("현대인의성경_룻", "룻기 ");
                            if (replaceWord.StartsWith("현대인의성경_삼상")) modifyWord = replaceWord.Replace("현대인의성경_삼상", "사무엘상 ");
                            if (replaceWord.StartsWith("현대인의성경_삼하")) modifyWord = replaceWord.Replace("현대인의성경_삼하", "사무엘하 ");
                            if (replaceWord.StartsWith("현대인의성경_왕상")) modifyWord = replaceWord.Replace("현대인의성경_왕상", "열왕기상 ");
                            if (replaceWord.StartsWith("현대인의성경_왕하")) modifyWord = replaceWord.Replace("현대인의성경_왕하", "열왕기하 ");
                            if (replaceWord.StartsWith("현대인의성경_대상")) modifyWord = replaceWord.Replace("현대인의성경_대상", "역대상 ");
                            if (replaceWord.StartsWith("현대인의성경_대하")) modifyWord = replaceWord.Replace("현대인의성경_대하", "역대하 ");
                            if (replaceWord.StartsWith("현대인의성경_스")) modifyWord = replaceWord.Replace("현대인의성경_스", "에스라 ");
                            if (replaceWord.StartsWith("현대인의성경_느")) modifyWord = replaceWord.Replace("현대인의성경_느", "느헤미야 ");
                            if (replaceWord.StartsWith("현대인의성경_에")) modifyWord = replaceWord.Replace("현대인의성경_에", "에스더 ");
                            if (replaceWord.StartsWith("현대인의성경_욥")) modifyWord = replaceWord.Replace("현대인의성경_욥", "욥기 ");
                            if (replaceWord.StartsWith("현대인의성경_시")) modifyWord = replaceWord.Replace("현대인의성경_시", "시편 ");
                            if (replaceWord.StartsWith("현대인의성경_잠")) modifyWord = replaceWord.Replace("현대인의성경_잠", "잠언 ");
                            if (replaceWord.StartsWith("현대인의성경_전")) modifyWord = replaceWord.Replace("현대인의성경_전", "전도서 ");
                            if (replaceWord.StartsWith("현대인의성경_아")) modifyWord = replaceWord.Replace("현대인의성경_아", "아가서 ");
                            if (replaceWord.StartsWith("현대인의성경_사")) modifyWord = replaceWord.Replace("현대인의성경_사", "이사야 ");
                            if (replaceWord.StartsWith("현대인의성경_렘")) modifyWord = replaceWord.Replace("현대인의성경_렘", "예레미야 ");
                            if (replaceWord.StartsWith("현대인의성경_애")) modifyWord = replaceWord.Replace("현대인의성경_애", "예레미야애가 ");
                            if (replaceWord.StartsWith("현대인의성경_겔")) modifyWord = replaceWord.Replace("현대인의성경_겔", "에스겔 ");
                            if (replaceWord.StartsWith("현대인의성경_단")) modifyWord = replaceWord.Replace("현대인의성경_단", "다니엘 ");
                            if (replaceWord.StartsWith("현대인의성경_호")) modifyWord = replaceWord.Replace("현대인의성경_호", "호세아 ");
                            if (replaceWord.StartsWith("현대인의성경_욜")) modifyWord = replaceWord.Replace("현대인의성경_욜", "요엘 ");
                            if (replaceWord.StartsWith("현대인의성경_암")) modifyWord = replaceWord.Replace("현대인의성경_암", "아모스 ");
                            if (replaceWord.StartsWith("현대인의성경_옵")) modifyWord = replaceWord.Replace("현대인의성경_옵", "오바댜 ");
                            if (replaceWord.StartsWith("현대인의성경_욘")) modifyWord = replaceWord.Replace("현대인의성경_욘", "요나 ");
                            if (replaceWord.StartsWith("현대인의성경_미")) modifyWord = replaceWord.Replace("현대인의성경_미", "미가 ");
                            if (replaceWord.StartsWith("현대인의성경_나")) modifyWord = replaceWord.Replace("현대인의성경_나", "나훔 ");
                            if (replaceWord.StartsWith("현대인의성경_합")) modifyWord = replaceWord.Replace("현대인의성경_합", "하박국 ");
                            if (replaceWord.StartsWith("현대인의성경_습")) modifyWord = replaceWord.Replace("현대인의성경_습", "스바냐 ");
                            if (replaceWord.StartsWith("현대인의성경_학")) modifyWord = replaceWord.Replace("현대인의성경_학", "학개 ");
                            if (replaceWord.StartsWith("현대인의성경_슥")) modifyWord = replaceWord.Replace("현대인의성경_슥", "스가랴 ");
                            if (replaceWord.StartsWith("현대인의성경_말")) modifyWord = replaceWord.Replace("현대인의성경_말", "말라기 ");
                            if (replaceWord.StartsWith("현대인의성경_마")) modifyWord = replaceWord.Replace("현대인의성경_마", "마태복음 ");
                            if (replaceWord.StartsWith("현대인의성경_막")) modifyWord = replaceWord.Replace("현대인의성경_막", "마가복음 ");
                            if (replaceWord.StartsWith("현대인의성경_눅")) modifyWord = replaceWord.Replace("현대인의성경_눅", "누가복음 ");
                            if (replaceWord.StartsWith("현대인의성경_요")) modifyWord = replaceWord.Replace("현대인의성경_요", "요한복음 ");
                            if (replaceWord.StartsWith("현대인의성경_행")) modifyWord = replaceWord.Replace("현대인의성경_행", "사도행전 ");
                            if (replaceWord.StartsWith("현대인의성경_롬")) modifyWord = replaceWord.Replace("현대인의성경_롬", "로마서 ");
                            if (replaceWord.StartsWith("현대인의성경_고전")) modifyWord = replaceWord.Replace("현대인의성경_고전", "고린도전서 ");
                            if (replaceWord.StartsWith("현대인의성경_고후")) modifyWord = replaceWord.Replace("현대인의성경_고후", "고린도후서 ");
                            if (replaceWord.StartsWith("현대인의성경_갈")) modifyWord = replaceWord.Replace("현대인의성경_갈", "갈라디아서 ");
                            if (replaceWord.StartsWith("현대인의성경_엡")) modifyWord = replaceWord.Replace("현대인의성경_엡", "에베소서 ");
                            if (replaceWord.StartsWith("현대인의성경_빌")) modifyWord = replaceWord.Replace("현대인의성경_빌", "빌립보서 ");
                            if (replaceWord.StartsWith("현대인의성경_골")) modifyWord = replaceWord.Replace("현대인의성경_골", "골로새서 ");
                            if (replaceWord.StartsWith("현대인의성경_살전")) modifyWord = replaceWord.Replace("현대인의성경_살전", "데살로니가전서 ");
                            if (replaceWord.StartsWith("현대인의성경_살후")) modifyWord = replaceWord.Replace("현대인의성경_살후", "데살로니가후서 ");
                            if (replaceWord.StartsWith("현대인의성경_딤전")) modifyWord = replaceWord.Replace("현대인의성경_딤전", "디모데전서 ");
                            if (replaceWord.StartsWith("현대인의성경_딤후")) modifyWord = replaceWord.Replace("현대인의성경_딤후", "디모데후서 ");
                            if (replaceWord.StartsWith("현대인의성경_딛")) modifyWord = replaceWord.Replace("현대인의성경_딛", "디도서 ");
                            if (replaceWord.StartsWith("현대인의성경_몬")) modifyWord = replaceWord.Replace("현대인의성경_몬", "빌레몬서 ");
                            if (replaceWord.StartsWith("현대인의성경_히")) modifyWord = replaceWord.Replace("현대인의성경_히", "히브리서 ");
                            if (replaceWord.StartsWith("현대인의성경_약")) modifyWord = replaceWord.Replace("현대인의성경_약", "야고보서 ");
                            if (replaceWord.StartsWith("현대인의성경_벧전")) modifyWord = replaceWord.Replace("현대인의성경_벧전", "베드로전서 ");
                            if (replaceWord.StartsWith("현대인의성경_벧후")) modifyWord = replaceWord.Replace("현대인의성경_벧후", "베드로후서 ");
                            if (replaceWord.StartsWith("현대인의성경_요일")) modifyWord = replaceWord.Replace("현대인의성경_요일", "요한일서 ");
                            if (replaceWord.StartsWith("현대인의성경_요이")) modifyWord = replaceWord.Replace("현대인의성경_요이", "요한이서 ");
                            if (replaceWord.StartsWith("현대인의성경_요삼")) modifyWord = replaceWord.Replace("현대인의성경_요삼", "요한삼서 ");
                            if (replaceWord.StartsWith("현대인의성경_유")) modifyWord = replaceWord.Replace("현대인의성경_유", "유다서 ");
                            if (replaceWord.StartsWith("현대인의성경_계")) modifyWord = replaceWord.Replace("현대인의성경_계", "요한계시록 ");

                            // 저장해 줍니다. 
                            CollectSearchForWordInBible.Add(modifyWord + "장 " + j + "절");
                            // 공백을 추가해 줍니다. 
                            CollectSearchForWordInBible.Add(string.Empty);
                        }
                    }
                }

                StreamWriter writer = new StreamWriter(outputFilePath);

                for (int i = 0; i < CollectSearchForWordInBible.Count; i++)
                {
                    writer.WriteLine(CollectSearchForWordInBible[i].Trim());
                }

                writer.Close();





                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\DDD", "*.txt", SearchOption.TopDirectoryOnly);

                //List<string> collectKorean = new List<string>();

                //List<string> CollectSearchForWordInBible = new List<string>();

                //string searchForWord = "십일조";

                //string outputFilePath = @"c:\Bible\EEE\" + searchForWord + ".txt";

                //string replaceWord = string.Empty;
                //string modifyWord = string.Empty;
                //for (int k = 0; k < CollectAllFiles.Length; k++)
                //{
                //    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);

                //    collectKorean = new List<string>(textValue.Split('\n'));

                //    for (int j = 0; j < collectKorean.Count; j++)
                //    {
                //        if (collectKorean[j].Contains(searchForWord))
                //        {
                //            CollectSearchForWordInBible.Add(collectKorean[j]);
                //            replaceWord = string.Empty;
                //            replaceWord = System.IO.Path.GetFileNameWithoutExtension(CollectAllFiles[k]);

                //            if (replaceWord.StartsWith("현대인의성경_창")) modifyWord = replaceWord.Replace("현대인의성경_창", "창세기 ");
                //            if (replaceWord.StartsWith("현대인의성경_출")) modifyWord = replaceWord.Replace("현대인의성경_출", "출애굽기 ");
                //            if (replaceWord.StartsWith("현대인의성경_레")) modifyWord = replaceWord.Replace("현대인의성경_레", "레위기 ");
                //            if (replaceWord.StartsWith("현대인의성경_민")) modifyWord = replaceWord.Replace("현대인의성경_민", "민수기 ");
                //            if (replaceWord.StartsWith("현대인의성경_신")) modifyWord = replaceWord.Replace("현대인의성경_신", "신명기 ");
                //            if (replaceWord.StartsWith("현대인의성경_수")) modifyWord = replaceWord.Replace("현대인의성경_수", "여호수아 ");
                //            if (replaceWord.StartsWith("현대인의성경_삿")) modifyWord = replaceWord.Replace("현대인의성경_삿", "사사기 ");
                //            if (replaceWord.StartsWith("현대인의성경_룻")) modifyWord = replaceWord.Replace("현대인의성경_룻", "룻기 ");
                //            if (replaceWord.StartsWith("현대인의성경_삼상")) modifyWord = replaceWord.Replace("현대인의성경_삼상", "사무엘상 ");
                //            if (replaceWord.StartsWith("현대인의성경_삼하")) modifyWord = replaceWord.Replace("현대인의성경_삼하", "사무엘하 ");
                //            if (replaceWord.StartsWith("현대인의성경_왕상")) modifyWord = replaceWord.Replace("현대인의성경_왕상", "열왕기상 ");
                //            if (replaceWord.StartsWith("현대인의성경_왕하")) modifyWord = replaceWord.Replace("현대인의성경_왕하", "열왕기하 ");
                //            if (replaceWord.StartsWith("현대인의성경_대상")) modifyWord = replaceWord.Replace("현대인의성경_대상", "역대상 ");
                //            if (replaceWord.StartsWith("현대인의성경_대하")) modifyWord = replaceWord.Replace("현대인의성경_대하", "역대하 ");
                //            if (replaceWord.StartsWith("현대인의성경_스")) modifyWord = replaceWord.Replace("현대인의성경_스", "에스라 ");
                //            if (replaceWord.StartsWith("현대인의성경_느")) modifyWord = replaceWord.Replace("현대인의성경_느", "느헤미야 ");
                //            if (replaceWord.StartsWith("현대인의성경_에")) modifyWord = replaceWord.Replace("현대인의성경_에", "에스더 ");
                //            if (replaceWord.StartsWith("현대인의성경_욥")) modifyWord = replaceWord.Replace("현대인의성경_욥", "욥기 ");
                //            if (replaceWord.StartsWith("현대인의성경_시")) modifyWord = replaceWord.Replace("현대인의성경_시", "시편 ");
                //            if (replaceWord.StartsWith("현대인의성경_잠")) modifyWord = replaceWord.Replace("현대인의성경_잠", "잠언 ");
                //            if (replaceWord.StartsWith("현대인의성경_전")) modifyWord = replaceWord.Replace("현대인의성경_전", "전도서 ");
                //            if (replaceWord.StartsWith("현대인의성경_아")) modifyWord = replaceWord.Replace("현대인의성경_아", "아가서 ");
                //            if (replaceWord.StartsWith("현대인의성경_사")) modifyWord = replaceWord.Replace("현대인의성경_사", "이사야 ");
                //            if (replaceWord.StartsWith("현대인의성경_렘")) modifyWord = replaceWord.Replace("현대인의성경_렘", "예레미야 ");
                //            if (replaceWord.StartsWith("현대인의성경_애")) modifyWord = replaceWord.Replace("현대인의성경_애", "예레미야애가 ");
                //            if (replaceWord.StartsWith("현대인의성경_겔")) modifyWord = replaceWord.Replace("현대인의성경_겔", "에스겔 ");
                //            if (replaceWord.StartsWith("현대인의성경_단")) modifyWord = replaceWord.Replace("현대인의성경_단", "다니엘 ");
                //            if (replaceWord.StartsWith("현대인의성경_호")) modifyWord = replaceWord.Replace("현대인의성경_호", "호세아 ");
                //            if (replaceWord.StartsWith("현대인의성경_욜")) modifyWord = replaceWord.Replace("현대인의성경_욜", "요엘 ");
                //            if (replaceWord.StartsWith("현대인의성경_암")) modifyWord = replaceWord.Replace("현대인의성경_암", "아모스 ");
                //            if (replaceWord.StartsWith("현대인의성경_옵")) modifyWord = replaceWord.Replace("현대인의성경_옵", "오바댜 ");
                //            if (replaceWord.StartsWith("현대인의성경_욘")) modifyWord = replaceWord.Replace("현대인의성경_욘", "요나 ");
                //            if (replaceWord.StartsWith("현대인의성경_미")) modifyWord = replaceWord.Replace("현대인의성경_미", "미가 ");
                //            if (replaceWord.StartsWith("현대인의성경_나")) modifyWord = replaceWord.Replace("현대인의성경_나", "나훔 ");
                //            if (replaceWord.StartsWith("현대인의성경_합")) modifyWord = replaceWord.Replace("현대인의성경_합", "하박국 ");
                //            if (replaceWord.StartsWith("현대인의성경_습")) modifyWord = replaceWord.Replace("현대인의성경_습", "스바냐 ");
                //            if (replaceWord.StartsWith("현대인의성경_학")) modifyWord = replaceWord.Replace("현대인의성경_학", "학개 ");
                //            if (replaceWord.StartsWith("현대인의성경_슥")) modifyWord = replaceWord.Replace("현대인의성경_슥", "스가랴 ");
                //            if (replaceWord.StartsWith("현대인의성경_말")) modifyWord = replaceWord.Replace("현대인의성경_말", "말라기 ");
                //            if (replaceWord.StartsWith("현대인의성경_마")) modifyWord = replaceWord.Replace("현대인의성경_마", "마태복음 ");
                //            if (replaceWord.StartsWith("현대인의성경_막")) modifyWord = replaceWord.Replace("현대인의성경_막", "마가복음 ");
                //            if (replaceWord.StartsWith("현대인의성경_눅")) modifyWord = replaceWord.Replace("현대인의성경_눅", "누가복음 ");
                //            if (replaceWord.StartsWith("현대인의성경_요")) modifyWord = replaceWord.Replace("현대인의성경_요", "요한복음 ");
                //            if (replaceWord.StartsWith("현대인의성경_행")) modifyWord = replaceWord.Replace("현대인의성경_행", "사도행전 ");
                //            if (replaceWord.StartsWith("현대인의성경_롬")) modifyWord = replaceWord.Replace("현대인의성경_롬", "로마서 ");
                //            if (replaceWord.StartsWith("현대인의성경_고전")) modifyWord = replaceWord.Replace("현대인의성경_고전", "고린도전서 ");
                //            if (replaceWord.StartsWith("현대인의성경_고후")) modifyWord = replaceWord.Replace("현대인의성경_고후", "고린도후서 ");
                //            if (replaceWord.StartsWith("현대인의성경_갈")) modifyWord = replaceWord.Replace("현대인의성경_갈", "갈라디아서 ");
                //            if (replaceWord.StartsWith("현대인의성경_엡")) modifyWord = replaceWord.Replace("현대인의성경_엡", "에베소서 ");
                //            if (replaceWord.StartsWith("현대인의성경_빌")) modifyWord = replaceWord.Replace("현대인의성경_빌", "빌립보서 ");
                //            if (replaceWord.StartsWith("현대인의성경_골")) modifyWord = replaceWord.Replace("현대인의성경_골", "골로새서 ");
                //            if (replaceWord.StartsWith("현대인의성경_살전")) modifyWord = replaceWord.Replace("현대인의성경_살전", "데살로니가전서 ");
                //            if (replaceWord.StartsWith("현대인의성경_살후")) modifyWord = replaceWord.Replace("현대인의성경_살후", "데살로니가후서 ");
                //            if (replaceWord.StartsWith("현대인의성경_딤전")) modifyWord = replaceWord.Replace("현대인의성경_딤전", "디모데전서 ");
                //            if (replaceWord.StartsWith("현대인의성경_딤후")) modifyWord = replaceWord.Replace("현대인의성경_딤후", "디모데후서 ");
                //            if (replaceWord.StartsWith("현대인의성경_딛")) modifyWord = replaceWord.Replace("현대인의성경_딛", "디도서 ");
                //            if (replaceWord.StartsWith("현대인의성경_몬")) modifyWord = replaceWord.Replace("현대인의성경_몬", "빌레몬서 ");
                //            if (replaceWord.StartsWith("현대인의성경_히")) modifyWord = replaceWord.Replace("현대인의성경_히", "히브리서 ");
                //            if (replaceWord.StartsWith("현대인의성경_약")) modifyWord = replaceWord.Replace("현대인의성경_약", "야고보서 ");
                //            if (replaceWord.StartsWith("현대인의성경_벧전")) modifyWord = replaceWord.Replace("현대인의성경_벧전", "베드로전서 ");
                //            if (replaceWord.StartsWith("현대인의성경_벧후")) modifyWord = replaceWord.Replace("현대인의성경_벧후", "베드로후서 ");
                //            if (replaceWord.StartsWith("현대인의성경_요일")) modifyWord = replaceWord.Replace("현대인의성경_요일", "요한일서 ");
                //            if (replaceWord.StartsWith("현대인의성경_요이")) modifyWord = replaceWord.Replace("현대인의성경_요이", "요한이서 ");
                //            if (replaceWord.StartsWith("현대인의성경_요삼")) modifyWord = replaceWord.Replace("현대인의성경_요삼", "요한삼서 ");
                //            if (replaceWord.StartsWith("현대인의성경_유")) modifyWord = replaceWord.Replace("현대인의성경_유", "유다서 ");
                //            if (replaceWord.StartsWith("현대인의성경_계")) modifyWord = replaceWord.Replace("현대인의성경_계", "요한계시록 ");


                //            CollectSearchForWordInBible.Add(modifyWord + "장 " + j + "절");

                //            CollectSearchForWordInBible.Add(string.Empty);
                //        }
                //    }
                //}

                //StreamWriter writer = new StreamWriter(outputFilePath);

                //for (int i = 0; i < CollectSearchForWordInBible.Count; i++)
                //{
                //    writer.WriteLine(CollectSearchForWordInBible[i].Trim());
                //}

                //writer.Close();
            }
            // 성경파일의 내용에 공백이 있다면 공백을 없애주고 저장합니다. 
            if (GUILayout.Button("A", GUILayout.Height(30)))
            {
                // c:\Bible\AAA 폴더의 모든 파일들을 가져옵니다. 
                string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.TopDirectoryOnly);

                string outputFilePath = string.Empty;

                List<string> collectKorean = new List<string>();
                // 모든 파일들을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Length; k++)
                {
                    // 파일의 내용을 문자열로 저장합니다. 
                    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
                    // 파일의 이름을 경로는 무시하고 파일이름만 가져옵니다. 
                    string fileName = Path.GetFileName(CollectAllFiles[k]);
                    // 저장될 파일 이름입니다. 
                    outputFilePath = @"c:\Bible\BBB\" + fileName;
                    // 파일의 내용이 저장된 문자열을 '\n'로 분리해서 collectKorean리스트에 저장합니다. 
                    collectKorean = new List<string>(textValue.Split('\n'));

                    Debug.Log("collectKorean.Count : " + collectKorean.Count);
                    // 파일을 저장할 준비를 합니다. 
                    StreamWriter writer = new StreamWriter(outputFilePath);
                    // collectKorean리스트를 대상으로 반복문 처리합니다. 
                    for (int i = 0; i < collectKorean.Count; i++)
                    {
                        // collectKorean리스트의 요소를 파일에 저장하고 앞뒤 공백이 있다면 없애 줍니다. 
                        writer.WriteLine(collectKorean[i].Trim());
                    }

                    writer.Close();
                }

                




                //string textA = @"c:\Bible\AAA\영단어004C.txt";

                //string textValueA = System.IO.File.ReadAllText(textA);
                //List<string> collectEnglishA = new List<string>(textValueA.Split('\n'));

                //// int[] range = { 0, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800, 3000, 3200, 3400, 3600, 3800, 4000, 4200, 4400, 4600, 4800, 5000, 5200, 5400, 5600, 5800, 5998};
                //// int[] range = { 0, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800, 3000, 3200, 3400, 3600, 3800, 4000, 4200, 4400, 4574 };
                //int[] range = { 0, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2444};

                //int startIndex = 0;
                //int endIndex = 1;

                //for (int i = 0; i < range.Length; i++)
                //{
                //    string outputFilePathA = @"c:\Bible\BBB\영단어004C" + i + ".txt";
                //    List<string> collectAll = new List<string>();


                //    for (int j = range[startIndex]; j < range[endIndex]; j++)
                //    {
                //        collectAll.Add(collectEnglishA[j]);
                //    }

                //    StreamWriter writer = new StreamWriter(outputFilePathA);

                //    for (int j = 0; j < collectAll.Count; j++)
                //    {
                //        writer.WriteLine(collectAll[j].TrimEnd());
                //    }

                //    writer.Close();

                //    startIndex++;
                //    endIndex++;
                //}



                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);

                //for (int k = 0; k < CollectAllFiles.Length; k++)
                //{
                //    string fileName = Path.GetFileName(CollectAllFiles[k]);
                //    string outputFilePath = @"c:\Bible\BBB\" + fileName;

                //    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);

                //    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                //    List<string> collectKoreanModify = new List<string>();

                //    Debug.Log("collectKorean.Count : " + collectKorean.Count);

                //    for (int i = 0; i < collectKorean.Count; i++)
                //    {
                //        collectKoreanModify.Add(collectKorean[i]);                        
                //    }

                //    Debug.Log("collectKoreanModify.Count : " + collectKoreanModify.Count);

                //    StreamWriter writer = new StreamWriter(outputFilePath);

                //    for (int i = 0; i < collectKoreanModify.Count; i++)
                //    {
                //        writer.WriteLine(collectKoreanModify[i].Trim());
                //        // writer.WriteLine(collectKoreanModify[i]);
                //    }

                //    writer.Close();
                //}



                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);

                //for (int k = 0; k < CollectAllFiles.Length; k++)
                //{
                //    string fileName = Path.GetFileName(CollectAllFiles[k]);
                //    string outputFilePath = @"c:\Bible\BBB\" + fileName;

                //    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);

                //    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                //    List<string> collectKoreanModify = new List<string>();

                //    for (int i = 0; i < collectKorean.Count; i = i + 2)
                //    {
                //        //if (i % 2 == 0)
                //        //    continue;

                //        collectKoreanModify.Add(collectKorean[i]);
                //    }

                //    StreamWriter writer = new StreamWriter(outputFilePath);

                //    for (int i = 0; i < collectKoreanModify.Count; i++)
                //    {
                //        writer.WriteLine(collectKoreanModify[i].TrimEnd());
                //    }

                //    writer.Close();
                //}



                //string textA = @"c:\Bible\AAA\영단어B영어_001A.txt";
                //string textB = @"c:\Bible\AAA\영단어B한글_001A.txt";

                //string outputFilePathA = @"c:\Bible\BBB\" + "TextC.txt";

                //string textValueA = System.IO.File.ReadAllText(textA);
                //List<string> collectEnglishA = new List<string>(textValueA.Split('\n'));
                //string textValueB = System.IO.File.ReadAllText(textB);
                //List<string> collectEnglishB = new List<string>(textValueB.Split('\n'));

                //List<string> collectAll = new List<string>();

                //for (int i = 0; i < collectEnglishA.Count; i++)
                //{
                //    collectAll.Add(collectEnglishA[i]);
                //    collectAll.Add(collectEnglishB[i]);
                //}

                //StreamWriter writer = new StreamWriter(outputFilePathA);

                //for (int i = 0; i < collectAll.Count; i++)
                //{
                //    writer.WriteLine(collectAll[i].TrimEnd());
                //}

                //writer.Close();

                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);

                //for (int k = 0; k < CollectAllFiles.Length; k++)
                //{
                //    string fileName = Path.GetFileName(CollectAllFiles[k]);
                //    string outputFilePath = @"c:\Bible\BBB\" + fileName;

                //    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);

                //    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                //    List<string> collectKoreanModify = new List<string>();

                //    for (int i = 0; i < collectKorean.Count - 1; i++)
                //    {
                //        if (!collectKorean[i].StartsWith(" "))
                //            collectKoreanModify.Add(collectKorean[i]);
                //    }

                //    StreamWriter writer = new StreamWriter(outputFilePath);

                //    for (int i = 0; i < collectKoreanModify.Count; i++)
                //    {
                //        writer.WriteLine(collectKoreanModify[i].TrimEnd());
                //    }

                //    writer.Close();
                //}



                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);

                //for (int k = 0; k < CollectAllFiles.Length; k++)
                //{
                //    string fileName = Path.GetFileName(CollectAllFiles[k]);
                //    string outputFilePath = @"c:\Bible\BBB\" + fileName;

                //    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);

                //    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                //    List<string> collectKoreanModify = new List<string>();

                //    for (int i = 0; i < collectKorean.Count - 1; i++)
                //    {
                //        string[] ModifyA = collectKorean[i].Split(".  ");
                //        if (ModifyA.Length > 1)
                //        {
                //            collectKoreanModify.Add(ModifyA[1]);
                //        }

                //        string[] ModifyB = collectKorean[i].Split(". ");
                //        if (ModifyB.Length > 1)
                //        {
                //            collectKoreanModify.Add(ModifyB[1]);
                //        }
                //    }

                //    StreamWriter writer = new StreamWriter(outputFilePath);

                //    for (int i = 0; i < collectKoreanModify.Count; i++)
                //    {
                //        writer.WriteLine(collectKoreanModify[i].TrimEnd());
                //    }

                //    writer.Close();
                //}
            }
            // 언리얼을 위해서 CSV파일로 저장해 줍니다. 
            if (GUILayout.Button("SaveCSV", GUILayout.Height(30)))
            {
                // c:\Bible\AAA의 모든 파일들을 가져옵니다. 
                string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\AAA", "*.txt", SearchOption.AllDirectories);
                // 모든 파일들을 대상으로 반복문 처리합니다. 
                for (int k = 0; k < CollectAllFiles.Length; k++)
                {
                    // 파일의 경로는 무시하고 파일 이름을 저장합니다. 
                    string fileName = Path.GetFileName(CollectAllFiles[k]);
                    string outputFilePath = @"c:\Bible\BBB\" + fileName;
                    // 파일의 내용을 문자열로 저장합니다. 
                    string textValue = System.IO.File.ReadAllText(CollectAllFiles[k]);
                    // 저장된 문자열을 '\n'로 구분해서 collectKorean리스트에 저장합니다. 
                    List<string> collectKorean = new List<string>(textValue.Split('\n'));
                    List<string> collectKoreanModify = new List<string>();
                    // 언리얼에서는 변수에 해당하는 부분이 필요합니다. 변수를 저장해 줍니다. 
                    collectKoreanModify.Add("key,message");
                    // collectKorean리스트를 대상으로 반복문 처리합니다. collectKorean리스트의 맨 끝의 공백을 포함하지 않습니다. 
                    for (int i = 0; i < collectKorean.Count - 1; i++)
                    {
                        // CSV파일은 ","로 구분됩니다. 문장안에 있는 ","를 "."로 변경해 줍니다. 
                        collectKorean[i] = collectKorean[i].Replace(",", ".");
                        // 문자열이 ""를 포함하고 있다면? ''로 변경해 줍니다. 
                        collectKorean[i] = collectKorean[i].Replace("\"", "'");
                        // 인덱스는 0부터 시작하지만 1부터 시작해야 합니다. 
                        collectKorean[i] = (i + 1) + "," + collectKorean[i];

                        collectKoreanModify.Add(collectKorean[i]);
                    }

                    StreamWriter writer = new StreamWriter(outputFilePath);

                    for (int i = 0; i < collectKoreanModify.Count; i++)
                    {
                        writer.WriteLine(collectKoreanModify[i].TrimEnd());
                    }

                    writer.Close();
                }
                // c:\Bible\BBB 폴더에 있는 모든 파일들을 가져옵니다. 
                CollectAllFiles = Directory.GetFiles(@"c:\Bible\BBB", "*.txt", SearchOption.AllDirectories);
                // 모든 파일들을 대상으로 반복문 처리합니다. 
                for (int i = 0; i < CollectAllFiles.Length; i++)
                {
                    // 파일에서 경로와 확장자는 무시하고 파일 이름을 가져옵니다. 
                    string fileName = Path.GetFileNameWithoutExtension(CollectAllFiles[i]);
                    // 최종 저장될 csv파일 이름입니다. 
                    string outputFilePath = @"c:\Bible\CCC\" + fileName + ".csv";
                    // csv파일로 카피해 줍니다. 
                    System.IO.File.Copy(CollectAllFiles[i], outputFilePath);
                }
            }

            if (GUILayout.Button("SaveUTF8", GUILayout.Height(30)))
            {
                SaveUTF8();
            }

            if (GUILayout.Button("AllToOneEasyBible", GUILayout.Height(30)))
            {
                AllToOneEasyBible();
            }

            if (GUILayout.Button("ConvertBibleEach", GUILayout.Height(30)))
            {
                ConvertBibleEach("현대인의성경");
            }
            // 하나의 영어 성경을 "장"별로 분리해 줍니다. 
            if (GUILayout.Button("ConvertBibleEachEnglish", GUILayout.Height(30)))
            {
                ConvertBibleEachEnglish("NASB");
            }
            // 성경의 내용의 "창1:1"에서 "창1:"를 없애 줍니다. 
            if (GUILayout.Button("RemoveFront", GUILayout.Height(30)))
            {
                RemoveFront();
            }
            // 성경의 내용의 "창1:1"에서 "창1:"를 없애 주었습니다. "1" 도 없애 줍니다. 
            if (GUILayout.Button("Remove1Other", GUILayout.Height(30)))
            {
                Remove1Other();
            }

            if (GUILayout.Button("CopyFiles", GUILayout.Height(30)))
            {
                string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                string[] Englishkeywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };
                int[] chapterCount = { 50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };
                // c:\Bible\DDD 폴더에 있는 모든 파일들을 가져옵니다. 
                string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\DDD", "*.txt", SearchOption.AllDirectories);

                Debug.Log(string.Format("CollectAllFiles.Length {0}", CollectAllFiles.Length));

                string newFile = string.Empty;
                // c:\Bible\DDD 폴더에 있는 모든 파일들을 대상으로 반복문 처리합니다. 
                for (int i = 0; i < CollectAllFiles.Length; i++)
                {
                    // 파일 이름을 가져옵니다. 
                    string oldFile = Path.GetFileName(CollectAllFiles[i]);
                    Debug.Log(string.Format("oldFile {0}", oldFile));
                    // 엑셀파일에 파일들의 이름의 한글로 되어 있습니다. 표준새번역_마, KJV_마
                    for (int j = 0; j < Englishkeywords.Length; j++)
                    {
                        if (oldFile.Contains(Englishkeywords[j]))
                        {
                            // 파일이름의 영어를 한글로 변경해 줍니다. 
                            newFile = oldFile.Replace(Englishkeywords[j], Koreankeywords[j]);
                        }
                    }
                    // 저장될 파일 위치입니다. 
                    newFile = @"c:\Bible\EEE\" + newFile;
                    Debug.Log(string.Format("newFile {0}", newFile));
                    // 이름이 변경된 파일을 c:\Bible\EEE\ 폴더에 카피해 줍니다. 
                    System.IO.File.Copy(CollectAllFiles[i], newFile);
                }





                //string[] Koreankeywords = { "창", "출", "레", "민", "신", "수", "삿", "룻", "삼상", "삼하", "왕상", "왕하", "대상", "대하", "스", "느", "에", "욥", "시", "잠", "전", "아", "사", "렘", "애", "겔", "단", "호", "욜", "암", "옵", "욘", "미", "나", "합", "습", "학", "슥", "말", "마", "막", "눅", "요", "행", "롬", "고전", "고후", "갈", "엡", "빌", "골", "살전", "살후", "딤전", "딤후", "딛", "몬", "히", "약", "벧전", "벧후", "요일", "요이", "요삼", "유", "계" };
                //string[] Englishkeywords = { "Gen", "Ex", "Lev", "Num", "Deut", "Josh", "Judg", "Ruth", "1Sam", "2Sam", "1Kin", "2Kin", "1Chr", "2Chr", "Ezra", "Neh", "Esther", "Job", "Ps", "Prov", "Eccles", "Song", "Is", "Jer", "Lam", "Ezek", "Dan", "Hos", "Joel", "Amos", "Obad", "Jonah", "Mic", "Nah", "Hab", "Zeph", "Hag", "Zech", "Mal", "Matt", "Mark", "Luke", "John", "Acts", "Rom", "1Cor", "2Cor", "Gal", "Eph", "Phil", "Col", "1Thess", "2Thess", "1Tim", "2Tim", "Titus", "Philem", "Heb", "James", "1Pet", "2Pet", "1John", "2John", "3John", "Jude", "Rev" };

                //string[] CollectAllFiles = Directory.GetFiles(@"c:\Bible\BBB", "*.txt", SearchOption.AllDirectories);

                //Debug.Log(string.Format("CollectAllFiles.Length {0}", CollectAllFiles.Length));

                //string newFile = string.Empty;

                //for (int i = 0; i < CollectAllFiles.Length; i++)
                //{
                //    //string oldFile = Path.GetFileName(CollectAllFiles[i]);
                //    //Debug.Log(string.Format("oldFile {0}", oldFile));

                //    for (int j = 0; j < Englishkeywords.Length; j++)
                //    {
                //        if (CollectAllFiles[i].Contains(Englishkeywords[j]))
                //        {
                //            newFile = CollectAllFiles[i].Replace(Englishkeywords[j], Koreankeywords[j]);
                //        }
                //    }

                //    // newFile = @"c:\Bible\BBB\" + "개역한글_" + oldFile;
                //    Debug.Log(string.Format("newFile {0}", newFile));

                //    System.IO.File.Copy(CollectAllFiles[i], newFile);

                //    //string oldFile = Path.GetFileName(CollectAllFiles[i]);
                //    //Debug.Log(string.Format("oldFile {0}", oldFile));

                //    //for (int j = 0; j < Englishkeywords.Length; j++)
                //    //{
                //    //    if (oldFile.Contains(Englishkeywords[j]))
                //    //    {
                //    //        newFile = Englishkeywords[j].Replace(Englishkeywords[j], Koreankeywords[j]);
                //    //    }
                //    //}

                //    //// newFile = @"c:\Bible\BBB\" + "개역한글_" + oldFile;
                //    //Debug.Log(string.Format("newFile {0}", newFile));

                //    //System.IO.File.Copy(CollectAllFiles[i], newFile);
                //}
            }

            if (GUILayout.Button("TestTest", GUILayout.Height(30)))
            {
                TestTest("믿음");
            }
        }
        GUILayout.EndVertical();
    }    
}
