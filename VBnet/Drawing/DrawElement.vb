

Public Class DrawElement

    Public Class Dimension
        Public Property x As Integer
        Public Property y As Integer
    End Class

    Public MustInherit Class Element
        Public Property Pos As Dimension
        Public Property Size As Dimension
        Public Property Color As Color

        Public MustOverride Sub Draw(ByRef g As System.Drawing.Graphics, Optional Scaling As Integer = 1)

    End Class

    Public Class Round
        Inherits Element
        'Implements IDrawingElement

        Public Sub New(x As Integer, y As Integer, radius As Integer)
            Me.Pos = New Dimension
            Me.Pos.x = x
            Me.Pos.y = y
            Me.Size = New Dimension
            Me.Size.x = 2 * radius
            Me.Size.y = 2 * radius
            Me.Color = New Color
            Me.Color = Color.Black
        End Sub

        Public Overrides Sub Draw(ByRef g As System.Drawing.Graphics, Optional Scaling As Integer = 1)
            Dim pen As New Pen(Me.Color)
            g.DrawEllipse(pen, Me.Pos.x * Scaling, Me.Pos.y * Scaling, Me.Size.x * Scaling, Me.Size.y * Scaling)
        End Sub

    End Class

    Public Class Square
        Inherits Element

        Public Sub New(x As Integer, y As Integer, s As Integer)
            Me.Pos = New Dimension
            Me.Pos.x = x
            Me.Pos.y = y
            Me.Size = New Dimension
            Me.Size.x = s
            Me.Size.y = s
            Me.Color = New Color
            Me.Color = Color.Black
        End Sub

        Public Overrides Sub Draw(ByRef g As System.Drawing.Graphics, Optional Scaling As Integer = 1)
            Dim pen As New Pen(Me.Color)
            g.DrawRectangle(pen, Me.Pos.x * Scaling, Me.Pos.y * Scaling, Me.Size.x * Scaling, Me.Size.y * Scaling)
        End Sub

    End Class

    Public Class Text
        Inherits Element

        Dim _string As String

        Public Sub New(x As Integer, y As Integer, Text As String)
            Me.Pos = New Dimension
            Me.Pos.x = x
            Me.Pos.y = y
            Me.Size = New Dimension
            Me.Color = New Color
            Me.Color = Color.Black
            Me._string = Text
        End Sub

        Public Overrides Sub Draw(ByRef g As System.Drawing.Graphics, Optional Scaling As Integer = 1)
            Dim pen As New Pen(Me.Color)
            Dim font As New Font("Consolas", 10)
            g.DrawString(_string, font, New SolidBrush(Me.Color), New PointF(Me.Pos.x * Scaling, Me.Pos.y * Scaling))
        End Sub

    End Class

    Public Class Rectangle
        Inherits Element

        Public Sub New(x As Integer, y As Integer, length As Integer, height As Integer)
            Me.Pos = New Dimension
            Me.Pos.x = x
            Me.Pos.y = y
            Me.Size = New Dimension
            Me.Size.x = length
            Me.Size.y = height
            Me.Color = Color.Black
        End Sub

        Public Overrides Sub Draw(ByRef g As Graphics, Optional Scaling As Integer = 1)
            Dim pen As New Pen(Me.Color)
            g.DrawRectangle(pen, Me.Pos.x * Scaling, Me.Pos.y * Scaling, Me.Size.x * Scaling, Me.Size.y * Scaling)
        End Sub

    End Class

    Public Class Line
        Inherits Element

        Public Sub New(start_x As Integer, start_y As Integer, end_x As Integer, end_y As Integer)
            Me.Pos = New Dimension
            Me.Pos.x = start_x
            Me.Pos.y = start_y
            Me.Size = New Dimension
            Me.Size.x = end_x
            Me.Size.y = end_y
            Me.Color = Color.Black
        End Sub

        Public Overrides Sub Draw(ByRef g As Graphics, Optional Scaling As Integer = 1)
            Dim pen As New Pen(Me.Color)
            g.DrawLine(pen, Me.Pos.x * Scaling, Me.Pos.y * Scaling, Me.Size.x * Scaling, Me.Size.y * Scaling)
        End Sub

    End Class

    Public Class DrawGroup

        Public Group As New List(Of Element)

        Private _name As String

        Public Sub New(Name As String)
            Me._name = Name
        End Sub

        Public Function GetMinX() As Integer
            Dim min As Integer = 0
            For Each element As Element In Group
                If (element.Pos.x < min) Then min = element.Pos.x
            Next
            Return min
        End Function

        Public Function GetMinY() As Integer
            Dim min As Integer = 0
            For Each element As Element In Group
                If (element.Pos.y < min) Then min = element.Pos.y
            Next
            Return min
        End Function

        Public Sub TranslateX(XShift As Integer, Optional Scaling As Integer = 1)
            For Each element As Element In Group
                element.Pos.x += (XShift * Scaling)
            Next
        End Sub

        Public Sub TranslateY(YShift As Integer, Optional Scaling As Integer = 1)
            For Each element As Element In Group
                element.Pos.y += (YShift * Scaling)
            Next
        End Sub

    End Class

    Public Class Drawing
        Inherits Dictionary(Of String, DrawGroup)

        Private _graphics As Graphics

        Public Property Scaling As Integer = 1

        Public Sub New(GraphicsHandler As Graphics)
            _graphics = GraphicsHandler
        End Sub

        Public Sub New()

        End Sub

        Public Sub SetGraphicsHandler(GraphicsHandler As Graphics)
            _graphics = GraphicsHandler
        End Sub

        Public Sub Redraw()

            _graphics.Clear(Color.White)
            For Each grp As KeyValuePair(Of String, DrawGroup) In Me
                For Each element In grp.Value.Group
                    element.Draw(_graphics, Scaling)
                Next
            Next

        End Sub

        Public Shadows Sub Add(Key As String, Value As Element)

            If (Not Me.ContainsKey(Key)) Then
                Dim grp As New DrawGroup(Key)
                MyBase.Add(Key, grp)
            End If
            Me.Item(Key).Group.Add(Value)

        End Sub

        Public Shadows Sub Add(Key As String, Value As DrawGroup)
            MyBase.Add(Key, Value)
        End Sub

        Public Sub TranslateX(XShift As Integer, Optional Redraw As Boolean = False)
            _graphics.Clear(Color.White)
            For Each grp As KeyValuePair(Of String, DrawGroup) In Me
                grp.Value.TranslateX(XShift)
            Next
            If (Redraw) Then Me.Redraw()
        End Sub

        Public Sub TranslateY(YShift As Integer, Optional Redraw As Boolean = False)
            _graphics.Clear(Color.White)
            For Each grp As KeyValuePair(Of String, DrawGroup) In Me
                grp.Value.TranslateY(YShift)
            Next
            If (Redraw) Then Me.Redraw()
        End Sub

    End Class

End Class
