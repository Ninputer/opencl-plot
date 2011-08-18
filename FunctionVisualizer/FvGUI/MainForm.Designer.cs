namespace FvGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUnitPixels = new System.Windows.Forms.TextBox();
            this.textBoxOriginX = new System.Windows.Forms.TextBox();
            this.textBoxOriginY = new System.Windows.Forms.TextBox();
            this.textBoxFunction = new System.Windows.Forms.TextBox();
            this.buttonRender = new System.Windows.Forms.Button();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 6;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxUnitPixels, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxOriginX, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxOriginY, 5, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxFunction, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonRender, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelErrorMessage, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panelImage, 0, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(707, 657);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "&F(x, y)=0:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Unit Pixels:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(238, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Origin &X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(473, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Origin &Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxUnitPixels
            // 
            this.textBoxUnitPixels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUnitPixels.Location = new System.Drawing.Point(109, 32);
            this.textBoxUnitPixels.Name = "textBoxUnitPixels";
            this.textBoxUnitPixels.Size = new System.Drawing.Size(123, 20);
            this.textBoxUnitPixels.TabIndex = 3;
            this.textBoxUnitPixels.Text = "100";
            this.textBoxUnitPixels.TextChanged += new System.EventHandler(this.textBoxUnitPixels_TextChanged);
            // 
            // textBoxOriginX
            // 
            this.textBoxOriginX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOriginX.Location = new System.Drawing.Point(344, 32);
            this.textBoxOriginX.Name = "textBoxOriginX";
            this.textBoxOriginX.Size = new System.Drawing.Size(123, 20);
            this.textBoxOriginX.TabIndex = 5;
            this.textBoxOriginX.Text = "0";
            this.textBoxOriginX.TextChanged += new System.EventHandler(this.textBoxOriginX_TextChanged);
            // 
            // textBoxOriginY
            // 
            this.textBoxOriginY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOriginY.Location = new System.Drawing.Point(579, 32);
            this.textBoxOriginY.Name = "textBoxOriginY";
            this.textBoxOriginY.Size = new System.Drawing.Size(125, 20);
            this.textBoxOriginY.TabIndex = 7;
            this.textBoxOriginY.Text = "0";
            this.textBoxOriginY.TextChanged += new System.EventHandler(this.textBoxOriginY_TextChanged);
            // 
            // textBoxFunction
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxFunction, 5);
            this.textBoxFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFunction.Location = new System.Drawing.Point(109, 3);
            this.textBoxFunction.Name = "textBoxFunction";
            this.textBoxFunction.Size = new System.Drawing.Size(595, 20);
            this.textBoxFunction.TabIndex = 1;
            this.textBoxFunction.Text = "x-y";
            this.textBoxFunction.TextChanged += new System.EventHandler(this.textBoxFunction_TextChanged);
            // 
            // buttonRender
            // 
            this.buttonRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRender.Location = new System.Drawing.Point(3, 61);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(100, 25);
            this.buttonRender.TabIndex = 8;
            this.buttonRender.Text = "&Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.buttonRender_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.labelErrorMessage, 5);
            this.labelErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelErrorMessage.Location = new System.Drawing.Point(109, 58);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(595, 31);
            this.labelErrorMessage.TabIndex = 9;
            this.labelErrorMessage.Text = "(Ready)";
            this.labelErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelImage
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.panelImage, 6);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(3, 92);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(701, 562);
            this.panelImage.TabIndex = 10;
            this.panelImage.SizeChanged += new System.EventHandler(this.panelImage_SizeChanged);
            this.panelImage.Paint += new System.Windows.Forms.PaintEventHandler(this.panelImage_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 657);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Function Visualizer v1.0 GPU Accelerated";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUnitPixels;
        private System.Windows.Forms.TextBox textBoxOriginX;
        private System.Windows.Forms.TextBox textBoxOriginY;
        private System.Windows.Forms.TextBox textBoxFunction;
        private System.Windows.Forms.Button buttonRender;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Panel panelImage;
    }
}

