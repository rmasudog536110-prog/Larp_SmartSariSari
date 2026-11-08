namespace Larp.winforms.MicroTenant.Sales
{
    partial class ReceiptForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox receiptText;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.receiptText = new System.Windows.Forms.RichTextBox();
            this.printButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Receipt Text

            this.receiptText.Location =
                new System.Drawing.Point(20, 20);

            this.receiptText.Name =
                "receiptText";

            this.receiptText.ReadOnly = true;

            this.receiptText.Size =
                new System.Drawing.Size(380, 530);

            this.receiptText.TabIndex = 0;

            this.receiptText.Font =
                new System.Drawing.Font(
                    "Consolas",
                    10F,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point);

            this.receiptText.BackColor =
                System.Drawing.Color.White;

            // Print Button

            this.printButton.Location =
                new System.Drawing.Point(220, 565);

            this.printButton.Name =
                "printButton";

            this.printButton.Size =
                new System.Drawing.Size(85, 40);

            this.printButton.TabIndex = 1;

            this.printButton.Text =
                "Print";

            this.printButton.UseVisualStyleBackColor =
                true;

            // Close Button

            this.closeButton.Location =
                new System.Drawing.Point(315, 565);

            this.closeButton.Name =
                "closeButton";

            this.closeButton.Size =
                new System.Drawing.Size(85, 40);

            this.closeButton.TabIndex = 2;

            this.closeButton.Text =
                "Close";

            this.closeButton.UseVisualStyleBackColor =
                true;

            // ReceiptForm

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(420, 625);

            this.Controls.Add(
                this.closeButton);

            this.Controls.Add(
                this.printButton);

            this.Controls.Add(
                this.receiptText);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;

            this.MinimizeBox = false;

            this.Name =
                "ReceiptForm";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Sales Receipt";

            this.ResumeLayout(false);
        }

        #endregion
    }
}