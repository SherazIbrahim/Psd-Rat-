Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.DirectoryServices
Imports Microsoft
Imports System.Runtime.InteropServices
Imports System.Text
Public Class IP_Address
    Public _Ip As IPEndPoint
    Public Domain As String = "Your domain which you created on NoIp"
    Function GetIPAddress(ByVal CompName As String) As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        Try
            With Dns.GetHostEntry(CompName)
                oAddr = New System.Net.IPAddress(.AddressList(0).Address)
                sAddr = oAddr.ToString
            End With
            GetIPAddress = sAddr
        Catch Excep As Exception
            Return IPAddress.Loopback.ToString
        End Try
    End Function
    Public Property IP As IPEndPoint
        Get
            Return _Ip
        End Get
        Set(ByVal value As IPEndPoint)
            value = _Ip
        End Set
    End Property
    Sub New()
        Try
            _Ip = New IPEndPoint(IPAddress.Parse(GetIPAddress(Domain)), 5000)
        Catch ex As Exception
        End Try
    End Sub
End Class
