using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class GradientButton : Button
{
    public Color ColorTop { get; set; } = Color.FromArgb(70, 110, 160);
    public Color ColorBottom { get; set; } = Color.FromArgb(30, 50, 80);
    public Color BorderColor { get; set; } = Color.Black;      // 🆕 لون الإطار
    public int BorderThickness { get; set; } = 2;              // 🆕 سماكة الإطار

    protected override void OnPaint(PaintEventArgs e)
    {
        // 🎨 تعبئة الخلفية بالتدرّج
        using (LinearGradientBrush brush = new LinearGradientBrush(
            this.ClientRectangle,
            ColorTop,
            ColorBottom,
            LinearGradientMode.Vertical))
        {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        // ✏️ رسم الإطار باللون والسماكة المحددة
        using (Pen pen = new Pen(BorderColor, BorderThickness))
        {
            e.Graphics.DrawRectangle(pen,
                BorderThickness / 2,
                BorderThickness / 2,
                this.Width - BorderThickness,
                this.Height - BorderThickness);
        }

        // 📝 رسم النص في المنتصف
        TextRenderer.DrawText(
            e.Graphics,
            this.Text,
            this.Font,
            this.ClientRectangle,
            this.ForeColor,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        );
    }
}
