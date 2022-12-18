Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace Wpf_PivotGrid_CustomFieldSort_Example

    Public Module DatabaseHelper

#Region "Fields"
        Private ReadOnly random As System.Random = New System.Random()

        Private ReadOnly FirstNames As String() = {"Julia", "Stephanie", "Alex", "John", "Curtis", "Keith", "Timothy", "Jack", "Miranda", "Alice"}

        Private ReadOnly LastNames As String() = {"Black", "White", "Brown", "Smith", "Cooper", "Parker", "Walker", "Hunter", "Burton", "Douglas", "Fox", "Simpson"}

        Private ReadOnly Adjectives As String() = {"Ancient", "Modern", "Mysterious", "Elegant", "Red", "Green", "Blue", "Amazing", "Wonderful", "Astonishing", "Lovely", "Beautiful", "Inexpensive", "Famous", "Magnificent", "Fancy"}

        Private ReadOnly ProductNames As String() = {"Ice Cubes", "Bicycle", "Desk", "Hamburger", "Notebook", "Tea", "Cellphone", "Butter", "Frying Pan", "Napkin", "Armchair", "Chocolate", "Yoghurt", "Statuette", "Keychain"}

        Private ReadOnly CategoryNames As String() = {"Business", "Presents", "Accessories", "Home", "Hobby"}

        Private ReadOnly CustomerNames As String()

        Private ReadOnly SalesPersonNames As String()

        Private ReadOnly Products As Wpf_PivotGrid_CustomFieldSort_Example.ProductDataRecord()

#End Region
        Sub New()
            Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.CustomerNames = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GenerateUniqueValues(Of System.[String])(CInt((Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](CInt((40)), CInt((50))))), CType((AddressOf Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GeneratePersonName), System.Func(Of System.[String]))).ToArray()
            Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.SalesPersonNames = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GenerateUniqueValues(Of System.[String])(CInt((8)), CType((AddressOf Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GeneratePersonName), System.Func(Of System.[String]))).ToArray()
            Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.Products = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GenerateProducts()
        End Sub

#Region "Public"
        Public Function CreateData() As ObservableCollection(Of Wpf_PivotGrid_CustomFieldSort_Example.MyOrderRow)
            Dim orderList As System.Collections.ObjectModel.ObservableCollection(Of Wpf_PivotGrid_CustomFieldSort_Example.MyOrderRow) = New System.Collections.ObjectModel.ObservableCollection(Of Wpf_PivotGrid_CustomFieldSort_Example.MyOrderRow)()
            For i As Integer = 0 To 1500 - 1
                Dim row As Wpf_PivotGrid_CustomFieldSort_Example.MyOrderRow = New Wpf_PivotGrid_CustomFieldSort_Example.MyOrderRow()
                row.ID = i
                Dim product = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetProduct()
                row.OrderDate = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetOrderDate()
                row.Quantity = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetQuantity()
                row.UnitPrice = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetProductPrice(product)
                row.ExtendedPrice = row.Quantity * row.UnitPrice
                row.CustomerName = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetCustomerName()
                row.ProductName = product.ProductName
                row.CategoryName = product.CategoryName
                row.SalesPersonName = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GetSalesPersonName()
                row.SalesPersonId = System.Array.IndexOf(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.SalesPersonNames, row.SalesPersonName)
                row.SalesPersonName = row.SalesPersonName & " " & row.SalesPersonId
                orderList.Add(row)
            Next

            Return orderList
        End Function

        Public Function GetOrderDate() As DateTime
            Return New System.DateTime(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](2017, 2019), Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](1, 13), Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](1, 28))
        End Function

        Public Function GetQuantity() As Integer
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](1, 100)
        End Function

        Public Function GetProductPrice(ByVal product As Wpf_PivotGrid_CustomFieldSort_Example.ProductDataRecord) As Decimal
            Dim price = product.UnitPrice * CDec((0.5 + Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.NextDouble()))
            Return System.Math.Round(price, 2)
        End Function

        Public Function GetProduct() As ProductDataRecord
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.Products(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.Products.Length))
        End Function

        Public Function GetCustomerName() As String
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.CustomerNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.CustomerNames.Length))
        End Function

        Public Function GetSalesPersonName() As String
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.SalesPersonNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.SalesPersonNames.Length))
        End Function

#End Region
#Region "Private"
        Private Function GenerateUniqueValues(Of T)(ByVal count As Integer, ByVal generateValue As System.Func(Of T)) As List(Of T)
            Dim values = New System.Collections.Generic.HashSet(Of T)()
            While values.Count < count
                Dim value = generateValue()
                If Not values.Contains(value) Then values.Add(value)
            End While

            Return values.ToList()
        End Function

        Private Function GenerateProducts() As Wpf_PivotGrid_CustomFieldSort_Example.ProductDataRecord()
            Return GenerateUniqueValues(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](80, 100), New Global.System.Func(Of System.String)(AddressOf Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GenerateProductName)).[Select](Function(productName) New Wpf_PivotGrid_CustomFieldSort_Example.ProductDataRecord With {.ProductName = productName, .UnitPrice = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](10, 500), .CategoryName = Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.GenerateCategoryName()}).ToArray()
        End Function

        Private Function GenerateCategoryName() As String
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.CategoryNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.CategoryNames.Length))
        End Function

        Private Function GeneratePersonName() As String
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.FirstNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.FirstNames.Length)) & " " & Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.LastNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.LastNames.Length))
        End Function

        Private Function GenerateProductName() As String
            Return Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.Adjectives(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.Adjectives.Length)) & " " & Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.ProductNames(Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.random.[Next](Wpf_PivotGrid_CustomFieldSort_Example.DatabaseHelper.ProductNames.Length))
        End Function
#End Region
    End Module

    Public Class ProductDataRecord

        Public Property ProductName As String

        Public Property CategoryName As String

        Public Property UnitPrice As Decimal
    End Class
End Namespace
