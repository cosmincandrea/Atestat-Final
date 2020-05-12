using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        class Student
        {
            public int nivel_mate, nivel_info, nivel_rom, id;
            public string Profesor;
        }
        Student X = new Student();
        public string Profesor = "0";
        public Form1()
        {
            InitializeComponent();
        }
        public DataTable Elev;
        public int Total_info;
        public int Total_mate;
        public int Total_rom;
        private void relatiiBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.relatiiBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.atestatDBDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'atestatDBDataSet.LectiiRomana' table. You can move, or remove it, as needed.
            this.lectiiRomanaTableAdapter.Fill(this.atestatDBDataSet.LectiiRomana);
            // TODO: This line of code loads data into the 'atestatDBDataSet.LectiiMate' table. You can move, or remove it, as needed.
            this.lectiiMateTableAdapter.Fill(this.atestatDBDataSet.LectiiMate);
            // TODO: This line of code loads data into the 'atestatDBDataSet.LectiiInfo' table. You can move, or remove it, as needed.
            this.lectiiInfoTableAdapter.Fill(this.atestatDBDataSet.LectiiInfo);
            // TODO: This line of code loads data into the 'atestatDBDataSet.Elev' table. You can move, or remove it, as needed.
            this.elevTableAdapter.Fill(this.atestatDBDataSet.Elev);
            // TODO: This line of code loads data into the 'atestatDBDataSet.Profesor' table. You can move, or remove it, as needed.
            this.profesorTableAdapter.Fill(this.atestatDBDataSet.Profesor);
            // TODO: This line of code loads data into the 'atestatDBDataSet.Relatii' table. You can move, or remove it, as needed.
            this.relatiiTableAdapter.Fill(this.atestatDBDataSet.Relatii);
            atestatDBDataSet.EnforceConstraints = false;
            DataTable dd = this.atestatDBDataSet.Profesor;
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                comboBox1.Items.Add(dd.Rows[i]["Nume"] + " " + dd.Rows[i]["Prenume"]);
            }
            Total_info = this.lectiiInfoTableAdapter.ScalarQuery1().Value;
            Total_mate = this.lectiiMateTableAdapter.ScalarQuery1().Value;
            Total_rom = this.lectiiRomanaTableAdapter.ScalarQuery1().Value;
            eroare1.Hide();
            Point tt = new Point(60, 45);
            panelElev.Hide();
            panelMate.Hide();
            pannStatistici.Hide();
            panelinfo.Hide();
            panelLectieRomana.Hide();
            panelElev.Location = tt;
            panelMate.Location = tt;
            panelinfo.Location = tt;
            pannStatistici.Location = tt;
            panelLectieRomana.Location = tt;
            panelStart.Show();
            FillStatistici();
        }

        void NXT()
        {
            label4.Text = "Bine ai venit " + Elev.Rows[0]["Prenume"];
            NivelInfo.Text = X.nivel_info + "/" + Total_info;
            NivelMate.Text = X.nivel_mate + "/" + Total_mate;
            NivelRO.Text = X.nivel_rom + "/" + Total_rom;
            setProf.Text = X.Profesor;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Profesor = comboBox1.Text;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtNume.Text != "" && txtPrenume.Text != "")
            {
                this.elevTableAdapter.FillByIndivid(this.atestatDBDataSet.Elev, txtNume.Text, txtPrenume.Text);
                Elev = this.atestatDBDataSet.Elev;
                if (Elev.Rows.Count == 1)
                {
                    X.nivel_info = Int32.Parse(Elev.Rows[0]["NielInfo"].ToString());
                    X.nivel_mate = Int32.Parse(Elev.Rows[0]["NivelMate"].ToString());
                    X.nivel_rom = Int32.Parse(Elev.Rows[0]["NivelRom"].ToString());
                    X.id = Int32.Parse(Elev.Rows[0]["idE"].ToString());
                    X.Profesor = Elev.Rows[0]["PProf"].ToString() + " " + Elev.Rows[0]["NProf"].ToString();
                    panelElev.Show();
                    NXT();
                }


            }
        }
        void FILLRO()
        {
            this.lectiiRomanaTableAdapter.FillByVal(this.atestatDBDataSet.LectiiRomana, X.nivel_rom + 1);
            DataTable XX = this.atestatDBDataSet.LectiiRomana;
            if (XX.Rows.Count > 0)
            {
                txtTitlu.Text = XX.Rows[0]["Titlu"].ToString();
                txtAutor.Text = XX.Rows[0]["Autor"].ToString();
                txtTema.Text = XX.Rows[0]["Tema"].ToString();
                txtDescriere.Text = XX.Rows[0]["Descriere"].ToString();
            }
        }
        private void buttonRo_Click(object sender, EventArgs e)
        {
            if (X.nivel_rom < Total_rom)
            {
                panelLectieRomana.Show();
                panelElev.Hide();
                FILLRO();
            }
        }
        void FillINFO()
        {
            this.lectiiInfoTableAdapter.FillByVal(this.atestatDBDataSet.LectiiInfo, X.nivel_info + 1);
            DataTable lectieI = this.atestatDBDataSet.LectiiInfo;
            if (lectieI.Rows.Count > 0)
            {
                txtInfo.Text = lectieI.Rows[0]["Titlu"].ToString();
                txtLink.Text = lectieI.Rows[0]["Algoritm"].ToString();
                txtExplicatie.Text = lectieI.Rows[0]["Explicatie"].ToString();
            }
        }
        private void buttonInfo_Click(object sender, EventArgs e)
        {
            if (X.nivel_info < Total_info)
            {
                FillINFO();
                panelinfo.Show();
                panelElev.Hide();
            }
        }
        void FILLMATE()
        {
            this.lectiiMateTableAdapter.FillByVal(this.atestatDBDataSet.LectiiMate, X.nivel_mate + 1);
            DataTable lectieM = this.atestatDBDataSet.LectiiMate;
            if (lectieM.Rows.Count > 0)
            {
                txtNumeMate.Text = lectieM.Rows[0]["Titlu"].ToString();
                txtEx.Text = lectieM.Rows[0]["Exercitii"].ToString();
                txtTeorie.Text = lectieM.Rows[0]["Teorie"].ToString();
            }
        }
        private void buttonMate_Click(object sender, EventArgs e)
        {
            if (X.nivel_mate < Total_mate)
            {
                panelMate.Show();
                panelElev.Hide();
                FILLMATE();
            }
        }

        private void back3_Click(object sender, EventArgs e)
        {
            panelinfo.Hide();
            panelElev.Show();
        }

        private void back1_Click(object sender, EventArgs e)
        {
            panelMate.Hide();
            panelElev.Show();
        }

        private void back2_Click(object sender, EventArgs e)
        {
            panelLectieRomana.Hide();
            panelElev.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelElev.Hide();
            panelStart.Show();
        }
        int Prelucrare()
        {

            string nume1 = "0";
            string nume2;
            string[] words = Profesor.Split(' ');

            foreach (var word in words)
            {
                if (nume1 == "0")
                    nume1 = word;
                else
                    nume2 = word;
            }
            this.profesorTableAdapter.FillByid(this.atestatDBDataSet.Profesor, nume1);
            DataTable rasp = this.atestatDBDataSet.Profesor;
            return Int32.Parse(rasp.Rows[0]["idP"].ToString());
        }
        private void signup_Click(object sender, EventArgs e)
        {
            if (txtNume.Text != "" && txtPrenume.Text != "" && Profesor != "0")
            {
                int rasp = this.elevTableAdapter.ScalarQuery1(txtNume.Text.ToString(), txtPrenume.Text.ToString()).Value;
                if(rasp == 0)
                {
                    this.elevTableAdapter.InsertQueryElev(txtNume.Text, txtPrenume.Text);
                    this.elevTableAdapter.FillByid(this.atestatDBDataSet.Elev, txtNume.Text, txtPrenume.Text);
                    DataTable xx = this.atestatDBDataSet.Elev;
                    int idElev = Int32.Parse(xx.Rows[0]["idE"].ToString());
                    int idProf = Prelucrare();
                    this.relatiiTableAdapter.Insert(idProf, idElev);
                }
                else
                    eroare1.Show();
            }
        }

        private void fininfo_Click(object sender, EventArgs e)
        {

            this.elevTableAdapter.UpdateQuery(X.nivel_rom,X.nivel_mate, X.nivel_info+1, X.id);
            X.nivel_info++;
            NXT();
            FillINFO();
            panelinfo.Hide();
            panelElev.Show();
        }

        private void FinRo_Click(object sender, EventArgs e)
        {
            this.elevTableAdapter.UpdateQuery(X.nivel_rom + 1, X.nivel_mate, X.nivel_info, X.id);
            X.nivel_rom++;
            NXT();
            panelLectieRomana.Hide();
            panelElev.Show();
            FILLRO();
        }

        private void finmate_Click(object sender, EventArgs e)
        {
            this.elevTableAdapter.UpdateQuery(X.nivel_rom , X.nivel_mate + 1, X.nivel_info, X.id);
            X.nivel_mate++;
            NXT();
            panelMate.Hide();
            panelElev.Show();
        }


        private void StatisticiProf_Click(object sender, EventArgs e)
        {
            panelStart.Hide();
            pannStatistici.Show();
            this.profesorTableAdapter.Fill(this.atestatDBDataSet.Profesor);
            DataTable dd = this.atestatDBDataSet.Profesor;
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                comboBox2.Items.Add(dd.Rows[i]["Nume"]);
            }
        }

        private void FillStatistici()
        {
            this.elevTableAdapter.Fill(this.atestatDBDataSet.Elev);
            DataTable elevi= this.atestatDBDataSet.Elev;
            for (int i = 0; i < elevi.Rows.Count; i++)
            {
                if (Convert.ToInt32(elevi.Rows[i]["NivelRom"].ToString()) == Total_rom
                    && Convert.ToInt32(elevi.Rows[i]["NivelMate"].ToString()) == Total_mate
                    && Convert.ToInt32(elevi.Rows[i]["NielInfo"].ToString()) == Total_info)
                    fin.Text += elevi.Rows[i]["Nume"].ToString()+ " " + elevi.Rows[i]["Prenume"].ToString() + "\n";
                else if (Convert.ToInt32(elevi.Rows[i]["NivelRom"].ToString()) == 0
                    && Convert.ToInt32(elevi.Rows[i]["NivelMate"].ToString()) == 0
                    && Convert.ToInt32(elevi.Rows[i]["NielInfo"].ToString()) == 0)
                    zero.Text += elevi.Rows[i]["Nume"].ToString()+" " + elevi.Rows[i]["Prenume"].ToString() + "\n";
                    else
                        st.Text += elevi.Rows[i]["Nume"].ToString()+" " + elevi.Rows[i]["Prenume"].ToString() + "\n";
            }
        }
        private void FillStatistici(String Prof)
        {
            fin.Clear();
            st.Clear();
            zero.Clear();
            this.elevTableAdapter.FillBy(this.atestatDBDataSet.Elev, Prof);
            DataTable elevi = this.atestatDBDataSet.Elev;
            for (int i = 0; i < elevi.Rows.Count; i++)
            {
                if (Convert.ToInt32(elevi.Rows[i]["NivelRom"].ToString()) == Total_rom
                    && Convert.ToInt32(elevi.Rows[i]["NivelMate"].ToString()) == Total_mate
                    && Convert.ToInt32(elevi.Rows[i]["NielInfo"].ToString()) == Total_info)
                    fin.Text += elevi.Rows[i]["Nume"].ToString() + " " + elevi.Rows[i]["Prenume"].ToString() + "\n";
                else if (Convert.ToInt32(elevi.Rows[i]["NivelRom"].ToString()) == 0
                    && Convert.ToInt32(elevi.Rows[i]["NivelMate"].ToString()) == 0
                    && Convert.ToInt32(elevi.Rows[i]["NielInfo"].ToString()) == 0)
                    zero.Text += elevi.Rows[i]["Nume"].ToString() + " " + elevi.Rows[i]["Prenume"].ToString() + "\n";
                else
                    st.Text += elevi.Rows[i]["Nume"].ToString() + " " + elevi.Rows[i]["Prenume"].ToString() + "\n";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String NumeProf = comboBox2.Text.ToString();
            FillStatistici(NumeProf);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pannStatistici.Hide();
            panelStart.Show();
        }


    }
}
