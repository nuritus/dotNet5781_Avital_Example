using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    ///  Information about who is the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// the user name
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// the password of the user
        /// </summary>
        public string UserPassword { set; get; }
        /// <summary>
        /// the access to management of the user
        /// </summary>
        public bool UserAccessManagement { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
