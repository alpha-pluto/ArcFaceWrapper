/*----------------------------------------------------------------
 *  
 * Winform demo for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceFormExtension.cs  
 * 文件功能描述：扩展功能
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afw.WinForm.Extension
{
    public static class FaceFormExtension
    {
        /// <summary>
        /// DataGridView鼠标右键点击行显示右键菜单
        /// </summary>
        public static void SetRightButtonDownShowContextMenuStrip(this DataGridView dgv, ContextMenuStrip cms, DataGridViewCellMouseEventArgs e)
        {
            //点击的是鼠标右键 且 点击行属于DataGridView的单元格的行（标题行的索引为-1）
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!dgv.Rows[e.RowIndex].Selected)//该行没有被选中
                {
                    dgv.ClearSelection();//清除当前选中的行
                    dgv.Rows[e.RowIndex].Selected = true;//设置选中行

                }

                cms.Show(Control.MousePosition.X, Control.MousePosition.Y);//显示右键菜单
            }
        }

    }
}
