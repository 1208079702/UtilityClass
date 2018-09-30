using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
public class SoapSerializeHelper
{
    /// <summary>
    /// Soap格式化器
    /// </summary>
    private static SoapFormatter soapFormatter = new SoapFormatter();

    /// <summary>
    /// 将内存对象序列化到内存流
    /// </summary>
    /// <param name="instance">需要序列化的对象</param>
    /// <returns>内存流</returns>
    public static MemoryStream InstanceDataToMemory(object instance)
    {
        if (instance == null) return null;
        MemoryStream memoStream = new MemoryStream();
        soapFormatter.Serialize(memoStream, instance);
        return memoStream;
    }


    /// <summary>
    /// 将内存流反序列化为内存对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="stream">内存流</param>
    /// <param name="isLeaveOpen">离开方法时，流是否保持开的状态</param>
    /// <returns>内存对象</returns>
    public static object MemoryToInstanceData(Stream stream, bool isLeaveOpen)
    {
        if (stream == null) return null;
        object obj = soapFormatter.Deserialize(stream);
        if (!isLeaveOpen)
            stream.Close();
        return obj;
    }
    public static object MemoryToInstanceData(Stream stream)
    {
        return MemoryToInstanceData(stream, false);
    }




    /// <summary>
    ///  序列化对象到文件
    /// </summary>
    /// <param name="instance">对象</param>
    /// <param name="filepath">文件路径</param>
    /// <param name="isAppend">追加还是创建</param>
    public static void InstanceDataToFile(object instance, string filepath, bool isAppend)
    {
        if (filepath == null) throw new Exception("文件名不能为空");
        if (instance == null) return;
        FileStream fileStream = null;
        if (!File.Exists(filepath))
        {
            fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
        }
        else
        {
            if (isAppend == false)
                fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            if (isAppend == true)
                fileStream = new FileStream(filepath, FileMode.Append, FileAccess.ReadWrite);
        }
        try
        {
            soapFormatter.Serialize(fileStream, instance);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
    }
    public static void InstanceDataToFile(object instance, string filepath)
    {
        InstanceDataToFile(instance, filepath, false);
    }

    /// <summary>
    /// 将文件反序列化到对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static object FileToInstanceData(string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
            throw new Exception("文件名不能为空！");
        if (!File.Exists(filepath))
            throw new Exception("文件不存在！");
        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
        object obj = null;
        try
        {
            obj = soapFormatter.Deserialize(fileStream);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
        return obj;
    }
}

