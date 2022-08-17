using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigObject : MonoBehaviour
{
    public Text Fname;
    public Text Ftype;
    public Text PreFname;
    public Text PreFtype;
    public Text Info_text;
    public ScrollRect scrollRect;

    private void OnEnable()
    {
        PreFname.text = Excel2JsonConroller.instance.FieldNameLine.ToString();
        PreFtype.text = Excel2JsonConroller.instance.FieldTypeLine.ToString();
        Info_text.text = "说明:暂不支持.xlsx格式表格";
        
    }
    public void SaveValue()
    {
        if (Fname.text==""|| Ftype.text=="")
        {
            Info_text.text += "\n" + "提示:请输入有效数值";
            scrollRect.verticalNormalizedPosition = -1;//使滑动条滚轮在最下方
        }
        else
        {
            Excel2JsonConroller.instance.FieldNameLine =int.Parse( Fname.text);
            Excel2JsonConroller.instance.FieldTypeLine = int.Parse(Ftype.text);
            PlayerPrefs.SetInt("FieldNameLineVal", Excel2JsonConroller.instance.FieldNameLine);
            PlayerPrefs.SetInt("FieldTypeLineVal", Excel2JsonConroller.instance.FieldTypeLine);
        }
    }
}
