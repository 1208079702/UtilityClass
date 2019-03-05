using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowSetting : MonoBehaviour {

    /// <summary>  
    /// 窗口宽度  
    /// </summary>  
    private int winWidth;
    /// <summary>  
    /// 窗口高度  
    /// </summary>  
    private int winHeight;
    /// <summary>  
    /// 窗口左上角x  
    /// </summary>  
    private int winPosX;
    /// <summary>  
    /// 窗口左上角y  
    /// </summary>  
    private int winPosY;

    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("User32.dll", EntryPoint = "FindWindow")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


    private IntPtr hwindow;


    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;

    void Start()
    {
        //获取窗口句柄
        hwindow = FindWindow(null, Consts.windowsIntPtr);
        StartCoroutine(FullScreen());
    }

    IEnumerator FullScreen()
    {
        //窗口切换带边框
        //Screen.SetResolution(Consts.screen_width,Consts.screen_height,false);
        //鼠标显示状态设置
        Cursor.visible = Consts.showcursor;
        yield return new WaitForSeconds(1);

        if (Consts.fullscreen == "0")
        {
            Screen.SetResolution(Consts.screen_width, Consts.screen_height, true);
        }
        else if (Consts.fullscreen == "1")
        {
            Screen.SetResolution(Consts.screen_width, Consts.screen_height, false);
        }
        else
        {
            if (!Application.isEditor)
            {
                try
                {
                    winWidth = Screen.width;
                    winHeight = Screen.height;
                    //显示器支持的所有分辨率  
                    //int i = Screen.resolutions.Length;
                    //int resWidth = Screen.resolutions[i - 1].width;
                    //int resHeight = Screen.resolutions[i - 1].height;
                    winPosX = 0;
                    winPosY = 0;
                    SetWindowLong(hwindow, GWL_STYLE, WS_POPUP);
                    bool result = SetWindowPos(hwindow, -1, winPosX, winPosY, winWidth, winHeight, SWP_SHOWWINDOW);
                }
                catch (Exception e )
                {
                    Debug.LogError("屏幕设置报错:" + e);
                }
            }
        }
        //yield return new WaitForSeconds(5);
        //SceneManager.LoadScene(1);
    }


}
