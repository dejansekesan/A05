using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace A05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
            PrikaziSve();
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Add("Ponedeljak");
            comboBox1.Items.Add("Utorak");
            comboBox1.Items.Add("Sreda");
            comboBox1.Items.Add("Četvrtak");
            comboBox1.Items.Add("Petak");
            comboBox1.Items.Add("Subota");
            comboBox1.Items.Add("Nedelja");
        }
        private void PrikaziSve()
        {
            listView1.Items.Clear();
            try
            {
                List<Aktivnost> spisak = Aktivnost.UcitajSve();
                string[] podaci = new string[5];
                foreach (Aktivnost a in spisak)
                {
                    podaci[0] = a.AktivnostID.ToString();
                    podaci[1] = a.NazivAktivnosti;
                    switch (a.DanID)
                    {
                        case 1: podaci[2] = "Ponedeljak"; break;
                        case 2: podaci[2] = "Utorak"; break;
                        case 3: podaci[2] = "Sreda"; break;
                        case 4: podaci[2] = "Četvrtak"; break;
                        case 5: podaci[2] = "Petak"; break;
                        case 6: podaci[2] = "Subota"; break;
                        case 7: podaci[2] = "Nedelja"; break;
                    }
                    podaci[3] = a.Pocetak.ToString("HH:mm");
                    podaci[4] = a.Zavrsetak.ToString("HH:mm");
                    ListViewItem zapis = new ListViewItem(podaci);
                    listView1.Items.Add(zapis);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ObrisiPolja()
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           try
        {
    if (textBox1.Text == String.Empty)
    {
        ObrisiPolja();
    }
    else
    {
        Aktivnost a = new Aktivnost(Convert.ToInt32(textBox1.Text));
        textBox2.Text = a.NazivAktivnosti;
        switch (a.DanID)
        {
            case 1: comboBox1.Text = "Ponedeljak"; break;
            case 2: comboBox1.Text = "Utorak"; break;
            case 3: comboBox1.Text = "Sreda"; break;
            case 4: comboBox1.Text = "Četvrtak"; break;
            case 5: comboBox1.Text = "Petak"; break;
            case 6: comboBox1.Text = "Subota"; break;
            case 7: comboBox1.Text = "Nedelja"; break;
        }
        textBox3.Text = a.Pocetak.ToString("HH:mm");
        textBox4.Text = a.Zavrsetak.ToString("HH:mm");
    }
}
catch {
    MessageBox.Show("Validnu sifru pls");
}
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                Aktivnost a = new Aktivnost();
                try
                {
                    if (textBox2.Text != String.Empty)
                    {
                        a.NazivAktivnosti = textBox2.Text;
                        if (comboBox1.SelectedIndex != -1)
                            a.DanID = comboBox1.SelectedIndex + 1;
                        else
                            a.DanID = 1;
                        if (textBox3.Text != String.Empty)
                            a.Pocetak = Convert.ToDateTime(textBox3.Text);
                        else
                            a.Pocetak = Convert.ToDateTime("00:00");
                        if (textBox4.Text != String.Empty)
                            a.Zavrsetak = Convert.ToDateTime(textBox4.Text);
                        else
                            a.Zavrsetak = Convert.ToDateTime("00:00");
                        if (a.Unesi())
                        {
                            MessageBox.Show(a.Message);
                            PrikaziSve();
                            ObrisiPolja();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Morate uneti naziv");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Polje Šifra mora biti prazno!");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
        }
        Form2 f2 = new Form2();
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Form3 f3 = new Form3();

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            f3.ShowDialog();
        }
    }
}
