<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuessVote.aspx.cs" Inherits="YaoHuo.Plugin.BBS.GuessVote" %>
    <%@ Import Namespace="YaoHuo.Plugin.Tool" %>

        <!DOCTYPE html>
        <html>

        <head runat="server">
            <title>竞猜下注</title>
            <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
            <link rel="stylesheet"
                href="//lf6-cdn-tos.bytecdntp.com/cdn/expire-1-M/bootstrap/5.1.3/css/bootstrap.min.css" />
        </head>

        <body>
            <form id="form1" runat="server">
                <div class="container">
                    <h2 class="mt-3">竞猜下注</h2>
                    <div class="card mt-3">
                        <div class="card-body">
                            <p class="card-text">
                                竞猜主题：
                                <asp:Literal ID="litGuessTitle" runat="server" />
                            </p>
                            <p class="card-text">
                                下注选项：
                                <asp:Literal ID="litSelectedOption" runat="server" />
                            </p>
                            <div class="form-group" style="display:ruby;">
                                <label for="ddlBetAmount">下注金额：</label>
                                <asp:DropDownList ID="ddlBetAmount" runat="server" CssClass="form-control"
                                    style="width:83px;">
                                </asp:DropDownList>
                            </div>
                            <div class="mt-3">
                                <asp:Button ID="btnConfirm" runat="server" Text="确认下注" OnClick="btnConfirm_Click"
                                    CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click"
                                    CssClass="btn btn-secondary ml-2" />
                            </div>
                            <asp:HiddenField ID="hdnSelectedOptionIndex" runat="server" />
                        </div>
                    </div>
                </div>
            </form>
        </body>

        </html>