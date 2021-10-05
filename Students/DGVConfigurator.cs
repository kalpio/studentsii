using System.Drawing;
using System.Windows.Forms;

namespace Students
{
    internal static class DGVConfigurator
    {
        public static void ConfigureDataGridView(ref DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.Gray;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }
    }
}
