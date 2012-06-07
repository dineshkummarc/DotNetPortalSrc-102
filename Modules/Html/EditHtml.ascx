<%@ Control Language="c#" autoeventwireup="true" Inherits="Portal.API.EditModule" %>
<%@ Import namespace="System.IO" %>
<script runat="server">

    private string GetPath()
    {
        return ModulePhysicalPath + ModuleRef + ".htm";
    }

    void Page_Load(object sender, EventArgs args)
    {
        if(!IsPostBack)
        {
            // Open file            
            if(File.Exists(GetPath()))
            {
                FileStream fs = File.OpenRead(GetPath());
                StreamReader sr = new StreamReader(fs);
                txt.Text = sr.ReadToEnd();
                fs.Close();
            }
        }
    }
    
    void OnSave(object sender, EventArgs args)
    {
        FileStream fs = null;
        try
        {
            fs = new FileStream(GetPath(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fs.SetLength(0); // Truncate
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(txt.Text);
            sw.Close();
            
        }
        finally
        {
            if(fs != null)
            {
                fs.Close();
            }
        }
        RedirectBack();
    }

</script>
<asp:TextBox id="txt" Width="100%" Height="300px" TextMode="MultiLine" Runat="server"></asp:TextBox>
<asp:LinkButton CssClass="LinkButton" runat="server" OnClick="OnSave">Save & Back</asp:LinkButton>
