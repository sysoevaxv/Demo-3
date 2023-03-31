using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lopushok
{
    public partial class UpdateForm : Form
    {
        MainForm form;
        public UpdateForm(MainForm form)
        {
            this.form = form;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataBase db = new DataBase())
                {
                    db.ExecuteNonQuery($"update product set MinCostForAgent = {(object)textBox1.Text} where articlenumber = '{form.products.Rows[form.page].ItemArray[2]}'");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось поменять стоимость продукции");
            }
            form.UpdateData();
            this.Close();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
