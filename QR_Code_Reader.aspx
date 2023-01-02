<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QR_Code_Reader.aspx.cs" Inherits="QR_Code_Reader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form enctype="multipart/form-data" action="http://api.qrserver.com/v1/read-qr-code/" method="POST">
        <!-- MAX_FILE_SIZE (maximum file size in bytes) must precede the file input field used to upload the QR code image -->
        <input type="hidden" name="MAX_FILE_SIZE" value="1048576" />
        <!-- The "name" of the input field has to be "file" as it is the name of the POST parameter -->
        Choose QR code image to read/scan: <input name="file" type="file" />
        <input type="submit" value="Read QR code" />
</form>
</body>
</html>
