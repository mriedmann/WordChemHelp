namespace WordChemHelp
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.chemHelpGroup = this.Factory.CreateRibbonGroup();
            this.formatFromularButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.chemHelpGroup.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.chemHelpGroup);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // chemHelpGroup
            // 
            this.chemHelpGroup.Items.Add(this.formatFromularButton);
            this.chemHelpGroup.Label = "ChemHelp";
            this.chemHelpGroup.Name = "chemHelpGroup";
            // 
            // formatFromularButton
            // 
            this.formatFromularButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.formatFromularButton.Image = global::WordChemHelp.Properties.Resources.brush_icon;
            this.formatFromularButton.Label = "Format Fomular";
            this.formatFromularButton.Name = "formatFromularButton";
            this.formatFromularButton.ShowImage = true;
            this.formatFromularButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.chemHelpGroup.ResumeLayout(false);
            this.chemHelpGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup chemHelpGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton formatFromularButton;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon1
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
