Public Class Form1

    Function checkExpLoop()
        If ListBox1.Items.Contains("+") Or ListBox1.Items.Contains("-") Or ListBox1.Items.Contains("*") Or ListBox1.Items.Contains("$") Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox1.Text Is "" Then
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()

            For Each character As String In TextBox1.Text
                ListBox1.Items.Add(character)
            Next

            ListBox1.SelectedIndex = 0

            If CheckBox1.Checked = False Then
                Button2.Enabled = True
                While ListBox1.Items.Count > 1

                    If checkExpLoop() Then
                        ListBox2.Items.Add("Expression Syntax Error!")
                        Exit While
                    End If

                    Button2.PerformClick()
                    Me.Refresh()

                    If CheckBox2.Checked Then
                        Threading.Thread.Sleep(500)
                    End If

                    If ListBox2.Items.Contains("Expression Syntax Error!") Then
                        Exit While
                    End If
                End While

                Button2.Enabled = False
            End If
        Else
            ListBox2.Items.Add("Expression Syntax Error!")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim beforeN = 0
            Dim beforeN2 = 0
            Dim Result = 0
            Dim exp = ""

            If ListBox1.SelectedItem.ToString = "-" Then
                exp = "-"
            ElseIf ListBox1.SelectedItem.ToString = "+" Then
                exp = "+"
            ElseIf ListBox1.SelectedItem.ToString = "*" Then
                exp = "*"
            ElseIf ListBox1.SelectedItem.ToString = "/" Then
                exp = "/"
            ElseIf ListBox1.SelectedItem.ToString = "$" Then
                exp = "$"
            End If

            If Not exp Is "" Then
                beforeN = Convert.ToInt32(ListBox1.Items.Item(ListBox1.SelectedIndex - 2))
                beforeN2 = Convert.ToInt32(ListBox1.Items.Item(ListBox1.SelectedIndex - 1))

                If exp = "-" Then
                    Result = beforeN - beforeN2
                ElseIf exp = "+" Then
                    Result = beforeN + beforeN2
                ElseIf exp = "*" Then
                    Result = beforeN * beforeN2
                ElseIf exp = "*" Then
                    Result = beforeN / beforeN2
                ElseIf exp = "$" Then
                    Result += beforeN ^ beforeN2
                End If

                ListBox2.Items.Add(beforeN & " " & exp & " " & beforeN2 & " = " & Result)
                ListBox1.Items.Insert(ListBox1.SelectedIndex - 2, Result)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex - 2)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex - 1)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            End If

            If ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
                ListBox1.SelectedIndex += 1
            End If
        Catch ex As Exception
            ListBox2.Items.Add("Expression Syntax Error!")
            Debug.Print(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = "51-325+**2+5-"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Button2.Enabled = CheckBox1.Checked
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Application.ProductName
        Me.MaximizeBox = False
        Me.DoubleBuffered = True
        Me.MaximumSize = New Size(Me.Width, Me.Height)
        Me.MinimumSize = New Size(Me.Width, Me.Height)
        Button2.Enabled = False
    End Sub
End Class
