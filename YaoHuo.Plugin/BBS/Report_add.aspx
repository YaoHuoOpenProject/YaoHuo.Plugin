<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Add.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Report_add" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
  Response.Write(WapTool.showTop(this.GetLang("举报帖子|舉報|tip-off"), wmlVo));//调用HEAD头部
  if (this.IsCheckManagerLvl("|00|01|02|03|04|","")==true)
  {
  Response.Write("<div class=\"title\">" + this.GetLang("举报帖子|舉報|tip-off") + "</div>");
  Response.Write(this.ERROR);
  if (this.INFO == "OK")
  {
      Response.Write("<div class=\"tip\">");
      Response.Write("<b>");
      Response.Write("举报成功！");
      Response.Write("</b>");
      Response.Write("</div>");
  }
  else if (this.INFO == "ALREADY_REPORTED")
  {
      Response.Write("<div class=\"tip\">");
      Response.Write("<b>");
      Response.Write("您已举报过该帖子，请勿重复举报！");
      Response.Write("</b>");
      Response.Write("</div>");
  }
  else
  {
%>
    <style>
    * { box-sizing: border-box; }
    h4 { font-size: 100%; font-weight: normal; }
    .M-box { width: 100%; border-radius: 2px; }
    .mod-warp { margin: 1rem 1rem .5rem; }
    .mod-title { font-size: 14px; line-height: 14px; color: #333; font-weight: bold; margin: 20px 0 13px; }
	.mod-list { font: 13px / 1.3 Arial, "Microsoft YaHei";margin-bottom: 15px; }
    .mod-list ul { list-style: none; display: flex; flex-wrap: wrap; justify-content: space-between; margin-left: 0; padding-left: 0; }
	.mod-list ul li { width: 30%; margin-bottom: 10px; height: 36px; line-height: 36px; text-align: center; background: rgb(242, 242, 245); border-radius: 4px; cursor: pointer; color: rgb(51, 51, 51); user-select: none; }
    .mod-list ul li:hover { color: #1abc9c; }
    .mod-list ul li.selected { color: #378d8d; background-color: #e5f3ee; font-weight: bold;}
    .mod-txt-explain { font-size: 12px; border-bottom: 1px solid #f1f1f1; }
    .mod-txt-explain p { color: #808080; line-height: 16px; }
    button { padding: .25rem .8rem; color: #fff; font-size: .8rem; background-color: #378d8d; border: none; border-radius: 4px; cursor: pointer; letter-spacing: 1px; }
    button:hover { background-color: #2c6d6d; }
    input[name="reportwhy"]:focus { border-color: #5db99d!important; }
    #specific_reason { width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; margin-bottom: 8px; }
  </style>
  <div class="M-box" id="pl_report_complaint">
    <div class="mod-warp">
      <h4 class="mod-title">请选择举报类型</h4>
      <div class="mod-list">
        <ul>
          <li title="广告推广">广告推广</li>
          <li title="邀请链接">邀请链接</li>
          <li title="色情低俗">色情低俗</li>
          <li title="时事政治">时事政治</li>
          <li title="人身攻击">人身攻击</li>
          <li title="不实信息">不实信息</li>
          <li title="引人不适">引人不适</li>
          <li title="恶意水帖">恶意水帖</li>
          <li title="违法内容">违法内容</li>
          <li title="其它违规">其它违规</li>
        </ul>
      </div>
      <div class="mod-txt-explain" node-type="classTipNode" style="display: none">
        <p></p>
      </div>
      <h4 class="mod-title" style="display: none" node-type="tagBox">请填写举报信息</h4>
      <div class="mod-list" style="display: none" node-type="tagBox">
        <form name="f" action="<%= http_start %>bbs/Report_add.aspx" method="post">
          <input type="text" id="specific_reason" name="reportwhy" placeholder="(选填) 提供详细的举报说明，便于更快处理。">
          <input type="hidden" name="action" value="gomod"/>
          <input type="hidden" name="id" value="<%= id %>"/>
          <input type="hidden" name="classid" value="<%= classid %>"/>
          <input type="hidden" name="siteid" value="<%= siteid %>"/>
          <input type="hidden" name="page" value="<%= page %>"/>
          <button type="submit" id="submit_button" style="display: none">提交</button>
        </form>
      </div>
    </div>
  </div>
  <script>
document.addEventListener("DOMContentLoaded", function() {
  const complaintList = document.querySelectorAll('.mod-list ul li');
  const specificReasonInput = document.getElementById('specific_reason');
  const classTipNode = document.querySelector('.mod-txt-explain');
  const tagBoxTitle = document.querySelector('h4[node-type="tagBox"]');
  const tagBoxInput = document.querySelector('.mod-list[node-type="tagBox"]');
  const submitButton = document.getElementById('submit_button');
  const reportTypeInput = document.createElement('input');
  reportTypeInput.type = 'hidden';
  reportTypeInput.name = 'reporttype';
  tagBoxInput.querySelector('form').appendChild(reportTypeInput);
  function toggleVisibility(visible) {
    classTipNode.style.display = visible ? 'block' : 'none';
    tagBoxTitle.style.display = visible ? 'block' : 'none';
    tagBoxInput.style.display = visible ? 'block' : 'none';
    submitButton.style.display = visible ? 'inline-block' : 'none';
  }
  complaintList.forEach(item => {
    item.addEventListener('click', function() {
      const isSelected = item.classList.toggle('selected');
      complaintList.forEach(link => {
        if (link !== item) link.classList.remove('selected');
      });
      toggleVisibility(isSelected);
      if (isSelected) {
        reportTypeInput.value = item.getAttribute('title');
      } else {
        reportTypeInput.value = '';
      }
    });
  });
});
  </script>
  <%
  }
  Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
  Response.Write("<a href=\"" + this.http_start + "bbs-" + this.id + ".html\">" + this.GetLang("返回帖子|返回帖子|Back to post") + "</a> ");
  Response.Write("<a href=\"" + this.http_start + "bbs/list.aspx?classid=" + this.classid + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
  Response.Write("</div></div>");
  Response.Write(WapTool.showDown(wmlVo));
  }
  %>