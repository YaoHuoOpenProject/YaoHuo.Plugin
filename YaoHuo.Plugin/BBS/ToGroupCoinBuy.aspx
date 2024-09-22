<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToGroupCoinBuy.aspx.cs" Inherits="YaoHuo.Plugin.BBS.toGroupCoinBuy" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("妖晶购买身份"), wmlVo)); // 显示头
    strhtml.Append("<div class=\"title\">");
    strhtml.Append("妖晶购买 | ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/togroupbuy.aspx?siteid=" + this.siteid + "&classid=" + this.classid + "&toid=" + this.toid + "\">" + this.GetLang("RMB购买") + "</a>");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>");
        strhtml.Append("购买身份成功！");
        strhtml.Append("</b><br />[<a href=\"/bbs/userinfo.aspx?touserid=" + this.userid + "\">进入空间查看</a>]<br />");
    }
    else if (this.INFO == "CLOSE")
    {
        strhtml.Append("<b>暂时无法购买此身份！</b><br />");
    }
    else if (this.INFO == "NOTRIGHT")
    {
        strhtml.Append("<b>*一次只能购买大于[当前我的身份级别]一级的身份级别！请购买其它身份级别！</b><br />");
    }
    else if (this.INFO == "PWERR")
    {
        strhtml.Append("<b>密码错误！</b><br />");
    }
    else if (this.INFO == "NUM")
    {
        strhtml.Append("<b>金额需要数字！</b><br />");
    }
    else if (this.INFO == "NOTMONEY")
    {
        strhtml.Append("<b>您的妖晶数量不足！</b><br />");
    }
    strhtml.Append("</div>");
    if (this.INFO != "OK" && this.INFO != "CLOSE" && this.INFO != "NOTRIGHT" && this.INFO != "MASTERNO")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("我当前妖晶 <b>" + userVo.money.ToString("f0") + "</b> [<a href=\"" + this.http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + this.siteid + "\">充值</a>]<br />");
        strhtml.Append("我当前身份 <b><span class=\"current-identity\">" + WapTool.GetMyID(userVo.idname, lang) + "<br />" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime, this.lang) + "</span></b><br />");
        strhtml.Append("<br />您要购买的身份:<b><span class=\"target-identity\">" + WapTool.showImg(idVo.subclassName) + "</span></b><br />");
        strhtml.Append("<form name=\"g1\" action=\"" + http_start + "bbs/togroupcoinbuy.aspx\" method=\"get\">");
        string numValue = Request.QueryString["num"] ?? (toid == "105" ? "12" : this.num);
        strhtml.Append(this.GetLang("购买|购买|month Number") + " <input type=\"number\" name=\"num\" value=\"" + numValue + "\" min=\"1\" style=\"margin-right:-5px;margin-left:-2px;width:30px;text-align:center\" " + (toid == "105" ? "min=\"12\"" : "") + " oninput=\"updatePrice()\" /> 个月");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"toid\" value=\"" + toid + "\" />");
        strhtml.Append("</form><br />");
        strhtml.Append("需要妖晶 <b><span id=\"price\" data-initial-price=\"" + (idVo.rank * 12500) + "\">" + (idVo.rank * 12500 * int.Parse(numValue)).ToString("F0") + "</span></b><br />");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/togroupcoinbuy.aspx\" method=\"post\">");
        strhtml.Append("输入密码 ");
        strhtml.Append("<input type=\"password\" name=\"changePW\" value=\"\" required /><br />");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"toid\" value=\"" + toid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"num\" value=\"" + num + "\" id=\"hiddenNum\" />");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\" />");
        strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("确定支付|确定支付|submit play") + "\" />");
        strhtml.Append("</form>");
        // 显示身份转换
        strhtml.Append("<div class=\"tip\" id=\"conversionInfo\" style=\"margin-top:10px;\">");
        strhtml.Append("当前身份可折算天数：<span id=\"convertibleDays\">0</span>天<br />");
        strhtml.Append("购买后预计有效天数：<span id=\"totalDays\">0</span>天");
        strhtml.Append("</div>");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"/wapindex.aspx?classid=171\">返回上级</a> ");
    strhtml.Append("<a href=\"/\">返回首页</a>");
    strhtml.Append("</div></div>");
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo)); // 显示底部
%>
<!-- 在页面中嵌入后端数据到JavaScript -->
<script>
    // 将后端传递的数据赋值给JavaScript变量
    var currentRank = <%= CurrentRank %>;
    var remainingDays = <%= RemainingDays %>;
    var targetRank = <%= TargetRank %>;
    var currentId = <%= CurrentId %>;
    var toid = <%= ToidNum %>;

    document.addEventListener("DOMContentLoaded", function () {
        const colorMap = {
            '绿色昵称': '#25a444',
            '红色昵称': '#FF0000',
            '蓝色昵称': '#228aff',
            '紫色昵称': '#c000ff',
            '粉色昵称': '#ff6363',
            '粉色昵称': '#ff00c0',
            '灰色昵称': '#BDBDBD',
            '/netimages/vip.gif': { name: '红名VIP', color: '#FF0000' },
            '/netimages/年费vip.gif': { name: '年费VIP', color: '#c000ff' },
            '/netimages/靓号.gif': { name: '靓', color: '#FF7F00' },
            '/netimages/帅.gif': { name: '帅', color: '#228aff' },
            '/netimages/newvip.gif': { name: '金名VIP', color: '#fa6700' }
        };

        function processIdentityElement(identityElement) {
            if (identityElement) {
                const identityText = identityElement.innerHTML;
                const regex = /^(绿色昵称|红色昵称|蓝色昵称|紫色昵称|粉色昵称|灰色昵称)/;
                const match = identityText.match(regex);

                if (match && colorMap[match[0]]) {
                    const coloredText = `<span style="color: ${colorMap[match[0]]};">${match[0]}</span>`;
                    identityElement.innerHTML = identityText.replace(match[0], coloredText);
                }

                const imgElement = identityElement.querySelector('img');
                if (imgElement) {
                    const imgSrc = imgElement.getAttribute('src').toLowerCase();
                    if (colorMap[imgSrc]) {
                        const { name, color } = colorMap[imgSrc];
                        imgElement.insertAdjacentHTML('afterend', `<span style="color: ${color}; margin-left: 3px;">${name}</span>`);
                    }
                }
            }
        }

        // 处理 current-identity
        const currentIdentityElement = document.querySelector('.current-identity');
        processIdentityElement(currentIdentityElement);

        // 处理 target-identity
        const targetIdentityElement = document.querySelector('.target-identity');
        processIdentityElement(targetIdentityElement);

        // 更新价格和转换信息
        updatePrice();
        updateConversionInfo();
    });

    function updatePrice() {
        const numInput = document.querySelector('input[name="num"]');
        const priceElement = document.getElementById('price');
        const num = parseInt(numInput.value) || 0;
        const initialPrice = parseFloat(priceElement.getAttribute('data-initial-price'));
        const totalPrice = num * initialPrice;
        priceElement.textContent = totalPrice.toFixed(0); // 修改为 toFixed(0) 以确保显示整数

        // 更新表单中的num值
        document.getElementById('hiddenNum').value = num;

        // 更新转换信息
        updateConversionInfo();
    }

    function updateConversionInfo() {
        // 使用后端传递的变量
        var convertibleDays = 0;
        var purchasedDays = (parseInt(document.querySelector('input[name="num"]').value) || 0) * 30;
        var totalDays = 0;

        if (toid === currentId) {
            // 同一身份，直接延长有效期
            convertibleDays = 0; // 不需要折算
            totalDays = remainingDays + purchasedDays;
        } else if (currentRank > 0 && targetRank > 0) {
            // 不同身份，进行转换
            var currentValue = (currentRank / 30) * remainingDays;
            var newIdentityDaysDecimal = (currentValue / targetRank) * 30;
            convertibleDays = Math.floor(newIdentityDaysDecimal);
            totalDays = convertibleDays + purchasedDays;
        } else {
            // 无法转换
            convertibleDays = 0;
            totalDays = purchasedDays;
        }

        // 更新页面显示
        if (convertibleDays > 0) {
            document.getElementById('convertibleDays').textContent = convertibleDays;
            document.getElementById('conversionInfo').style.display = 'block';
        } else {
            document.getElementById('convertibleDays').parentElement.style.display = 'none';
        }
        document.getElementById('totalDays').textContent = totalDays;
    }
</script>