using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Usermods.Shape;
using Usermods.Fpoint;
using Usermods.ExtList;

namespace Sharp
{
    public partial class frmMain : Form
    {
        //энумы для рисования по выбору
        private enum eMode : int {shapeCross, shapeLine, shapeCircle, shapeRect, modeSelect};
        private eMode mode = eMode.shapeCross;
        //фигурки для окна фигур
        private ImageList imglstFigures = new ImageList();

        //for line
        private Point line_beg;
        private bool btn_down = false;
        //если нажат Контрол
        private bool ctrl = false;

        //получает объект Editor открытого в данный момент окна
        private Editor CurrentEditor
        {
            get
            {
                if (tcSheets.SelectedTab != null)
                {
                    return tcSheets.SelectedTab.Tag as Editor;
                }
                else
                    return null;
            }
        }

        //получает текущий PaintBox
        private PictureBox CurrentPb
        {
            get
            {
                if (tcSheets.SelectedTab != null)
                {
                    return tcSheets.SelectedTab.Controls[0] as PictureBox;
                }
                else
                {
                    return null;
                }
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        //создает новую вкладку
        private int NewPage()
        { 
            TabPage tpNew = new TabPage("Untitled");
            tpNew.AutoScroll = true;

            PictureBox pbNew = new PictureBox();
            pbNew.Width = tcSheets.Width - 8;
            pbNew.Height = tcSheets.Height - 26;
            pbNew.BorderStyle = BorderStyle.FixedSingle;
            pbNew.MouseDown += Canvas_MouseDown;
            pbNew.MouseUp += Canvas_MouseUp;
            pbNew.MouseMove += Canvas_MouseMove;

            Editor edNew = new Editor(pbNew.Width, pbNew.Height, ref pbNew, (int)tpNew.Handle);
            edNew.onException += ShowErr;
            edNew.onRefresh += AfterDraw;
            edNew.onFileNameChanged += RefCaption;
            edNew.onShapesChanged += RefShapes;

            tpNew.Tag = edNew;
            tpNew.Controls.Add(pbNew);
            tcSheets.TabPages.Add(tpNew);

            return tcSheets.TabCount-1;
        }

        //функция - закрыть вкладку с индексом @index
        private void CloseFile(int index)
        {
            BeforeClose(index);
            if ((index >= 0) && (index < tcSheets.TabCount))
            {
                tcSheets.TabPages[index].Tag = null;
                tcSheets.TabPages[index].Controls.Clear();
                tcSheets.TabPages[index].Dispose();
            }
        }

        //сохранить объект @ed
        private void SaveEditor(Editor ed)
        {
            if (ed.cur_fname != "")
            {
                ed.SaveToFile(ed.cur_fname);
            }
            else
            {
                this.SaveAs(ed);
            }
        }

        //сохранить все объекты Editor
        private void SaveAllEditors()
        {
            for (int i = 0; i < tcSheets.TabCount; i++)
            {
                Editor tempeditor = GetEditr(i);
                SaveEditor(tempeditor);
            }
        }

        //событие, присылаемое перед закрытием вкладки, на вход подается индекс элемента
        private void BeforeClose(int index)
        {
            if (index == -1) return;
            Editor tempeditor = GetEditr(index);
            if (tempeditor.shapelist_modificated)
            {
                string fname = tempeditor.cur_fname;
                if (fname == "")
                    fname = "Untitled";
                if (MessageBox.Show("File '" + fname + "' has been modified.\nDo you want to save it?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveEditor(tempeditor);
                }
            }
        }

        //прочекать все открытые вкладки и закрыть их
        private void BeforeClose()
        {
            for (int i = 0; i < tcSheets.TabCount; i++)
            {
                BeforeClose(i);
            }
        }

        //получает объект @Editor по его индексу
        private Editor GetEditr(int index)
        {
            return tcSheets.TabPages[index].Tag as Editor; 
        }

        //присылается, когда какое-либо из окон сгенерило ошибку
        public void ShowErr(string message, string code)
        {
            MessageBox.Show("Ошибка: " + message + "\r\nОписание: " + code, "Ошибка");
        }

        //присылается, когда какому-лио из окон требуется отрисовка превьюшки
        public void AfterDraw()
        {
            if (CurrentPb != null)
            {
                pbPreview.Image = CurrentPb.Image;
            }
            else
            {
                pbPreview.Image = null;
            }
        }

        //получает объект @TabPage по хэндлу контрола
        private TabPage GetTabIndex(System.IntPtr handle)
        {
            return Control.FromHandle(handle) as TabPage;
        }

        //присылается, когда у окна с индексом Index изменился заголовок
        public void RefCaption(int index, string caption)
        {
            GetTabIndex((System.IntPtr)index).Text = caption;
        }

        //Присылается, когда изменился список фигур
        public void RefShapes(int index, ref ExtList<Shape> shapelist)
        {
            lvShapes.Items.Clear();

            for(int i =0; i < shapelist.Count; i++)
            {
                ListViewItem lvNItem = new ListViewItem(shapelist[i].description);

                if (shapelist[i] is sCross) lvNItem.ImageIndex = 0;
                if (shapelist[i] is sLine) lvNItem.ImageIndex = 1;
                if (shapelist[i] is sCircle) lvNItem.ImageIndex = 2;
                if (shapelist[i] is sCircle) lvNItem.ImageIndex = 3;
                if (shapelist[i] is sRect) lvNItem.ImageIndex = 4;

                lvShapes.Items.Add(lvNItem);
            }
        }

        //Menu: File -> New
        private void NewFile()
        {
            int newpage = this.NewPage();
            tcSheets.SelectedIndex = newpage;
            CurrentEditor.Refresh();
        }

        //Menu: File -> Save
        private void Save()
        {
            if (CurrentEditor == null) return;
            if (CurrentEditor.cur_fname != "")
            {
                CurrentEditor.SaveToFile(CurrentEditor.cur_fname);
            }
            else
            {
                this.SaveAs(CurrentEditor);
            }
        }
        
        //Menu: File -> Save As
        private void SaveAs(Editor ed)
        {
            if (ed == null) return;
            dlgSave.FileName = ed.cur_fname != "" ? System.IO.Path.GetFileName(ed.cur_fname) : "Untitled.shp";

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                if (dlgSave.FilterIndex == 2)
                {
                    CurrentPb.Image.Save(dlgSave.FileName);
                }
                else
                {
                    ed.SaveToFile(dlgSave.FileName);
                    ed.cur_fname = dlgSave.FileName;
                }
            }
        }

        //Menu: File -> Load
        private void LoadFile()
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                int newpage = this.NewPage();
                GetEditr(newpage).LoadFromFile(dlgOpen.FileName);
                GetEditr(newpage).cur_fname = dlgOpen.FileName;

                tcSheets.SelectedIndex = newpage;
                GetEditr(newpage).Refresh();
            }
        }

        //стандартные обработчики
        //Save button
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        //Load button
        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.LoadFile();
        }

        //Refresh button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (CurrentEditor == null) return;
            
            CurrentEditor.Refresh();
        }

        //Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (CurrentEditor == null) return;

            CurrentEditor.ClearList();
            CurrentEditor.Refresh();
        }

        //Preview area mouse click
        private void pbPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (CurrentPb == null) return;

            int x = Convert.ToInt32(CurrentPb.Width / pbPreview.Width) * e.X;
            int y = Convert.ToInt32(CurrentPb.Height / pbPreview.Height) * e.Y;

            if ((x > 0) && (y > 0) && (x < tcSheets.SelectedTab.HorizontalScroll.Maximum) && (y < tcSheets.SelectedTab.VerticalScroll.Maximum))
            {
                tcSheets.SelectedTab.HorizontalScroll.Value = x;
                tcSheets.SelectedTab.VerticalScroll.Value = y;
            }
        }

        //File - New
        private void tmiFileNew_Click(object sender, EventArgs e)
        {
            this.NewFile();
        }

        //File - Close
        private void tmiFileClose_Click(object sender, EventArgs e)
        {
            this.CloseFile(tcSheets.SelectedIndex);
        }

        //File - Exit
        private void tmiFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //File - Save
        private void tmiFileSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        //File - SaveAs
        private void tmiFileSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs(CurrentEditor);
        }

        //File - Load
        private void tmiFileLoad_Click(object sender, EventArgs e)
        {
            this.LoadFile();
        }

        //Press "-" in preview area
        private void pbMinus_Click(object sender, EventArgs e)
        {
            if (CurrentPb == null) return;
            if ((CurrentPb.Width <= tcSheets.Width) || (CurrentPb.Height <= tcSheets.Height)) return;

            CurrentPb.Width -= 400;
            CurrentPb.Height -= 400;

            CurrentEditor.RedefineXY(
                CurrentPb.Width,
                CurrentPb.Height);
            CurrentEditor.Refresh();
        }

        //Press "+" in preview area
        private void pbPlus_Click(object sender, EventArgs e)
        {
            if (CurrentPb == null) return;
            if (CurrentPb.Width > 2400) return;

            CurrentPb.Width += 400;
            CurrentPb.Height += 400;

            CurrentEditor.RedefineXY(
                CurrentPb.Width, 
                CurrentPb.Height);
            CurrentEditor.Refresh();
        }

        //Menu -> About
        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
            fa.Dispose();
        }

        //Mode - Cross
        private void tsCross_Click(object sender, EventArgs e)
        {
            tsCross.Checked = true;
            tsLine.Checked = false;
            tsCircle.Checked = false;
            tsSelect.Checked = false;
            tsRect.Checked = false;

            mode = eMode.shapeCross;
        }

        //Mode - Line
        private void tsLine_Click(object sender, EventArgs e)
        {
            tsCross.Checked = false;
            tsLine.Checked = true;
            tsCircle.Checked = false;
            tsSelect.Checked = false;
            tsRect.Checked = false;

            mode = eMode.shapeLine;
        }

        //Mode - Circle
        private void tsCircle_Click(object sender, EventArgs e)
        {
            tsCross.Checked = false;
            tsLine.Checked = false;
            tsCircle.Checked = true;
            tsSelect.Checked = false;
            tsRect.Checked = false;

            mode = eMode.shapeCircle;
        }

        //Mode - Figure Select
        private void tsSelect_Click(object sender, EventArgs e)
        {
            tsCross.Checked = false;
            tsLine.Checked = false;
            tsCircle.Checked = false;
            tsSelect.Checked = true;
            tsRect.Checked = false;

            mode = eMode.modeSelect;
        }

        private void tsRect_Click(object sender, EventArgs e)
        {
            tsCross.Checked = false;
            tsLine.Checked = false;
            tsCircle.Checked = false;
            tsSelect.Checked = false;
            tsRect.Checked = true;

            mode = eMode.shapeRect;
        }

        //Mouse button down on PaintBox
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            btn_down = true;

            switch (mode)
            {
                case eMode.shapeCross:
                    CurrentEditor.AddCross(e.X, e.Y); 
                    break;
                case eMode.shapeLine:
                    CurrentEditor.PrepareToTempDraw();
                    line_beg = new Point(e.X, e.Y);
                    break;
                case eMode.shapeCircle:
                    CurrentEditor.PrepareToTempDraw();
                    line_beg = new Point(e.X, e.Y);
                    break;
                case eMode.shapeRect:
                    CurrentEditor.PrepareToTempDraw();
                    line_beg = new Point(e.X, e.Y);
                    break;
            }
        }

        //Mouse cursor move on PaintBox
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!btn_down) return;

            switch (mode)
            {
                case eMode.shapeLine:
                    CurrentEditor.TempLine(line_beg, new Point(e.X, e.Y));
                    break;
                case eMode.shapeCircle:
                    CurrentEditor.TempEllipse(line_beg, new Point(e.X, e.Y));
                    break;
               case eMode.shapeRect:
                    CurrentEditor.TempRect(line_beg, new Point(e.X, e.Y));
                    break;
            }
        }

        //Mouse button up on PaintBox
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            btn_down = false;

            switch (mode)
            {
                case eMode.shapeLine:
                    CurrentEditor.AddLine(line_beg, new Point(e.X, e.Y));
                    break;
                case eMode.shapeCircle:
                    CurrentEditor.AddEllipse(line_beg, new Point(e.X, e.Y));
                    break;
                case eMode.shapeRect:
                    CurrentEditor.AddRect(line_beg, new Point(e.X, e.Y));
                    break;
                case eMode.modeSelect:
                    //multiselect
                    if(!this.ctrl)
                        lvShapes.SelectedIndices.Clear();
                    
                    //near shape
                    int val = CurrentEditor.GetNearShape(new Point(e.X, e.Y));
                    if (val != -1)
                        lvShapes.SelectedIndices.Add(val);
                    //then @SelectFigures(int[] indexes) called
                    break;
            }
        }

        //Key down
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            this.ctrl = e.Control;
        }

        //Key up
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            this.ctrl = e.Control;
        }

        //Изменился индекс текущей вкладки
        private void tcSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfterDraw();
            
            if (CurrentEditor == null)
                lvShapes.Items.Clear();
            else
                CurrentEditor.Refresh();
        }

        //Главная форма закрывается
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            BeforeClose();
        }

        //Menu - Save All
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveAllEditors();
        }

        //Main form is loaded. Preparing.
        private void frmMain_Load(object sender, EventArgs e)
        {
            imglstFigures.Images.Add(Sharp.Properties.Resources.gtk_close_7736);
            imglstFigures.Images.Add(Sharp.Properties.Resources.stock_draw_line_3200);
            imglstFigures.Images.Add(Sharp.Properties.Resources.stock_draw_circle_unfilled_5329);
            imglstFigures.Images.Add(Sharp.Properties.Resources.rect);

            lvShapes.SmallImageList = imglstFigures;
        }

        //В списке фигур что-то поменялось
        private void lvShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentEditor != null)
            {
                int[] a = new int[lvShapes.SelectedIndices.Count];
                lvShapes.SelectedIndices.CopyTo(a, 0);
                CurrentEditor.SelectFigures(a);
            }
        }

        //"Удалить фигуру"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentEditor != null)
            {
                int[] a = new int[lvShapes.SelectedIndices.Count];
                lvShapes.SelectedIndices.CopyTo(a, 0);
                CurrentEditor.DeleteFigures(a);
                CurrentEditor.Refresh();
            }
        }
    }
}
