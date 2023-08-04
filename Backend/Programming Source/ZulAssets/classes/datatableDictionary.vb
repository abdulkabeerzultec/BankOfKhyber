
Public Class DataTableDictionary
    Implements IDictionary

    '** fields
    Dim _ht As Hashtable
    Dim _keys As ArrayList
    Dim _values As ArrayList

    Public Sub New(ByVal dt As DataTable, ByVal keyColumn As String, ByVal valueColumn As String)
        Init(dt.DefaultView, keyColumn, valueColumn)
    End Sub

    Public Sub New(ByVal dv As DataView, ByVal keyColumn As String, ByVal valueColumn As String)
        Init(dv, keyColumn, valueColumn)
    End Sub
    Private Sub Init(ByVal dv As DataView, ByVal keycolumn As String, ByVal valueColumn As String)
        _ht = New Hashtable
        _keys = New ArrayList
        _values = New ArrayList
        Dim index As Integer

        For index = 0 To dv.Count - 1
            Dim key As Object = dv(index)(keycolumn)
            Dim value As Object = dv(index)(valueColumn)
            _ht.Add(key, value)
            _keys.Add(key)
            _values.Add(value)
        Next
    End Sub

    '** IDictionary
    Public Property Item(ByVal key As Object) As Object Implements IDictionary.Item
        Get
            Return _ht(key)
        End Get
        Set(ByVal Value As Object)
            Throw New NotImplementedException
        End Set
    End Property

    ReadOnly Property IsFixedSize() As Boolean Implements IDictionary.IsFixedSize
        Get
            Return True
        End Get
    End Property

    ReadOnly Property IsReadOnly() As Boolean Implements IDictionary.IsReadOnly
        Get
            Return True
        End Get
    End Property

    ReadOnly Property Keys() As ICollection Implements IDictionary.Keys
        Get
            Return _keys
        End Get
    End Property

    ReadOnly Property Values() As ICollection Implements IDictionary.Values
        Get
            Return _values
        End Get
    End Property

    Sub Add(ByVal key As Object, ByVal value As Object) Implements IDictionary.Add
        Throw New NotImplementedException
    End Sub

    Sub Clear() Implements IDictionary.Clear
        Throw New NotImplementedException
    End Sub

    Function Contains(ByVal key As Object) As Boolean Implements IDictionary.Contains
        Return _ht.Contains(key)
    End Function

    Sub Remove(ByVal key As Object) Implements IDictionary.Remove
        Throw New NotImplementedException
    End Sub

    Overridable Overloads Function GetEnumerator1() As System.Collections.IDictionaryEnumerator Implements IDictionary.GetEnumerator
        Throw New NotImplementedException
    End Function

    '** ICollection
    ReadOnly Property Count() As Integer Implements ICollection.Count
        Get
            Return _ht.Count
        End Get
    End Property

    ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
        Get
            Return _ht.IsSynchronized
        End Get
    End Property

    ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
        Get
            Return _ht.SyncRoot
        End Get
    End Property

    Sub CopyTo(ByVal arr As System.Array, ByVal i As Integer) Implements ICollection.CopyTo
        Throw New NotImplementedException
    End Sub

    '** IEnumerable
    Overridable Overloads Function GetEnumerator() As System.Collections.IEnumerator Implements IEnumerable.GetEnumerator
        Return _keys.GetEnumerator()
    End Function

End Class