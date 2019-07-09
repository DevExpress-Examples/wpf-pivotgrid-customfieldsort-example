Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.PivotGrid
Imports System
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows

Namespace Wpf_PivotGrid_CustomFieldSort_Example
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window

		Public Property OrderSourceList() As ObservableCollection(Of MyOrderRow)
		Public Sub New()
			InitializeComponent()
			AddHandler pivotGridControl1.CustomFieldSort, AddressOf PivotGridControl1_CustomFieldSort
			AddHandler checkEdit1.Checked, AddressOf CheckEdit1_Checked
			AddHandler checkEdit1.Unchecked, AddressOf CheckEdit1_Unchecked
		End Sub

		Private Sub CheckEdit1_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Set the default sorting algorithm.
			fieldSalesPerson.SortMode = FieldSortMode.Default
		End Sub

		Private Sub CheckEdit1_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Enable the CustomFieldSort event for the fieldSalesPerson field.
			fieldSalesPerson.SortMode = FieldSortMode.Custom
		End Sub

		Private Sub PivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As DevExpress.Xpf.PivotGrid.PivotCustomFieldSortEventArgs)
			If e.Field.FieldName = "SalesPersonName" Then
				Dim result As Integer
				If e.SortLocation = FieldSortLocation.Pivot Then
					Dim orderValue1 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SalesPersonId"), orderValue2 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SalesPersonId")
					result = Comparer.Default.Compare(orderValue1, orderValue2)
				Else
					' Compare last names.
					result = Comparer.Default.Compare(e.Value1.ToString().Split(" "c)(1), e.Value2.ToString().Split(" "c)(1))
					' If last names are the same, compare first names.
					If result = 0 Then
						result = Comparer.Default.Compare(e.Value1.ToString().Split(" "c)(0), e.Value2.ToString().Split(" "c)(0))
					End If
				End If
				e.Result = result
				e.Handled = True
			End If
		End Sub

		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			OrderSourceList = DatabaseHelper.CreateData()
			pivotGridControl1.DataSource = OrderSourceList
			pivotGridControl1.BestFitArea = DevExpress.Xpf.PivotGrid.FieldBestFitArea.FieldHeader
			pivotGridControl1.BestFit()
		End Sub
	End Class
End Namespace
