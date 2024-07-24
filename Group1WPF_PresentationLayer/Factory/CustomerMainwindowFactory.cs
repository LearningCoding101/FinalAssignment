using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.View.CustomerView;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment2.Factory {

    public delegate CustomerMainWindow CustomerMainWindowFactory(IServiceProvider serviceProvider, int customerId);
}
