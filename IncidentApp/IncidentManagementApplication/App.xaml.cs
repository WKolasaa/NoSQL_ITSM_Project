using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DAL;

namespace IncidentManagementApplication
{
    //dfgfghfkhgfgfg
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DAOService dbService;

        public App()
        {
            this.dbService = new DAOService();
            //dbService.getEmployees(); // breakpoint in DAO foreach, and uncomment this for testing if db works
        }
    }
}
