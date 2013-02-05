<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessageBoard_WebRole._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Golfer Message Board</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMessageBoard" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="general">
        <div class="title">
            <h1>
                Golfer Message Board
            </h1>
        </div>
        <div class="inputSection">
            <dl>
                <dt>
                    <label for="NameLabel">Name:</label></dt>
                <dd>
                    <asp:TextBox 
                       ID="txtName" 
                       runat="server" 
                       CssClass="field"/>
                    <asp:RequiredFieldValidator 
                      ID="NameRequiredValidator" 
                      runat="server" 
                      ControlToValidate="txtName"
                      Text="*" />
                </dd>
                <dt>
                    <label for="MessageLabel">Message:</label>
                </dt>
                <dd>
                    <asp:TextBox 
                       ID="txtMessage" 
                       runat="server" 
                       TextMode="MultiLine" 
                       CssClass="field" />
                    <asp:RequiredFieldValidator 
                       ID="MessageRequiredValidator" 
                       runat="server" 
                       ControlToValidate="txtMessage"
                       Text="*" />
                </dd>
            </dl>
            <div class="submitSection">
                <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text = "Send" />
            </div>
        </div>

        <asp:UpdatePanel ID="upMessageBoard" runat="server">
            <ContentTemplate>
                <asp:DataList
                    ID="dlMessages"
                    runat="server"
                    DataSourceID="dsMessages">
                    <ItemTemplate>
                        <div class="signature">
                            <div class="signatureDescription">
                                <div class="signatureName">
                                    <%# Eval("GolferName") %>
                                </div>
                                <div class="signatureSays">
                                    says
                                </div>
                                <div class="signatureDate">
                                    <%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>
                                </div>
                                <div class="signatureMessage">
                                    "<%# Eval("GolferMessage") %>"
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <asp:Timer 
                    ID="tmrRefreshMsgs" 
                    runat="server"
                    Interval="15000"
                    OnTick="tmrRefreshMsgs_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource
            ID="dsMessages"
            runat="server"
            SelectMethod="GetEntries"
            TypeName="MessageBoard_Data.MessageBoardDataSource">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
