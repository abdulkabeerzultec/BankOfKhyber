﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3623
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
    Friend Class Strings
        
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("ZulLib.Strings", GetType(Strings).Assembly)
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
        '''  Looks up a localized string similar to &amp;Select.
        '''</summary>
        Friend Shared ReadOnly Property btnSelectRecordCaption() As String
            Get
                Return ResourceManager.GetString("btnSelectRecordCaption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select Current Record..
        '''</summary>
        Friend Shared ReadOnly Property btnSelectRecordHint() As String
            Get
                Return ResourceManager.GetString("btnSelectRecordHint", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Discard changes?.
        '''</summary>
        Friend Shared ReadOnly Property CancelConfirmation() As String
            Get
                Return ResourceManager.GetString("CancelConfirmation", resourceCulture)
            End Get
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
        '''  Looks up a localized string similar to This record cannot be saved because errors exists, please check the errors in the form and try again..
        '''</summary>
        Friend Shared ReadOnly Property CanNotSave() As String
            Get
                Return ResourceManager.GetString("CanNotSave", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Do you want to delete the selected item?.
        '''</summary>
        Friend Shared ReadOnly Property DeleteConfirmation() As String
            Get
                Return ResourceManager.GetString("DeleteConfirmation", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to already exists, change it and try again..
        '''</summary>
        Friend Shared ReadOnly Property FieldValueExisted() As String
            Get
                Return ResourceManager.GetString("FieldValueExisted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Do you want to print?.
        '''</summary>
        Friend Shared ReadOnly Property PrintConfirmation() As String
            Get
                Return ResourceManager.GetString("PrintConfirmation", resourceCulture)
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
        '''  Looks up a localized string similar to Do you want to save changes?.
        '''</summary>
        Friend Shared ReadOnly Property SaveConfirmation() As String
            Get
                Return ResourceManager.GetString("SaveConfirmation", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to You should save parent record first!.
        '''</summary>
        Friend Shared ReadOnly Property SaveParentFirst() As String
            Get
                Return ResourceManager.GetString("SaveParentFirst", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter a value, Field must not be empty..
        '''</summary>
        Friend Shared ReadOnly Property valRulenotEmpty() As String
            Get
                Return ResourceManager.GetString("valRulenotEmpty", resourceCulture)
            End Get
        End Property
    End Class
End Namespace