using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Service;

namespace IncidentManagementApplication.pages
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        TicketService tService;

        public TicketPage()
        {
            InitializeComponent();
            tService = new TicketService();
            resetFields();
        }

        private void setComboBoxWithEnum(ComboBox comboBox, Enum defaultEnum)
        {
            comboBox.ItemsSource = Enum.GetValues(defaultEnum.GetType());
            comboBox.SelectedItem = defaultEnum;
        }

        private void setDatePickerFields(DatePicker dp)
        {
            dp.Text = DateTime.Now.ToString("f");
            dp.IsEnabled = false;
        }

        private void setTicketIDField()
        {
            int id = getLastTicketID();
            id++;
            txtBoxTicketId.Text = id.ToString();
        }

        private int getLastTicketID()
        {
            return tService.getLastTicketID();
        }

        private void resetFields()
        {
            setTicketIDField();
            setComboBoxWithEnum(comboBoxTicketSeverity, Severity.None);
            setComboBoxWithEnum(comboBoxTicketStatus, Status.Open);
            setDatePickerFields(datePickTicketCreation);
            setDatePickerFields(datePickTicketUpdate);
            setComboBoxWithEnum(comboBoxIncidentType, IncidentType.Unknown);
            txtBoxIncidentReporter.Text = String.Empty;
            // Once Salman is done with login, add an automatic Reporter assign, instead of manual input
            txtBoxIncidentDescription.Text = String.Empty;
        }

        // Ticket creation - Ignas
        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = getTicket();
            createTicket(ticket);
            resetFields();
        }

        private void createTicket(Ticket ticket)
        {
            tService.createNewTicket(ticket);
        }

        private Ticket getTicket()
        {
            int incidentId = int.Parse(txtBoxTicketId.Text);
            int incidentType = (int)comboBoxIncidentType.SelectedItem;
            string incidentReporter = txtBoxIncidentReporter.Text;
            string incidentDesc = txtBoxIncidentDescription.Text;
            Incident incident = new Incident(incidentId, incidentType, incidentReporter, incidentDesc);
            int ticketId = int.Parse(txtBoxTicketId.Text);
            int ticketSeverity = (int)comboBoxTicketSeverity.SelectedItem;
            int ticketStatus = (int)comboBoxTicketStatus.SelectedItem;
            DateTime dateCreated = datePickTicketCreation.SelectedDate ?? DateTime.Now;
            DateTime dateUpdated = datePickTicketUpdate.SelectedDate ?? DateTime.Now;
            Ticket ticket = new Ticket(ticketId, ticketSeverity, ticketStatus, dateCreated, dateUpdated, incident);
            return ticket;
        }

        private void btnEditTicket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
