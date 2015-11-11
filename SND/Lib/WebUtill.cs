using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace SND
{
    public class WebUtill
    {
        #region request
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="name">파라미터이름</param>
        /// <returns>string 벨류</returns>
        public static string Request(string name)
        {
            string returnValue = string.Empty;
            if (HttpContext.Current.Request[name] != null)
            {
                string temp = HttpContext.Current.Request[name]
                .Replace("'", "")
                .Replace("--", "")
                .Replace("--, #", " ")
                .Replace("/* */", " ")
                .Replace("' or 1=1--", " ")
                .Replace("union", " ")
                .Replace("select", " ")
                .Replace("delete", " ")
                .Replace("insert", " ")
                .Replace("update", " ")
                .Replace("drop", " ")
                .Replace("on error resume", " ")
                .Replace("execute", " ")
                .Replace("windows", " ")
                .Replace("boot", " ")
                .Replace("-1 or", " ")
                .Replace("-1' or", " ")
                .Replace("../", " ")
                .Replace("unexisting", " ")
                .Replace("win.ini", " ");
                returnValue = temp;
            }
            return returnValue;
        }
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="name">파라미터이름</param>
        /// <returns>int 벨류</returns>
        public static int RequestByInt(string name)
        {
            int returnValue = 0;
            try
            {
                if (HttpContext.Current.Request[name] != null && HttpContext.Current.Request[name].Trim() != "")
                {
                    string temp = HttpContext.Current.Request[name]
                       .Replace("'", "")
                       .Replace("--", "")
                       .Replace("--, #", " ")
                       .Replace("/* */", " ")
                       .Replace("' or 1=1--", " ")
                       .Replace("union", " ")
                       .Replace("select", " ")
                       .Replace("delete", " ")
                       .Replace("insert", " ")
                       .Replace("update", " ")
                       .Replace("drop", " ")
                       .Replace("on error resume", " ")
                       .Replace("execute", " ")
                       .Replace("windows", " ")
                       .Replace("boot", " ")
                       .Replace("-1 or", " ")
                       .Replace("-1' or", " ")
                       .Replace("../", " ")
                       .Replace("unexisting", " ")
                       .Replace("win.ini", " ");
                    returnValue = int.Parse(temp);
                }
            }
            catch (Exception ex)
            {
                
            }
            return returnValue;
        }
        #endregion

        #region Data Handler
        public static int ConvertInt(object obj)
        {
            try
            {
                if (obj == null
                    || obj == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static DateTime ConvertDatetime(object obj)
        {
            try
            {
                if (obj.ToString() == "")
                {
                    obj = DateTime.Now.ToShortDateString();
                }
                if (obj.ToString().Length == 8)
                {
                    obj = obj.ToString().Substring(0, 4) + "-" + obj.ToString().Substring(4, 2) + "-" + obj.ToString().Substring(6, 2);
                }
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
        public static DateTime ConvertDatetime_S(object obj)
        {
            try
            {
                if (obj.ToString() == "")
                {
                    obj = DateTime.Now.ToShortDateString();
                }
                if (obj.ToString().Length == 8)
                {
                    obj = obj.ToString().Substring(0, 4) + "-" + obj.ToString().Substring(4, 2) + "-" + obj.ToString().Substring(6, 2) + " 00:00:00";
                }
                if (obj.ToString().Length == 10)
                {
                    obj = obj.ToString() + " 00:00:00";
                }
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
        public static DateTime ConvertDatetime_E(object obj)
        {
            try
            {
                if (obj.ToString() == "")
                {
                    obj = DateTime.Now.AddDays(1).AddMinutes(-1).ToShortDateString();
                }
                if (obj.ToString().Length == 8)
                {
                    obj = obj.ToString().Substring(0, 4) + "-" + obj.ToString().Substring(4, 2) + "-" + obj.ToString().Substring(6, 2) + " 23:59:59";
                }
                if (obj.ToString().Length == 10)
                {
                    obj = obj.ToString() + " 23:59:59";
                }
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        } 
        #endregion

        public static string ImageSave(string path, HttpPostedFileBase file)
        {
            string extension = file.FileName.Split('.')[1];
            string name = string.Format("{0:yyyyMMdd}", DateTime.Now);
            DirectoryInfo info = new DirectoryInfo(path);
            if(info.Exists)
            {
                int count = info.GetFiles().Count();
                name = name + "_" + count + "." + extension;
            }
            else
            {
                info.Create();
                name = name + "." + extension;
            }
            file.SaveAs(path + name);
            return name;
        }

        public static void ImageDelete(string path, string name)
        {
            FileInfo info = new FileInfo(path + name);
            if(info.Exists)
            {
                info.Delete();
            }
        }

    }
}