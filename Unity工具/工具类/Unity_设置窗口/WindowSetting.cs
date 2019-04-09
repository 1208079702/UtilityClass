using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowSetting : MonoBehaviour
{
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);// 设置窗口边框
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);//设置窗口位置、大小

    [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);//查找窗口



    void Start()
    {
        //鼠标显示状态设置
        Cursor.visible = Consts.IsShowCursor;
        SetScreenModel();
    }

    private void SetScreenModel()
    {
        if (Consts.SceneModel == 0)
        {
            // 全屏
            Screen.SetResolution(Consts.ScreenWidth, Consts.ScreenHeight, true);
        }
        else if (Consts.SceneModel == 1)
        {
            //窗口非全屏
            Screen.SetResolution(Consts.ScreenWidth, Consts.ScreenHeight, false);
        }
        else
        {
            if (!Application.isEditor)
            {
                try
                {
                    SetWinNotBorder();
                }
                catch (Exception e)
                {
                    Debug.LogError("屏幕设置报错:" + e.Message);
                }
            }
        }
    }

    private void SetWinNotBorder()
    {
        IntPtr _winPtr = FindWindow(null, Consts.WindowIntPtr);//获取句柄
        SetWindowLong(_winPtr, GWL_STYLE, WS_POPUP);//设置窗口边框为 无边框
        // 设置窗口位置和大小
        int winPosX = (int)(1f * Consts.ScreenWidth / Consts.ScreenHorCount * Consts.ScreenHorIndex);
        int winPosY = (int)(1f * Consts.ScreenHeight / Consts.ScreenVerCount * Consts.ScreenVerIndex);
        SetWindowPos(_winPtr, -1, winPosX, winPosY, Consts.ScreenWidth, Consts.ScreenHeight, SWP_SHOWWINDOW);
    }

}
