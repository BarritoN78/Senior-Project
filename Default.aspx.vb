Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Security
Public Class _Default
    Inherits Page

    Private periph As Integer 'Used to determine SQL loop length

    'Defining Arrays
    Dim str_serialno() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_itemdesc() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_stateno() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_itno() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_notes() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_devtype() As String = {"", "", "", "", "", "", "", "", "", ""}
    Dim str_orderidarray() As String = {"", "", "", "", "", "", "", "", "", ""}

    Dim int_drpdeviceindex As Integer
    Dim RecordExists As Boolean = False
    Dim InvalidDate As Boolean = False

    Dim G5SQL As SqlConnection = New SqlConnection("Server=198.71.227.2; Database=AAA-SCC-CPT-275-Capstone; User ID=scccpt275capstone; Password=scccpt275capstone")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If ViewState("periph") IsNot Nothing Then
            periph = ViewState("periph")
        Else
            periph = 0
            ViewState("periph") = periph
        End If
        If ViewState("str_itemdesc") IsNot Nothing Then
            str_itemdesc = ViewState("str_itemdesc")
            str_itno = ViewState("str_itno")
            str_stateno = ViewState("str_stateno")
            str_serialno = ViewState("str_serialno")
            str_notes = ViewState("str_notes")
            str_devtype = ViewState("str_devtype")
            str_orderidarray = ViewState("str_orderidarray")
            RecordExists = ViewState("RecordExists")
            InvalidDate = ViewState("InvalidDate")
        Else
            ViewState("str_itemdesc") = str_itemdesc
            ViewState("str_itno") = str_itno
            ViewState("str_stateno") = str_stateno
            ViewState("str_serialno") = str_serialno
            ViewState("str_notes") = str_notes
            ViewState("str_devtype") = str_devtype
            ViewState("str_orderidarrat") = str_orderidarray
            ViewState("RecordExists") = RecordExists
            ViewState("InvalidDate") = InvalidDate
        End If
        Dim int_Year As Integer = 1990
        Dim monthidx As Integer = drp_Month.SelectedIndex
        Dim yearidx As Integer = drp_Year.SelectedIndex
        Dim dayidx As Integer = drp_Day.SelectedIndex
        Dim deviceidx As Integer = drp_Device.SelectedIndex
        drp_Month.Items.Clear()
        drp_Year.Items.Clear()
        drp_Day.Items.Clear()
        drp_Device_Update()
        For count = 0 To 50 Step 1
            drp_Year.Items.Add(int_Year + count)
            If count < 12 Then
                drp_Month.Items.Add(count + 1)
            End If
            If count < 31 Then
                drp_Day.Items.Add(count + 1)
            End If
        Next
        If IsPostBack Then
            drp_Month.SelectedIndex = monthidx
            drp_Year.SelectedIndex = yearidx
            drp_Day.SelectedIndex = dayidx
            drp_Device.SelectedIndex = deviceidx
        Else
            drp_Month.SelectedIndex = (Month(Date.Now) - 1)
            drp_Day.SelectedIndex = (Day(Date.Now) - 1)
            drp_Year.SelectedIndex = (Year(Date.Now) - 1990)
        End If
        FormFilterOut()
        lbl_ValidDate.Visible = False
    End Sub

    Protected Sub AddPeripheral(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Periph_Add.Click
        pn_Periph_Select.Visible = True
        btn_Periph_Rem.Visible = True
        If periph < 9 Then
            periph += 1
            ViewState("periph") = periph
            str_itemdesc(periph) = "*New Device*"
            ViewState("str_itemdesc") = str_itemdesc
            ViewState("str_itno") = str_itno
            ViewState("str_stateno") = str_stateno
            ViewState("str_serialno") = str_serialno
            ViewState("str_notes") = str_notes
            ViewState("str_devtype") = str_devtype
            drp_Device.Items.Add(str_itemdesc(periph))
            drp_Device.SelectedIndex = periph
            TextboxUpdate(periph)
        Else
            Response.Write("<script language=""javascript"">alert('Maximum number of devices reached');</script>")
        End If
    End Sub

    Protected Sub RemovePeripheral(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Periph_Rem.Click
        Dim currentindex As Integer = drp_Device.SelectedIndex
        Dim shiftloop As Integer
        Dim count As Integer = 0
        If periph > 0 Then
            periph -= 1
            ViewState("periph") = periph
            If currentindex > 0 Then
                drp_Device.SelectedIndex = currentindex - 1
            End If
            str_itemdesc(currentindex) = ""
            str_itno(currentindex) = ""
            str_serialno(currentindex) = ""
            str_stateno(currentindex) = ""
            str_notes(currentindex) = ""
            str_devtype(currentindex) = ""
            shiftloop = periph - currentindex
            While count <= shiftloop
                str_itemdesc(currentindex + count) = str_itemdesc(currentindex + count + 1)
                str_itno(currentindex + count) = str_itno(currentindex + count + 1)
                str_serialno(currentindex + count) = str_serialno(currentindex + count + 1)
                str_stateno(currentindex + count) = str_stateno(currentindex + count + 1)
                str_notes(currentindex + count) = str_notes(currentindex + count + 1)
                str_devtype(currentindex + count) = str_devtype(currentindex + count + 1)
                If (currentindex + count + 1) > 10 Then
                    str_itemdesc(currentindex + count + 1) = ""
                    str_itno(currentindex + count + 1) = ""
                    str_serialno(currentindex + count + 1) = ""
                    str_stateno(currentindex + count + 1) = ""
                    str_notes(currentindex + count + 1) = ""
                    str_devtype(currentindex + count + 1) = ""
                End If
                count += 1
            End While
            TextboxUpdate(drp_Device.SelectedIndex)
            ViewState("str_itemdesc") = str_itemdesc
            ViewState("str_itno") = str_itno
            ViewState("str_stateno") = str_stateno
            ViewState("str_serialno") = str_serialno
            ViewState("str_notes") = str_notes
            ViewState("str_devtype") = str_devtype
            drp_Device_Update()
            If periph = 0 Then
                pn_Periph_Select.Visible = False
                btn_Periph_Rem.Visible = False
            End If
        Else
            Response.Write("<script language=""javascript"">alert('Minimum number of devices reached');</script>")
        End If
    End Sub

    Protected Sub drp_Device_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drp_Device.SelectedIndexChanged
        Dim currentindex As Integer = drp_Device.SelectedIndex
        ViewState("str_itemdesc") = str_itemdesc
        ViewState("str_itno") = str_itno
        ViewState("str_stateno") = str_stateno
        ViewState("str_serialno") = str_serialno
        ViewState("str_notes") = str_notes
        ViewState("str_devtype") = str_devtype

        'Load Selected Data
        TextboxUpdate(currentindex)
    End Sub

    Protected Sub drp_Device_Update()
        Dim deviceloopctrl As Integer = 1
        drp_Device.Items.Clear()
        If periph >= 0 Then
            drp_Device.Items.Add(str_itemdesc(0))
            While deviceloopctrl <= periph
                drp_Device.Items.Add(str_itemdesc(deviceloopctrl))
                deviceloopctrl += 1
            End While
        End If
    End Sub

    Protected Sub TextboxUpdate(index As Integer)
        txt_ItemDesc.Text = str_itemdesc(index)
        txt_ITNo.Text = str_itno(index)
        txt_SerialNo.Text = str_serialno(index)
        txt_StateNo.Text = str_stateno(index)
        txt_Notes.Text = str_notes(index)
        Select Case (str_devtype(index))
            Case "Laptop"
                drp_DevType.SelectedIndex = 0
            Case "Monitor"
                drp_DevType.SelectedIndex = 1
            Case "PC Tower"
                drp_DevType.SelectedIndex = 2
            Case "Mouse"
                drp_DevType.SelectedIndex = 3
            Case "Keyboard"
                drp_DevType.SelectedIndex = 4
            Case "Tablet"
                drp_DevType.SelectedIndex = 5
            Case Else
                drp_DevType.SelectedIndex = 6
                pn_EnterType.Visible = True
                txt_EnterType.Text = str_devtype(index)
        End Select
    End Sub

    Protected Sub DateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drp_Day.SelectedIndexChanged
        Dim month As Integer = drp_Month.SelectedIndex
        Dim day As Integer = (1 + drp_Day.SelectedIndex)
        Dim year As Integer = (1990 + drp_Year.SelectedIndex)
        Select Case (month)
            Case 1
                If (year Mod 4) = 0 Then
                    If day > 29 Then
                        lbl_ValidDate.Visible = True
                        InvalidDate = True
                    Else
                        lbl_ValidDate.Visible = False
                        InvalidDate = False
                    End If
                Else
                    If day > 28 Then
                        lbl_ValidDate.Visible = True
                        InvalidDate = True
                    Else
                        lbl_ValidDate.Visible = False
                        InvalidDate = False
                    End If
                End If
            Case 3, 5, 8, 10
                If day > 30 Then
                    lbl_ValidDate.Visible = True
                    InvalidDate = True
                Else
                    lbl_ValidDate.Visible = False
                    InvalidDate = False
                End If
            Case Else
                lbl_ValidDate.Visible = False
                InvalidDate = False
        End Select
        ViewState("InvalidDate") = InvalidDate
    End Sub

    Protected Sub FormFiltersChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drp_Type.SelectedIndexChanged, chk_Return.CheckedChanged, rd_NewHireY.CheckedChanged
        FormFilterOut()
    End Sub

    Protected Sub TextboxTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_ItemDesc.TextChanged, txt_ITNo.TextChanged, txt_SerialNo.TextChanged, txt_StateNo.TextChanged, txt_Notes.TextChanged, txt_EnterType.TextChanged, drp_DevType.SelectedIndexChanged
        UpdateVariables(drp_Device.SelectedIndex)
    End Sub
    Private Sub UpdateVariables(index As Integer)
        If txt_ItemDesc.Text.Equals(Nothing) Then
            str_itemdesc(index) = ""
        Else
            str_itemdesc(index) = txt_ItemDesc.Text
        End If
        If txt_ITNo.Text.Equals(Nothing) Then
            str_itno(index) = ""
        Else
            str_itno(index) = txt_ITNo.Text
        End If
        If txt_SerialNo.Text.Equals(Nothing) Then
            str_serialno(index) = ""
        Else
            str_serialno(index) = txt_SerialNo.Text
        End If
        If txt_StateNo.Text.Equals(Nothing) Then
            str_stateno(index) = ""
        Else
            str_stateno(index) = txt_StateNo.Text
        End If
        If txt_Notes.Text.Equals(Nothing) Then
            str_notes(index) = ""
        Else
            str_notes(index) = txt_Notes.Text
        End If
        Select Case (drp_DevType.SelectedIndex)
            Case 6
                str_devtype(index) = txt_EnterType.Text
            Case Else
                str_devtype(index) = drp_DevType.SelectedItem.Text
        End Select
    End Sub
    Private Sub FormFilterOut()
        pn_FTAuth.Visible = False
        pn_facEULA.Visible = False
        pn_studEULA.Visible = False
        pn_CollegeID.Visible = False
        pn_NewHire.Visible = False
        pn_Periph_Select.Visible = False
        btn_Periph_Add.Visible = False
        btn_Periph_Rem.Visible = False
        pn_EnterType.Visible = False
        pn_OrderID.Visible = False
        If drp_Type.SelectedIndex = 1 Or drp_Type.SelectedIndex = 5 Then
            rd_NewHireN.Checked = True
            rd_NewHireY.Checked = False
        End If
        PeriphShow()
        CCAuthShow()
        ReturnShow()
        TypeShow()
        EnterTypeShow()
        RecordExistsShow()
        ValidDateShow()
    End Sub

    Protected Sub SubmitClick(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Submit.Click
        Try
            'SQL Query Strings
            Dim G5_Peopleinsert As String = "INSERT INTO G5_People(user_id, user_first_name, user_last_name, help_desk_no," &
                                            " college_id, email, phone_number) VALUES(@uid, @fname, @lname, @helpdeskno, @colid, @email, @phone)"
            Dim G5_Assetsinsert As String = "INSERT INTO G5_Assets(serial_number, item_description, device_type, state_num," &
                                                " it_number, checkout) VALUES(@serialno, @itemdesc, @devtype, @stateno, @itno, @checkedout)"
            Dim G5_Checkinginsert As String = "INSERT INTO G5_Checking(order_id, serial_number, user_type, accessories, user_id," &
                                                " authorize_by, full_time_employee, user_signature, check_out, due_date, return_condition, notes)" &
                                                " VALUES(@orderno, @serialno, @type, @accauth, @uid, @auth_by, @ftempauth, @usersign, @codate, @duedate, @retstat, @notes)"

            Dim G5_Peopleupdate As String = "UPDATE G5_People SET user_first_name = @fname, user_last_name = @lname, help_desk_no = @helpdeskno," &
                                                " college_id = @colid, email = @email, phone_number = @phone" &
                                                " WHERE user_id = @uid"
            Dim G5_Assetsupdate As String = "UPDATE G5_Assets SET item_description = @itemdesc, device_type = @devtype, state_num = @stateno," &
                                                " it_number = @itno, checkout = @checkedout" &
                                                " WHERE serial_number = @serialno"
            Dim G5_Checkingupdate As String = "UPDATE G5_Checking SET serial_number = @serialno, user_type = @type, accessories = @accauth, user_id = @uid," &
                                                " authorize_by = @auth_by, full_time_employee = @ftempauth, user_signature = @usersign, due_date = @duedate, return_condition = @retstat, notes = @notes" &
                                                " WHERE order_id = @orderno"

            'SQL Execution Variables
            Dim inputtransaction As SqlTransaction
            Dim Peoplecomm As SqlCommand = New SqlCommand("", G5SQL)
            Dim Assetscomm As SqlCommand = New SqlCommand("", G5SQL)
            Dim Checkingcomm As SqlCommand = New SqlCommand("", G5SQL)

            'Extra Variables
            Dim str_date As String = drp_Year.SelectedItem.ToString() + "-" + drp_Month.SelectedItem.ToString() + "-" + drp_Day.SelectedItem.ToString()
            Dim dt_date As Date = Convert.ToDateTime(str_date)
            Dim ch_chk As Char = " "
            Dim str_ordernoinit As String = ""
            Dim str_orderno As String = ""
            Dim str_uid As String = ""
            Dim str_errorMessage As String = ""
            Dim SQLCommit As Boolean = True
            Dim SQLloop As Integer = 0

            'Set Command string based on new or existing record
            If RecordExists = False Then
                Peoplecomm.CommandText = G5_Peopleinsert
                Assetscomm.CommandText = G5_Assetsinsert
                Checkingcomm.CommandText = G5_Checkinginsert
            Else
                Peoplecomm.CommandText = G5_Peopleupdate
                Assetscomm.CommandText = G5_Assetsupdate
                Checkingcomm.CommandText = G5_Checkingupdate
            End If

            'Checking for invalid static data
            If chk_EULAread.Checked = False Then
                SQLCommit = False
                str_errorMessage += "\n User Must Read EULA; "
            End If
            If txt_FName.Text = Nothing Then
                SQLCommit = False
                str_errorMessage += "\n First Name cannot be null; "
            End If
            If txt_LName.Text = Nothing Then
                SQLCommit = False
                str_errorMessage += "\n Last Name cannot be null; "
            End If
            If txt_AuthBy.Text = Nothing Then
                SQLCommit = False
                str_errorMessage += "\n Authorized By cannot be null; "
            End If
            If txt_UserSign.Text = Nothing Then
                SQLCommit = False
                str_errorMessage += "\n User Signature cannot be null; "
            End If
            If lbl_ValidDate.Visible Then
                SQLCommit = False
                str_errorMessage += "\n Date entered is invalid; "
            End If

            'Opening Connection to SQL DB
            OpenSQL()

            'Determine initial order number. New Record Only
            If RecordExists = False Then
                Dim OrderCountsQuery As String = "SELECT COUNT(*) FROM G5_Checking" &
                                                    " WHERE order_id NOT LIKE @param"
                Dim OrderCountsSQL As SqlCommand = New SqlCommand(OrderCountsQuery, G5SQL)
                OrderCountsSQL.Parameters.AddWithValue("@param", "%P%")
                Dim OrderCountsRes As Integer = OrderCountsSQL.ExecuteScalar() + 1
                If OrderCountsRes < 10 Then
                    str_ordernoinit = "0" + OrderCountsRes.ToString
                Else
                    str_ordernoinit = OrderCountsRes.ToString
                End If
            End If

            'Determine user id
            Dim UserIDSQL As SqlCommand = New SqlCommand("", G5SQL)
            If RecordExists = False Then 'New Records
                Dim NewUserIDQuery As String = "SELECT COUNT(*) FROM G5_People" &
                                        " WHERE user_id LIKE @param"
                UserIDSQL.CommandText = NewUserIDQuery
                Select Case (drp_Type.SelectedIndex)
                    Case 0 'Default. user_type is a not null field
                        SQLCommit = False
                    Case 1 'Student
                        str_uid = "S"
                    Case 2, 3, 4 'Faculty/Staff
                        str_uid = "W"
                    Case 5 'Other
                        str_uid = "O"
                End Select
                UserIDSQL.Parameters.AddWithValue("@param", str_uid + "%")
                Dim UserIDRes As Integer = UserIDSQL.ExecuteScalar() + 1
                If UserIDRes < 10 Then
                    str_uid += "0" + UserIDRes.ToString
                Else
                    str_uid += UserIDRes.ToString
                End If
            Else 'Existing Records
                Dim ExistingUserIDQuery As String = "SELECT user_id FROM G5_Checking" &
                                                    " WHERE order_id = @orderno"
                UserIDSQL.CommandText = ExistingUserIDQuery
                UserIDSQL.Parameters.AddWithValue("@orderno", str_orderidarray(0))
                str_uid = UserIDSQL.ExecuteScalar()
            End If

            'Determine accessory authorizataion
            Dim ch_CCAuth As Char = " "
            If chk_CCAuth.Checked = True Then
                ch_CCAuth = "Y"
            Else
                ch_CCAuth = "N"
            End If

            'Defining and Filling Static SQL Parameters
            Dim uid As SqlParameter = New SqlParameter("@uid", str_uid)
            Dim uid_1 As SqlParameter = New SqlParameter("@uid", str_uid)
            Dim fname As SqlParameter = New SqlParameter("@fname", txt_FName.Text)
            Dim lname As SqlParameter = New SqlParameter("@lname", txt_LName.Text)
            Dim type As SqlParameter = New SqlParameter("@type", drp_Type.SelectedItem.ToString())
            Dim helpdeskno As SqlParameter = New SqlParameter("@helpdeskno", txt_HelpDeskNo.Text)
            Dim colid As SqlParameter = New SqlParameter("@colid", txt_CollegeID.Text)
            Dim email As SqlParameter = New SqlParameter("@email", txt_Email.Text)
            Dim phone As SqlParameter = New SqlParameter("@phone", txt_PhoneNo.Text)
            Dim auth_by As SqlParameter = New SqlParameter("@auth_by", txt_AuthBy.Text)
            Dim ftempauth As SqlParameter = New SqlParameter("@ftempauth", txt_FTAuth.Text)
            Dim usersign As SqlParameter = New SqlParameter("@usersign", txt_UserSign.Text)
            Dim codate As SqlParameter = New SqlParameter("@codate", Date.Now)
            Dim duedate As SqlParameter = New SqlParameter("@duedate", dt_date)
            Dim retstat As SqlParameter = New SqlParameter("@retstat", drp_ReturnStatus.SelectedItem.ToString())
            Dim accauth As SqlParameter = New SqlParameter("@accauth", ch_CCAuth)

            'Defining Dynamic SQL Parameters
            Dim serialno As SqlParameter
            Dim itemdesc As SqlParameter
            Dim serialno_1 As SqlParameter
            Dim stateno As SqlParameter
            Dim itno As SqlParameter
            Dim checkedout As SqlParameter
            Dim orderno As SqlParameter
            Dim notes As SqlParameter
            Dim devtype As SqlParameter

            'Setting transactions for each SQL
            inputtransaction = G5SQL.BeginTransaction()
            Peoplecomm.Transaction = inputtransaction
            Assetscomm.Transaction = inputtransaction
            Checkingcomm.Transaction = inputtransaction

            If SQLCommit = True Then
                'Peoplecomm Parameters
                Peoplecomm.Parameters.Add(uid)
                Peoplecomm.Parameters.Add(fname)
                Peoplecomm.Parameters.Add(lname)
                Peoplecomm.Parameters.Add(helpdeskno)
                Peoplecomm.Parameters.Add(colid)
                Peoplecomm.Parameters.Add(email)
                Peoplecomm.Parameters.Add(phone)
                Peoplecomm.ExecuteNonQuery()
            End If

            'Adding Parameters to each SQL and executing
            'Looping SQL Executions for added peripherals
            While SQLloop <= periph And SQLCommit = True
                'Checking checkin status based on serial number. Returns noting if the serial number does not exist in the DB
                Dim COStatusQuery As String = "SELECT checkout FROM G5_Assets" &
                                                " WHERE serial_number = @serialno"
                Dim COStatusSQL As SqlCommand = New SqlCommand(COStatusQuery, G5SQL)
                COStatusSQL.Transaction = inputtransaction
                COStatusSQL.Parameters.AddWithValue("@serialno", str_serialno(SQLloop))
                Dim COStatusRes As String = COStatusSQL.ExecuteScalar()
                'Assigns value to ch_chk and validates data based on status. 
                If COStatusRes = "N" Or COStatusRes = Nothing Then
                    ch_chk = "Y"
                    If chk_Return.Checked = True Then
                        SQLCommit = False
                        str_errorMessage += "\n " + str_serialno(SQLloop) + " is not currently checked out; " 'A device cannot be returned if you already have it
                    End If
                Else
                    ch_chk = "N"
                    If chk_Return.Checked = False Then
                        SQLCommit = False
                        str_errorMessage += "\n " + str_serialno(SQLloop) + " is already checked out; " 'A checked-out device cannot be rechecked-out without being returned
                    End If
                End If
                checkedout = New SqlParameter("@checkedout", ch_chk)

                'Filling Dynamic Parameters and Checking for invalid data
                If str_serialno(SQLloop) = "" Then
                    serialno = New SqlParameter("@serialno", Nothing)
                    serialno_1 = New SqlParameter("@serialno", Nothing)
                    SQLCommit = False
                    str_errorMessage += "\n str_serialno(" + SQLloop + ") : Serial # cannot be null; "
                Else
                    serialno = New SqlParameter("@serialno", str_serialno(SQLloop))
                    serialno_1 = New SqlParameter("@serialno", str_serialno(SQLloop))
                End If
                If str_stateno(SQLloop).Length = 7 Then
                    stateno = New SqlParameter("@stateno", str_stateno(SQLloop))
                Else
                    stateno = New SqlParameter("@stateno", Nothing)
                    SQLCommit = False
                    str_errorMessage += "\n str_stateno(" + SQLloop + ") : State # can only be 7 characters long; "
                End If
                If str_itno(SQLloop) = "" Then
                    itno = New SqlParameter("@itno", Nothing)
                    SQLCommit = False
                    str_errorMessage += "\n str_itno(" + SQLloop + ") : IT # cannot be null; "
                Else
                    itno = New SqlParameter("@itno", str_itno(SQLloop))
                End If
                If RecordExists = False Then
                    If SQLloop > 0 Then
                        str_orderno = str_ordernoinit + "P" + SQLloop.ToString
                    Else
                        str_orderno = str_ordernoinit
                    End If
                Else
                    str_orderno = str_orderidarray(SQLloop)
                End If
                orderno = New SqlParameter("@orderno", str_orderno)
                notes = New SqlParameter("@notes", str_notes(SQLloop))
                itemdesc = New SqlParameter("@itemdesc", str_itemdesc(SQLloop))
                devtype = New SqlParameter("@devtype", str_devtype(SQLloop))

                If SQLCommit = True Then
                    'Assetscomm Parameters
                    Assetscomm.Parameters.Clear()
                    Assetscomm.Parameters.Add(serialno)
                    Assetscomm.Parameters.Add(itemdesc)
                    Assetscomm.Parameters.Add(stateno)
                    Assetscomm.Parameters.Add(itno)
                    Assetscomm.Parameters.Add(checkedout)
                    Assetscomm.Parameters.Add(devtype)
                    Assetscomm.ExecuteNonQuery()

                    'Checking Parameters
                    Checkingcomm.Parameters.Clear()
                    Checkingcomm.Parameters.Add(orderno)
                    Checkingcomm.Parameters.Add(serialno_1)
                    Checkingcomm.Parameters.Add(uid_1)
                    Checkingcomm.Parameters.Add(auth_by)
                    Checkingcomm.Parameters.Add(ftempauth)
                    Checkingcomm.Parameters.Add(usersign)
                    If RecordExists = False Then
                        Checkingcomm.Parameters.Add(codate)
                    End If
                    Checkingcomm.Parameters.Add(duedate)
                    Checkingcomm.Parameters.Add(retstat)
                    Checkingcomm.Parameters.Add(type)
                    Checkingcomm.Parameters.Add(notes)
                    Checkingcomm.Parameters.Add(accauth)
                    Checkingcomm.ExecuteNonQuery()
                End If
                SQLloop += 1
            End While

            'Commiting the SQL Transaction
            If SQLCommit = True Then
                inputtransaction.Commit()
                Response.Write("<script language=""javascript"">alert('Form Submitted and Email Sent');</script>")
            Else
                inputtransaction.Rollback()
                Response.Write("<script language=""javascript"">alert('Form Failed to Submit: " + str_errorMessage + "');</script>")
                My.Log.WriteEntry(str_errorMessage)
            End If
            G5SQL.Close()
        Catch ex As Exception
            Response.Write("<script language=""javascript"">alert('Form Failed to Submit: " + ex.Message + "');</script>")
            My.Log.WriteEntry(ex.Message)
        End Try
    End Sub

    Protected Sub RetrieveClick(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Retrieve.Click
        Dim str_initorderid As String = ""
        Dim str_orderid As String = ""
        Dim str_uid As String = ""
        Dim str_CC_Auth As String = ""
        Dim str_usertype As String = ""
        Dim int_periphloop As Integer = 0
        Dim count As Integer = 0
        Dim str_PeriphCountquery As String = "SELECT COUNT(*) FROM G5_Checking WHERE order_id LIKE @order_id"
        Dim str_Retrievequery_Checking As String = "SELECT * FROM G5_Checking WHERE order_id LIKE @order_id"
        Dim str_Retrievequery_Assets As String = "SELECT * FROM G5_Assets WHERE serial_number = @serial_no"
        Dim str_Retrievequery_People As String = "SELECT * FROM G5_People WHERE user_id = @uid"
        Dim PeriphCount_comm As SqlCommand = New SqlCommand(str_PeriphCountquery, G5SQL)
        Dim order_id As SqlParameter
        Dim serial_no As SqlParameter
        Dim uid As SqlParameter
        Dim SQLRead As SqlDataAdapter = New SqlDataAdapter(New SqlCommand("", G5SQL))
        Dim dt As DataTable = New DataTable
        If txt_Order_ID.Text IsNot Nothing Then
            str_initorderid = txt_Order_ID.Text
            OpenSQL()
            str_orderid = str_initorderid + "%"
            order_id = New SqlParameter("@order_id", str_orderid)
            PeriphCount_comm.Parameters.Add(order_id)
            int_periphloop = PeriphCount_comm.ExecuteScalar - 1
            PeriphCount_comm.Parameters.Clear()
            periph = int_periphloop
            If int_periphloop < 10 Then
                If int_periphloop > 0 Then
                    SQLRead.SelectCommand.CommandText = str_Retrievequery_Checking
                    SQLRead.SelectCommand.Parameters.Add(order_id)
                    SQLRead.Fill(dt)
                    For Each Row As DataRow In dt.Rows
                        str_orderidarray(count) = Row("order_id").ToString()
                        str_serialno(count) = Row("serial_number").ToString()
                        str_notes(count) = Row("notes").ToString()
                        str_uid = Row("user_id").ToString()
                        str_CC_Auth = Row("accessories").ToString()
                        str_usertype = Row("user_type").ToString()
                        txt_FTAuth.Text = Row("full_time_employee").ToString()
                        txt_AuthBy.Text = Row("authorize_by").ToString()
                        txt_UserSign.Text = Row("user_signature").ToString()
                        count += 1
                    Next
                    Select Case (str_usertype)
                        Case "Student"
                            drp_Type.SelectedIndex = 1
                        Case "Full Time Faculty"
                            drp_Type.SelectedIndex = 2
                        Case "Part Time Faculty"
                            drp_Type.SelectedIndex = 3
                        Case "Part Time Staff"
                            drp_Type.SelectedIndex = 4
                        Case Else
                            drp_Type.SelectedIndex = 5
                    End Select
                    If str_CC_Auth = "Y" Then
                        rd_NewHireY.Checked = True
                        chk_CCAuth.Checked = True
                    Else
                        rd_NewHireN.Checked = False
                        chk_CCAuth.Checked = False
                    End If
                    count = 0
                    dt.Reset()
                    SQLRead.SelectCommand.CommandText = str_Retrievequery_Assets
                    While count <= int_periphloop
                        SQLRead.SelectCommand.Parameters.Clear()
                        serial_no = New SqlParameter("@serial_no", str_serialno(count))
                        SQLRead.SelectCommand.Parameters.Add(serial_no)
                        SQLRead.Fill(dt)
                        For Each Row As DataRow In dt.Rows
                            str_itno(count) = Row("it_number").ToString()
                            str_itemdesc(count) = Row("item_description").ToString()
                            str_devtype(count) = Row("device_type").ToString()
                            str_stateno(count) = Row("state_num").ToString()
                        Next
                        count += 1
                    End While
                    dt.Reset()
                    uid = New SqlParameter("@uid", str_uid)
                    SQLRead.SelectCommand.CommandText = str_Retrievequery_People
                    SQLRead.SelectCommand.Parameters.Clear()
                    SQLRead.SelectCommand.Parameters.Add(uid)
                    SQLRead.Fill(dt)
                    For Each Row As DataRow In dt.Rows
                        txt_LName.Text = Row("user_last_name").ToString()
                        txt_FName.Text = Row("user_first_name").ToString()
                        txt_CollegeID.Text = Row("college_id").ToString()
                        txt_Email.Text = Row("email").ToString()
                        txt_PhoneNo.Text = Row("phone_number").ToString()
                        txt_HelpDeskNo.Text = Row("help_desk_no").ToString()
                    Next
                    ViewState("periph") = periph
                    ViewState("str_itemdesc") = str_itemdesc
                    ViewState("str_itno") = str_itno
                    ViewState("str_stateno") = str_stateno
                    ViewState("str_serialno") = str_serialno
                    ViewState("str_notes") = str_notes
                    ViewState("str_devtype") = str_devtype
                    ViewState("str_orderidarray") = str_orderidarray
                    drp_Device_Update()
                    FormFilterOut()
                    drp_Device.SelectedIndex = 0
                    TextboxUpdate(0)
                Else
                    Response.Write("<script language=""javascript"">alert('The order id entered provided no results. Retry with different order id');</script>")
                    txt_Order_ID.Text = String.Empty
                End If
            Else
                    Response.Write("<script language=""javascript"">alert('The order id entered provided too many results. Retry with a more specific order id');</script>")
            End If
            G5SQL.Close()
        Else
            Response.Write("<script language=""javascript"">alert('The SQL query needs an order id for reference');</script>")
        End If
    End Sub

    Protected Sub rd_Records_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rd_Record_Exists.CheckedChanged, rd_Record_New.CheckedChanged
        If rd_Record_Exists.Checked = True Then
            RecordExists = True
        ElseIf rd_Record_New.Checked = True Then
            RecordExists = False
        Else
            RecordExists = False
        End If
        txt_ItemDesc.Text = String.Empty
        txt_ITNo.Text = String.Empty
        txt_SerialNo.Text = String.Empty
        txt_StateNo.Text = String.Empty
        txt_HelpDeskNo.Text = String.Empty
        txt_Notes.Text = String.Empty
        txt_Order_ID.Text = String.Empty
        txt_FName.Text = String.Empty
        txt_LName.Text = String.Empty
        txt_PhoneNo.Text = String.Empty
        txt_Email.Text = String.Empty
        txt_EnterType.Text = String.Empty
        txt_CollegeID.Text = String.Empty
        txt_AuthBy.Text = String.Empty
        txt_FTAuth.Text = String.Empty
        txt_UserSign.Text = String.Empty
        drp_Device.Items.Clear()
        drp_DevType.SelectedIndex = 0
        drp_Type.SelectedIndex = 0
        drp_ReturnStatus.SelectedIndex = 0
        chk_CCAuth.Checked = False
        chk_Return.Checked = False
        chk_EULAread.Checked = False
        str_itemdesc = {"", "", "", "", "", "", "", "", "", ""}
        str_itno = {"", "", "", "", "", "", "", "", "", ""}
        str_stateno = {"", "", "", "", "", "", "", "", "", ""}
        str_serialno = {"", "", "", "", "", "", "", "", "", ""}
        str_devtype = {"", "", "", "", "", "", "", "", "", ""}
        str_notes = {"", "", "", "", "", "", "", "", "", ""}
        str_orderidarray = {"", "", "", "", "", "", "", "", "", ""}
        periph = 0
        ViewState("periph") = periph
        ViewState("str_itemdesc") = str_itemdesc
        ViewState("str_itno") = str_itno
        ViewState("str_stateno") = str_stateno
        ViewState("str_serialno") = str_serialno
        ViewState("str_notes") = str_notes
        ViewState("str_orderidarray") = str_orderidarray
        ViewState("RecordExists") = RecordExists
        FormFilterOut()
        drp_Device_Update()
    End Sub

    Private Sub PeriphShow()
        pn_Periph_Select.Visible = True
        If RecordExists = False Then
            If periph > 0 Then
                pn_Periph_Select.Visible = True
                btn_Periph_Rem.Visible = True
            Else
                pn_Periph_Select.Visible = False
                btn_Periph_Rem.Visible = False
            End If
            btn_Periph_Add.Visible = True
        Else
            btn_Periph_Add.Visible = False
            btn_Periph_Rem.Visible = False
            pn_Periph_Select.Visible = True
        End If
    End Sub

    Private Sub EnterTypeShow()
        If drp_DevType.SelectedIndex = 6 Then
            pn_EnterType.Visible = True
        End If
    End Sub

    Private Sub ReturnShow()
        If chk_Return.Checked = True Then
            pn_Returns.Visible = True
        Else
            pn_Returns.Visible = False
        End If
    End Sub

    Private Sub CCAuthShow()
        If rd_NewHireY.Checked = True Then
            pn_CCAuth.Visible = True
        Else
            pn_CCAuth.Visible = False
            chk_CCAuth.Checked = False
        End If
    End Sub

    Private Sub TypeShow()
        Select Case (drp_Type.SelectedIndex)
            Case 1
                pn_FTAuth.Visible = True
                pn_studEULA.Visible = True
                pn_CollegeID.Visible = True
            Case 2, 3, 4
                pn_CollegeID.Visible = True
                pn_facEULA.Visible = True
                pn_NewHire.Visible = True
            Case Else
                'Do Nothing
        End Select
    End Sub

    Private Sub ValidDateShow()
        If InvalidDate = True Then
            lbl_ValidDate.Visible = True
        Else
            lbl_ValidDate.Visible = False
        End If
    End Sub

    Private Sub RecordExistsShow()
        If rd_Record_Exists.Checked = True Then
            pn_OrderID.Visible = True
        End If
    End Sub

    Private Sub OpenSQL()
        Try
            G5SQL.Open()
        Catch ex As Exception
            My.Log.WriteEntry(ex.Message)
            OpenSQL()
        End Try
    End Sub
End Class