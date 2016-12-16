

Public Class Form1


    Private _artwork As DrawElement.Drawing

    Public Sub SetDrawing(ByRef Artwork As DrawElement.Drawing)
        _artwork = Artwork
        _artwork.SetGraphicsHandler(pnlDrawing.CreateGraphics)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '_artwork = New DrawElement.Drawing(picDraw.CreateGraphics)

        'Dim art1 As New DrawElement.DrawGroup("art1")

        'Dim element As DrawElement.Element
        'element = New DrawElement.Round(5, 5, 10)
        'art1.Group.Add(element)
        'element = New DrawElement.Square(10, 5, 4)
        'art1.Group.Add(element)
        'element = New DrawElement.Line(50, 50, 200, 200)
        'art1.Group.Add(element)

        '_artwork.Groups.Add(art1)

        '_artwork.Redraw()

        pnlDrawing.AutoScroll = True
        pnlDrawing.AutoScrollMinSize = New Size(1000, 1000)

        ComboBox1.DataSource = New BindingSource(_artwork, Nothing)
        ComboBox1.DisplayMember = "Key"
        ComboBox1.ValueMember = "Value"
        ComboBox1.SelectedIndex = 0

    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        '_artwork.SetGraphicsHandler(pnlDrawing.CreateGraphics)
        _artwork.Redraw()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If (Not IsNothing(_artwork)) Then
            '_artwork.SetGraphicsHandler(picDraw.CreateGraphics)
            _artwork.Redraw()
        End If

    End Sub

    Private Sub Panel1_Scroll(sender As Object, e As ScrollEventArgs) Handles pnlDrawing.Scroll
        pnlDrawing.Invalidate()

        '_artwork.SetGraphicsHandler(picDraw.CreateGraphics)
        '_artwork.Redraw()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        _artwork.Scaling += 2

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        _artwork.Scaling -= 2

    End Sub

    Private Sub pnlDrawing_Paint(sender As Object, e As PaintEventArgs) Handles pnlDrawing.Paint
        e.Graphics.TranslateTransform(pnlDrawing.AutoScrollPosition.X, pnlDrawing.AutoScrollPosition.Y)
    End Sub

    Private Sub pnlDrawing_Click(sender As Object, e As EventArgs) Handles pnlDrawing.Click
        Dim pos As MouseEventArgs = CType(e, MouseEventArgs)
        _artwork.TranslateX(pos.X)
        _artwork.TranslateY(pos.Y, True)
    End Sub
End Class
