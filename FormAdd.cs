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
    public partial class FormAdd : Form
    {
        DataBase db = new DataBase();
        public FormAdd()
        {
            InitializeComponent();
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            comboBox_product_type.Items.Clear();
            DataTable productTypes = db.ExecuteSql("select * from ProductType");
            foreach (DataRow row in productTypes.Rows)
            {

                comboBox_product_type.Items.Add(row.ItemArray[1]);
            }
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            string title = textBox_title.Text;
            string article = textBox_artic_num.Text;
            double mincost = Convert.ToDouble(textBox_min_cost_for_ag.Text);
            string image = textBox_image_path.Text;
            int pers_count = Convert.ToInt32(textBox_prod_per_count.Text);
            int workshop_num = Convert.ToInt32(textBox_prod_workshop_num.Text);
            try
            {
                db.ExecuteNonQuery($"insert into Product values ('{title}', '{article}', {mincost}, '{image}', (select id from producttype where title = '{comboBox_product_type.SelectedItem}'), {pers_count}, {workshop_num});");
                MessageBox.Show("Продукт добавлен");
            }
            catch
            {
                MessageBox.Show("Продукт неудалось добавить");
            }
        }
    }
}
