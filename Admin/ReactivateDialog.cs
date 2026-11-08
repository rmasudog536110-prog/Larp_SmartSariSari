using System;
using System.Drawing;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    /// <summary>Minimal prompt: pick a plan and an expiry date, used by AdminForm's Reactivate action.</summary>
    public partial class ReactivateDialog : Form
    {
        private readonly ComboBox _cmbPlan;
        private readonly DateTimePicker _dtExpiry;

        public string Plan => _cmbPlan.SelectedItem?.ToString();
        public DateTime Expiry => _dtExpiry.Value;

        public ReactivateDialog()
        {
            Text = "Reactivate Tenant";
            Size = new Size(340, 240);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblPlan = new Label { Text = "Plan", Location = new Point(20, 20), AutoSize = true };
            _cmbPlan = new ComboBox
            {
                Location = new Point(20, 45),
                Width = 280,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbPlan.Items.AddRange(new object[] { "Basic", "Standard", "Premium" });
            _cmbPlan.SelectedIndex = 0;

            var lblExpiry = new Label { Text = "New expiry date", Location = new Point(20, 80), AutoSize = true };
            _dtExpiry = new DateTimePicker
            {
                Location = new Point(20, 105),
                Width = 280,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddMonths(1)
            };

            var btnOk = new Button
            {
                Text = "Reactivate",
                DialogResult = DialogResult.OK,
                Location = new Point(120, 140),
                Width = 90
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(215, 140),
                Width = 85
            };

            Controls.AddRange(new Control[] { lblPlan, _cmbPlan, lblExpiry, _dtExpiry, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}