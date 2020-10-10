﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSet_DataTable
{
    public partial class Form1 : Form
    {

        DataSet ds = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }


        void veriOlustur()
        {
            //categories tablosuna kategori ekle
            DataRow newRow1 = ds.Tables["Categories"].NewRow();
            newRow1["CategoryName"] = "Bilgisayar";

            DataRow newRow2 = ds.Tables["Categories"].NewRow();
            newRow2["CategoryName"] = "Beyaz Eşya";

            DataRow newRow3 = ds.Tables["Categories"].NewRow();
            newRow3["CategoryName"] = "Elektronik";


            ds.Tables["Categories"].Rows.Add(newRow1);
            ds.Tables["Categories"].Rows.Add(newRow2);
            ds.Tables["Categories"].Rows.Add(newRow3);

            //product tablosuna ürünleri ekle
            DataRow productRow3 = ds.Tables["Products"].NewRow();
            productRow3["ProductName"] = "Sony Müzik Seti";
            productRow3["CategoryID"] = 3;

            DataRow productRow2 = ds.Tables["Products"].NewRow();
            productRow2["ProductName"] = "Arçelik Buzdolabı";
            productRow2["CategoryID"] = 2;

            DataRow productRow1 = ds.Tables["Products"].NewRow();
            productRow1["ProductName"] = "Casper";
            productRow1["CategoryID"] = 1;


            ds.Tables[1].Rows.Add(productRow3);
            ds.Tables[1].Rows.Add(productRow2);
            ds.Tables[1].Rows.Add(productRow1);
        
        }

        void tablolariOlustur()
        {
            DataTable categories = new DataTable("Categories");
            DataColumn categoryID = new DataColumn();
            categoryID.AllowDBNull = false;
            categoryID.AutoIncrement = true;
            categoryID.AutoIncrementSeed = 1;
            categoryID.AutoIncrementStep = 1;
            categoryID.ColumnName = "CategoryID";
            categoryID.DataType = typeof(int);

            DataColumn categoryName = new DataColumn();
            categoryName.AllowDBNull = false;
            categoryName.ColumnName = "CategoryName";
            categoryName.DataType = typeof(string);

            categories.Columns.Add(categoryID);
            categories.Columns.Add(categoryName);


            //product tablosunu olustur.

            DataTable products = new DataTable("Products");
            DataColumn productID = new DataColumn();
            productID.AllowDBNull = false;
            productID.AutoIncrement = true;
            productID.AutoIncrementSeed = 1;
            productID.AutoIncrementStep = 1;
            productID.ColumnName = "ProductID";
            productID.DataType = typeof(int);

            DataColumn productName = new DataColumn();
            productName.AllowDBNull = false;
            productName.ColumnName = "ProductName";
            productName.DataType = typeof(string);

            DataColumn productCategoryID = new DataColumn();
            productCategoryID.AllowDBNull = false;
            productCategoryID.AutoIncrement = true;
            productCategoryID.AutoIncrementSeed = 1;
            productCategoryID.AutoIncrementStep = 1;
            productCategoryID.ColumnName = "CategoryID";
            productCategoryID.DataType = typeof(int);

            
            products.Columns.Add(productName);
            products.Columns.Add(productCategoryID);
            products.Columns.Add(productID);


            ds.Tables.Add(categories);
            ds.Tables.Add(products);


            //tablolar arası ilişkiyi oluştur

            DataRelation drel = new DataRelation("CategoriesProduct",categoryID,productCategoryID);
            ds.Relations.Add(drel);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            veriOlustur();

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tablolariOlustur();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int categoryID = (int)dataGridView1.SelectedRows[0].Cells["CategoryID"].Value;

            DataRow[] rows = ds.Tables["Categories"].Select("CategoryID = " + categoryID);
            string productList = string.Empty;

            foreach (DataRow r in rows[0].GetChildRows("CategoriesProduct"))
            {
                productList += r["ProductName"].ToString() + Environment.NewLine;
            }
            MessageBox.Show(productList);
        }
    }
}
