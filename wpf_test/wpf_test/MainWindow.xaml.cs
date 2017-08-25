﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace wpf_test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///         
    public class UserInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public int idCompany { get; set; }
        }
    public partial class MainWindow : Window
    {
        wpftestEntities2 db;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new wpftestEntities2();
            dataGrid1.ItemsSource = db.users.ToList();
            dataGrid2.ItemsSource = db.Company.ToList();
            Company com = new Company();
            comboBox2.ItemsSource = db.Company.ToList();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                users us = new users();
                us.name = textBox1.Text;
                us.login = textBox2.Text;
                us.password = textBox3.Text;
                us.idCompany = Convert.ToInt32(comboBox2.SelectedValue);
                db.users.AddObject(us);
                db.SaveChanges();
                dataGrid1.ItemsSource = db.users.ToList();
            }
            else
                MessageBox.Show("Заполните все поля", "Ошибка");
         }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            users us = (users)dataGrid1.SelectedItem;
            var selected = db.users.Where(w => w.id == us.id).FirstOrDefault();
            selected.login = textBox2.Text;
            selected.name = textBox1.Text;
            selected.password = textBox3.Text;
            selected.idCompany = Convert.ToInt32(comboBox2.SelectedValue);
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                db.SaveChanges();
                dataGrid1.ItemsSource = db.users.ToList();
            }
            else
                MessageBox.Show("Заполните все поля", "Ошибка");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {   
            users us = (users)dataGrid1.SelectedItem;
            var selected = db.users.Where(w => w.id == us.id).FirstOrDefault();
            db.users.DeleteObject(selected);
            db.SaveChanges();
            dataGrid1.ItemsSource = db.users.ToList();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                users us = (users)dataGrid1.SelectedItem;
                textBox1.Text = us.name;
                textBox2.Text = us.login;
                textBox3.Text = us.password;
                comboBox2.SelectedValue = us.idCompany;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                Company com = new Company();
                com.name = textBox5.Text;
                com.ContractStatus = comboBox1.Text;
                db.Company.AddObject(com);
                db.SaveChanges();
                dataGrid2.ItemsSource = db.Company.ToList();
            }
            else
                MessageBox.Show("Заполните все поля", "Ошибка");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                Company com = (Company)dataGrid2.SelectedItem;
                var selected = db.Company.Where(w => w.id == com.id).FirstOrDefault();
                selected.name = textBox5.Text;
                selected.ContractStatus = comboBox1.Text;
                db.SaveChanges();
                dataGrid2.ItemsSource = db.Company.ToList();
            }
            else
                MessageBox.Show("Заполните все поля", "Ошибка");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid2.SelectedItem != null)
            {
                Company com = (Company)dataGrid2.SelectedItem;
                var selected = db.Company.Where(w => w.id == com.id).FirstOrDefault();
                db.Company.DeleteObject(selected);
                db.SaveChanges();
                dataGrid2.ItemsSource = db.Company.ToList();
                dataGrid1.ItemsSource = db.users.ToList();
            }
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Company com = (Company)dataGrid2.SelectedItem;;
            users us = new users();
            var selected = db.Company.Where(w => w.id == com.id).FirstOrDefault();
            textBox5.Text = com.name;
            comboBox1.Text = com.ContractStatus;

            var selectedUs = db.users.Where(w => w.idCompany == com.id).FirstOrDefault();
            List<UserInfo> d = new List<UserInfo>();
            var q = (from t1 in db.users
                     where t1.idCompany == com.id
                     select new UserInfo() 
                     {
                         id=t1.id, name=t1.name, login=t1.login, password=t1.password, idCompany=t1.idCompany
                     });
            d=q.ToList();
            dataGrid3.ItemsSource = d;

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
