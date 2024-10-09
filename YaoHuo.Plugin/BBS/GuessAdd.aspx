<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuessAdd.aspx.cs" Inherits="YaoHuo.Plugin.BBS.GuessAdd" %>

    <!DOCTYPE html>
    <html>

    <head runat="server">
        <title>发表竞猜帖</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link rel="stylesheet" href="//lf6-cdn-tos.bytecdntp.com/cdn/expire-1-M/bootstrap/5.1.3/css/bootstrap.min.css">
    </head>

    <body>
        <form id="form1" runat="server" class="container mt-3">
            <h2 class="mb-4">发表竞猜帖</h2>
            <div class="form-group">
                <label for="txtTitle">帖子标题：</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtContent">帖子内容：</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control">
                </asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtGuessTitle">竞猜主题：(最多12个字)</label>
                <asp:TextBox ID="txtGuessTitle" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDeadline">截止时间：</label>
                <asp:TextBox ID="txtDeadline" runat="server" CssClass="form-control"></asp:TextBox>
                <small class="form-text text-muted">(格式: yyyy-MM-dd HH:mm)</small>
            </div>
            <div class="form-group">
                <label for="txtOption1">选项1：(最多5个字)</label>
                <asp:TextBox ID="txtOption1" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtOption2">选项2：(最多5个字)</label>
                <asp:TextBox ID="txtOption2" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="发表竞猜帖" OnClick="btnSubmit_Click"
                CssClass="btn btn-primary" />
            <asp:HiddenField ID="hdnClassId" runat="server" Value='<%# classid %>' />
        </form>
    </body>

    </html>