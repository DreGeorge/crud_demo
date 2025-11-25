Imports MySql.Data.MySqlClient

Public Class Form1

    Dim connectionString As String = "server=localhost; userid=root; password=root; database=crud_demo_db;"
    Dim selectedID As Integer = -1


    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                MessageBox.Show("Connected successfully!")
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click

        Dim age As Integer
        If Not Integer.TryParse(TextBoxAge.Text, age) Then
            MsgBox("Please enter a valid age.")
            Exit Sub
        End If

        Dim query As String = "INSERT INTO student_tbl (name, age, email) VALUES (@name, @age, @email)"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", age)
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)

                    cmd.ExecuteNonQuery()
                    MsgBox("Record added successfully!")
                End Using
            End Using

            LoadTable()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub ButtonRead_Click(sender As Object, e As EventArgs) Handles ButtonRead.Click
        LoadTable()
    End Sub

    Private Sub LoadTable()
        Dim query As String = "SELECT * FROM student_tbl"

        Try
            Using conn As New MySqlConnection(connectionString)
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
                DataGridView1.Columns("id").Visible = False
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

        If selectedID = -1 Then
            MsgBox("Please select a row to update.")
            Exit Sub
        End If

        Dim age As Integer
        If Not Integer.TryParse(TextBoxAge.Text, age) Then
            MsgBox("Please enter a valid age.")
            Exit Sub
        End If

        Dim query As String = "UPDATE student_tbl SET name=@name, age=@age, email=@email WHERE id=@id"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", selectedID)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", age)
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Record updated!")
            LoadTable()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        If selectedID = -1 Then
            MsgBox("Please select a row to delete.")
            Exit Sub
        End If

        Dim query As String = "DELETE FROM student_tbl WHERE id=@id"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", selectedID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Record deleted!")
            LoadTable()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            selectedID = Integer.Parse(row.Cells("id").Value.ToString())

            TextBoxName.Text = row.Cells("name").Value.ToString()
            TextBoxAge.Text = row.Cells("age").Value.ToString()
            TextBoxEmail.Text = row.Cells("email").Value.ToString()
        End If

    End Sub

End Class
