<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MOVIE_FORM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbboxCompanies = New System.Windows.Forms.ComboBox()
        Me.btnFamilyFriendly = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbboxFilter = New System.Windows.Forms.ComboBox()
        Me.cmbboxSort = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtboxSearch = New System.Windows.Forms.TextBox()
        Me.ListViewMovies = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnDwnld = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBoxQualifier = New System.Windows.Forms.TextBox()
        Me.txtBoxDelimiter = New System.Windows.Forms.TextBox()
        Me.chkBoxData = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbboxCompanies)
        Me.Panel1.Controls.Add(Me.btnFamilyFriendly)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmbboxFilter)
        Me.Panel1.Controls.Add(Me.cmbboxSort)
        Me.Panel1.Location = New System.Drawing.Point(7, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(110, 192)
        Me.Panel1.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 85)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Companies"
        '
        'cmbboxCompanies
        '
        Me.cmbboxCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxCompanies.FormattingEnabled = True
        Me.cmbboxCompanies.Location = New System.Drawing.Point(3, 102)
        Me.cmbboxCompanies.Name = "cmbboxCompanies"
        Me.cmbboxCompanies.Size = New System.Drawing.Size(105, 21)
        Me.cmbboxCompanies.TabIndex = 10
        '
        'btnFamilyFriendly
        '
        Me.btnFamilyFriendly.Location = New System.Drawing.Point(2, 165)
        Me.btnFamilyFriendly.Margin = New System.Windows.Forms.Padding(2)
        Me.btnFamilyFriendly.Name = "btnFamilyFriendly"
        Me.btnFamilyFriendly.Size = New System.Drawing.Size(104, 24)
        Me.btnFamilyFriendly.TabIndex = 9
        Me.btnFamilyFriendly.Text = "Family Friendly"
        Me.btnFamilyFriendly.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 44)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Filter"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Sort By"
        '
        'cmbboxFilter
        '
        Me.cmbboxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxFilter.FormattingEnabled = True
        Me.cmbboxFilter.Location = New System.Drawing.Point(2, 60)
        Me.cmbboxFilter.Name = "cmbboxFilter"
        Me.cmbboxFilter.Size = New System.Drawing.Size(105, 21)
        Me.cmbboxFilter.TabIndex = 6
        '
        'cmbboxSort
        '
        Me.cmbboxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxSort.FormattingEnabled = True
        Me.cmbboxSort.Items.AddRange(New Object() {"Ascending length", "Descending length", "", "Ascending rating", "Descending rating", "", "Ascending year", "Descending year", "", "Ascending vote", "Descending vote"})
        Me.cmbboxSort.Location = New System.Drawing.Point(2, 21)
        Me.cmbboxSort.Name = "cmbboxSort"
        Me.cmbboxSort.Size = New System.Drawing.Size(105, 21)
        Me.cmbboxSort.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem, Me.AccountToolStripMenuItem, Me.StatusToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1032, 24)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.ListToolStripMenuItem.Text = "Login"
        '
        'AccountToolStripMenuItem
        '
        Me.AccountToolStripMenuItem.Name = "AccountToolStripMenuItem"
        Me.AccountToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.AccountToolStripMenuItem.Text = "Watch later"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'txtboxSearch
        '
        Me.txtboxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtboxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtboxSearch.Location = New System.Drawing.Point(306, 57)
        Me.txtboxSearch.Name = "txtboxSearch"
        Me.txtboxSearch.Size = New System.Drawing.Size(493, 20)
        Me.txtboxSearch.TabIndex = 11
        Me.txtboxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ListViewMovies
        '
        Me.ListViewMovies.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ListViewMovies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        ListViewGroup1.Header = "ListViewGroup"
        ListViewGroup1.Name = "ListViewGroup1"
        Me.ListViewMovies.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        Me.ListViewMovies.HideSelection = False
        Me.ListViewMovies.Location = New System.Drawing.Point(120, 27)
        Me.ListViewMovies.Name = "ListViewMovies"
        Me.ListViewMovies.Size = New System.Drawing.Size(881, 529)
        Me.ListViewMovies.TabIndex = 10
        Me.ListViewMovies.UseCompatibleStateImageBehavior = False
        '
        'btnDwnld
        '
        Me.btnDwnld.Location = New System.Drawing.Point(7, 524)
        Me.btnDwnld.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDwnld.Name = "btnDwnld"
        Me.btnDwnld.Size = New System.Drawing.Size(105, 22)
        Me.btnDwnld.TabIndex = 13
        Me.btnDwnld.Text = "Download"
        Me.btnDwnld.UseVisualStyleBackColor = True
        Me.btnDwnld.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 496)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Export users data"
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 509)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "backup file"
        Me.Label5.Visible = False
        '
        'txtBoxQualifier
        '
        Me.txtBoxQualifier.Location = New System.Drawing.Point(10, 379)
        Me.txtBoxQualifier.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBoxQualifier.Name = "txtBoxQualifier"
        Me.txtBoxQualifier.Size = New System.Drawing.Size(106, 20)
        Me.txtBoxQualifier.TabIndex = 16
        Me.txtBoxQualifier.Visible = False
        '
        'txtBoxDelimiter
        '
        Me.txtBoxDelimiter.Location = New System.Drawing.Point(9, 315)
        Me.txtBoxDelimiter.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBoxDelimiter.Name = "txtBoxDelimiter"
        Me.txtBoxDelimiter.Size = New System.Drawing.Size(104, 20)
        Me.txtBoxDelimiter.TabIndex = 17
        Me.txtBoxDelimiter.Visible = False
        '
        'chkBoxData
        '
        Me.chkBoxData.AutoSize = True
        Me.chkBoxData.Location = New System.Drawing.Point(9, 438)
        Me.chkBoxData.Margin = New System.Windows.Forms.Padding(2)
        Me.chkBoxData.Name = "chkBoxData"
        Me.chkBoxData.Size = New System.Drawing.Size(87, 17)
        Me.chkBoxData.TabIndex = 18
        Me.chkBoxData.Text = "Append data"
        Me.chkBoxData.UseVisualStyleBackColor = True
        Me.chkBoxData.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 300)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Delimiter"
        Me.Label6.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 364)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Text Qualifier"
        Me.Label7.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(824, 54)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(104, 24)
        Me.btnSearch.TabIndex = 21
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'MOVIE_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(1032, 584)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkBoxData)
        Me.Controls.Add(Me.txtBoxDelimiter)
        Me.Controls.Add(Me.txtBoxQualifier)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnDwnld)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtboxSearch)
        Me.Controls.Add(Me.ListViewMovies)
        Me.Name = "MOVIE_FORM"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmbboxFilter As ComboBox
    Friend WithEvents cmbboxSort As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AccountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtboxSearch As TextBox
    Friend WithEvents ListViewMovies As ListView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnFamilyFriendly As Button
    Friend WithEvents cmbboxCompanies As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnDwnld As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtBoxQualifier As TextBox
    Friend WithEvents txtBoxDelimiter As TextBox
    Friend WithEvents chkBoxData As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents StatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnSearch As Button
End Class
