<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuessMod.aspx.cs" Inherits="YaoHuo.Plugin.BBS.GuessMod" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>更新竞猜结果</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link rel="stylesheet" href="//lf6-cdn-tos.bytecdntp.com/cdn/expire-1-M/bootstrap/5.1.3/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mt-3">更新竞猜结果</h2>
            <div class="card mt-3">
                <div class="card-body">
                    <div class="mb-3">
                        <label for="lblGuessTitle" class="form-label">竞猜主题：</label>
                        <asp:Label ID="lblGuessTitle" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="mb-3">
                        <label for="lblDeadline" class="form-label">截止时间：</label>
                        <asp:Label ID="lblDeadline" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblResult" runat="server" CssClass="form-control-plaintext" Visible="false"></asp:Label>
                    </div>
                    <asp:PlaceHolder ID="phOptions" runat="server">
                        <div class="mb-3">
                            <label for="ddlOptions" class="form-label">选择获胜选项：</label>
                            <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <asp:Button ID="btnSubmit" runat="server" Text="提交结果" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                    </asp:PlaceHolder>
                    <asp:HiddenField ID="hdnGuessingId" runat="server" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>