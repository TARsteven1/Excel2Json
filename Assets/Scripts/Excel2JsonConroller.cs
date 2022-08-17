using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class Excel2JsonConroller : MonoBehaviour
{
    public Text TargetPath;
    public Text OutPath;
    public Text info_Text;
    public static Excel2JsonConroller instance;
    public ScrollRect scrollRect;
    public int FieldNameLine;    

    public int FieldTypeLine;    
    //[SerializeField]
    //int FieldValueLine;
    [SerializeField]
    string ExtName;

     List<string> temp = new List<string>();
     List<string> temp2 = new List<string>();
     List<JsonData> temp3 = new List<JsonData>();
    bool isDone;
    bool isWorking;


    public void Excute()
    {
        isWorking = true;
        if (TargetPath.text == "请选择配置表文件夹" || OutPath.text == "请选择输出Json的文件夹")
        {
            info_Text.text += "\n" + "请选择正确的文件夹!";

        }
        else
        {
            GetFile(TargetPath.text, FilenameList, ExtName/*@".xlsx"*/);
            //Debug.Log(TargetPath.text + @"\" + FilenameList[0]);
            //info_Text.text += "\n" + TargetPath.text + @"\" + FilenameList[0];
            ChangeExcel(TargetPath.text + @"\");

        }
    }
    public void ChangeExcel(string path)
    {
        try
        {
            
            StartCoroutine(ChangeExcelFunction(path));
        }
        catch (Exception ex)
        {
            //Debug.Log(ex);
            info_Text.text += "\n" + ex.ToString();
            throw ex;
        }
    }
    IEnumerator ChangeExcelFunction(string path)
    {
        for (int j = 0; j < FilenameList.Count; j++)
        {
            // Debug.Log(arg + FilenameList[j]);
            Excel xls = ExcelHelper.LoadExcel(path + FilenameList[j]);
            // Debug.Log(xls.Tables[0].NumberOfRows + "HH" + xls.Tables[0].NumberOfColumns);
            //xls.ShowLog();
            for (int column = 1; column <= xls.Tables[0].NumberOfColumns; column++)
            {
                temp.Add(string.Format("{0} ", xls.Tables[0].GetValue(FieldNameLine, column)));
            }

            for (int row = FieldTypeLine + 1; row <= xls.Tables[0].NumberOfRows; row++)
            {
                JsonData data = new JsonData();//写入json格式的数据
                for (int column = 1; column <= xls.Tables[0].NumberOfColumns; column++)
                {
                    temp2.Add(string.Format("{0} ", xls.Tables[0].GetValue(row, column)));
                    data[temp[column - 1]] = string.Format("{0} ", xls.Tables[0].GetValue(row, column));
                }
                temp3.Add(data);
                //Debug.Log(System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(data.ToJson())));

                temp2.Clear();
            }

            JsonData data2 = new JsonData();//写入json格式的数据
            string TempType = FilenameList[j].Replace(ExtName, "");
            data2[TempType] = new JsonData();
            for (int i = 0; i < temp3.Count; i++)
            {
                data2[TempType].Add(temp3[i]);

            }
            //Debug.Log(System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(data2.ToJson())));
            SavedDataToJson(TempType, System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(data2.ToJson())));
            // SavedDataToJson(TempType,data2);
            if (j + 1 == FilenameList.Count)
            {
                isDone = true;
                isWorking = false;
                FilenameList.Clear();
                //弹出窗口
                info_Text.text += "\n" + "##############操作完成###############";
                scrollRect.verticalNormalizedPosition = -1;//使滑动条滚轮在最下方

            }
        }
        yield return null;
    }
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        info_Text.text = /*"\n" +*/ @"-->等待操作";
        if (PlayerPrefs.HasKey("TargetPath"))
        {
            TargetPath.text = PlayerPrefs.GetString("TargetPath");
            //PlayerPrefs.SetString("TargetPath", TargetPath.text);
        }
        if (PlayerPrefs.HasKey("OutPath"))
        {
            OutPath.text = PlayerPrefs.GetString("OutPath");
        }        
        if (PlayerPrefs.HasKey("FieldNameLine"))
        {
            FieldNameLine = PlayerPrefs.GetInt("FieldNameLineVal");
        }
        else
        {
            FieldNameLine = 1;
        }
        if (PlayerPrefs.HasKey("FieldTypeLine"))
        {
            FieldTypeLine = PlayerPrefs.GetInt("FieldTypeLineVal");
        }
        else
        {
            FieldTypeLine = 2;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isDone)
            {
                //跳转回unity

                isDone = false;
            }
            else if (!isWorking)
            {
            Excute();

            }

        }
    }

    private static List<string> FilenameList = new List<string>();
    public static List<string> GetFile(string path, List<string> FileList/*, string RelativePath*/,string extName)
    {
        
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] fil = dir.GetFiles();
        DirectoryInfo[] dii = dir.GetDirectories();
        foreach (FileInfo f in fil)
        {
            //int size = Convert.ToInt32(f.Length);
            //long size = f.Length;
            if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
            {
            FileList.Add(f.Name);//添加文件路径到列表中
                
            }
        }
        //获取子文件夹内的文件列表，递归遍历
        foreach (DirectoryInfo d in dii)
        {
            GetFile(d.Name, FileList/*, RelativePath*/, extName);
        }
        return FileList;
    }
    public void SavedDataToJson(string MapName,string data)
    {
        //string JsonString = JsonUtility.ToJson(data);
        //if (!File.Exists(OutPath.text+@"\" + MapName + @".json"))
        //    {
        //    File.Create(OutPath.text + @"\" + MapName + @".json");
        //    // string JsonString = JsonUtility.ToJson(data);
        //    // StreamWriter sw = new StreamWriter("file:///" + Application.dataPath + LoadPath + mapData.MapName/*+ ".Map"*/);
        //    }
        
        StreamWriter sw = new StreamWriter(OutPath.text + @"\" + MapName+@".json");
            sw.Write(data);
                sw.Close();
                sw.Dispose();
               // Debug.Log(OutPath.text + @"\" + MapName + @".json");
        info_Text.text +="\n"+"Finished:" +OutPath.text + @"\" + MapName + @".json";
       
        SaveConfig();
    }
    void SaveConfig()
    {
        PlayerPrefs.SetString("TargetPath", TargetPath.text);
        PlayerPrefs.SetString("OutPath", OutPath.text);
    }
}
