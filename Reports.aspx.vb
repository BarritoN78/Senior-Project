Imports System.Data.SqlClient
Public Class WebForm1
    Inherits System.Web.UI.Page


    Dim str_SQLselect As String = ""
    Dim str_filter_text As String = ""
    Dim dt_filter_date As Date = Date.Now
    Dim FilterIsDate As Boolean = False
    Dim G5SQL As SqlConnection = New SqlConnection("Server=198.71.227.2; Database=AAA-SCC-CPT-275-Capstone; User ID=scccpt275capstone; Password=scccpt275capstone")
    Dim Selectcomm As SqlCommand = New SqlCommand(str_SQLselect, G5SQL)
    Dim ds As DataSet = New DataSet()
    Dim sqladpt As SqlDataAdapter = New SqlDataAdapter(Selectcomm)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ViewState("SQLselect") IsNot Nothing Then
            str_SQLselect = ViewState("SQLselect")
            str_filter_text = ViewState("filter_text")
            FilterIsDate = ViewState("FilterIsDate")
            dt_filter_date = ViewState("filterdate")
        Else
            str_SQLselect = "G5Checking"
            str_filter_text = ""
            FilterIsDate = False
            dt_filter_date = Date.Now
            ViewState("filterdate") = dt_filter_date
            ViewState("SQLselect") = str_SQLselect
            ViewState("filter_text") = str_filter_text
            ViewState("FilterIsDate") = FilterIsDate
        End If
        grd_Data_DataChanger()
        Dim filteridx As Integer = drp_Tables.SelectedIndex
        drp_FilterBy_Filters(drp_Tables.SelectedItem.ToString)
        Dim int_Year As Integer = 1990
        Dim monthidx As Integer = drp_MonthFilter.SelectedIndex
        Dim yearidx As Integer = drp_YearFilter.SelectedIndex
        Dim dayidx As Integer = drp_DayFilter.SelectedIndex
        drp_MonthFilter.Items.Clear()
        drp_YearFilter.Items.Clear()
        drp_DayFilter.Items.Clear()
        For count = 0 To 50 Step 1
            drp_YearFilter.Items.Add(int_Year + count)
            If count < 12 Then
                drp_MonthFilter.Items.Add(count + 1)
            End If
            If count < 31 Then
                drp_DayFilter.Items.Add(count + 1)
            End If
        Next
        If IsPostBack Then
            drp_MonthFilter.SelectedIndex = monthidx
            drp_YearFilter.SelectedIndex = yearidx
            drp_DayFilter.SelectedIndex = dayidx
        Else
            drp_MonthFilter.SelectedIndex = (Month(Date.Now) - 1)
            drp_DayFilter.SelectedIndex = (Day(Date.Now) - 1)
            drp_YearFilter.SelectedIndex = (Year(Date.Now) - 1990)
        End If
        FormFilterOut()
    End Sub

    Protected Sub drp_Tables_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_Tables.SelectedIndexChanged
        drp_FilterBy.SelectedIndex = 0
        txt_TextFilter.Text = String.Empty
        str_filter_text = ""
        ViewState("filter_text") = str_filter_text
        drp_FilterBy_Filters(drp_Tables.SelectedItem.ToString)
    End Sub

    Protected Sub drp_FilterBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_FilterBy.SelectedIndexChanged
        txt_TextFilter.Text = String.Empty
        str_filter_text = ""
        ViewState("filter_text") = str_filter_text
    End Sub

    Protected Sub drp_FilterBy_Filters(str_tableslct As String)
        Dim idx As Integer = drp_FilterBy.SelectedIndex
        drp_FilterBy.Items.Clear()
        If str_tableslct = "Checking" Then
            drp_FilterBy.Items.Add("Order #")
            drp_FilterBy.Items.Add("Accessories")
            drp_FilterBy.Items.Add("Authorized By")
            drp_FilterBy.Items.Add("Full Time Employee Authorization")
            drp_FilterBy.Items.Add("Notes")
            drp_FilterBy.Items.Add("Checked Out")
            drp_FilterBy.Items.Add("Return Status")
            drp_FilterBy.Items.Add("User ID")
            drp_FilterBy.Items.Add("User Type")
            drp_FilterBy.Items.Add("User Signature")
            drp_FilterBy.Items.Add("Serial #")
            drp_FilterBy.Items.Add("Overdue")
            str_SQLselect = "G5Checking"
        ElseIf str_tableslct = "Assets" Then
            drp_FilterBy.Items.Add("Serial #")
            drp_FilterBy.Items.Add("IT #")
            drp_FilterBy.Items.Add("State #")
            drp_FilterBy.Items.Add("Device Type")
            drp_FilterBy.Items.Add("Availability")
            drp_FilterBy.Items.Add("Item Description")
            str_SQLselect = "G5ASSETS"
        ElseIf str_tableslct = "People" Then
            drp_FilterBy.Items.Add("User ID")
            drp_FilterBy.Items.Add("First Name")
            drp_FilterBy.Items.Add("Last Name")
            drp_FilterBy.Items.Add("College ID")
            drp_FilterBy.Items.Add("Help Desk #")
            drp_FilterBy.Items.Add("Email")
            drp_FilterBy.Items.Add("Phone #")
            str_SQLselect = "G5People"
        End If
        Try
            drp_FilterBy.SelectedIndex = idx
        Catch ex As Exception
            idx = 0
            drp_FilterBy.SelectedIndex = idx
        End Try
        ViewState("SQLselect") = str_SQLselect
        grd_Data_DataChanger()
    End Sub

    Protected Sub btn_Filter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Filter.Click
        Dim str_date As String = drp_YearFilter.SelectedItem.Text + "-" + drp_MonthFilter.SelectedItem.Text + "-" + drp_DayFilter.SelectedItem.Text
        Dim str_filter As String = "%" + txt_TextFilter.Text + "%"
        Select Case (drp_Tables.SelectedIndex)
            Case 0 'Checking
                Select Case (drp_FilterBy.SelectedIndex)
                    Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                        Select Case (drp_FilterBy.SelectedIndex)
                            Case 0 'Order #
                                str_SQLselect = "G5_Checking_filter_order_id"
                            Case 1 'Accessories
                                str_SQLselect = "G5_Checking_filter_accessories"
                            Case 2 'Authorized By
                                str_SQLselect = "G5_Checking_filter_authorize_by"
                            Case 3 'Full Time Employee Authorization
                                str_SQLselect = "G5_Checking_filter_full_time_employee"
                            Case 4 'Notes
                                str_SQLselect = "G5_Checking_filter_notes"
                            Case 5 'Checked Out
                                str_SQLselect = "G5_Checking_filter_check_out"
                            Case 6 'Return Status
                                str_SQLselect = "G5_Checking_filter_return_condition"
                            Case 7 'User ID
                                str_SQLselect = "G5_Checking_filter_user_id"
                            Case 8 'User Type
                                str_SQLselect = "G5_Checking_filter_user_type"
                            Case 9 'User Signature
                                str_SQLselect = "G5_Checking_filter_user_signature"
                            Case 10 'Serial #
                                str_SQLselect = "G5_Checking_filter_serial_number"
                        End Select
                        str_filter_text = str_filter
                    Case 11 'Overdue
                        str_SQLselect = "G5_Checking_filter_due_date"
                        FilterIsDate = True
                        ViewState("FilterIsDate") = FilterIsDate
                End Select
            Case 1 'Assets
                Select Case (drp_FilterBy.SelectedIndex)
                    Case 0 'Serial #
                        str_SQLselect = "G5_Assets_filter_serial_number"
                    Case 1 'IT #
                        str_SQLselect = "G5_Assets_filter_it_number"
                    Case 2 'State #
                        str_SQLselect = "G5_Assets_filter_state_num"
                    Case 3 'Device Type
                        str_SQLselect = "G5_Assets_filter_device_type"
                    Case 4 'Availability
                        str_SQLselect = "G5_Assets_filter_checkout"
                    Case 5 'Item Description
                        str_SQLselect = "G5_Assets_filter_item_description"
                End Select
                str_filter_text = str_filter
            Case 2 'People
                Select Case (drp_FilterBy.SelectedIndex)
                    Case 0 'User ID
                        str_SQLselect = "G5_People_filter_user_id"
                    Case 1 'First Name
                        str_SQLselect = "G5_People_filter_user_first_name"
                    Case 2 'Last Name
                        str_SQLselect = "G5_People_filter_user_last_name"
                    Case 3 'College ID
                        str_SQLselect = "G5_People_filter_college_id"
                    Case 4 'Help Desk #
                        str_SQLselect = "G5_People_filter_help_desk_no"
                    Case 5 'Email
                        str_SQLselect = "G5_People_filter_email"
                    Case 6 'Phone #
                        str_SQLselect = "G5_People_filter_phone_number"
                End Select
                str_filter_text = str_filter
        End Select
        If FilterIsDate Then
            Try
                dt_filter_date = Convert.ToDateTime(str_date)
            Catch
                Response.Write("<script language=""javascript"">alert('The date entered was invalid. Today's Date: " + Date.Today.ToString + " used instead');</script>")
                dt_filter_date = Date.Now
            End Try
        End If
        ViewState("filterdate") = dt_filter_date
        ViewState("filter_text") = str_filter_text
        ViewState("SQLselect") = str_SQLselect
        grd_Data_DataChanger()
    End Sub

    Private Sub FormFilterOut()
        pn_DateFilter.Visible = False
        pn_TextFilter.Visible = False
        Select Case (drp_Tables.SelectedIndex)
            Case 0 'Checking
                Select Case (drp_FilterBy.SelectedIndex)
                    Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                        pn_TextFilter.Visible = True
                    Case Else
                        pn_DateFilter.Visible = True
                End Select
            Case 1, 2 'Assets / People
                pn_TextFilter.Visible = True
        End Select
    End Sub

    Private Sub grd_Data_DataChanger()
        Selectcomm.CommandText = str_SQLselect
        Selectcomm.CommandType = CommandType.StoredProcedure
        Selectcomm.Parameters.Clear()
        If str_SQLselect.Contains("filter") Then
            If FilterIsDate Then
                Selectcomm.Parameters.AddWithValue("@FILTERTEXT", dt_filter_date)
            Else
                Selectcomm.Parameters.AddWithValue("@FILTERTEXT", str_filter_text)
            End If
        End If
        OpenSQL()
        ds.Reset()
        sqladpt.Fill(ds)

        grd_Data.DataSource = ds
        grd_Data.DataBind()
        G5SQL.Close()
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