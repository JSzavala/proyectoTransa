/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 07:53 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using proyectoInventario.backEnd.conexionBd;
using proyectoInventario.backEnd.POCOS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyectoInventario
{
    /// <summary>
    /// Description of Login.
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
        InitializeComponent();
        }
    
        void LblTitleLoginClick(object sender, EventArgs e)
        {
            
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsConsultas consultas = new clsConsultas();
            Usuario usuarioAutenticado = consultas.AutenticarUsuario(usuario, contrasena);

            if (usuarioAutenticado != null)
            {
                MessageBox.Show($"Bienvenido {usuarioAutenticado.Nombre} ({usuarioAutenticado.Rol})", "Acceso concedido");
                    
                globales.clsGlobales.RolActual = usuarioAutenticado.Rol.ToString();
                // Abrir menú principal según rol
                if (usuarioAutenticado.Rol == RolUsuario.Dueña)
                {
                    Main adminForm = new Main();
                    adminForm.Show();
                }
                else
                {
                    Main vendedorForm = new Main();
                    vendedorForm.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
