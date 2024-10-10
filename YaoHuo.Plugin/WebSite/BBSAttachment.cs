using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager.Model;
using KeLin.ClassManager.BLL;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.WebSite
{
    public class BBSAttachment
    {
        public static string ProcessAttachments(long isdown, string smallimg, string siteid, string classid, string id, string lpage, string stypelink, string saveUpFilesPath, string sitemoneyname, string http_start, wap2_attachment_BLL attachmentBLL)
        {
            if (isdown <= 0L)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            string chargeInfo = GetAttachmentChargeInfo(smallimg, sitemoneyname);
            List<wap2_attachment_Model> attachmentList = GetAttachmentList(attachmentBLL, id);

            if (attachmentList != null)
            {
                AppendAttachmentSummary(stringBuilder, attachmentList.Count, chargeInfo);
            }

            int maxAttachments = GetMaxAttachments(smallimg);
            AppendAttachmentInfo(stringBuilder, attachmentList, maxAttachments, siteid, classid, id, http_start, saveUpFilesPath);

            if (attachmentList != null && attachmentList.Count > maxAttachments - 1)
            {
                AppendViewAllAttachmentsLink(stringBuilder, http_start, siteid, classid, id, lpage, stypelink);
            }

            return stringBuilder.ToString();
        }

        private static string GetAttachmentChargeInfo(string smallimg, string sitemoneyname)
        {
            string text4 = "";
            string text5 = WapTool.getArryString(smallimg, '|', 17);
            string text6 = WapTool.getArryString(smallimg, '|', 18);
            if (!WapTool.IsNumeric(text5)) text5 = "0";
            if (!WapTool.IsNumeric(text6)) text6 = "0";

            if (long.Parse(text5) > 0L)
            {
                text4 = "扣" + text5 + "个" + sitemoneyname;
            }
            if (long.Parse(text6) > 0L)
            {
                if (text4 != "") text4 += "/";
                text4 = text4 + "送" + text6 + "个" + sitemoneyname;
            }
            if (text4 != "") text4 = "(" + text4 + ")";

            return text4;
        }

        private static List<wap2_attachment_Model> GetAttachmentList(wap2_attachment_BLL attachmentBLL, string id)
        {
            return attachmentBLL.GetListVo(" book_type='bbs' and book_id=" + long.Parse(id));
        }

        private static void AppendAttachmentSummary(StringBuilder stringBuilder, int attachmentCount, string chargeInfo)
        {
            stringBuilder.Append("<div class='attachment'>");
            stringBuilder.Append("<span class='attachmenSum'>");
            stringBuilder.Append("<span class='attachmentext'>共有</span>");
            stringBuilder.Append("<span class='attachmentlistnum'>" + attachmentCount + "</span>");
            stringBuilder.Append("<span class='attachmentext'>个附件</span>");
            stringBuilder.Append("<span class='attachmentCharge'>" + chargeInfo + "</span>");
            stringBuilder.Append("</span>");
        }

        private static int GetMaxAttachments(string smallimg)
        {
            string text7 = WapTool.getArryString(smallimg, '|', 33);
            if (!WapTool.IsNumeric(text7)) text7 = "2";
            if (text7 == "0") text7 = "1000";
            return int.Parse(text7);
        }

        private static void AppendAttachmentInfo(StringBuilder stringBuilder, List<wap2_attachment_Model> attachmentList, int maxAttachments, string siteid, string classid, string id, string http_start, string saveUpFilesPath)
        {
            if (attachmentList == null) return;

            for (int i = 0; i < attachmentList.Count && i < maxAttachments; i++)
            {
                var attachment = attachmentList[i];
                stringBuilder.Append("<div class='attachmentinfo'><span class=\"downloadname\"><span class=\"attachmentnumber\">");
                stringBuilder.Append(i + 1 + ".");
                stringBuilder.Append("</span><span class='attachmentname'><span class='attachmentitle'>");
                stringBuilder.Append(attachment.book_title);
                stringBuilder.Append("</span>");

                AppendFileExtension(stringBuilder, attachment);
                AppendFileSize(stringBuilder, attachment);
                AppendFileContent(stringBuilder, attachment, http_start, siteid, classid, id, saveUpFilesPath);

                stringBuilder.Append("<span class=\"attachmentNote\">");
                stringBuilder.Append(attachment.book_content + "");
                stringBuilder.Append("</span>");
                stringBuilder.Append("</div>");
            }
        }

        private static void AppendFileExtension(StringBuilder stringBuilder, wap2_attachment_Model attachment)
        {
            if (attachment.book_ext.Trim() != "" && attachment.book_ext.Trim() != "mov")
            {
                stringBuilder.Append("<span class=\"FileExtension\">");
                stringBuilder.Append("." + attachment.book_ext);
                stringBuilder.Append("</span></span>");
            }
        }

        private static void AppendFileSize(StringBuilder stringBuilder, wap2_attachment_Model attachment)
        {
            if (attachment.book_size.Trim() != "")
            {
                stringBuilder.Append("<span class=\"attachmentsize\">");
                stringBuilder.Append("(" + attachment.book_size + ")");
                stringBuilder.Append("</span></span>");
            }
        }

        private static void AppendFileContent(StringBuilder stringBuilder, wap2_attachment_Model attachment, string http_start, string siteid, string classid, string id, string saveUpFilesPath)
        {
            if (attachment.book_ext.Trim() != "" && ".jpg|.jpeg|.png|.gif|.webp".IndexOf(attachment.book_ext.ToLower()) >= 0)
            {
                AppendImageContent(stringBuilder, attachment, http_start);
            }
            else if (attachment.book_ext.Trim() != "" && ("mov|flv|m3u8|mp4").IndexOf(attachment.book_ext.ToLower()) >= 0)
            {
                AppendVideoContent(stringBuilder, attachment, http_start, siteid, classid, id, saveUpFilesPath);
            }
            else
            {
                AppendDownloadLink(stringBuilder, attachment, http_start, siteid, classid, id, saveUpFilesPath);
            }
        }

        private static void AppendImageContent(StringBuilder stringBuilder, wap2_attachment_Model attachment, string http_start)
        {
            string imageUrl = http_start + "bbs/" + attachment.book_file;
            if (attachment.book_file.ToLower().StartsWith("http"))
            {
                imageUrl = attachment.book_file;
            }
            stringBuilder.Append("<span class=\"attachmentimage\">");
            stringBuilder.Append("<a href='" + http_start + "bbs/" + HttpUtility.UrlDecode(attachment.book_file) + "'>");
            stringBuilder.Append("<img src='" + imageUrl + "' referrerpolicy='no-referrer'/></a>");
            stringBuilder.Append("</span>");
        }

        private static void AppendVideoContent(StringBuilder stringBuilder, wap2_attachment_Model attachment, string http_start, string siteid, string classid, string id, string saveUpFilesPath)
        {
            string fileExt = WapTool.right(attachment.book_file.ToLower(), 3);
            if (("mov|flv|m3u8|mp4").IndexOf(fileExt) >= 0)
            {
                stringBuilder.Append("<span class=\"videoplay\"><video src='" + attachment.book_file + "' autobuffer='true' width='100%' height='100%' poster='/NetImages/play.gif' controls>{不支持在线播放，请更换浏览器}</video>");
            }
            else
            {
                AppendDownloadLink(stringBuilder, attachment, http_start, siteid, classid, id, saveUpFilesPath);
            }
        }

        private static void AppendDownloadLink(StringBuilder stringBuilder, wap2_attachment_Model attachment, string http_start, string siteid, string classid, string id, string saveUpFilesPath)
        {
            stringBuilder.Append("</span><span class=\"downloadlink\"><span class=\"downloadurl\"><a class='urlbtn'  href='" + http_start + "bbs/download.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;book_id=" + id + "&amp;id=" + attachment.ID + "&amp;RndPath=" + saveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(attachment.book_title) + "." + attachment.book_ext + "'>点击下载</a></span><span class=\"downloadcount\">(" + attachment.book_click + "次)</span></span>");
        }

        private static void AppendViewAllAttachmentsLink(StringBuilder stringBuilder, string http_start, string siteid, string classid, string id, string lpage, string stypelink)
        {
            stringBuilder.Append("<div class='btBox'><div class='bt1'>");
            stringBuilder.Append("<a href='" + http_start + "bbs/book_view_showfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + stypelink + "'>{查看所有附件}</a> ");
            stringBuilder.Append("</div></div>");
        }
    }
}