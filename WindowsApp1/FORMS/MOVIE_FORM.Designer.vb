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
        Me.txtboxSearch = New System.Windows.Forms.TextBox()
        Me.ListViewMovies = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
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
        Me.Panel1.Location = New System.Drawing.Point(9, 33)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(147, 236)
        Me.Panel1.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Companies"
        '
        'cmbboxCompanies
        '
        Me.cmbboxCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxCompanies.FormattingEnabled = True
        Me.cmbboxCompanies.Location = New System.Drawing.Point(4, 125)
        Me.cmbboxCompanies.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbboxCompanies.Name = "cmbboxCompanies"
        Me.cmbboxCompanies.Size = New System.Drawing.Size(139, 24)
        Me.cmbboxCompanies.TabIndex = 10
        '
        'btnFamilyFriendly
        '
        Me.btnFamilyFriendly.Location = New System.Drawing.Point(3, 203)
        Me.btnFamilyFriendly.Name = "btnFamilyFriendly"
        Me.btnFamilyFriendly.Size = New System.Drawing.Size(139, 30)
        Me.btnFamilyFriendly.TabIndex = 9
        Me.btnFamilyFriendly.Text = "Family Friendly"
        Me.btnFamilyFriendly.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Filter"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Sort By"
        '
        'cmbboxFilter
        '
        Me.cmbboxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxFilter.FormattingEnabled = True
        Me.cmbboxFilter.Location = New System.Drawing.Point(3, 74)
        Me.cmbboxFilter.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbboxFilter.Name = "cmbboxFilter"
        Me.cmbboxFilter.Size = New System.Drawing.Size(139, 24)
        Me.cmbboxFilter.TabIndex = 6
        '
        'cmbboxSort
        '
        Me.cmbboxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbboxSort.FormattingEnabled = True
        Me.cmbboxSort.Items.AddRange(New Object() {"Ascending length", "Descending length", "", "Ascending rating", "Descending rating", "", "Ascending year", "Descending year", "", "Ascending vote", "Descending vote"})
        Me.cmbboxSort.Location = New System.Drawing.Point(3, 26)
        Me.cmbboxSort.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbboxSort.Name = "cmbboxSort"
        Me.cmbboxSort.Size = New System.Drawing.Size(139, 24)
        Me.cmbboxSort.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem, Me.AccountToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1376, 28)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(60, 24)
        Me.ListToolStripMenuItem.Text = "Login"
        '
        'AccountToolStripMenuItem
        '
        Me.AccountToolStripMenuItem.Name = "AccountToolStripMenuItem"
        Me.AccountToolStripMenuItem.Size = New System.Drawing.Size(98, 24)
        Me.AccountToolStripMenuItem.Text = "Watch later"
        '
        'txtboxSearch
        '
        Me.txtboxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtboxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtboxSearch.Location = New System.Drawing.Point(408, 70)
        Me.txtboxSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtboxSearch.Name = "txtboxSearch"
        Me.txtboxSearch.Size = New System.Drawing.Size(656, 22)
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
        Me.ListViewMovies.Location = New System.Drawing.Point(160, 33)
        Me.ListViewMovies.Margin = New System.Windows.Forms.Padding(4)
        Me.ListViewMovies.Name = "ListViewMovies"
        Me.ListViewMovies.Size = New System.Drawing.Size(1173, 650)
        Me.ListViewMovies.TabIndex = 10
        Me.ListViewMovies.UseCompatibleStateImageBehavior = False
        '
        'MOVIE_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(1376, 719)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtboxSearch)
        Me.Controls.Add(Me.ListViewMovies)
        Me.Margin = New System.Windows.Forms.Padding(4)
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
End Class
