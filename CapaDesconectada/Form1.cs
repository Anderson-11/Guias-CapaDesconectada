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

        private void RellenarForm(Customer cliente)
        {
            if (cliente != null)
            {
                tboxCustomerID.Text = cliente.CustomerID;
                tboxCompanyName.Text = cliente.CompanyName;
                tboxContactName.Text = cliente.ContactName;
                tboxContactTitle.Text = cliente.ContactTitle;
                tboxAddres.Text = cliente.Address;
            }
            if (cliente == null)
            {
                MessageBox.Show("objeto No Encontrado ");
            }
        }

        private void Limpiar()
        {
            tboxCustomerID.Text = "";
            tboxCompanyName.Text = "";
            tboxContactName.Text = "";
            tboxContactTitle.Text = "";
            tboxAddres.Text = "";
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

            if (cliente == null)
            {
                MessageBox.Show("El objeto es null");
            }
            else
            {
                var listaClientes = new List<Customer> { cliente };
                gridNoTipado.DataSource = listaClientes;
                RellenarForm(cliente);
                tboxCustomerID.Enabled = false;
            }
        }

        private void btnInsertarCliente_Click(object sender, EventArgs e)
        {
            var cliente = CrearCliente();
            int insertados = customerRepository.InsertarCliente(cliente);
            MessageBox.Show($"{insertados} Registrado");
            Limpiar();
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

        private void btnActualizarNT_Click(object sender, EventArgs e)
        {
            var cliente = CrearCliente();
            var actuali = customerRepository.ActualizarCliente(cliente);
            MessageBox.Show($"{actuali} filas actulizadas");
            Limpiar();
            tbxEncontrado.Text = "";
            tboxCustomerID.Enabled = true;
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
                tbxBuscarTip.Text = "";
                RellenarForm(objeto1);
                tboxCustomerID.Enabled = false;
            }
        }

        private void btnInsertarTip_Click(object sender, EventArgs e)
        {
            var cliente = CrearCliente();
            int resultado = adaptador.Insert(cliente.CustomerID, cliente.CompanyName, cliente.ContactName, cliente.ContactTitle, cliente.Address, cliente.City, cliente.Region, cliente.PostalCode, cliente.Country, cliente.Phone,
                cliente.Fax);
            if (resultado > 0)
            {
                MessageBox.Show("Se ingreso");
                Limpiar();
            }
        }

        private void btnActualizarTip_Click(object sender, EventArgs e)
        {
            var fila = adaptador.GetDataBy(tboxCustomerID.Text);

            if (fila != null)
            {
                var datoOriginal = customerRepository.ExtraerInformacionCliente(fila);
                var datosModificados = CrearCliente();

                var filas = adaptador.Actualizar(
                    datosModificados.CustomerID,
                    datosModificados.CompanyName,
                    datosModificados.ContactName,
                    datosModificados.ContactTitle,
                    datosModificados.Address,
                    datosModificados.City,
                    datosModificados.Region,
                    datosModificados.PostalCode,
                    datosModificados.Country,
                    datosModificados.Phone,
                    datosModificados.Fax, datoOriginal.CustomerID
                );

                MessageBox.Show($"{filas} filas modificadas");

                Limpiar();
                tbxEncontradoTip.Text = "";
                tboxCustomerID.Enabled = true;
            }
        }
        #endregion
    }
}
