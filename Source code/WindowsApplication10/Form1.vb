Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Net


Public Class Form1
    Public buttonplay As System.Drawing.Bitmap
    Public buttonstop As System.Drawing.Bitmap
    Public client As WebClient = New WebClient
    Public archivo As String
    Public c As Integer




    Sub cargartop()
        Label1.Visible = False
        DataGridView1.Visible = True
        DataGridView1.Rows.Clear()
        Try
            Dim m_xmld As XmlDocument
            Dim Nombre, Posicion, yturl, nyt As String
            m_xmld = New XmlDocument()
            m_xmld.Load("C:\GESTY\list.xml")

            For i = 8 To 107
                Nombre = m_xmld.ChildNodes(1).ChildNodes(i).ChildNodes(2).InnerText
                Posicion = i - 7
                nyt = Replace(Nombre, " ", "+")
                nyt = Replace(nyt, "á", "a")
                nyt = Replace(nyt, "é", "e")
                nyt = Replace(nyt, "í", "i")
                nyt = Replace(nyt, "ó", "o")
                nyt = Replace(nyt, "ú", "u")
                nyt = Replace(nyt, "Á", "A")
                nyt = Replace(nyt, "É", "E")
                nyt = Replace(nyt, "Í", "I")
                nyt = Replace(nyt, "Ó", "O")
                nyt = Replace(nyt, "Ú", "U")
                nyt = Replace(nyt, "]", "")
                nyt = Replace(nyt, "[", "")
                nyt = Replace(nyt, "&", "y")
                yturl = "http://www.youtube.com/results?search_query=" + nyt + "&sp=EgQQARgB"
                DataGridView1.Rows.Add(Posicion, buttonplay, Nombre, yturl)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim id As Integer
        Label1.Visible = True
        DataGridView1.Visible = False
        Select Case ComboBox1.SelectedIndex
            Case 0
                id = 20
            Case 1
                id = 2
            Case 2
                id = 4
            Case 3
                id = 22
            Case 4
                id = 5
            Case 5
                id = 3
            Case 6
                id = 6
            Case 7
                id = 17
            Case 8
                id = 7
            Case 9
                id = 50
            Case 10
                id = 18
            Case 11
                id = 8
            Case 12
                id = 11
            Case 13
                id = 12
            Case 14
                id = 14
            Case 15
                id = 15
            Case 16
                id = 24
            Case 17
                id = 21
            Case 18
                id = 16
            Case 19
                id = 19
        End Select
        EjecutarTop(id)
        cargartop()
        listo()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Visible = False
        DataGridView1.Visible = True
        buttonplay = System.Drawing.Bitmap.FromFile("C:\GESTY\img\play.png")
        buttonstop = System.Drawing.Bitmap.FromFile("C:\GESTY\img\stop_.png")
    End Sub


    Public Sub EjecutarTop(ids As String)
        ToolStripStatusLabel2.Text = "BUSCANDO TOPS..."
        Dim ruta As String
        Dim p As Process
        ruta = " -i " & ids
        p = Process.Start("C:\GESTY\DTop.pyw", ruta)
        p.WaitForExit()
    End Sub

    Public Sub EjecutarSearch(ids As String)
        ToolStripStatusLabel2.Text = "BUSCANDO..."
        Dim ruta As String
        Dim p As Process
        ruta = " -s " & ids
        p = Process.Start("C:\GESTY\DSL.pyw", ruta)
        p.WaitForExit()
    End Sub

    Public Sub EjecutarPlay(ids As String)
        ToolStripStatusLabel2.Text = "OBTENIENDO RUTA..."
        Dim ruta As String
        Dim p As Process
        ruta = " -l " & ids
        p = Process.Start("C:\GESTY\SER.pyw", ruta)
        p.WaitForExit()
    End Sub

    Public Sub EjecutarDPlay(ids As String)
        ToolStripStatusLabel2.Text = "OBTENIENDO RUTA..."
        Dim ruta As String
        Dim p As Process
        ruta = " -l " & ids
        p = Process.Start("C:\GESTY\DSER.pyw", ruta)
        p.WaitForExit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rserach As String
        Label1.Visible = True
        DataGridView1.Visible = False
        rserach = TextBox1.Text.Replace(" ", "+")
        EjecutarSearch(rserach)
        cargarSearch()
        listo()
    End Sub

    Sub cargarSearch()
        ToolStripStatusLabel2.Text = "MOSTRANDO DATOS..."
        Label1.Visible = False
        DataGridView1.Visible = True
        DataGridView1.Rows.Clear()
        Try
            Dim m_xmld As XmlDocument
            Dim Nombre, Posicion, yturl As String
            m_xmld = New XmlDocument()

            m_xmld.Load("C:\GESTY\listsearch.xml")

            For i = 0 To 14
                Nombre = m_xmld.ChildNodes(0).ChildNodes(0).ChildNodes(i).ChildNodes(0).InnerText
                yturl = m_xmld.ChildNodes(0).ChildNodes(0).ChildNodes(i).ChildNodes(1).InnerText
                Posicion = i + 1
                DataGridView1.Rows.Add(Posicion, buttonplay, Nombre, yturl)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub cargarExternalPlay()
        ToolStripStatusLabel2.Text = "CARGANDO REPRODUCTOR..."
        Dim m_xmld As XmlDocument
        Dim playurl As String

        m_xmld = New XmlDocument()
        m_xmld.Load("C:\GESTY\listdown.xml")
        playurl = m_xmld.ChildNodes(0).ChildNodes(0).ChildNodes(0).InnerText
        Dim x As String
        x = Shell("C:\Program Files (x86)\K-Lite Codec Pack\MPC-HC64\mpc-hc64.exe " & playurl, AppWinStyle.NormalNoFocus, False)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim dwurl
        dwurl = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value
        If dwurl.Contains("search") Then
            EjecutarDPlay(dwurl)
        Else
            EjecutarPlay(dwurl)
        End If
        cargarInternalPlay()
        listo()
    End Sub

    Sub listo()
        ToolStripStatusLabel2.Text = "LISTO"
        DataGridView1.Focus()
    End Sub

    Sub cargarInternalPlay()
        ToolStripStatusLabel2.Text = "CARGANDO REPRODUCTOR..."
        Dim m_xmld As XmlDocument
        Dim playurl As String

        m_xmld = New XmlDocument()
        m_xmld.Load("C:\GESTY\listdown.xml")
        playurl = m_xmld.ChildNodes(0).ChildNodes(0).ChildNodes(0).InnerText
        AxWindowsMediaPlayer1.URL = playurl
        AxWindowsMediaPlayer1.Ctlcontrols.play()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim filas As Integer
        filas = DataGridView1.RowCount
        If DataGridView1.CurrentCell.ColumnIndex = 1 Then

            If DataGridView1.CurrentCell.Tag <> "Stop" Then
                For i = 0 To filas - 1
                    DataGridView1.Rows(i).Cells(1).Value = buttonplay
                    DataGridView1.Rows(i).Cells(1).Tag = "Play"
                Next
                DataGridView1.CurrentCell.Tag = "Stop"
                DataGridView1.CurrentCell.Value = buttonstop
                Dim dwurl
                dwurl = DataGridView1.CurrentRow.Cells(3).Value
                If dwurl.Contains("search") Then
                    EjecutarDPlay(dwurl)
                Else
                    EjecutarPlay(dwurl)
                End If
                cargarInternalPlay()
                listo()
                ToolStripStatusLabel4.Text = DataGridView1.CurrentRow.Cells(2).Value
            ElseIf DataGridView1.CurrentCell.Tag = "Stop" Then
                For i = 0 To filas - 1
                    DataGridView1.Rows(i).Cells(1).Value = buttonplay
                    DataGridView1.Rows(i).Cells(1).Tag = "Play"
                Next
                DataGridView1.CurrentCell.Tag = "Play"
                DataGridView1.CurrentCell.Value = buttonplay
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                ToolStripStatusLabel4.Text = "DETENIDO"

            End If

        End If

    End Sub

    Private Sub cliente_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        ToolStripProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
    End Sub

    Private Sub cliente_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ToolStripProgressBar1.Value = 0
        Button3.Enabled = True
        c = c + 1
        If c = 1 Then
            cover()
        End If
    End Sub

    Sub down()
        Dim dwurl As String
        Button3.Enabled = False
        AddHandler client.DownloadProgressChanged, AddressOf cliente_ProgressChanged
        AddHandler client.DownloadFileCompleted, AddressOf cliente_DownloadCompleted
        archivo = "C:\GESTY\Musica\" & DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value & ".webm"
        archivo = archivo.Replace("á", "a")
        archivo = archivo.Replace("é", "e")
        archivo = archivo.Replace("í", "i")
        archivo = archivo.Replace("ó", "o")
        archivo = archivo.Replace("ú", "u")
        archivo = archivo.Replace("Á", "A")
        archivo = archivo.Replace("É", "E")
        archivo = archivo.Replace("Í", "I")
        archivo = archivo.Replace("Ó", "O")
        archivo = archivo.Replace("Ú", "U")
        archivo = archivo.Replace("]", "")
        archivo = archivo.Replace("[", "")
        archivo = archivo.Replace("&", "y")
        dwurl = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value
        If dwurl.Contains("search") Then
            EjecutarDPlay(dwurl)
        Else
            EjecutarPlay(dwurl)
        End If
        Dim m_xmld As XmlDocument
        Dim durl As String

        m_xmld = New XmlDocument()
        m_xmld.Load("C:\GESTY\listdown.xml")
        durl = m_xmld.ChildNodes(0).ChildNodes(0).ChildNodes(0).InnerText

        If IO.Directory.Exists("C:\GESTY\Musica") Then
        Else
            On Error Resume Next
            If Err.Number = 0 Then
                IO.Directory.CreateDirectory("C:\GESTY\Musica")
            Else
                Err.Clear()
            End If
        End If
        If IO.File.Exists(archivo) Then
            Button3.Enabled = True
        Else
            On Error Resume Next
            If Err.Number = 0 Then
                client.DownloadFileAsync(New Uri(durl), archivo)
            Else
                MsgBox(Err.Description)
            End If
            Err.Clear()
        End If
    End Sub

    Sub cover()
        Dim p As Process
        Dim Ruta As String
        Ruta = "-m """ & archivo & """"
        p = Process.Start("C:\GESTY\COVER.pyw", Ruta)
        p.WaitForExit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        c = 0
        down()
    End Sub
End Class
