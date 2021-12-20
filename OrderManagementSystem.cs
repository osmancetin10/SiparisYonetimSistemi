using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagementSystem
{
    public partial class OrderManagementSystem : Form
    {

        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; DataBase=OrderManagementSystem; user ID=postgres; password=7787047");

        public OrderManagementSystem()
        {
            InitializeComponent();
            listCountry(true);
            listColor(true);
            listAddressType(true);
            listPaymentType(true);
            listCatalog(true);
            listShippingType(true);
            listManager(true);
            listWarehouse(true);
            listProduct(true);
            listTransportFirm(true);
            listCustomer(true);
            listAddress(true);
            listPayment(true);
            listOrder(true);
        }

        #region Country Functions
        private void btnListCountry_Click(object sender, EventArgs e)
        {
            listCountry(false);
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("insert into country (name, short_name, code) values (@p1, @p2, @p3)", con);
            command.Parameters.AddWithValue("@p1", txtCountryName.Text);
            command.Parameters.AddWithValue("@p2", txtCountryShortName.Text);
            command.Parameters.AddWithValue("@p3", Convert.ToInt32(nbrCountryCode.Text));
            command.ExecuteNonQuery();
            con.Close();

            listCountry(false);
            clearCountryTextBoxes();
            MessageBox.Show("Ülke Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateCountry(int id, string name, string short_name, int code)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("call update_country(:p_id,:p_name, :p_short_name, :p_code)", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_short_name", short_name);
            command.Parameters.AddWithValue("p_code", code);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listCountry(false);
            MessageBox.Show("Ülke Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listCountry(bool addButton) {
            string query = "select * from country";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCountry = new DataSet();
            dAdapter.Fill(dsCountry);
            countryGrid.DataSource = dsCountry.Tables[0];

            countryGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteCountry";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.countryGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateCountry";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.countryGrid.Columns.Add(updateButton);
                }
            }
            
        }

        private void clearCountryTextBoxes()
        {
            txtCountryName.Clear();
            txtCountryShortName.Clear();
            nbrCountryCode.Clear();
        }

        private void deleteCountry(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("Delete from country where id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listCountry(false);
        }
        
        private void countryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (countryGrid.Columns[e.ColumnIndex].Name == "DeleteCountry" && 
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                int countryId = Convert.ToInt32(countryGrid.CurrentRow.Cells["id"].Value);
                deleteCountry(countryId);
            }

            if (countryGrid.Columns[e.ColumnIndex].Name == "UpdateCountry" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int countryId = Convert.ToInt32(countryGrid.CurrentRow.Cells["id"].Value);
                string countryName = countryGrid.CurrentRow.Cells["name"].Value.ToString();
                string countryShortName = countryGrid.CurrentRow.Cells["short_name"].Value.ToString();
                int code = Convert.ToInt32(countryGrid.CurrentRow.Cells["code"].Value);
                updateCountry(countryId, countryName, countryShortName, code);
            }
        }
        #endregion

        #region Color Functions
        private void btnListColor_Click(object sender, EventArgs e)
        {
            listColor(false);
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("insert into color (name) values (@p1)", con);
            command.Parameters.AddWithValue("@p1", txtColorName.Text);
            command.ExecuteNonQuery();
            con.Close();

            listColor(false);
            clearColorTextBoxes();
            MessageBox.Show("Renk Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateColor(int id, string name)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("call update_color(:p_id,:p_name)", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listColor(false);
            MessageBox.Show("Renk Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listColor(bool addButton)
        {
            string query = "select * from color";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsColor = new DataSet();
            dAdapter.Fill(dsColor);
            colorGrid.DataSource = dsColor.Tables[0];

            colorGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteColor";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.colorGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateColor";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.colorGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearColorTextBoxes()
        {
            txtColorName.Clear();
        }

        private void deleteColor(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM color WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listColor(false);
        }

        private void colorGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (colorGrid.Columns[e.ColumnIndex].Name == "DeleteColor" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int colorId = Convert.ToInt32(colorGrid.CurrentRow.Cells["id"].Value);
                deleteColor(colorId);
            }

            if (colorGrid.Columns[e.ColumnIndex].Name == "UpdateColor" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int colorId = Convert.ToInt32(colorGrid.CurrentRow.Cells["id"].Value);
                string colorName = colorGrid.CurrentRow.Cells["name"].Value.ToString();
                updateColor(colorId, colorName);
            }
        }
        #endregion

        #region Address Type Functions
        private void btnListAddressType_Click(object sender, EventArgs e)
        {
            listAddressType(false);
        }

        private void btnAddAddressType_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO address_type (name) VALUES (@p1)", con);
            command.Parameters.AddWithValue("@p1", txtAddressTypeName.Text);
            command.ExecuteNonQuery();
            con.Close();

            listAddressType(false);
            clearAddressTypeTextBoxes();
            MessageBox.Show("Adres Tipi Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateAddressType(int id, string name)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("CALL update_address_type(:p_id,:p_name)", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listAddressType(false);
            MessageBox.Show("Adres Tipi Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listAddressType(bool addButton)
        {
            string query = "SELECT * FROM address_type";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsAddressType = new DataSet();
            dAdapter.Fill(dsAddressType);
            addressTypeGrid.DataSource = dsAddressType.Tables[0];

            addressTypeGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteAddressType";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.addressTypeGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateAddressType";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.addressTypeGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearAddressTypeTextBoxes()
        {
            txtAddressTypeName.Clear();
        }

        private void deleteAddressType(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM address_type WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listAddressType(false);
        }

        private void addressTypeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (addressTypeGrid.Columns[e.ColumnIndex].Name == "DeleteAddressType" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int AddressTypeId = Convert.ToInt32(addressTypeGrid.CurrentRow.Cells["id"].Value);
                deleteAddressType(AddressTypeId);
            }

            if (addressTypeGrid.Columns[e.ColumnIndex].Name == "UpdateAddressType" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int AddressTypeId = Convert.ToInt32(addressTypeGrid.CurrentRow.Cells["id"].Value);
                string AddressTypeName = addressTypeGrid.CurrentRow.Cells["name"].Value.ToString();
                updateAddressType(AddressTypeId, AddressTypeName);
            }
        }
        #endregion

        #region Payment Type Functions
        private void btnListPaymentType_Click(object sender, EventArgs e)
        {
            listPaymentType(false);
        }

        private void btnAddPaymentType_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO payment_type (name) VALUES (@p1)", con);
            command.Parameters.AddWithValue("@p1", txtPaymentTypeName.Text);
            command.ExecuteNonQuery();
            con.Close();

            listPaymentType(false);
            clearPaymentTypeTextBoxes();
            MessageBox.Show("Ödeme Tipi Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updatePaymentType(int id, string name)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("CALL update_payment_type(:p_id,:p_name)", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listPaymentType(false);
            MessageBox.Show("Ödeme Tipi Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listPaymentType(bool addButton)
        {
            string query = "SELECT * FROM payment_type";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsPaymentType = new DataSet();
            dAdapter.Fill(dsPaymentType);
            paymentTypeGrid.DataSource = dsPaymentType.Tables[0];

            paymentTypeGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeletePaymentType";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.paymentTypeGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdatePaymentType";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.paymentTypeGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearPaymentTypeTextBoxes()
        {
            txtPaymentTypeName.Clear();
        }

        private void deletePaymentType(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM payment_type WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listPaymentType(false);
        }

        private void paymentTypeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (paymentTypeGrid.Columns[e.ColumnIndex].Name == "DeletePaymentType" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int PaymentTypeId = Convert.ToInt32(paymentTypeGrid.CurrentRow.Cells["id"].Value);
                deletePaymentType(PaymentTypeId);
            }

            if (paymentTypeGrid.Columns[e.ColumnIndex].Name == "UpdatePaymentType" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int PaymentTypeId = Convert.ToInt32(paymentTypeGrid.CurrentRow.Cells["id"].Value);
                string PaymentTypeName = paymentTypeGrid.CurrentRow.Cells["name"].Value.ToString();
                updatePaymentType(PaymentTypeId, PaymentTypeName);
            }
        }
        #endregion

        #region Catalog Functions
        private void btnListCatalog_Click(object sender, EventArgs e)
        {
            listCatalog(false);
        }

        private void btnAddCatalog_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO catalog (name) VALUES (@p1)", con);
            command.Parameters.AddWithValue("@p1", txtCatalogName.Text);
            command.ExecuteNonQuery();
            con.Close();

            listCatalog(false);
            clearCatalogTextBoxes();
            MessageBox.Show("Katalog Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateCatalog(int id, string name)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE catalog SET name = :p_name WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listCatalog(false);
            MessageBox.Show("Katalog Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listCatalog(bool addButton)
        {
            string query = "SELECT * FROM catalog";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCatalog = new DataSet();
            dAdapter.Fill(dsCatalog);
            catalogGrid.DataSource = dsCatalog.Tables[0];

            catalogGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteCatalog";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.catalogGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateCatalog";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.catalogGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearCatalogTextBoxes()
        {
            txtCatalogName.Clear();
        }

        private void deleteCatalog(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM catalog WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listCatalog(false);
        }

        private void catalogGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (catalogGrid.Columns[e.ColumnIndex].Name == "DeleteCatalog" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int catalogId = Convert.ToInt32(catalogGrid.CurrentRow.Cells["id"].Value);
                deleteCatalog(catalogId);
            }

            if (catalogGrid.Columns[e.ColumnIndex].Name == "UpdateCatalog" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int catalogId = Convert.ToInt32(catalogGrid.CurrentRow.Cells["id"].Value);
                string catalogName = catalogGrid.CurrentRow.Cells["name"].Value.ToString();
                updateCatalog(catalogId, catalogName);
            }
        }
        #endregion

        #region Shipping Type Functions
        private void btnListShippingType_Click(object sender, EventArgs e)
        {
            listShippingType(false);
        }

        private void btnAddShippingType_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO shipping_type (name) VALUES (@p1)", con);
            command.Parameters.AddWithValue("@p1", txtShippingTypeName.Text);
            command.ExecuteNonQuery();
            con.Close();

            listShippingType(false);
            clearShippingTypeTextBoxes();
            MessageBox.Show("Taşıma Tipi Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateShippingType(int id, string name)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE shipping_type SET name = :p_name WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listShippingType(false);
            MessageBox.Show("Taşıma Tipi Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listShippingType(bool addButton)
        {
            string query = "SELECT * FROM shipping_type";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsShippingType = new DataSet();
            dAdapter.Fill(dsShippingType);
            shippingTypeGrid.DataSource = dsShippingType.Tables[0];

            shippingTypeGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteShippingType";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.shippingTypeGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateShippingType";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.shippingTypeGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearShippingTypeTextBoxes()
        {
            txtShippingTypeName.Clear();
        }

        private void deleteShippingType(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM shipping_type WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listShippingType(false);
        }

        private void shippingTypeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (shippingTypeGrid.Columns[e.ColumnIndex].Name == "DeleteShippingType" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ShippingTypeId = Convert.ToInt32(shippingTypeGrid.CurrentRow.Cells["id"].Value);
                deleteShippingType(ShippingTypeId);
            }

            if (shippingTypeGrid.Columns[e.ColumnIndex].Name == "UpdateShippingType" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ShippingTypeId = Convert.ToInt32(shippingTypeGrid.CurrentRow.Cells["id"].Value);
                string ShippingTypeName = shippingTypeGrid.CurrentRow.Cells["name"].Value.ToString();
                updateShippingType(ShippingTypeId, ShippingTypeName);
            }
        }
        #endregion

        #region Manager Functions
        private void btnListManager_Click(object sender, EventArgs e)
        {
            listManager(false);
        }

        private void btnAddManager_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO manager (name, salary, birth_date, birth_place) VALUES (@p1, @p2, @p3, @p4)", con);
            command.Parameters.AddWithValue("@p1", txtManagerName.Text);
            command.Parameters.AddWithValue("@p2", Convert.ToDecimal(txtManagerSalary.Text));
            command.Parameters.AddWithValue("@p3", txtManagerBirthDate.Value);
            command.Parameters.AddWithValue("@p4", txtManagerBirthPlace.Text);
            command.ExecuteNonQuery();
            con.Close();

            listManager(false);
            clearManagerTextBoxes();
            MessageBox.Show("Yönetici Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateManager(int id, string name, decimal salary, DateTime birthDate, string birthPlace)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE manager SET name = :p_name, salary = :p_salary, birth_date = :p_birth_date, birth_place = :p_birth_place  WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_salary", salary);
            command.Parameters.AddWithValue("p_birth_date", birthDate);
            command.Parameters.AddWithValue("p_birth_place", birthPlace);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listManager(false);
            MessageBox.Show("Yönetici Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listManager(bool addButton)
        {
            string query = "SELECT * FROM manager";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsManager = new DataSet();
            dAdapter.Fill(dsManager);
            managerGrid.DataSource = dsManager.Tables[0];

            managerGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteManager";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.managerGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateManager";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.managerGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearManagerTextBoxes()
        {
            txtManagerName.Clear();
            txtManagerSalary.Clear();
            txtManagerBirthPlace.Clear();
            txtManagerBirthDate.Value = DateTime.Now;
        }

        private void deleteManager(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM manager WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listManager(false);
        }

        private void managerGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (managerGrid.Columns[e.ColumnIndex].Name == "DeleteManager" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int managerId = Convert.ToInt32(managerGrid.CurrentRow.Cells["id"].Value);
                deleteManager(managerId);
            }

            if (managerGrid.Columns[e.ColumnIndex].Name == "UpdateManager" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int managerId = Convert.ToInt32(managerGrid.CurrentRow.Cells["id"].Value);
                string managerName = managerGrid.CurrentRow.Cells["name"].Value.ToString();
                decimal managerSalary = Convert.ToDecimal(managerGrid.CurrentRow.Cells["salary"].Value.ToString());
                DateTime managerBirthDate = Convert.ToDateTime(managerGrid.CurrentRow.Cells["birth_date"].Value.ToString());
                string managerBirthPlace = managerGrid.CurrentRow.Cells["birth_place"].Value.ToString();
                updateManager(managerId, managerName, managerSalary, managerBirthDate, managerBirthPlace);
            }
        }
        #endregion

        #region Warehouse Functions
        private void btnListWarehouse_Click(object sender, EventArgs e)
        {
            listWarehouse(false);
        }

        private void btnAddWarehouse_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO warehouse (name, manager_id) VALUES (@p1, @p2)", con);
            command.Parameters.AddWithValue("@p1", txtWarehouseName.Text);
            command.Parameters.AddWithValue("@p2", Convert.ToInt32(drpManager.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listWarehouse(false);
            clearWarehouseTextBoxes();
            MessageBox.Show("Depo Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateWarehouse(int id, string name, int managerId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE warehouse SET name = :p_name, manager_id = :p_manager_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_manager_id", managerId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listWarehouse(false);
            MessageBox.Show("Depo Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listWarehouse(bool addButton)
        {
            string query = "SELECT * FROM warehouse";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsWarehouse = new DataSet();
            dAdapter.Fill(dsWarehouse);
            warehouseGrid.DataSource = dsWarehouse.Tables[0];

            query = "SELECT * FROM manager";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsManager = new DataSet();
            dAdapter.Fill(dsManager);

            drpManager.DataSource = dsManager.Tables[0];
            drpManager.ValueMember = "id";
            drpManager.DisplayMember = "name";

            if (addButton)
            {
                DataGridViewComboBoxColumn drpManagerGrd = new DataGridViewComboBoxColumn();
                {
                    drpManagerGrd.Name = "drpManagerGrd";
                    drpManagerGrd.HeaderText = "Yönetici";
                    drpManagerGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpManagerGrd.DataPropertyName = "manager_id";
                    drpManagerGrd.DataSource = dsManager.Tables[0];
                    drpManagerGrd.ValueMember = "id";
                    drpManagerGrd.DisplayMember = "name";

                    this.warehouseGrid.Columns.Add(drpManagerGrd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteWarehouse";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.warehouseGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateWarehouse";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.warehouseGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.warehouseGrid.Columns.Remove("drpManagerGrd");
                this.warehouseGrid.Columns.Remove("DeleteWarehouse");
                this.warehouseGrid.Columns.Remove("UpdateWarehouse");
                
                DataGridViewComboBoxColumn drpManagerGrd = new DataGridViewComboBoxColumn();
                {
                    drpManagerGrd.Name = "drpManagerGrd";
                    drpManagerGrd.HeaderText = "Yönetici";
                    drpManagerGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpManagerGrd.DataPropertyName = "manager_id";
                    drpManagerGrd.DataSource = dsManager.Tables[0];
                    drpManagerGrd.ValueMember = "id";
                    drpManagerGrd.DisplayMember = "name";

                    this.warehouseGrid.Columns.Add(drpManagerGrd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteWarehouse";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.warehouseGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateWarehouse";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.warehouseGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in warehouseGrid.Rows)
            {
                if(row.Cells["manager_id"].Value != null)
                {
                    int managerId = Convert.ToInt32(row.Cells["manager_id"].Value);
                    row.Cells["drpManagerGrd"].Value = managerId;
                }
            }

            warehouseGrid.Columns["id"].Visible = false;
            warehouseGrid.Columns["manager_id"].Visible = false;

        }

        private void clearWarehouseTextBoxes()
        {
            txtWarehouseName.Clear();
            drpManager.SelectedIndex = 0;
        }

        private void deleteWarehouse(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM warehouse WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listWarehouse(false);
        }

        private void warehouseGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (warehouseGrid.Columns[e.ColumnIndex].Name == "DeleteWarehouse" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int WarehouseId = Convert.ToInt32(warehouseGrid.CurrentRow.Cells["id"].Value);
                deleteWarehouse(WarehouseId);
            }

            if (warehouseGrid.Columns[e.ColumnIndex].Name == "UpdateWarehouse" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int WarehouseId = Convert.ToInt32(warehouseGrid.CurrentRow.Cells["id"].Value);
                string WarehouseName = warehouseGrid.CurrentRow.Cells["name"].Value.ToString();
                int ManagerId = Convert.ToInt32(warehouseGrid.CurrentRow.Cells["manager_id"].Value);
                updateWarehouse(WarehouseId, WarehouseName, ManagerId);
            }
        }
        #endregion

        #region Product Functions
        private void btnListProduct_Click(object sender, EventArgs e)
        {
            listProduct(false);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO product (catalog_id, name, price, color_id, warehouse_id, country_of_origin_id) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", con);
            command.Parameters.AddWithValue("@p1", Convert.ToInt32(drpProductCatalog.SelectedValue));
            command.Parameters.AddWithValue("@p2", txtProductName.Text);
            command.Parameters.AddWithValue("@p3", Convert.ToDouble(txtProductPrice.Text));
            command.Parameters.AddWithValue("@p4", Convert.ToInt32(drpProductColor.SelectedValue));
            command.Parameters.AddWithValue("@p5", Convert.ToInt32(drpProductWarehouse.SelectedValue));
            command.Parameters.AddWithValue("@p6", Convert.ToInt32(drpProductCountryOfOrigin.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listProduct(false);
            clearProductTextBoxes();
            MessageBox.Show("Ürün Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateProduct(int id, int catalogId, string name, double price, int colorId, int warehouseId, int countryOfOriginId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE product SET catalog_id = :p_catalog_id, name = :p_name, price = :p_price, color_id = :p_color_id, warehouse_id = :p_warehouse_id, country_of_origin_id = :p_country_of_origin_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_catalog_id", catalogId);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_price", price);
            command.Parameters.AddWithValue("p_color_id", colorId);
            command.Parameters.AddWithValue("p_warehouse_id", warehouseId);
            command.Parameters.AddWithValue("p_country_of_origin_id", countryOfOriginId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listProduct(false);
            MessageBox.Show("Ürün Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listProduct(bool addButton)
        {
            string query = "SELECT * FROM product";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsProduct = new DataSet();
            dAdapter.Fill(dsProduct);
            productGrid.DataSource = dsProduct.Tables[0];

            query = "SELECT * FROM catalog";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCatalog = new DataSet();
            dAdapter.Fill(dsCatalog);
            drpProductCatalog.DataSource = dsCatalog.Tables[0];
            drpProductCatalog.ValueMember = "id";
            drpProductCatalog.DisplayMember = "name";

            query = "SELECT * FROM color";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsColor = new DataSet();
            dAdapter.Fill(dsColor);
            drpProductColor.DataSource = dsColor.Tables[0];
            drpProductColor.ValueMember = "id";
            drpProductColor.DisplayMember = "name";

            query = "SELECT * FROM warehouse";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsWarehouse = new DataSet();
            dAdapter.Fill(dsWarehouse);
            drpProductWarehouse.DataSource = dsWarehouse.Tables[0];
            drpProductWarehouse.ValueMember = "id";
            drpProductWarehouse.DisplayMember = "name";

            query = "SELECT * FROM country";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCountryOfOrigin = new DataSet();
            dAdapter.Fill(dsCountryOfOrigin);
            drpProductCountryOfOrigin.DataSource = dsCountryOfOrigin.Tables[0];
            drpProductCountryOfOrigin.ValueMember = "id";
            drpProductCountryOfOrigin.DisplayMember = "name";

            if (addButton)
            {
                DataGridViewComboBoxColumn drpCatalogGrd = new DataGridViewComboBoxColumn();
                {
                    drpCatalogGrd.Name = "drpCatalogGrd";
                    drpCatalogGrd.HeaderText = "Katalog";
                    drpCatalogGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCatalogGrd.DataPropertyName = "catalog_id";
                    drpCatalogGrd.DataSource = dsCatalog.Tables[0];
                    drpCatalogGrd.ValueMember = "id";
                    drpCatalogGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpCatalogGrd);
                }

                DataGridViewComboBoxColumn drpColorGrd = new DataGridViewComboBoxColumn();
                {
                    drpColorGrd.Name = "drpColorGrd";
                    drpColorGrd.HeaderText = "Renk";
                    drpColorGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpColorGrd.DataPropertyName = "color_id";
                    drpColorGrd.DataSource = dsColor.Tables[0];
                    drpColorGrd.ValueMember = "id";
                    drpColorGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpColorGrd);
                }

                DataGridViewComboBoxColumn drpWarehouseGrd = new DataGridViewComboBoxColumn();
                {
                    drpWarehouseGrd.Name = "drpWarehouseGrd";
                    drpWarehouseGrd.HeaderText = "Depo";
                    drpWarehouseGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpWarehouseGrd.DataPropertyName = "warehouse_id";
                    drpWarehouseGrd.DataSource = dsWarehouse.Tables[0];
                    drpWarehouseGrd.ValueMember = "id";
                    drpWarehouseGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpWarehouseGrd);
                }

                DataGridViewComboBoxColumn drpCountryOfOriginGrd = new DataGridViewComboBoxColumn();
                {
                    drpCountryOfOriginGrd.Name = "drpCountryOfOriginGrd";
                    drpCountryOfOriginGrd.HeaderText = "Menşei Ülke";
                    drpCountryOfOriginGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCountryOfOriginGrd.DataPropertyName = "country_of_origin_id";
                    drpCountryOfOriginGrd.DataSource = dsCountryOfOrigin.Tables[0];
                    drpCountryOfOriginGrd.ValueMember = "id";
                    drpCountryOfOriginGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpCountryOfOriginGrd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteProduct";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.productGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateProduct";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.productGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.productGrid.Columns.Remove("drpCatalogGrd");
                this.productGrid.Columns.Remove("drpColorGrd");
                this.productGrid.Columns.Remove("drpWarehouseGrd");
                this.productGrid.Columns.Remove("drpCountryOfOriginGrd");
                this.productGrid.Columns.Remove("DeleteProduct");
                this.productGrid.Columns.Remove("UpdateProduct");

                DataGridViewComboBoxColumn drpCatalogGrd = new DataGridViewComboBoxColumn();
                {
                    drpCatalogGrd.Name = "drpCatalogGrd";
                    drpCatalogGrd.HeaderText = "Katalog";
                    drpCatalogGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCatalogGrd.DataPropertyName = "catalog_id";
                    drpCatalogGrd.DataSource = dsCatalog.Tables[0];
                    drpCatalogGrd.ValueMember = "id";
                    drpCatalogGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpCatalogGrd);
                }

                DataGridViewComboBoxColumn drpColorGrd = new DataGridViewComboBoxColumn();
                {
                    drpColorGrd.Name = "drpColorGrd";
                    drpColorGrd.HeaderText = "Renk";
                    drpColorGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpColorGrd.DataPropertyName = "color_id";
                    drpColorGrd.DataSource = dsColor.Tables[0];
                    drpColorGrd.ValueMember = "id";
                    drpColorGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpColorGrd);
                }

                DataGridViewComboBoxColumn drpWarehouseGrd = new DataGridViewComboBoxColumn();
                {
                    drpWarehouseGrd.Name = "drpWarehouseGrd";
                    drpWarehouseGrd.HeaderText = "Depo";
                    drpWarehouseGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpWarehouseGrd.DataPropertyName = "warehouse_id";
                    drpWarehouseGrd.DataSource = dsWarehouse.Tables[0];
                    drpWarehouseGrd.ValueMember = "id";
                    drpWarehouseGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpWarehouseGrd);
                }

                DataGridViewComboBoxColumn drpCountryOfOriginGrd = new DataGridViewComboBoxColumn();
                {
                    drpCountryOfOriginGrd.Name = "drpCountryOfOriginGrd";
                    drpCountryOfOriginGrd.HeaderText = "Menşei Ülke";
                    drpCountryOfOriginGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCountryOfOriginGrd.DataPropertyName = "country_of_origin_id";
                    drpCountryOfOriginGrd.DataSource = dsCountryOfOrigin.Tables[0];
                    drpCountryOfOriginGrd.ValueMember = "id";
                    drpCountryOfOriginGrd.DisplayMember = "name";

                    this.productGrid.Columns.Add(drpCountryOfOriginGrd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteProduct";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.productGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateProduct";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.productGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in productGrid.Rows)
            {
                if (row.Cells["catalog_id"].Value != null)
                {
                    int CatalogId = Convert.ToInt32(row.Cells["catalog_id"].Value);
                    row.Cells["drpCatalogGrd"].Value = CatalogId;
                }

                if (row.Cells["color_id"].Value != null)
                {
                    int colorId = Convert.ToInt32(row.Cells["color_id"].Value);
                    row.Cells["drpColorGrd"].Value = colorId;
                }

                if (row.Cells["warehouse_id"].Value != null)
                {
                    int WarehouseId = Convert.ToInt32(row.Cells["warehouse_id"].Value);
                    row.Cells["drpWarehouseGrd"].Value = WarehouseId;
                }

                if (row.Cells["country_of_origin_id"].Value != null)
                {
                    int CountryOfOriginId = Convert.ToInt32(row.Cells["country_of_origin_id"].Value);
                    row.Cells["drpCountryOfOriginGrd"].Value = CountryOfOriginId;
                }
            }

            productGrid.Columns["id"].Visible = false;
            productGrid.Columns["catalog_id"].Visible = false;
            productGrid.Columns["color_id"].Visible = false;
            productGrid.Columns["warehouse_id"].Visible = false;
            productGrid.Columns["country_of_origin_id"].Visible = false;

        }

        private void clearProductTextBoxes()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            drpProductCatalog.SelectedIndex = 0;
            drpProductColor.SelectedIndex = 0;
            drpProductWarehouse.SelectedIndex = 0;
            drpProductCountryOfOrigin.SelectedIndex = 0;
        }

        private void deleteProduct(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM product WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listProduct(false);
        }

        private void productGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productGrid.Columns[e.ColumnIndex].Name == "DeleteProduct" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ProductId = Convert.ToInt32(productGrid.CurrentRow.Cells["id"].Value);
                deleteProduct(ProductId);
            }

            if (productGrid.Columns[e.ColumnIndex].Name == "UpdateProduct" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ProductId = Convert.ToInt32(productGrid.CurrentRow.Cells["id"].Value);
                string ProductName = productGrid.CurrentRow.Cells["name"].Value.ToString();
                double ProductPrice = Convert.ToDouble(productGrid.CurrentRow.Cells["price"].Value.ToString());
                int CatalogId = Convert.ToInt32(productGrid.CurrentRow.Cells["catalog_id"].Value);
                int ColorId = Convert.ToInt32(productGrid.CurrentRow.Cells["color_id"].Value);
                int WarehouseId = Convert.ToInt32(productGrid.CurrentRow.Cells["warehouse_id"].Value);
                int CountryOfOriginId = Convert.ToInt32(productGrid.CurrentRow.Cells["country_of_origin_id"].Value);
                updateProduct(ProductId, CatalogId, ProductName, ProductPrice, ColorId, WarehouseId, CountryOfOriginId);
            }
        }
        #endregion

        #region TransportFirm Functions
        private void btnListTransportFirm_Click(object sender, EventArgs e)
        {
            listTransportFirm(false);
        }

        private void btnAddTransportFirm_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO transport_firm (name, shipping_type_id) VALUES (@p1, @p2)", con);
            command.Parameters.AddWithValue("@p1", txtTransportFirmName.Text);
            command.Parameters.AddWithValue("@p2", Convert.ToInt32(drpTransportFirmShippingType.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listTransportFirm(false);
            clearTransportFirmTextBoxes();
            MessageBox.Show("Taşıma Firması Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateTransportFirm(int id, string name, int shippingTypeId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE transport_firm SET name = :p_name, shipping_type_id = :p_shipping_type_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_shipping_type_id", shippingTypeId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listTransportFirm(false);
            MessageBox.Show("Taşıma Tipi Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listTransportFirm(bool addButton)
        {
            string query = "SELECT * FROM transport_firm";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsTransportFirm = new DataSet();
            dAdapter.Fill(dsTransportFirm);
            transportFirmGrid.DataSource = dsTransportFirm.Tables[0];

            query = "SELECT * FROM shipping_type";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsShippingType = new DataSet();
            dAdapter.Fill(dsShippingType);

            drpTransportFirmShippingType.DataSource = dsShippingType.Tables[0];
            drpTransportFirmShippingType.ValueMember = "id";
            drpTransportFirmShippingType.DisplayMember = "name";
            

            if (addButton)
            {
                DataGridViewComboBoxColumn drpShippingTypeGrd = new DataGridViewComboBoxColumn();
                {
                    drpShippingTypeGrd.Name = "drpShippingTypeGrd";
                    drpShippingTypeGrd.HeaderText = "Taşıma Tipi";
                    drpShippingTypeGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpShippingTypeGrd.DataPropertyName = "shipping_type_id";
                    drpShippingTypeGrd.DataSource = dsShippingType.Tables[0];
                    drpShippingTypeGrd.ValueMember = "id";
                    drpShippingTypeGrd.DisplayMember = "name";

                    this.transportFirmGrid.Columns.Add(drpShippingTypeGrd);

                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteTransportFirm";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.transportFirmGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateTransportFirm";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.transportFirmGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.transportFirmGrid.Columns.Remove("drpShippingTypeGrd");
                this.transportFirmGrid.Columns.Remove("DeleteTransportFirm");
                this.transportFirmGrid.Columns.Remove("UpdateTransportFirm");

                DataGridViewComboBoxColumn drpShippingTypeGrd = new DataGridViewComboBoxColumn();
                {
                    drpShippingTypeGrd.Name = "drpShippingTypeGrd";
                    drpShippingTypeGrd.HeaderText = "Taşıma Tipi";
                    drpShippingTypeGrd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpShippingTypeGrd.DataPropertyName = "shipping_type_id";
                    drpShippingTypeGrd.DataSource = dsShippingType.Tables[0];
                    drpShippingTypeGrd.ValueMember = "id";
                    drpShippingTypeGrd.DisplayMember = "name";

                    this.transportFirmGrid.Columns.Add(drpShippingTypeGrd);

                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteTransportFirm";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.transportFirmGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateTransportFirm";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.transportFirmGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in transportFirmGrid.Rows)
            {
                if (row.Cells["shipping_type_id"].Value != null)
                {
                    int shippingTypeId = Convert.ToInt32(row.Cells["shipping_type_id"].Value);
                    row.Cells["drpShippingTypeGrd"].Value = shippingTypeId;
                }
            }

            transportFirmGrid.Columns["id"].Visible = false;
            transportFirmGrid.Columns["shipping_type_id"].Visible = false;

        }

        private void clearTransportFirmTextBoxes()
        {
            txtTransportFirmName.Clear();
            drpTransportFirmShippingType.SelectedIndex = 0;
        }

        private void deleteTransportFirm(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM transport_firm WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listTransportFirm(false);
        }

        private void transportFirmGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (transportFirmGrid.Columns[e.ColumnIndex].Name == "DeleteTransportFirm" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int TransportFirmId = Convert.ToInt32(transportFirmGrid.CurrentRow.Cells["id"].Value);
                deleteTransportFirm(TransportFirmId);
            }

            if (transportFirmGrid.Columns[e.ColumnIndex].Name == "UpdateTransportFirm" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int TransportFirmId = Convert.ToInt32(transportFirmGrid.CurrentRow.Cells["id"].Value);
                string TransportFirmName = transportFirmGrid.CurrentRow.Cells["name"].Value.ToString();
                int shippingTypeId = Convert.ToInt32(transportFirmGrid.CurrentRow.Cells["shipping_type_id"].Value);
                updateTransportFirm(TransportFirmId, TransportFirmName, shippingTypeId);
            }
        }
        #endregion

        #region Customer Functions
        private void btnListCustomer_Click(object sender, EventArgs e)
        {
            listCustomer(false);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO customer (name, birth_place) VALUES (@p1, @p2)", con);
            command.Parameters.AddWithValue("@p1", txtCustomerName.Text);
            command.Parameters.AddWithValue("@p2", txtCustomerBirthPlace.Text);
            command.ExecuteNonQuery();
            con.Close();

            listCustomer(false);
            clearCustomerTextBoxes();
            MessageBox.Show("Kullanıcı Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateCustomer(int id, string name, string birthPlace)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE customer SET name = :p_name, birth_place = :p_birth_place  WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_birth_place", birthPlace);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listCustomer(false);
            MessageBox.Show("Kullanıcı Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listCustomer(bool addButton)
        {
            string query = "SELECT * FROM customer";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCustomer = new DataSet();
            dAdapter.Fill(dsCustomer);
            CustomerGrid.DataSource = dsCustomer.Tables[0];

            CustomerGrid.Columns["id"].Visible = false;
            if (addButton)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteCustomer";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.CustomerGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateCustomer";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.CustomerGrid.Columns.Add(updateButton);
                }
            }

        }

        private void clearCustomerTextBoxes()
        {
            txtCustomerName.Clear();
            txtCustomerBirthPlace.Clear();
        }

        private void deleteCustomer(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM customer WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listCustomer(false);
        }

        private void CustomerGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CustomerGrid.Columns[e.ColumnIndex].Name == "DeleteCustomer" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int CustomerId = Convert.ToInt32(CustomerGrid.CurrentRow.Cells["id"].Value);
                deleteCustomer(CustomerId);
            }

            if (CustomerGrid.Columns[e.ColumnIndex].Name == "UpdateCustomer" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int CustomerId = Convert.ToInt32(CustomerGrid.CurrentRow.Cells["id"].Value);
                string CustomerName = CustomerGrid.CurrentRow.Cells["name"].Value.ToString();
                string CustomerBirthPlace = CustomerGrid.CurrentRow.Cells["birth_place"].Value.ToString();
                updateCustomer(CustomerId, CustomerName, CustomerBirthPlace);
            }
        }
        #endregion

        #region Address Functions
        private void btnListAddress_Click(object sender, EventArgs e)
        {
            listAddress(false);
        }

        private void btnAddAddress_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO address (full_address, address_type_id, customer_id) VALUES (@p1, @p2, @p3)", con);
            command.Parameters.AddWithValue("@p1", txtFullAddress.Text);
            command.Parameters.AddWithValue("@p2", Convert.ToInt32(drpAddressType.SelectedValue));
            command.Parameters.AddWithValue("@p3", Convert.ToInt32(drpAddressCustomer.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listAddress(false);
            clearAddressTextBoxes();
            MessageBox.Show("Adres Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateAddress(int id, string fullAddress, int AddressTypeId, int CustomerId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE address SET full_address = :p_full_address, address_type_id = :p_address_type_id, customer_id = :p_customer_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_full_address", fullAddress);
            command.Parameters.AddWithValue("p_address_type_id", AddressTypeId);
            command.Parameters.AddWithValue("p_customer_id", CustomerId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listAddress(false);
            MessageBox.Show("Adres Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listAddress(bool addButton)
        {
            string query = "SELECT * FROM address";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsAddress = new DataSet();
            dAdapter.Fill(dsAddress);
            AddressGrid.DataSource = dsAddress.Tables[0];

            query = "SELECT * FROM address_type";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsAddressType = new DataSet();
            dAdapter.Fill(dsAddressType);
            drpAddressType.DataSource = dsAddressType.Tables[0];
            drpAddressType.ValueMember = "id";
            drpAddressType.DisplayMember = "name";

            query = "SELECT * FROM customer";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCustomer = new DataSet();
            dAdapter.Fill(dsCustomer);
            drpAddressCustomer.DataSource = dsCustomer.Tables[0];
            drpAddressCustomer.ValueMember = "id";
            drpAddressCustomer.DisplayMember = "name";

            if (addButton)
            {
                DataGridViewComboBoxColumn drpAddressType1Grd = new DataGridViewComboBoxColumn();
                {
                    drpAddressType1Grd.Name = "drpAddressAddressTypeGrd";
                    drpAddressType1Grd.HeaderText = "Adres Tipi";
                    drpAddressType1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpAddressType1Grd.DataPropertyName = "address_type_id";
                    drpAddressType1Grd.DataSource = dsAddressType.Tables[0];
                    drpAddressType1Grd.ValueMember = "id";
                    drpAddressType1Grd.DisplayMember = "name";

                    this.AddressGrid.Columns.Add(drpAddressType1Grd);
                }

                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpAddressCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.AddressGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteAddress";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.AddressGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateAddress";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.AddressGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.AddressGrid.Columns.Remove("drpAddressAddressTypeGrd");
                this.AddressGrid.Columns.Remove("drpAddressCustomerGrd");
                this.AddressGrid.Columns.Remove("DeleteAddress");
                this.AddressGrid.Columns.Remove("UpdateAddress");

                DataGridViewComboBoxColumn drpAddressType1Grd = new DataGridViewComboBoxColumn();
                {
                    drpAddressType1Grd.Name = "drpAddressAddressTypeGrd";
                    drpAddressType1Grd.HeaderText = "Adres Tipi";
                    drpAddressType1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpAddressType1Grd.DataPropertyName = "address_type_id";
                    drpAddressType1Grd.DataSource = dsAddressType.Tables[0];
                    drpAddressType1Grd.ValueMember = "id";
                    drpAddressType1Grd.DisplayMember = "name";

                    this.AddressGrid.Columns.Add(drpAddressType1Grd);
                }

                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpAddressCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.AddressGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteAddress";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.AddressGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateAddress";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.AddressGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in AddressGrid.Rows)
            {
                if (row.Cells["address_type_id"].Value != null)
                {
                    int AddressTypeId = Convert.ToInt32(row.Cells["address_type_id"].Value);
                    row.Cells["drpAddressAddressTypeGrd"].Value = AddressTypeId;
                }

                if (row.Cells["customer_id"].Value != null)
                {
                    int AddressTypeId = Convert.ToInt32(row.Cells["customer_id"].Value);
                    row.Cells["drpAddressCustomerGrd"].Value = AddressTypeId;
                }
            }

            AddressGrid.Columns["id"].Visible = false;
            AddressGrid.Columns["address_type_id"].Visible = false;
            AddressGrid.Columns["customer_id"].Visible = false;

        }

        private void clearAddressTextBoxes()
        {
            txtFullAddress.Clear();
            drpAddressType.SelectedIndex = 0;
            drpAddressCustomer.SelectedIndex = 0;
        }

        private void deleteAddress(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM address WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listAddress(false);
        }

        private void AddressGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (AddressGrid.Columns[e.ColumnIndex].Name == "DeleteAddress" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int AddressId = Convert.ToInt32(AddressGrid.CurrentRow.Cells["id"].Value);
                deleteAddress(AddressId);
            }

            if (AddressGrid.Columns[e.ColumnIndex].Name == "UpdateAddress" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int AddressId = Convert.ToInt32(AddressGrid.CurrentRow.Cells["id"].Value);
                string FullAddresss = AddressGrid.CurrentRow.Cells["full_address"].Value.ToString();
                int AddressTypeId = Convert.ToInt32(AddressGrid.CurrentRow.Cells["address_type_id"].Value);
                int CustomerId = Convert.ToInt32(AddressGrid.CurrentRow.Cells["customer_id"].Value);
                updateAddress(AddressId, FullAddresss, AddressTypeId, CustomerId);
            }
        }
        #endregion

        #region Payment Functions
        private void btnListPayment_Click(object sender, EventArgs e)
        {
            listPayment(false);
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO payment (name, payment_type_id, customer_id) VALUES (@p1, @p2, @p3)", con);
            command.Parameters.AddWithValue("@p1", txtPaymentName.Text);
            command.Parameters.AddWithValue("@p2", Convert.ToInt32(drpPaymentMethodPaymentType.SelectedValue));
            command.Parameters.AddWithValue("@p3", Convert.ToInt32(drpPaymentCustomer.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listPayment(false);
            clearPaymentTextBoxes();
            MessageBox.Show("Ödeme Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updatePayment(int id, string paymentName, int PaymentTypeId, int CustomerId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE payment SET name = :p_payment_name, payment_type_id = :p_payment_type_id, customer_id = :p_customer_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_payment_name", paymentName);
            command.Parameters.AddWithValue("p_payment_type_id", PaymentTypeId);
            command.Parameters.AddWithValue("p_customer_id", CustomerId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listPayment(false);
            MessageBox.Show("Ödeme Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listPayment(bool addButton)
        {
            string query = "SELECT * FROM payment";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsPayment = new DataSet();
            dAdapter.Fill(dsPayment);
            paymentGrid.DataSource = dsPayment.Tables[0];

            query = "SELECT * FROM payment_type";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsPaymentType = new DataSet();
            dAdapter.Fill(dsPaymentType);
            drpPaymentMethodPaymentType.DataSource = dsPaymentType.Tables[0];
            drpPaymentMethodPaymentType.ValueMember = "id";
            drpPaymentMethodPaymentType.DisplayMember = "name";

            query = "SELECT * FROM customer";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCustomer = new DataSet();
            dAdapter.Fill(dsCustomer);
            drpPaymentCustomer.DataSource = dsCustomer.Tables[0];
            drpPaymentCustomer.ValueMember = "id";
            drpPaymentCustomer.DisplayMember = "name";

            if (addButton)
            {
                DataGridViewComboBoxColumn drpPaymentType1Grd = new DataGridViewComboBoxColumn();
                {
                    drpPaymentType1Grd.Name = "drpPaymentPaymentTypeGrd";
                    drpPaymentType1Grd.HeaderText = "Ödeme Tipi";
                    drpPaymentType1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpPaymentType1Grd.DataPropertyName = "payment_type_id";
                    drpPaymentType1Grd.DataSource = dsPaymentType.Tables[0];
                    drpPaymentType1Grd.ValueMember = "id";
                    drpPaymentType1Grd.DisplayMember = "name";

                    this.paymentGrid.Columns.Add(drpPaymentType1Grd);
                }

                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpPaymentCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.paymentGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeletePayment";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.paymentGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdatePayment";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.paymentGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.paymentGrid.Columns.Remove("drpPaymentPaymentTypeGrd");
                this.paymentGrid.Columns.Remove("drpPaymentCustomerGrd");
                this.paymentGrid.Columns.Remove("DeletePayment");
                this.paymentGrid.Columns.Remove("UpdatePayment");

                DataGridViewComboBoxColumn drpPaymentType1Grd = new DataGridViewComboBoxColumn();
                {
                    drpPaymentType1Grd.Name = "drpPaymentPaymentTypeGrd";
                    drpPaymentType1Grd.HeaderText = "Ödeme Tipi";
                    drpPaymentType1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpPaymentType1Grd.DataPropertyName = "payment_type_id";
                    drpPaymentType1Grd.DataSource = dsPaymentType.Tables[0];
                    drpPaymentType1Grd.ValueMember = "id";
                    drpPaymentType1Grd.DisplayMember = "name";

                    this.paymentGrid.Columns.Add(drpPaymentType1Grd);
                }

                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpPaymentCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.paymentGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeletePayment";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.paymentGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdatePayment";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.paymentGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in paymentGrid.Rows)
            {
                if (row.Cells["payment_type_id"].Value != null)
                {
                    int PaymentTypeId = Convert.ToInt32(row.Cells["payment_type_id"].Value);
                    row.Cells["drpPaymentPaymentTypeGrd"].Value = PaymentTypeId;
                }

                if (row.Cells["customer_id"].Value != null)
                {
                    int PaymentTypeId = Convert.ToInt32(row.Cells["customer_id"].Value);
                    row.Cells["drpPaymentCustomerGrd"].Value = PaymentTypeId;
                }
            }

            paymentGrid.Columns["id"].Visible = false;
            paymentGrid.Columns["payment_type_id"].Visible = false;
            paymentGrid.Columns["customer_id"].Visible = false;

        }

        private void clearPaymentTextBoxes()
        {
            txtPaymentName.Clear();
            drpPaymentMethodPaymentType.SelectedIndex = 0;
            drpPaymentCustomer.SelectedIndex = 0;
        }

        private void deletePayment(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM payment WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listPayment(false);
        }

        private void paymentGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (paymentGrid.Columns[e.ColumnIndex].Name == "DeletePayment" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int PaymentId = Convert.ToInt32(paymentGrid.CurrentRow.Cells["id"].Value);
                deletePayment(PaymentId);
            }

            if (paymentGrid.Columns[e.ColumnIndex].Name == "UpdatePayment" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int PaymentId = Convert.ToInt32(paymentGrid.CurrentRow.Cells["id"].Value);
                string paymentName = paymentGrid.CurrentRow.Cells["name"].Value.ToString();
                int PaymentTypeId = Convert.ToInt32(paymentGrid.CurrentRow.Cells["payment_type_id"].Value);
                int CustomerId = Convert.ToInt32(paymentGrid.CurrentRow.Cells["customer_id"].Value);
                updatePayment(PaymentId, paymentName, PaymentTypeId, CustomerId);
            }
        }
        #endregion

        #region Order Functions
        private void btnListOrder_Click(object sender, EventArgs e)
        {
            listOrder(false);
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"order\" (name, order_date, customer_id, payment_id, transport_firm_id, product_id) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", con);
            command.Parameters.AddWithValue("@p1", txtOrderName.Text);
            command.Parameters.AddWithValue("@p2", txtOrderDate.Value);
            command.Parameters.AddWithValue("@p3", Convert.ToInt32(drpOrderCustomer.SelectedValue));
            command.Parameters.AddWithValue("@p4", Convert.ToInt32(drpOrderPaymentType.SelectedValue));
            command.Parameters.AddWithValue("@p5", Convert.ToInt32(drpOrderTransportFirm.SelectedValue));
            command.Parameters.AddWithValue("@p6", Convert.ToInt32(drpOrderProduct.SelectedValue));
            command.ExecuteNonQuery();
            con.Close();

            listOrder(false);
            clearOrderTextBoxes();
            MessageBox.Show("Sipariş Ekleme İşlemi Başarıyla Tamamlandı");

        }

        private void updateOrder(int id, string name, DateTime orderDate, int customerId, int paymentId, int transportFirmId, int productId)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE \"order\" SET name = :p_name, order_date = :p_order_date, customer_id = :p_customer_id, payment_id = :p_payment_id, transport_firm_id = :p_transport_firm_id, product_id = :p_product_id WHERE id = :p_id", con);
            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_name", name);
            command.Parameters.AddWithValue("p_order_date", orderDate);
            command.Parameters.AddWithValue("p_customer_id", customerId);
            command.Parameters.AddWithValue("p_payment_id", paymentId);
            command.Parameters.AddWithValue("p_transport_firm_id", transportFirmId);
            command.Parameters.AddWithValue("p_product_id", productId);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            con.Close();

            listOrder(false);
            MessageBox.Show("Sipariş Güncelleme İşlemi Başarıyla Tamamlandı");

        }

        private void listOrder(bool addButton)
        {
            string query = "SELECT * FROM \"order\"";
            NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsOrder = new DataSet();
            dAdapter.Fill(dsOrder);
            OrderGrid.DataSource = dsOrder.Tables[0];

            query = "SELECT * FROM customer";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsCustomer = new DataSet();
            dAdapter.Fill(dsCustomer);
            drpOrderCustomer.DataSource = dsCustomer.Tables[0];
            drpOrderCustomer.ValueMember = "id";
            drpOrderCustomer.DisplayMember = "name";

            query = "SELECT * FROM payment";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsPayment = new DataSet();
            dAdapter.Fill(dsPayment);
            drpOrderPaymentType.DataSource = dsPayment.Tables[0];
            drpOrderPaymentType.ValueMember = "id";
            drpOrderPaymentType.DisplayMember = "name";

            query = "SELECT * FROM transport_firm";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsTransportFirm = new DataSet();
            dAdapter.Fill(dsTransportFirm);
            drpOrderTransportFirm.DataSource = dsTransportFirm.Tables[0];
            drpOrderTransportFirm.ValueMember = "id";
            drpOrderTransportFirm.DisplayMember = "name";

            query = "SELECT * FROM product";
            dAdapter = new NpgsqlDataAdapter(query, con);
            DataSet dsProduct = new DataSet();
            dAdapter.Fill(dsProduct);
            drpOrderProduct.DataSource = dsProduct.Tables[0];
            drpOrderProduct.ValueMember = "id";
            drpOrderProduct.DisplayMember = "name";

            if (addButton)
            {
                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewComboBoxColumn drpPayment1Grd = new DataGridViewComboBoxColumn();
                {
                    drpPayment1Grd.Name = "drpPaymentGrd";
                    drpPayment1Grd.HeaderText = "Ödeme";
                    drpPayment1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpPayment1Grd.DataPropertyName = "payment_id";
                    drpPayment1Grd.DataSource = dsPayment.Tables[0];
                    drpPayment1Grd.ValueMember = "id";
                    drpPayment1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpPayment1Grd);
                }

                DataGridViewComboBoxColumn drpTransportFirm1Grd = new DataGridViewComboBoxColumn();
                {
                    drpTransportFirm1Grd.Name = "drpTransportFirmGrd";
                    drpTransportFirm1Grd.HeaderText = "Taşıma Firması";
                    drpTransportFirm1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpTransportFirm1Grd.DataPropertyName = "TransportFirm_id";
                    drpTransportFirm1Grd.DataSource = dsTransportFirm.Tables[0];
                    drpTransportFirm1Grd.ValueMember = "id";
                    drpTransportFirm1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpTransportFirm1Grd);
                }

                DataGridViewComboBoxColumn drpProduct1Grd = new DataGridViewComboBoxColumn();
                {
                    drpProduct1Grd.Name = "drpProductGrd";
                    drpProduct1Grd.HeaderText = "Ürün";
                    drpProduct1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpProduct1Grd.DataPropertyName = "product_id";
                    drpProduct1Grd.DataSource = dsProduct.Tables[0];
                    drpProduct1Grd.ValueMember = "id";
                    drpProduct1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpProduct1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteOrder";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.OrderGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateOrder";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.OrderGrid.Columns.Add(updateButton);
                }

            }
            else
            {
                this.OrderGrid.Columns.Remove("drpCustomerGrd");
                this.OrderGrid.Columns.Remove("drpPaymentGrd");
                this.OrderGrid.Columns.Remove("drpTransportFirmGrd");
                this.OrderGrid.Columns.Remove("drpProductGrd");
                this.OrderGrid.Columns.Remove("DeleteOrder");
                this.OrderGrid.Columns.Remove("UpdateOrder");

                DataGridViewComboBoxColumn drpCustomer1Grd = new DataGridViewComboBoxColumn();
                {
                    drpCustomer1Grd.Name = "drpCustomerGrd";
                    drpCustomer1Grd.HeaderText = "Müşteri";
                    drpCustomer1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpCustomer1Grd.DataPropertyName = "customer_id";
                    drpCustomer1Grd.DataSource = dsCustomer.Tables[0];
                    drpCustomer1Grd.ValueMember = "id";
                    drpCustomer1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpCustomer1Grd);
                }

                DataGridViewComboBoxColumn drpPayment1Grd = new DataGridViewComboBoxColumn();
                {
                    drpPayment1Grd.Name = "drpPaymentGrd";
                    drpPayment1Grd.HeaderText = "Ödeme";
                    drpPayment1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpPayment1Grd.DataPropertyName = "payment_id";
                    drpPayment1Grd.DataSource = dsPayment.Tables[0];
                    drpPayment1Grd.ValueMember = "id";
                    drpPayment1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpPayment1Grd);
                }

                DataGridViewComboBoxColumn drpTransportFirm1Grd = new DataGridViewComboBoxColumn();
                {
                    drpTransportFirm1Grd.Name = "drpTransportFirmGrd";
                    drpTransportFirm1Grd.HeaderText = "Taşıma Firması";
                    drpTransportFirm1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpTransportFirm1Grd.DataPropertyName = "TransportFirm_id";
                    drpTransportFirm1Grd.DataSource = dsTransportFirm.Tables[0];
                    drpTransportFirm1Grd.ValueMember = "id";
                    drpTransportFirm1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpTransportFirm1Grd);
                }

                DataGridViewComboBoxColumn drpProduct1Grd = new DataGridViewComboBoxColumn();
                {
                    drpProduct1Grd.Name = "drpProductGrd";
                    drpProduct1Grd.HeaderText = "Ürün";
                    drpProduct1Grd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    drpProduct1Grd.DataPropertyName = "product_id";
                    drpProduct1Grd.DataSource = dsProduct.Tables[0];
                    drpProduct1Grd.ValueMember = "id";
                    drpProduct1Grd.DisplayMember = "name";

                    this.OrderGrid.Columns.Add(drpProduct1Grd);
                }

                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                {
                    deleteButton.Name = "DeleteOrder";
                    deleteButton.HeaderText = "Sil";
                    deleteButton.Text = "Sil";
                    deleteButton.UseColumnTextForButtonValue = true;
                    this.OrderGrid.Columns.Add(deleteButton);
                }

                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                {
                    updateButton.Name = "UpdateOrder";
                    updateButton.HeaderText = "Güncelle";
                    updateButton.Text = "Güncelle";
                    updateButton.UseColumnTextForButtonValue = true;
                    this.OrderGrid.Columns.Add(updateButton);
                }
            }

            foreach (DataGridViewRow row in OrderGrid.Rows)
            {
                if (row.Cells["customer_id"].Value != null)
                {
                    int CustomerId = Convert.ToInt32(row.Cells["customer_id"].Value);
                    row.Cells["drpCustomerGrd"].Value = CustomerId;
                }

                if (row.Cells["payment_id"].Value != null)
                {
                    int paymentId = Convert.ToInt32(row.Cells["payment_id"].Value);
                    row.Cells["drpPaymentGrd"].Value = paymentId;
                }

                if (row.Cells["transport_firm_id"].Value != null)
                {
                    int transportFirmId = Convert.ToInt32(row.Cells["transport_firm_id"].Value);
                    row.Cells["drpTransportFirmGrd"].Value = transportFirmId;
                }

                if (row.Cells["product_id"].Value != null)
                {
                    int productId = Convert.ToInt32(row.Cells["product_id"].Value);
                    row.Cells["drpProductGrd"].Value = productId;
                }
            }

            OrderGrid.Columns["id"].Visible = false;
            OrderGrid.Columns["customer_id"].Visible = false;
            OrderGrid.Columns["payment_id"].Visible = false;
            OrderGrid.Columns["transport_firm_id"].Visible = false;
            OrderGrid.Columns["product_id"].Visible = false;

        }

        private void clearOrderTextBoxes()
        {
            txtOrderName.Clear();
            txtOrderDate.Value = DateTime.Now;
            drpOrderCustomer.SelectedIndex = 0;
            drpOrderPaymentType.SelectedIndex = 0;
            drpOrderProduct.SelectedIndex = 0;
            drpOrderTransportFirm.SelectedIndex = 0;
        }

        private void deleteOrder(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"order\" WHERE id = @p1", con);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            con.Close();

            listOrder(false);
        }

        private void OrderGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OrderGrid.Columns[e.ColumnIndex].Name == "DeleteOrder" &&
                MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int OrderId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["id"].Value);
                deleteOrder(OrderId);
            }

            if (OrderGrid.Columns[e.ColumnIndex].Name == "UpdateOrder" &&
                MessageBox.Show("Kaydı güncellemek istediğinizden emin misiniz?", "Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int OrderId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["id"].Value);
                string OrderName = OrderGrid.CurrentRow.Cells["name"].Value.ToString();
                DateTime OrderDate = Convert.ToDateTime(OrderGrid.CurrentRow.Cells["order_date"].Value);
                int CustomerId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["customer_id"].Value);
                int PaymentId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["payment_id"].Value);
                int TransportFirmId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["transport_firm_id"].Value);
                int ProductId = Convert.ToInt32(OrderGrid.CurrentRow.Cells["product_id"].Value);
                updateOrder(OrderId, OrderName, OrderDate, CustomerId, PaymentId, TransportFirmId, ProductId);
            }
        }
        #endregion
    }
}
