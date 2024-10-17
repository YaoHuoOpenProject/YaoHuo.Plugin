<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_change.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_change" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {
        wmlVo.timer = "1";
        wmlVo.strUrl = "./bbs/book_view_admin.aspx?siteid=" + this.siteid + "&classid=" + this.Request.Form["toclassid"] + "&lpage=" + this.lpage + "&id=" + this.id;
    }
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("转移版块|轉移欄目|Transfer part"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang("转移帖子版块|轉移操作|Change operation") + "</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append(this.ERROR);

    bool isTransferSuccess = false; // 添加转移成功标志

    if (this.INFO == "OK")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("转移成功！|轉移成功！|Transfer modified"));
        strhtml.Append("<a style=\"font-size: unset;\" href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.Request.Form["toclassid"] + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("自动返回管理|自動返回管理|Auto return to admin") + "</a> ");
        strhtml.Append("</b><br/>");
        isTransferSuccess = true; // 设置标志为 true

    }
    else if (this.INFO == "SELECT")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("请选择版块！|請選擇欄目！|Please select the part!"));
        strhtml.Append("</b><br/>");
    }
    strhtml.Append("</div>");

    if (!isTransferSuccess) // 仅在转移前时显示版块内容
    {
        strhtml.Append("<div class=\"content\">");
        //strhtml.Append(this.GetLang("原版块ID|原欄目ID|Original classID") + "<span class=\"recolon\">:</span>" + classid + "<br/>");
        strhtml.Append(this.GetLang("原版块名称|原欄目名|Class Name") + "<span class=\"recolon\">:</span>" + classVo.classname + "<br/>");
        strhtml.Append(this.GetLang("转移后版块|轉移後欄目|Change ClassID") + "<span class=\"recolon\">:</span><br/>");

        // 平铺选项
        strhtml.Append("<div class=\"mod-list\"><ul>");
        for (int i = 0; (classList != null && i < classList.Count); i++)
        {
            if (classList[i].classname != classVo.classname) // 排除当前版块
            {
                strhtml.Append("<li title=\"" + classList[i].classid + "_" + classList[i].classname + "\">" + classList[i].classname + "</li>");
            }
        }
        strhtml.Append("</ul></div>");
        strhtml.Append("<form name=\"go\" action=\"book_view_change.aspx\" method=\"post\" id=\"transferForm\">");
        strhtml.Append("<input type=\"hidden\" name=\"toclassid\" id=\"toclassid\" value=\"" + this.classid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
        strhtml.Append("</form></div>");
    }

    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbslist-" + this.classid + ".html\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.Request.Form["toclassid"] + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>
<style>
    * {
        box-sizing: border-box;
    }

    h4 {
        font-size: 100%;
        font-weight: normal;
    }

    .M-box {
        width: 100%;
        border-radius: 2px;
    }

    .mod-warp {
        margin: 1rem 1rem .5rem;
    }

    .mod-title {
        font-size: 14px;
        line-height: 14px;
        color: #333;
        font-weight: bold;
        margin: 20px 0 13px;
    }

    .mod-list {
        font: 13px / 1.3 Arial, "Microsoft YaHei";
        margin-bottom: -18px;
    }

        .mod-list ul {
            list-style: none;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-left: 0;
            padding-left: 0;
        }

            .mod-list ul li {
                width: 30%;
                margin-bottom: 10px;
                height: 36px;
                line-height: 36px;
                text-align: center;
                background: rgb(242, 242, 245);
                border-radius: 4px;
                cursor: pointer;
                color: rgb(51, 51, 51);
                user-select: none;
            }

                .mod-list ul li:hover {
                    color: #1abc9c;
                }

                .mod-list ul li.selected {
                    color: #378d8d;
                    background-color: #e5f3ee;
                    font-weight: bold;
                }

            .mod-list ul .placeholder {
                width: 30%;
                visibility: hidden;
            }

    .mod-txt-explain {
        font-size: 12px;
        border-bottom: 1px solid #f1f1f1;
    }

        .mod-txt-explain p {
            color: #808080;
            line-height: 16px;
        }

    button {
        padding: .25rem .8rem;
        color: #fff;
        font-size: .8rem;
        background-color: #378d8d;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        letter-spacing: 1px;
    }

        button:hover {
            background-color: #2c6d6d;
        }

    input[name="reportwhy"]:focus {
        border-color: #5db99d !important;
    }

    #specific_reason {
        width: 100%;
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 4px;
        margin-bottom: 8px;
    }
</style>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        const classListItems = document.querySelectorAll('.mod-list ul li');
        const toClassIdInput = document.getElementById('toclassid');
        const transferForm = document.getElementById('transferForm');

        classListItems.forEach(item => {
            item.addEventListener('click', function () {
                classListItems.forEach(link => link.classList.remove('selected'));
                item.classList.add('selected');
                toClassIdInput.value = item.getAttribute('title').split('_')[0];

                // 直接提交表单
                transferForm.submit();
            });
        });

        // 添加占位元素
        const ul = document.querySelector('.mod-list ul');
        const itemsPerRow = 3; // 每行显示的项目数
        const itemCount = classListItems.length;
        const placeholdersNeeded = itemsPerRow - (itemCount % itemsPerRow);

        if (placeholdersNeeded < itemsPerRow) {
            for (let i = 0; i < placeholdersNeeded; i++) {
                const placeholder = document.createElement('li');
                placeholder.classList.add('placeholder');
                ul.appendChild(placeholder);
            }
        }
    });
</script>