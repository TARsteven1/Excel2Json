using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Threading;

public partial class switchApp :MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern void SwitchToThisWindow(IntPtr hWnd, Boolean fAltTab);

    public static void SwitchProcess(string processPath, string processName)
    {

        Process[] temp = Process.GetProcessesByName(processName);
        //if (temp.Length > 0)
        //{

        foreach (Process proc in temp)
        {
            //switch to process by name
            SwitchToThisWindow(proc.MainWindowHandle, true);

        }
        //UnityEngine.Debug.Log(temp.Length);
        //for (int i = 0; i < temp.Length; i++)
        //{
        //    UnityEngine.Debug.Log(i);

        //    IntPtr handle = temp[i].MainWindowHandle;
        //    //Thread.Sleep(1000);
        //    SwitchToThisWindow(handle, false);
        //}
       // }
        //else
        //    Process.Start(processPath);
    }
    // 按钮事件
    public static void ChangeApp()
    {
        //SwitchProcess(@"D:\Debug\Form.exe", "Form");
        SwitchProcess(@"C:\Users\Administrator\AppData\Local\Postman\Postman.exe", "Postman");

    }

    
   
}


