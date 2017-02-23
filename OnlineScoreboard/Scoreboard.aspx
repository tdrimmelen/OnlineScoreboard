<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scoreboard.aspx.cs" Inherits="OnlineScoreboard.Scoreboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="refresh" content="20" >
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="theTime" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="XX-Large" Text="??:??" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="theScore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="XX-Large" Text="?? - ??" ForeColor="Red"></asp:Label>
    
    </div>
    </form>
</body>
</html>
