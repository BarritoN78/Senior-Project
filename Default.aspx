
<%@ Page Title="Check-in/Checkout" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Grp_5_Web_App._Default" EnableSessionState="True"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="equipment">
        <h2>
       <asp:RadioButton ID="rd_Record_New" runat="server" Text="New Record" AutoPostBack="true" GroupName="grp_Record" CssClass="grp_Record"/>
            <asp:RadioButton ID="rd_Record_Exists" runat="server" Text="Existing Record" AutoPostBack="true" GroupName="grp_Record" CssClass="grp_Record" />
            Equipment Information
        </h2>
        <asp:Panel ID="pn_Periph_Select" runat="server">
            <div class="Periph_Select">
                <asp:Label ID="lbl_Device" runat="server" Text="Device:"></asp:Label>
                <asp:DropDownList ID="drp_Device" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_Device_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pn_OrderID" runat="server">
            <div class="Order_ID">
                <asp:Table ID="tbl_Order_ID" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="120px">
                            <asp:Label ID="lbl_Order_ID" runat="server" Text="Order ID:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="150px">                            
                            <asp:TextBox ID="txt_Order_ID" runat="server" Width="125px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btn_Retrieve" runat="server" Text="Retrieve Data" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </asp:Panel>
        <asp:Table ID="tbl_Equipment" runat="server">
            <asp:TableRow>
                <asp:TableCell Width="120px">
                    <asp:Label ID="lbl_ItemDesc" runat="server" Text="Item Description:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="150px">
                    <asp:TextBox ID="txt_ItemDesc" Width="125px" class="textbox" runat="server" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="60px">
                    <asp:Label ID="lbl_SerialNo" runat="server" Text="Serial #:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="150px">
                    <asp:TextBox ID="txt_SerialNo" Width="125px" runat="server" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lbl_HelpDeskNo" runat="server" Text="Help Desk #:" Width="90px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txt_HelpDeskNo" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lbl_StateNo" runat="server" Text="State #:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txt_StateNo" Width="125px" runat="server" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lbl_ITNo" runat="server" Text="IT #:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txt_ITNo" Width="125px" runat="server" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>                
                <asp:TableCell>
                    <asp:Label ID="lbl_Date" runat="server" Text="Due Date:" Width="70px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drp_Month" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DateChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drp_Day" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DateChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drp_Year" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DateChanged">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lbl_ValidDate" runat="server" Text="The date you entered is invalid" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lbl_DevType" runat="server" Text="Device Type:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drp_DevType" runat="server" AutoPostBack="true">
                        <asp:ListItem>Laptop</asp:ListItem>
                        <asp:ListItem>Monitor</asp:ListItem>
                        <asp:ListItem>PC Tower</asp:ListItem>
                        <asp:ListItem>Mouse</asp:ListItem>
                        <asp:ListItem>Keyboard</asp:ListItem>
                        <asp:ListItem>Tablet</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="3">
                    <asp:Panel ID="pn_EnterType" runat="server">
                        <asp:Label ID="lbl_EnterType" runat="server" Text="Enter Type:"></asp:Label>
                        <asp:TextBox ID="txt_EnterType" runat="server" AutoPostBack="true"></asp:TextBox>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lbl_Notes" runat="server" Text="Notes:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="3">
                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine" Width="200px" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox ID="chk_Return" runat="server" AutoPostBack="true" Text="Return?"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tbl_Periphs" runat="server" Width="97%">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pn_Periph_Rem" runat="server">
                        <asp:Button ID="btn_Periph_Rem" CSSClass="Periph_Rem" runat="server" Text="Remove Device" />
                    </asp:Panel>
                    <asp:Panel ID="pn_Periph_Add" runat="server">
                        <asp:Button ID="btn_Periph_Add" CSSClass="Periph_Add" runat="server" Text="Add Device" />
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <div class="personinfo">        
        <h2>User Information</h2>
        <asp:Table ID="tbl_personinfo" runat="server">
            <asp:TableRow>
                <asp:TableCell Width="80px">
                    <asp:Label ID="lbl_FName" runat="server" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="150px">
                    <asp:TextBox ID="txt_FName" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="80px">
                    <asp:Label ID="lbl_LName" runat="server" Text="Last Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="150px">
                    <asp:TextBox ID="txt_LName" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lbl_Type" runat="server" Text="User Type:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drp_Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FormFiltersChanged">
                        <asp:ListItem>---Select Type---</asp:ListItem>
                        <asp:ListItem>Student</asp:ListItem>
                        <asp:ListItem>Full Time Faculty</asp:ListItem>
                        <asp:ListItem>Part Time Faculty</asp:ListItem>
                        <asp:ListItem>Part Time Staff</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="pn_NewHire" runat="server">
                        <asp:Label ID="lbl_NewHire" runat="server" Text="New Hire?"></asp:Label>
                        <asp:RadioButton ID="rd_NewHireY" runat="server" Text="Yes" AutoPostBack="true"  GroupName="grp_NewHire"/>
                        <asp:RadioButton ID="rd_NewHireN" runat="server" Text="No" AutoPostBack="true" GroupName="grp_NewHire"/>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lbl_PhoneNo" runat="server" Text="Phone #:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txt_PhoneNo" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lbl_Email" runat="server" Text="Email:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txt_Email" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:Panel ID="pn_CollegeID" runat="server">
                        <asp:Table ID="tbl_CollegeID" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lbl_CollegeID" runat="server" Text="College ID:" Width="80px"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txt_CollegeID" runat="server"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <asp:Panel ID="pn_CCAuth" runat="server">
    <div class="CCAuth">
        <h2>Accessory Authorization</h2>
        <asp:Table ID="tbl_CCAuth" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lbl_CCAuth" runat="server" Text="Authorized to obtain accesories?"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox ID="chk_CCAuth" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </asp:Panel>

    <asp:Panel ID="pn_Signatures" runat="server">
    <div class ="AuthBy">
        <h2>Signatures</h2>
        <asp:Table ID="tbl_Signatures" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pn_UserSign" runat="server">
                        <asp:Label ID="lbl_UserSign" runat="server" Text="User Signature:"></asp:Label>
                        <asp:Textbox ID="txt_UserSign" runat="server"></asp:Textbox>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="pn_AuthBy" runat="server">
                        <asp:Label ID="lbl_AuthBy" Width="100px" runat="server" Text="Authorized By:"></asp:Label>
                        <asp:TextBox ID="txt_AuthBy" runat="server"></asp:TextBox>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="pn_FTAuth" runat="server">
                        <asp:Label ID="lbl_FTAuth" Width="200px" runat="server" Text="Full Time Employee Signature:"></asp:Label>
                        <asp:TextBox ID="txt_FTAuth" runat="server"></asp:TextBox>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </asp:Panel>

    <asp:Panel ID="pn_Returns" runat="server">
    <div class="Returns">
        <h2>Return Information</h2>
        <asp:Table ID="tbl_Returns" runat="server">
            <asp:TableRow>
                <asp:TableCell Width="120px">
                    <asp:Label ID="lbl_ReturnStatus" runat="server" Text="Return Status:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="120px">
                    <asp:DropDownList ID="drp_ReturnStatus" runat="server">
                        <asp:ListItem>Not Returned</asp:ListItem>
                        <asp:ListItem>Returned in Good Condition</asp:ListItem>
                        <asp:ListItem>Returned but Damaged</asp:ListItem>
                        <asp:ListItem>Stolen</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </asp:Panel>

    <asp:Panel ID="pn_EULA" runat="server">
    <div class="eula">
        <h2>Equipment Usage Agreement</h2>
        <p>All policies or procedures presented by this agreement are subject to and directed by the college’s main policies set forth in the Policies and Procedures section made available on the MySCC Portal.</p>

        <asp:Panel ID="pn_facEULA" runat="server">
        <div>
            <p>All equipment presented or managed by the Information Technologies Department are owned by Spartanburg Community College.  As an employee of SCC and a member of a particular department within the college, you and your department accept responsibility for any damages or losses to the equipment you are now checking out for use.  Should any damage or loss occur while the employee is in possession of these items, the employee agrees that their department has accepted the responsibility for the $250 deductible.  Any data created or stored on this equipment is also considered the property of your department and ultimately Spartanburg Community College.</p>
            <p>SCC employees that sign for these inventory items may use these items on and off campus at their discretion.  Adjunct employees may use this equipment for a maximum of two semesters as long as the individual is actively employed by SCC.   The employee named below must also routinely return the checked out items below as needed for inventory, updating, and/or repairs as needed.  In the event of an inventory audit, this document is to serve as validation that these items are secured by and remain in the possession of the employee named below. By receiving /checking out equipment from the Information Technologies Department you and your department are aware of and accept responsibility for the items you are checking out. </p>
        </div>
        </asp:Panel>

        <asp:Panel ID="pn_studEULA" runat="server">
        <div>
            <p>Your signature on this agreement gives you the freedom to utilize this equipment on and off the properties of Spartanburg Community College for the length of a semester.  You agree to promptly return this equipment to the SCC IT Department at the end of each semester for the purpose of asset tracking, updating, inspection, or replacement.  In the event of an inventory audit, this document validates that these items are secured by, and remain in your possession. By receiving the designated equipment and signing this agreement, you accept responsibility for the item/s you are checking out.</p>
            <p>Due to the intent of this agreement, neither Spartanburg Community College as a whole, or the SCC IT Department are to be held responsible or liable for the loss of any data stored on this device.  It is recommended that as a member of the SCC Community, you utilize the cloud-based storage and solutions offered by Microsoft Office 365 and provided by Spartanburg Community College for all your data retention needs. </p>
        </div>
        </asp:Panel>
    </div>
    </asp:Panel>

    <div class="EULAread">
        <asp:CheckBox ID="chk_EULAread" runat="server" Text="By checking the box, you verify that the information entered is accurate and the user has agreed to the Equipment Usage Agreement" />
    </div>

    <div class="SubButton">
        <asp:Button ID="btn_Submit" runat="server" Text="Submit"/>
    </div>
</asp:Content>
