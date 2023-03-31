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
    public partial class MainForm : Form
    {
        public int page = 0;
        public DataTable products;
        DataTable propuctType;
        DataTable productTypes;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            foreach (DataRow row in productTypes.Rows)
            {

                comboBox1.Items.Add(row.ItemArray[1]);
            }

        }

        public void UpdateData()
        {
            using (DataBase db = new DataBase())
            {
                products = db.ExecuteSql("select * from Product");
                propuctType = db.ExecuteSql($"select Title from ProductType where id = '{products.Rows[page].ItemArray[5].ToString()}'");
                productTypes = db.ExecuteSql("select * from ProductType");
            }
            SelectPageData();
        }

        public void SelectPageData()
        {
            string path = @"C:\Users\sysoe\OneDrive\Рабочий стол\Промежуточный контроль\Сессия 1\";

            string cpath = path + products.Rows[page].ItemArray[4].ToString();
            textBoxTitle.Text = products.Rows[page].ItemArray[1].ToString();
            textBoxCost.Text = products.Rows[page].ItemArray[3].ToString();
            comboBox1.SelectedItem = propuctType.Rows[0].ItemArray[0].ToString();

            try
            {
                pictureBox1.Image = Image.FromFile(cpath);
            }
            catch
            {
                cpath = path + @"products\picture.png";
                pictureBox1.Image = Image.FromFile(cpath);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (page < products.Rows.Count)
                {
                    page += 1;
                    SelectPageData();
                }
            }
            catch
            {

            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (page > 0)
                {
                    page -= 1;
                    SelectPageData();
                }
            }
            catch
            {

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (DataBase db = new DataBase())
            {
                db.ExecuteNonQuery($"delete from product where ArticleNumber = '{products.Rows[page].ItemArray[2]}'");
            }
            UpdateData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            new UpdateForm(this).Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new FormAdd().Show();
        }
    }
}
