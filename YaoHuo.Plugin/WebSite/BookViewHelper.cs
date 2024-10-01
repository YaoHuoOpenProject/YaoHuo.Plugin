using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class BookViewHelper
    {
        public static string ProcessContent(string content, ref int totalPage, int CurrentPage, int pageSize, string viewLeave)
        {
            if (content.IndexOf("[next]") > 0)
            {
                content = content.Replace("[next]", "\uff3e");
                string[] array = content.Split('\uff3e');
                totalPage = array.Length;
                if (array[totalPage - 1] == "")
                {
                    totalPage--;
                }
                if (viewLeave != "")
                {
                    if (int.Parse(viewLeave) < totalPage)
                    {
                        string text3 = "";
                        for (int i = int.Parse(viewLeave); i < totalPage; i++)
                        {
                            text3 += array[i];
                        }
                        content = text3;
                    }
                }
                else
                {
                    content = array[CurrentPage - 1];
                }
            }
            else
            {
                try
                {
                    totalPage = content.Length / pageSize;
                    if (content.Length > totalPage * pageSize)
                    {
                        totalPage++;
                    }
                    if (viewLeave != "")
                    {
                        if (Convert.ToInt32(viewLeave) * pageSize < content.Length)
                        {
                            content = content.Substring(Convert.ToInt32(viewLeave) * pageSize, content.Length - Convert.ToInt32(viewLeave) * pageSize);
                        }
                    }
                    else if (CurrentPage > totalPage)
                    {
                        content = "";
                    }
                    else if (totalPage > 1 && CurrentPage >= totalPage)
                    {
                        CurrentPage = totalPage;
                        content = content.Substring((CurrentPage - 1) * pageSize, content.Length - (CurrentPage - 1) * pageSize);
                    }
                    else if (totalPage > 1 && CurrentPage < totalPage)
                    {
                        content = content.Substring((CurrentPage - 1) * pageSize, pageSize);
                    }
                }
                catch (Exception ex)
                {
                    content += WapTool.ErrorToString(ex.ToString());
                }
            }

            content = content.Replace("[next]", "");
            content = content.Replace("\uff3e", "");

            return content;
        }

        public static string ShowNickName_color(long userid, string nickname, List<user_Model> userListVo_IDName, string lang, string ver)
        {
            if (userListVo_IDName == null)
            {
                return nickname;
            }
            int num = 0;
            while (userListVo_IDName != null && num < userListVo_IDName.Count)
            {
                if (userListVo_IDName[num].userid != userid)
                {
                    num++;
                    continue;
                }
                nickname = WapTool.GetColorNickName(userListVo_IDName[num].idname, nickname, lang, ver, userListVo_IDName[num].endTime);
                break;
            }
            return nickname;
        }
    }
}