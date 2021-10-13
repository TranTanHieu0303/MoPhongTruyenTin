using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTT_DeTai12
{
    public partial class Form1 : Form
    {
        List<List<bool>> matrix_sinh = new List<List<bool>>();
        List<List<bool>> matrix_kiemtra = new List<List<bool>>();
        Hamming h = new Hamming();
        public Form1()
        {
            InitializeComponent();
            List<string> str = new List<string>();
            str.Add("1000110");
            str.Add("0100101");
            str.Add("0010011");
            str.Add("0001111");
            addMatrix_Sinh(str);
            List<string> str_kt = new List<string>();
            str_kt.Add("1101100");
            str_kt.Add("1011010");
            str_kt.Add("0111001");
            addMatrix_KiemTra(str_kt);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            hienthiMatranSinh();
            hienthiMatranKiemTra();

            ////string s = "0111000";
            //string s = "0011010";
            ////string s = "1101100";
            //List<bool> m = s.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
            //h.decode_matrix(m, matrix_check);


        }
        public void hienthiMatranSinh()
        {
            int hang = matrix_sinh.Count;
            int cot = matrix_sinh[0].Count;
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Width = 28 * cot;
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(20, 20);
                    textBox.Name = "txt_" + i + "_" + j;
                    textBox.Text = (matrix_sinh[i][j] ? 1 : 0).ToString();
                    flowLayoutPanel1.Controls.Add(textBox);

                }
            }
            flowLayoutPanel1.Enabled = false;
        }
        public void hienthiMatranKiemTra()
        {
            int hang = matrix_kiemtra.Count;
            int cot = matrix_kiemtra[0].Count;
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Width = 28 * cot;
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < cot; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(20, 20);
                    textBox.Name = "txt_" + i + "_" + j;
                    textBox.Text = (matrix_kiemtra[i][j] ? 1 : 0).ToString();
                    flowLayoutPanel2.Controls.Add(textBox);

                }
            }
            flowLayoutPanel2.Enabled = false;
        }
        public void addMatrix_Sinh(List<string> lst)
        {
            for(int i = 0; i < lst.Count;i++)
            {
                List<bool> item = lst[i].ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
                matrix_sinh.Add(item);
            }    
        }
        public void addMatrix_KiemTra(List<string> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                List<bool> item = lst[i].ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
                matrix_kiemtra.Add(item);
            }
        }
        //public List<List<bool>> MaTranCheck()
        //{
        //    List<List<bool>> matrix_check = new List<List<bool>>();
        //    string check0 = "1101100";
        //    List<bool> m0 = check0.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
        //    matrix_check.Add(m0);
        //    string check1 = "1011010";
        //    List<bool> m1 = check1.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
        //    matrix_check.Add(m1);
        //    string check2 = "0111001";
        //    List<bool> m2 = check2.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
        //    matrix_check.Add(m2);
        //    return matrix_check;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if(s=="")
            {
                MessageBox.Show("Nhập thông tin");
                return;
            }    
            List<bool> m = s.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToList();
            //string a = "";
            //for (int i = 0; i < m.Count; i++)
            //{
            //    int item = m[i] ? 1 : 0;
            //    a += item.ToString();
            //}
            //textBox1.Text = a; 
            
            //h.matrangoloi(lst);

            //Mã hóa thành từ mã.
            List<List<bool>> lst_1 = h.split(m, matrix_sinh.Count);
            List<bool> lst_code = new List<bool>();
            for (int i = 0; i < lst_1.Count; i++)
            {
                lst_code.AddRange(h.encode(lst_1[i], matrix_sinh));
            }
            //List<bool> list = h.encode(m,lst);

            //In từ mã.
            string code = "";
            for (int i = 0; i < lst_code.Count; i++)
            {
                int item = lst_code[i] ? 1 : 0;
                code += item.ToString();
            }
            textBox2.Text = code;


            //for (int i = 0; i < list.Count; i++)
            //{  
            //    int item = list[i] ? 1 : 0;
            //    code += item.ToString();
            //}
            //string code = "";
            //for (int i = 0; i < list.Count; i++)
            //{
            //    int item = list[i] ? 1 : 0;
            //    code += item.ToString();
            //}


            //In mã có lỗi được random.
            string code_er = "";
            List<bool> list_er = h.random_error(lst_code, (float)1 / code.Length);
            for (int i = 0; i < list_er.Count; i++)
            {
                int item = list_er[i] ? 1 : 0;
                code_er += item.ToString();
            }
            textBox3.Text = code_er;

            //In Lỗi
            string loi = "";
            for (int i = 0; i < lst_code.Count; i++)
            {
                //int item_er = list_er[i] ? 1 : 0;
                //int item = list[i] ? 1 : 0;
                //if(item!=item_er)
                //{
                //    loi += item_er.ToString();
                //}    
                bool l = (list_er[i] ^ lst_code[i]);
                int item = l ? 1 : 0;
                loi += item.ToString();

            }
            textBox4.Text = loi;


            //Giải mã.

            List<List<bool>> lst_de = h.split(list_er, matrix_kiemtra[0].Count);
            List<bool> list_out = new List<bool>();
            for(int i = 0;i<lst_de.Count;i++)
            {
                list_out.AddRange(h.decode_matrix(lst_de[i], matrix_kiemtra));
            }    
            //List<bool> list_out = h.decode(list_er, m.Count);
            //List<bool> list_out = h.decode_matrix(list_er,);

            //In Thong tin được giải mã.
            string strout = "";
            for (int i = 0; i < list_out.Count; i++)
            {
                int item = list_out[i] ? 1 : 0;
                strout += item.ToString();
            }
            textBox5.Text = strout;

            //In Thông Báo
            int kt = 0;
            for (int i = 0; i < m.Count; i++)
            {  
                bool l = (m[i] ^ list_out[i]);
                if (!l)
                    kt = 1;
                else
                {
                    kt = 0;
                    break;
                }
            }
            if (kt==1)
                lal_ketqua.Text = "Giải mã đúng";
            else
                lal_ketqua.Text = "Giải mã sai, hãy thử lại";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int hang = int.Parse(nb_hang.Value.ToString());
            int cot = int.Parse(nb_cot.Value.ToString());
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel1.Enabled = true;
            flowLayoutPanel2.Enabled = true;
            flowLayoutPanel1.Width = 28 * cot;
            flowLayoutPanel2.Width = 28 * cot;
            for (int i = 0; i <hang; i++)
            {
                for(int j  = 0; j <cot; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(20,20);
                    textBox.KeyDown += TextBox_KeyDown;
                    textBox.KeyPress += TextBox_KeyPress;
                    textBox.Name = "txt_" + i + "_" + j;
                    flowLayoutPanel1.Controls.Add(textBox);
                    if (i < hang - 1)
                    {
                        TextBox textBox2 = new TextBox();
                        textBox2.Size = new Size(20, 20);
                        textBox2.Name = "txt2_" + i + "_" + j;
                        textBox2.KeyUp += TextBox2_KeyUp;
                        textBox2.KeyDown += TextBox2_KeyDown;
                        textBox2.KeyPress += TextBox2_KeyPress;
                        flowLayoutPanel2.Controls.Add(textBox2);
                    }
                }    
            }   
            
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            else
            {
                if (Char.IsDigit(e.KeyChar))
                    if (e.KeyChar != '1' && e.KeyChar != '0')
                        e.Handled = true;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.TextLength >= 1)
                t.Text = "";
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.TextLength >= 1)
                t.Text = "";
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            else
            {
                if (Char.IsDigit(e.KeyChar))
                    if (e.KeyChar != '1' && e.KeyChar != '0')
                        e.Handled = true;
            }    
        }


        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==0)
            {
                button2.Visible = false;
                btn_huy.Visible = false;
                btn_xacnhan.Visible = false;
                nb_hang.Value = matrix_sinh.Count;
                nb_cot.Value = matrix_sinh[0].Count;
                nb_cot.Enabled = false;
                nb_hang.Enabled = false;
                hienthiMatranSinh();
                hienthiMatranKiemTra();
            } 
            else
            {
                button2.Visible = true;
                btn_huy.Visible = true;
                btn_xacnhan.Visible = true;
                nb_hang.Enabled = true;
                nb_cot.Enabled = true;
                nb_cot.Value = 0;
                nb_hang.Value = 0;
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();

            }    
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            btn_huy.Visible = false;
            btn_xacnhan.Visible = false;
            nb_hang.Value = matrix_sinh.Count;
            nb_cot.Value = matrix_sinh[0].Count;
            nb_cot.Enabled = false;
            nb_hang.Enabled = false;
            comboBox1.SelectedIndex = 0;
            hienthiMatranSinh();
            hienthiMatranKiemTra();
        }

        private void btn_xacnhan_Click(object sender, EventArgs e)
        {


            //Lay ma tran sinh
            List<string> lst_str_sinh = new List<string>();
            int i = 0;
            while(i<flowLayoutPanel1.Controls.Count)
            {
                string str_sinh = "";
                for (int j = i;j<nb_cot.Value+i;j++)
                {
                    str_sinh += flowLayoutPanel1.Controls[j].Text;
                }
                i += (int)nb_cot.Value;
                lst_str_sinh.Add(str_sinh);
            }
            matrix_sinh.Clear();
            addMatrix_Sinh(lst_str_sinh);

            //Lay ma tran kiem tra.
            List<string> lst_str_kiemtra = new List<string>();

            int k = 0;
            while (k < flowLayoutPanel2.Controls.Count)
            {
                string str_kiemtra = "";
                for (int j = k; j < nb_cot.Value + k; j++)
                {
                    str_kiemtra += flowLayoutPanel2.Controls[j].Text;
                }
                k += (int)nb_cot.Value;
                lst_str_kiemtra.Add(str_kiemtra);
            }
            matrix_kiemtra.Clear();
            addMatrix_KiemTra(lst_str_kiemtra);

            button2.Visible = false;
            btn_huy.Visible = false;
            btn_xacnhan.Visible = false;
            nb_hang.Value = matrix_sinh.Count;
            nb_cot.Value = matrix_sinh[0].Count;
            nb_cot.Enabled = false;
            nb_hang.Enabled = false;
            comboBox1.SelectedIndex = 0;
            hienthiMatranSinh();
            hienthiMatranKiemTra();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            else
            {
                if (Char.IsDigit(e.KeyChar))
                    if (e.KeyChar != '1' && e.KeyChar != '0')
                        e.Handled = true;
            }
        }
    }
}
