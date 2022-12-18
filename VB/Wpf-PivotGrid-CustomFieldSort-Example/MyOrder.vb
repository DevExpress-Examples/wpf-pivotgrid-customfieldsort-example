Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Wpf_PivotGrid_CustomFieldSort_Example

    Public Class MyOrderRow

        Private _ID As Integer, _OrderDate As DateTime, _Quantity As Integer, _UnitPrice As Decimal, _CustomerName As String, _ProductName As String, _CategoryName As String, _SalesPersonName As String, _SalesPersonId As Integer, _ExtendedPrice As Decimal

        Public Property ID As Integer
            Get
                Return _ID
            End Get

            Friend Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Public Property OrderDate As DateTime
            Get
                Return _OrderDate
            End Get

            Friend Set(ByVal value As DateTime)
                _OrderDate = value
            End Set
        End Property

        Public Property Quantity As Integer
            Get
                Return _Quantity
            End Get

            Friend Set(ByVal value As Integer)
                _Quantity = value
            End Set
        End Property

        Public Property UnitPrice As Decimal
            Get
                Return _UnitPrice
            End Get

            Friend Set(ByVal value As Decimal)
                _UnitPrice = value
            End Set
        End Property

        Public Property CustomerName As String
            Get
                Return _CustomerName
            End Get

            Friend Set(ByVal value As String)
                _CustomerName = value
            End Set
        End Property

        Public Property ProductName As String
            Get
                Return _ProductName
            End Get

            Friend Set(ByVal value As String)
                _ProductName = value
            End Set
        End Property

        Public Property CategoryName As String
            Get
                Return _CategoryName
            End Get

            Friend Set(ByVal value As String)
                _CategoryName = value
            End Set
        End Property

        Public Property SalesPersonName As String
            Get
                Return _SalesPersonName
            End Get

            Friend Set(ByVal value As String)
                _SalesPersonName = value
            End Set
        End Property

        Public Property SalesPersonId As Integer
            Get
                Return _SalesPersonId
            End Get

            Friend Set(ByVal value As Integer)
                _SalesPersonId = value
            End Set
        End Property

        Public Property ExtendedPrice As Decimal
            Get
                Return _ExtendedPrice
            End Get

            Friend Set(ByVal value As Decimal)
                _ExtendedPrice = value
            End Set
        End Property
    End Class
End Namespace
