using System;
using System.Collections.Generic;

public class Data_Building
{
    [NonSerialized]
    public string Path = "/MaterialPicture/";
    /// <summary>
    /// ID
    /// </summary>
    public int Id;
    /// <summary>
    /// 类别
    /// </summary>
    public string Identification;
    /// <summary>
    /// 对象类别,类似医院，学校
    /// </summary>
    public string Type;
    /// <summary>
    /// 单位数据名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 允许显示数据的最小年份
    /// </summary>
    public int MinYears;

    /// <summary>
    /// 允许显示数据的最大年份
    /// </summary>
    public int MaxYears;
  
   
    /// <summary>
    /// 单位数据标识图片地址
    /// </summary>
    public string _IdentificationMap;


    ///// <summary>
    ///// 单位数据标识图片UI位置
    ///// </summary>
    //[NonSerialized]
    //public Vector2 PictureLocation;

    public int PictureLocationX;
    public int PictureLocationY;

    ///// <summary>
    ///// 单位数据标识图片UI锚点
    ///// </summary>
    //[NonSerialized]
    //public Vector2 PopUpsPoint;

    public int PopUpsPointX;
    public int PopUpsPointY;

    ///// <summary>
    ///// 单位数据标识图片UI尺寸大小 SizeDelet;
    ///// </summary>
    //[NonSerialized]
    //public Vector2 PopUpsImageSize;

    public int PopUpsImageSizeX;
    public int PopUpsImageSizeY;


    /// <summary>
    /// 单位数据标识图片UI弹窗详情页背景图片地址
    /// </summary>

    public string _PopUpsImage;

    /// <summary>
    /// 单位数据标识图片UI弹窗详情页内容图片地址
    /// </summary>
    public List<string> _InnerImaPath = new List<string>();

    /// <summary>
    /// 单位数据标识图片UI弹窗详情页内容文字
    /// </summary>
    public List<string> InnerTxt = new List<string>();



    ////以下为加完完成包含图片的数据///////////

    ///// <summary>
    ///// 单位数据标识图片
    ///// </summary>
    //[NonSerialized]
    //public Texture2D IdentificationMap;
    ///// <summary>
    ///// 单位数据标识图片UI弹窗详情页底图图片
    ///// </summary>
    //[NonSerialized]
    //public Texture2D PopUpsImage;

    ///// <summary>
    ///// 单位数据标识图片UI弹窗详情页内容图片
    ///// </summary>
    //[NonSerialized]
    //public List<Texture2D> InnerImaPath = new List<Texture2D>();
}
