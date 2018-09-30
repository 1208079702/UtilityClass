/*
 *     主题： 配置文件读写工具类
 *
 *     使用方法：将本脚本挂在到游戏对象，根据 ConstData 类修改 ReadIniFile 方法.
 *              之后程序需要用到配置文件的数据，全部从 ConstData 类获得
 *  
 *     使用条件：配置文件的路径需要和 _configpath 一致
 *              配置文件的格式参考 config.ini
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

/// <summary>
/// 配置文件读写帮助类
/// </summary>
public class ConfigHelper : MonoBehaviour
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="section">文件中的分类</param>
    /// <param name="key">键值</param>
    /// <param name="def">当键值不存在，使用的默认值</param>
    /// <param name="builder">对象</param>
    /// <param name="size">获取的长度</param>
    /// <param name="filePath">路径</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder builder, int size, string filePath);
    /// <summary>
    /// 写入配置文件
    /// </summary>
    /// <param name="section">文件中的分类</param>
    /// <param name="key">键值</param>
    /// <param name="val">内容</param>
    /// <param name="filePath">路径</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    /// <summary>
    /// 配置文件路径
    /// </summary>
    private string _configpath;
    void Awake()
    {
        DontDestroyOnLoad(this);
        _configpath = Application.streamingAssetsPath + @"/config.ini";
        ReadIniFile();
    }

    public void ReadIniFile()
    {
        ConstData.ServerIp = ReadIniData("config", "serverip", "127.0.0.1", this._configpath);
        ConstData.ServerPort = int.Parse(ReadIniData("config", "port", "60000", this._configpath));
    }

    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="Section">配置文件的部分</param>
    /// <param name="Key">key值</param>
    /// <param name="def">默认值</param>
    /// <param name="filePath">文件路径</param>
    /// <returns></returns>
    private string ReadIniData(string section, string key, string def, string filePath)
    {
        if (File.Exists(filePath))
        {
            StringBuilder builder = new StringBuilder(1024);
            long length = GetPrivateProfileString(section, key, def, builder, 1024, filePath);
            if (length > 0)
                return builder.ToString();
        }
        return string.Empty;
    }


    /// <summary>
    /// 写入配置文件
    /// </summary>
    /// <param name="group"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void WriteIniFile(string section, string key, string value)
    {
        bool result = WriteIniData(section, key, value, this._configpath);
        if (result == false)
            throw new Exception("写入文件失败");
    }
    private static bool WriteIniData(string section, string key, string value, string iniFilePath)
    {
        bool result = false;
        if (File.Exists(iniFilePath))
        {
            long length = WritePrivateProfileString(section, key, value, iniFilePath);
            if (length > 0)
                result = true;
        }
        return result;
    }

}
