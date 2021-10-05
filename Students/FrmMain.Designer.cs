namespace Students
{
    partial class FrmMain
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
            this.frmMainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStudentList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCourse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCourseAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCourseList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnrollment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddStudentToCourse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEnrollmentsList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnrollmentsCourses = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusBar = new System.Windows.Forms.StatusBar();
            this.messagePanel1 = new Students.MessagePanel();
            this.frmMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // frmMainMenu
            // 
            this.frmMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuStudent,
            this.mnuCourse,
            this.mnuEnrollment});
            this.frmMainMenu.Location = new System.Drawing.Point(0, 0);
            this.frmMainMenu.Name = "frmMainMenu";
            this.frmMainMenu.Size = new System.Drawing.Size(863, 24);
            this.frmMainMenu.TabIndex = 1;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClose});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(103, 22);
            this.mnuClose.Text = "Close";
            this.mnuClose.Click += new System.EventHandler(this.MnuClose_Click);
            // 
            // mnuStudent
            // 
            this.mnuStudent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStudentList});
            this.mnuStudent.Name = "mnuStudent";
            this.mnuStudent.Size = new System.Drawing.Size(65, 20);
            this.mnuStudent.Text = "Students";
            // 
            // mnuStudentList
            // 
            this.mnuStudentList.Name = "mnuStudentList";
            this.mnuStudentList.Size = new System.Drawing.Size(101, 22);
            this.mnuStudentList.Text = "List...";
            this.mnuStudentList.Click += new System.EventHandler(this.MnuStudentList_Click);
            // 
            // mnuCourse
            // 
            this.mnuCourse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCourseAdd,
            this.toolStripSeparator1,
            this.mnuCourseList});
            this.mnuCourse.Name = "mnuCourse";
            this.mnuCourse.Size = new System.Drawing.Size(61, 20);
            this.mnuCourse.Text = "Courses";
            // 
            // mnuCourseAdd
            // 
            this.mnuCourseAdd.Name = "mnuCourseAdd";
            this.mnuCourseAdd.Size = new System.Drawing.Size(105, 22);
            this.mnuCourseAdd.Text = "Add...";
            this.mnuCourseAdd.Click += new System.EventHandler(this.MnuCourseAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(102, 6);
            // 
            // mnuCourseList
            // 
            this.mnuCourseList.Name = "mnuCourseList";
            this.mnuCourseList.Size = new System.Drawing.Size(105, 22);
            this.mnuCourseList.Text = "List...";
            this.mnuCourseList.Click += new System.EventHandler(this.MnuCourseList_Click);
            // 
            // mnuEnrollment
            // 
            this.mnuEnrollment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddStudentToCourse,
            this.toolStripSeparator2,
            this.mnuEnrollmentsList,
            this.mnuEnrollmentsCourses});
            this.mnuEnrollment.Name = "mnuEnrollment";
            this.mnuEnrollment.Size = new System.Drawing.Size(82, 20);
            this.mnuEnrollment.Text = "Enrollments";
            // 
            // mnuAddStudentToCourse
            // 
            this.mnuAddStudentToCourse.Name = "mnuAddStudentToCourse";
            this.mnuAddStudentToCourse.Size = new System.Drawing.Size(205, 22);
            this.mnuAddStudentToCourse.Text = "Add student to course...";
            this.mnuAddStudentToCourse.Click += new System.EventHandler(this.mnuAddStudentToCourse_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuEnrollmentsList
            // 
            this.mnuEnrollmentsList.Name = "mnuEnrollmentsList";
            this.mnuEnrollmentsList.Size = new System.Drawing.Size(205, 22);
            this.mnuEnrollmentsList.Text = "List courses for student...";
            this.mnuEnrollmentsList.Click += new System.EventHandler(this.mnuEnrollmentsList_Click);
            // 
            // mnuEnrollmentsCourses
            // 
            this.mnuEnrollmentsCourses.Name = "mnuEnrollmentsCourses";
            this.mnuEnrollmentsCourses.Size = new System.Drawing.Size(205, 22);
            this.mnuEnrollmentsCourses.Text = "List students for course...";
            this.mnuEnrollmentsCourses.Click += new System.EventHandler(this.mnuEnrollmentsCourses_Click);
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.Location = new System.Drawing.Point(0, 456);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Size = new System.Drawing.Size(863, 22);
            this.mainStatusBar.TabIndex = 3;
            // 
            // messagePanel1
            // 
            this.messagePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messagePanel1.Location = new System.Drawing.Point(0, 420);
            this.messagePanel1.Name = "messagePanel1";
            this.messagePanel1.Size = new System.Drawing.Size(863, 36);
            this.messagePanel1.TabIndex = 5;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 478);
            this.Controls.Add(this.messagePanel1);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.frmMainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.frmMainMenu;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Student\'s Catalog";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.frmMainMenu.ResumeLayout(false);
            this.frmMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Students.MessagePanel messagePanel1;

        private System.Windows.Forms.StatusBar mainStatusBar;

        private System.Windows.Forms.ToolStripMenuItem mnuEnrollment;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuCourseList;

        private System.Windows.Forms.ToolStripMenuItem mnuCourse;
        private System.Windows.Forms.ToolStripMenuItem mnuCourseAdd;

        private System.Windows.Forms.ToolStripMenuItem mnuStudent;
        private System.Windows.Forms.ToolStripMenuItem mnuStudentList;

        private System.Windows.Forms.ToolStripMenuItem mnuClose;

        private System.Windows.Forms.ToolStripMenuItem mnuFile;

        private System.Windows.Forms.MenuStrip frmMainMenu;

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuAddStudentToCourse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuEnrollmentsList;
        private System.Windows.Forms.ToolStripMenuItem mnuEnrollmentsCourses;
    }
}