Imports MySql.Data.MySqlClient

Public Class Form1

    Dim connectionString As String = "server=localhost; userid=root; password=root; database=crud_demo_db;"



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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBoxName.Text = selectedRow.Cells("name").Value.ToString()
            TextBoxAge.Text = selectedRow.Cells("age").Value.ToString()
            TextBoxEmail.Text = selectedRow.Cells("email").Value.ToString()
            TextBoxHiddenId.Text = selectedRow.Cells("id").Value.ToString()
        End If

    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Dim query As String = "UPDATE student_tbl SET name = @name, age = @age, email = @email WHERE id = @id"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(TextBoxHiddenId.Text))
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)

                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record updated successfully.")
                End Using
            End Using

            LoadTable()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Dim query As String = "DELETE FROM student_tbl WHERE id = @id"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(TextBoxHiddenId.Text))
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record deleted successfully")

                    TextBoxName.Clear()
                    TextBoxAge.Clear()
                    TextBoxEmail.Clear()
                    TextBoxHiddenId.Clear()
                End Using
            End Using

            LoadTable()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
