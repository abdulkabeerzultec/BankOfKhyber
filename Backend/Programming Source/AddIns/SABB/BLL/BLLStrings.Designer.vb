﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5477
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class BLLStrings
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("SABBPlugin.BLLStrings", GetType(BLLStrings).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This record cannot be deleted as related records exists..
        '''</summary>
        Friend Shared ReadOnly Property CanNotDelete() As String
            Get
                Return ResourceManager.GetString("CanNotDelete", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Administrator Role can not be modified..
        '''</summary>
        Friend Shared ReadOnly Property CantChangeAdminRole() As String
            Get
                Return ResourceManager.GetString("CantChangeAdminRole", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SysAdmin user can not be modified..
        '''</summary>
        Friend Shared ReadOnly Property CantChangeAdminUser() As String
            Get
                Return ResourceManager.GetString("CantChangeAdminUser", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Administrator Role can not be deleted..
        '''</summary>
        Friend Shared ReadOnly Property CantDeleteAdminRole() As String
            Get
                Return ResourceManager.GetString("CantDeleteAdminRole", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SysAdmin User can not be deleted..
        '''</summary>
        Friend Shared ReadOnly Property CantDeleteAdminUser() As String
            Get
                Return ResourceManager.GetString("CantDeleteAdminUser", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The selected item can not be under selected parent..
        '''</summary>
        Friend Shared ReadOnly Property CheckParentItem() As String
            Get
                Return ResourceManager.GetString("CheckParentItem", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Default currency not found, please select default currency from backend and try again..
        '''</summary>
        Friend Shared ReadOnly Property CurrencyNotFound() As String
            Get
                Return ResourceManager.GetString("CurrencyNotFound", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Device already linked with different employee, it&apos;s not posible to link more than one employee with same device..
        '''</summary>
        Friend Shared ReadOnly Property DeviceAlreadyLinkedWithEmp() As String
            Get
                Return ResourceManager.GetString("DeviceAlreadyLinkedWithEmp", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while deleting the record!..
        '''</summary>
        Friend Shared ReadOnly Property ErrorWhileDelete() As String
            Get
                Return ResourceManager.GetString("ErrorWhileDelete", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while saving the record!..
        '''</summary>
        Friend Shared ReadOnly Property ErrorWhileSaving() As String
            Get
                Return ResourceManager.GetString("ErrorWhileSaving", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to From and To warehouse must be different from each others..
        '''</summary>
        Friend Shared ReadOnly Property FromToWarehouseSame() As String
            Get
                Return ResourceManager.GetString("FromToWarehouseSame", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Total receipts amount cannot be greater than invoice total amount..
        '''</summary>
        Friend Shared ReadOnly Property InvalidReceiptAmount() As String
            Get
                Return ResourceManager.GetString("InvalidReceiptAmount", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The item SKU already available in this price list, please choose another SKU item and try again..
        '''</summary>
        Friend Shared ReadOnly Property ItemPriceAlreadyExist() As String
            Get
                Return ResourceManager.GetString("ItemPriceAlreadyExist", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid user name or password. Please try again..
        '''</summary>
        Friend Shared ReadOnly Property LoginFailed() As String
            Get
                Return ResourceManager.GetString("LoginFailed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Insufficient Permission, current logged in user is not authorized to perform this action..
        '''</summary>
        Friend Shared ReadOnly Property NotAuthorized() As String
            Get
                Return ResourceManager.GetString("NotAuthorized", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Password can not be empty..
        '''</summary>
        Friend Shared ReadOnly Property PasswordCantBeEmpty() As String
            Get
                Return ResourceManager.GetString("PasswordCantBeEmpty", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Password must be at least three chars..
        '''</summary>
        Friend Shared ReadOnly Property PasswordLengthshouldbethree() As String
            Get
                Return ResourceManager.GetString("PasswordLengthshouldbethree", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Quantity must be not equal to zero..
        '''</summary>
        Friend Shared ReadOnly Property QTYEqualZero() As String
            Get
                Return ResourceManager.GetString("QTYEqualZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Record not found, it could be deleted or modified, refersh current view and try again..
        '''</summary>
        Friend Shared ReadOnly Property RecordNotFound() As String
            Get
                Return ResourceManager.GetString("RecordNotFound", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Transfer must contain at least one item..
        '''</summary>
        Friend Shared ReadOnly Property TransferItem() As String
            Get
                Return ResourceManager.GetString("TransferItem", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to User already linked with different employee, it&apos;s not posible to link more than one employee with same user..
        '''</summary>
        Friend Shared ReadOnly Property UserAlreadyLinkedWithEmp() As String
            Get
                Return ResourceManager.GetString("UserAlreadyLinkedWithEmp", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The user can not complete the logon process, because the selected user is inactive..
        '''</summary>
        Friend Shared ReadOnly Property UserInactive() As String
            Get
                Return ResourceManager.GetString("UserInactive", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Vehicle already linked with different employee, it&apos;s not posible to link more than one employee with same vehicle..
        '''</summary>
        Friend Shared ReadOnly Property VehicleAlreadyLinkedWithEmp() As String
            Get
                Return ResourceManager.GetString("VehicleAlreadyLinkedWithEmp", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
