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
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


    private IntPtr _winPtr;
    void Start()
    {
        //获取窗口句柄
        _winPtr = FindWindow(null, ProjectConsts.WindowIntPtr);
        StartCoroutine(FullScreen());
    }

    IEnumerator FullScreen()
    {
        //鼠标显示状态设置
        Cursor.visible = ProjectConsts.IsShowCursor;
        yield return new WaitForSeconds(1);
        if (ProjectConsts.SceneModel == 0)
        {
            // 全屏
            Screen.SetResolution(ProjectConsts.ScreenWidth, ProjectConsts.ScreenHeight, true);
        }
        else if (ProjectConsts.SceneModel == 1)
        {
            //窗口非全屏
            Screen.SetResolution(ProjectConsts.ScreenWidth, ProjectConsts.ScreenHeight, false);
        }
        else
        {
            if (!Application.isEditor)
            {
                try
                {
                    int winPosX = (int)(1f * ProjectConsts.ScreenWidth / ProjectConsts.ScreenHorCount * ProjectConsts.ScreenHorIndex);
                    int winPosY = (int)(1f * ProjectConsts.ScreenHeight / ProjectConsts.ScreenVerCount * ProjectConsts.ScreenVerIndex);
                    SetWindowLong(_winPtr, GWL_STYLE, WS_POPUP);
                    SetWindowPos(_winPtr, -1, winPosX, winPosY, ProjectConsts.ScreenWidth, ProjectConsts.ScreenHeight, SWP_SHOWWINDOW);
                }
                catch (Exception e)
                {
                    Debug.LogError("屏幕设置报错:" + e.Message);
                }
            }
        }
    }


}
