using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

    [ContextMenu("测试读")]
    void TestRead()
    {
       string excelPath = Application.dataPath + "/Test/Test2.xlsx";
       // string excelPath =  @"D:\Excel/Test.xlsx";
        Excel xls = ExcelHelper.LoadExcel(excelPath);
        xls.ShowLog();
        



    }

    [ContextMenu("测试写")]
    void TestWrite()
    {
        string excelPath = Application.dataPath + "/Test/Test.xlsx";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        Excel xls = ExcelHelper.LoadExcel(excelPath);
        

        xls.Tables[0].SetValue(2, 3, "hahha");
        xls.ShowLog();
        ExcelHelper.SaveExcel(xls, outputPath);
    }

    [ContextMenu("测试生成脚本")]
    void TestMakeCs()
    {
        string path = Application.dataPath + "/Test/Test4.xlsx";
        Excel xls = ExcelHelper.LoadExcel(path);
        ExcelDeserializer ed = new ExcelDeserializer();
        ed.FieldNameLine = 1;
        ed.FieldTypeLine = 2;
        ed.FieldValueLine = 3;
       
        ed.IgnoreSymbol = "#";
        
        ed.ModelPath = Application.dataPath + "/Excel4Unity/DataItem.txt";
        ed.GenerateCS(xls.Tables[0]);
    }

    

}
