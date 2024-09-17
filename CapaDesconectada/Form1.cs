using AccesoDatos;
using CapaDesconectada.NorthwindTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDesconectada
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region No tipado
        CustomerRepository customerRepository = new CustomerRepository();
        private void btnObtenerNoTipado_Click(object sender, EventArgs e)
        {
            gridNoTipado.DataSource = customerRepository.ObtenerTodos();
        }

        private void btnBuscarNT_Click(object sender, EventArgs e)
        {
            var cliente = customerRepository.ObtenerPorID(tbxBusquedaNT.Text);

            var encontrado = cliente.CompanyName;
            tbxEncontrado.Text = encontrado;
            tbxBusquedaNT.Text = "";
        }

        private void btnInsertarCliente_Click(object sender, EventArgs e)
        {
            var cliente = CrearCliente();
            int insertados = customerRepository.InsertarCliente(cliente);
            MessageBox.Show($"{insertados} Registrado");
            tboxCustomerID.Text = "";
            tboxCompanyName.Text = "";
            tboxContactName.Text = "";
            tboxContactTitle.Text = "";
            tboxAddres.Text = "";
        }

        private Customer CrearCliente()
        {
            var cliente = new Customer
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddres.Text,
            };
            MessageBox.Show(cliente.CompanyName, "Ingresado");
            return cliente;
        }
        #endregion

        #region Tipado
        CustomersTableAdapter adaptador = new CustomersTableAdapter();
        private void btnObtenerTipado_Click(object sender, EventArgs e)
        {
            var customers = adaptador.GetData();
            gridTipado.DataSource = customers;
        }

        private void btnBuscarTip_Click(object sender, EventArgs e)
        {
            var customer = adaptador.GetDataBy(tbxBuscarTip.Text);

            if (customer != null)
            {
                var objeto1 = customerRepository.ExtraerInformacionCliente(customer);
                var encontrado = objeto1.CompanyName;
                tbxEncontradoTip.Text = encontrado;
            }
        }
        #endregion
    }
}
