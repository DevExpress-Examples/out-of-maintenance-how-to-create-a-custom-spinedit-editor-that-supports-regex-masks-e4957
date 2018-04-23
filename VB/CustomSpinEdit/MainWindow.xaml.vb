Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Globalization
Imports DevExpress.Xpf.Editors
Imports System.Threading

Namespace CustomSpinEdit
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            Thread.CurrentThread.CurrentCulture = New CultureInfo("de-DE")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("de-DE")
            DoubleValue = 112.25
            DataContext = Me
            InitializeComponent()
        End Sub
        Private _DoubleValue As Double
        Public Property DoubleValue() As Double
            Get
                Return _DoubleValue
            End Get
            Set(ByVal value As Double)
                _DoubleValue = value
                Debug.WriteLine(value)
            End Set
        End Property
        Private _IntegerValue As Integer
        Public Property IntegerValue() As Integer
            Get
                Return _IntegerValue
            End Get
            Set(ByVal value As Integer)
                _IntegerValue = value
                Debug.WriteLine(value)
            End Set
        End Property
    End Class

    Public Class MySpinEdit
        Inherits ButtonEdit

        Public Shared ReadOnly IncrementProperty As DependencyProperty = DependencyProperty.Register("Increment", GetType(Double), GetType(MySpinEdit), New PropertyMetadata(1.0))
        Public Shared ReadOnly DecimalValueProperty As DependencyProperty = DependencyProperty.Register("DecimalValue", GetType(Object), GetType(MySpinEdit), New PropertyMetadata(Nothing, AddressOf OnDecimalValuePropertyChanged))
        Public Shared ReadOnly ValueTypeProperty As DependencyProperty = DependencyProperty.Register("ValueType", GetType(Type), GetType(MySpinEdit), New PropertyMetadata(GetType(Double)))

        Public Property ValueType() As Type
            Get
                Return CType(GetValue(ValueTypeProperty), Type)
            End Get
            Set(ByVal value As Type)
                SetValue(ValueTypeProperty, value)
            End Set
        End Property

        Shared Sub New()
            MaskTypeProperty.OverrideMetadata(GetType(MySpinEdit), New FrameworkPropertyMetadata(MaskType.RegEx))
            HorizontalContentAlignmentProperty.OverrideMetadata(GetType(MySpinEdit), New FrameworkPropertyMetadata(HorizontalAlignment.Right))
            MaskUseAsDisplayFormatProperty.OverrideMetadata(GetType(MySpinEdit), New FrameworkPropertyMetadata(True))
            AllowDefaultButtonProperty.OverrideMetadata(GetType(MySpinEdit), New FrameworkPropertyMetadata(False))
            EditValueProperty.OverrideMetadata(GetType(MySpinEdit), New FrameworkPropertyMetadata(AddressOf OnEditValuePropertyChanged))
        End Sub
        Private Shared Sub OnDecimalValuePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, MySpinEdit).OnDecimalValueChanged(e)
        End Sub
        Private Shared Sub OnEditValuePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, MySpinEdit).OnEditValueChanged(e)
        End Sub

        Public Property Increment() As Double
            Get
                Return CDbl(GetValue(IncrementProperty))
            End Get
            Set(ByVal value As Double)
                SetValue(IncrementProperty, value)
            End Set
        End Property
        Public Property DecimalValue() As Object
            Get
                Return DirectCast(GetValue(DecimalValueProperty), Object)
            End Get
            Set(ByVal value As Object)
                SetValue(DecimalValueProperty, value)
            End Set
        End Property

        Public Sub New()
            Dim buttons As New SpinButtonInfo()
            AddHandler buttons.SpinUpClick, AddressOf OnSpinUpClick
            AddHandler buttons.SpinDownClick, AddressOf OnSpinDownClick
            Me.Buttons.Add(buttons)
        End Sub
        Private Sub OnSpinUpClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Increase()
        End Sub
        Private Sub OnSpinDownClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Decrease()
        End Sub
        Protected Overrides Sub OnPreviewKeyDown(ByVal e As System.Windows.Input.KeyEventArgs)
            If e.Key = System.Windows.Input.Key.Up Then
                Increase()
            ElseIf e.Key = System.Windows.Input.Key.Down Then
                Decrease()
            Else
                MyBase.OnPreviewKeyDown(e)
            End If
        End Sub
        Public Sub Increase()
            Debug.WriteLine("Up")
            SpinCore(True)
        End Sub
        Public Sub Decrease()
            Debug.WriteLine("Down")
            SpinCore(False)
        End Sub
        Private Sub SpinCore(ByVal up As Boolean)
            If ValueType Is GetType(Integer) Then
                Dim integerIncrement As Integer = Convert.ToInt32(Increment)
                DecimalValue = If(up, DirectCast(DecimalValue, Integer) + integerIncrement, DirectCast(DecimalValue, Integer) - integerIncrement)
            End If
            If ValueType Is GetType(Double) Then
                DecimalValue = If(up, DirectCast(DecimalValue, Double) + Increment, DirectCast(DecimalValue, Double) - Increment)
            End If
        End Sub
        Private Sub OnDecimalValueChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If TypeOf DecimalValue Is Integer Then
                EditValue = DirectCast(DecimalValue, Integer).ToString(Thread.CurrentThread.CurrentUICulture)
            ElseIf TypeOf DecimalValue Is Double Then
                EditValue = DirectCast(DecimalValue, Double).ToString(Thread.CurrentThread.CurrentUICulture)
            Else
                EditValue = If(DecimalValue Is Nothing, Nothing, DecimalValue.ToString())
            End If
        End Sub
        Private Sub OnEditValueChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If Not String.IsNullOrEmpty(TryCast(EditValue, String)) Then
                If ValueType Is GetType(Integer) Then
                    Try
                        Dim value As Integer = Convert.ToInt32(EditValue, Thread.CurrentThread.CurrentUICulture)
                        If Not value.Equals(DecimalValue) Then
                            DecimalValue = value
                        End If
                        Return
                    Catch
                    End Try
                End If
                If ValueType Is GetType(Double) Then
                    Try
                        Dim value As Double = Convert.ToDouble(EditValue, Thread.CurrentThread.CurrentUICulture)
                        If Not value.Equals(DecimalValue) Then
                            DecimalValue = value
                        End If
                        Return
                    Catch
                    End Try
                End If
            End If
            DecimalValue = Nothing
        End Sub
    End Class
End Namespace