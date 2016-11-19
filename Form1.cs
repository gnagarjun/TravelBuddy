using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections;
using System.Dynamic;

namespace calHacks2016Traveler
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            float distance = 0;
            //string from = origin.Text;

            string origin = sourceTextBox.Text;
            string destination = destTextBox.Text;

            string priority = comboBox1.SelectedItem.ToString();
            string url=string.Empty;
            if (string.Equals("Save my time", priority))
            {
                //Save my time
                
            }
            if (string.Equals("Save my money", priority.ToString()))
            {
                //Save my money
                url = "https://api.sandbox.amadeus.com/v1.2/flights/low-fare-search?origin=IST&destination=BOS&departure_date=2016-12-15&number_of_results=3&apikey=7qLFu6R2w4mhfPJvHk1SSdw9uiodewPJ";
                string[] date = dateBox.Text.ToString().Split('-');
                System.Net.WebClient wc = new WebClient();
                byte[] response = wc.DownloadData(url);
                string content = System.Text.Encoding.ASCII.GetString(response);

                WebClient client = new WebClient();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic item = serializer.Deserialize<object>(content);

                List<string> listInfo = new List<string>();
                int count = 0;
                listView1.Items.Clear();
                listView1.FullRowSelect = true;
                listView1.View = View.Details;
                listView1.Columns.Add("Option");
                listView1.Columns.Add("Destination Airport");
                listView1.Columns.Add("Ticket Price");
                listView1.Columns.Add("Airlines");

                foreach (Dictionary<string, object> d in item["results"])
                {
                    if (count == 10) { break; }
                    ListViewItem lvi = new ListViewItem();
                    foreach (var par in d)
                    {
                       // MessageBox.Show(par["itenaries"].ToString());
                    }
                    
                   
                }


            }
            if (string.Equals("Help me explore places", priority.ToString()))
            {
                //Help me explore places
                string[] date = dateBox.Text.ToString().Split('-');
                //sample working url  url = @"https://api.sandbox.amadeus.com/v1.2/flights/inspiration-search?apikey=7jKl9U9xtstyE6ftcAsGZw9DnqbeIjHs&origin=NYC&departure_date=2016-11-16";
                url = @"https://api.sandbox.amadeus.com/v1.2/flights/inspiration-search?apikey=7jKl9U9xtstyE6ftcAsGZw9DnqbeIjHs&origin=" + origin + "&departure_date=" + date[0] + "-" + date[1] + "-" + date[2];
                System.Net.WebClient wc = new WebClient();
                byte[] response = wc.DownloadData(url);
                string content = System.Text.Encoding.ASCII.GetString(response);

                WebClient client = new WebClient();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic item = serializer.Deserialize<object>(content);
                // string name = item["name"];
                List<string> listInfo = new List<string>();
                int count = 0;
                listView1.Items.Clear();
                listView1.FullRowSelect = true;
                listView1.View = View.Details;
                listView1.Columns.Add("Option");
                listView1.Columns.Add("Destination Airport");
                listView1.Columns.Add("Ticket Price");
                listView1.Columns.Add("Airlines");

                foreach (Dictionary<string, object> d in item["results"])
                {
                    if (count == 10) { break; }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (count + 1).ToString();
                    lvi.SubItems.Add(d["destination"].ToString());
                    lvi.SubItems.Add(d["price"].ToString());
                    lvi.SubItems.Add(d["airline"].ToString());
                    listView1.Items.Add(lvi);
                    count++;
                }

                //travel intelligence
                string dateTime = DateTime.Today.ToString();
                string[] dateNow = dateTime.Split(' ')[0].ToString().Split('/');


                dateNow[2] = (Int32.Parse(dateNow[2]) - 1).ToString();        
                url = @"https://api.sandbox.amadeus.com/v1.2/travel-intelligence/top-destinations?apikey=7jKl9U9xtstyE6ftcAsGZw9DnqbeIjHs&period="+dateNow[2]+"-"+dateNow[0]+"&origin="+origin;
                response = wc.DownloadData(url);
                content = System.Text.Encoding.ASCII.GetString(response);
                serializer = new JavaScriptSerializer();
                item = serializer.Deserialize<object>(content);
                // string name = item["name"];
                listInfo = new List<string>();
                int trendyCount = 0;

                listView2.Items.Clear();
                listView2.FullRowSelect = true;
                listView2.View = View.Details;
                listView2.Columns.Add("Option");
                listView2.Columns.Add("Trendy Destinations");
                listView2.Columns.Add("Travelers last year");
                foreach (Dictionary<string, object> d in item["results"])
                {
                    if (trendyCount == 10) { break; }
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (trendyCount+1).ToString();
                    lvi.SubItems.Add(d["destination"].ToString());
                    lvi.SubItems.Add(d["travelers"].ToString());
                    listView2.Items.Add(lvi);
                    trendyCount++;
                }
                //travel intelligence

                //worked before -> string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
                //ser - worked  string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?origins="+origin+"&destinations="+destination+"&key=AIzaSyAIw81_BYtUGphT4l_z8DuuPZ1xDK7n-BI";

            }

            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                destTextBox.ReadOnly = true;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ticket booked with the selected option!");
        }
    }
}
