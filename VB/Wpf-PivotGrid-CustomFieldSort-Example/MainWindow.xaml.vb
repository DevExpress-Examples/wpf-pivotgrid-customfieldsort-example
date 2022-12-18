Imports DevExpress.Xpf.PivotGrid
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.Windows

Namespace Wpf_PivotGrid_CustomFieldSort_Example

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Property OrderSourceList As ObservableCollection(Of MyOrderRow)

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Me.pivotGridControl1.CustomFieldSort, AddressOf Me.PivotGridControl1_CustomFieldSort
            AddHandler Me.checkEdit1.Checked, AddressOf Me.CheckEdit1_Checked
            AddHandler Me.checkEdit1.Unchecked, AddressOf Me.CheckEdit1_Unchecked
        End Sub

        Private Sub CheckEdit1_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Set the default sorting algorithm.
            Me.fieldSalesPerson.SortMode = FieldSortMode.Default
        End Sub

        Private Sub CheckEdit1_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Enable the CustomFieldSort event for the fieldSalesPerson field.
            Me.fieldSalesPerson.SortMode = FieldSortMode.Custom
        End Sub

        Private Sub PivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As PivotCustomFieldSortEventArgs)
            If Equals(e.Field.FieldName, "SalesPersonName") Then
                Dim result As Integer
                If e.SortLocation = FieldSortLocation.Pivot Then
                    Dim orderValue1 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SalesPersonId"), orderValue2 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SalesPersonId")
                    result = Comparer.Default.Compare(orderValue1, orderValue2)
                Else
                    ' Compare last names.
                    result = Comparer.Default.Compare(e.Value1.ToString().Split(" "c)(1), e.Value2.ToString().Split(" "c)(1))
                    ' If last names are the same, compare first names.
                    If result = 0 Then result = Comparer.Default.Compare(e.Value1.ToString().Split(" "c)(0), e.Value2.ToString().Split(" "c)(0))
                End If

                e.Result = result
                e.Handled = True
            End If
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OrderSourceList = CreateData()
            Me.pivotGridControl1.DataSource = OrderSourceList
            Me.pivotGridControl1.BestFitArea = FieldBestFitArea.FieldHeader
            Me.pivotGridControl1.BestFit()
        End Sub
    End Class
End Namespace
